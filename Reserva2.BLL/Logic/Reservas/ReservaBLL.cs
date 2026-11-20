using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic.Reservas
{
    /// <summary>
    /// Lógica de negocio principal para la gestión de reservas.
    /// Implementa CU.Cli.001, CU.Cli.002, CU.Cli.004, CU.Com.015, CU.Sis.001 y CU.Sis.004.
    /// </summary>
    public class ReservaBLL
    {
        private readonly ReservaRepository _repo = new ReservaRepository();
        private readonly DisponibilidadRepository _dispRepo = new DisponibilidadRepository();
        private readonly PagoRepository _pagoRepo = new PagoRepository();
        private readonly ComercioRepository _comercioRepo = new ComercioRepository();
        private readonly ReglaNegocioBLL _reglas = new ReglaNegocioBLL();
        private readonly ListaEsperaBLL _listaEspera = new ListaEsperaBLL();

        /// <summary>
        /// Obtiene el usuario actualmente logueado o lanza excepción.
        /// </summary>
        /// <returns>Usuario de la sesión actual.</returns>
        /// <exception cref="Exception">Si no hay sesión iniciada.</exception>
        private static Services.DomainModel.Composite.Usuario UsuarioActual()
        {
            return SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
        }

        /// <summary>
        /// Realiza una reserva sobre un slot de disponibilidad. Valida disponibilidad del elemento,
        /// límite semanal de reservas y bloquea el slot de forma concurrente.
        /// </summary>
        /// <param name="idDisponibilidad">Identificador del slot a reservar.</param>
        /// <param name="nombreCliente">Nombre opcional del cliente (para reservas de terceros).</param>
        /// <returns>La reserva creada con estado Pendiente.</returns>
        /// <exception cref="Exception">Si el slot no existe, no está disponible o se superó el límite semanal.</exception>
        /// <exception cref="SlotNoDisponibleException">Si otro cliente tomó el slot simultáneamente.</exception>
        public Reserva Reservar(int idDisponibilidad, string? nombreCliente = null)
        {
            var usuario = UsuarioActual();

            var slot = _dispRepo.GetById(idDisponibilidad)
                ?? throw new Exception("El slot seleccionado no existe.");
            var elemento = slot.Elemento;
            var sucursal = elemento.Sucursal;
            var comercio = _comercioRepo.GetById(sucursal.IdComercio);

            if (!comercio.Activo || !sucursal.Activo || elemento.Estado != "Activo")
                throw new Exception("El elemento no está disponible para reservar.");

            if (slot.Fecha + slot.HoraInicio <= DateTime.Now)
                throw new Exception("El horario seleccionado ya pasó.");

            // FA 6.1: límite semanal de reservas en el comercio
            int maxSemana = _reglas.ObtenerEntero(comercio.IdComercio, ReglaNegocioBLL.MaxReservasSemana);
            var (lunes, domingo) = SemanaDe(slot.Fecha);
            if (_repo.ContarActivas(usuario.IdUsuario, comercio.IdComercio, lunes, domingo) >= maxSemana)
                throw new Exception($"Alcanzaste el máximo de {maxSemana} reservas por semana en este comercio.");

            int minutos = _reglas.ObtenerEntero(comercio.IdComercio, ReglaNegocioBLL.MinutosExpiracion);
            var ahora = DateTime.Now;
            var reserva = new Reserva
            {
                Estado = EstadoReserva.Pendiente,
                FechaReserva = ahora,
                ExpiraEn = ahora.AddMinutes(minutos),
                IdUsuario = usuario.IdUsuario,
                IdDisponibilidad = idDisponibilidad,
                NombreCliente = string.IsNullOrWhiteSpace(nombreCliente) ? null : nombreCliente.Trim()
            };

            // FA 7.1: el slot lo tomó otro cliente
            if (!_repo.CrearPendiente(reserva))
                throw new SlotNoDisponibleException("El slot ya fue reservado por otro cliente.");

            BitacoraService.Registrar("Alta", "Reserva",
                $"Reserva pendiente de {elemento.Nombre} el {slot.Fecha:dd/MM/yyyy} {slot.HoraInicio:hh\\:mm}",
                reserva.IdReserva);

            ExpiracionReservasService.Instancia.Iniciar();

            return reserva;
        }

        /// <summary>
        /// Obtiene las reservas del usuario actualmente logueado, expirando las vencidas previamente.
        /// </summary>
        /// <returns>Lista de reservas del usuario actual.</returns>
        public List<Reserva> ListarMisReservas()
        {
            var usuario = UsuarioActual();
            ExpirarVencidas();
            return _repo.GetByUsuario(usuario.IdUsuario);
        }

        /// <summary>
        /// Obtiene las reservas visibles para el usuario actual según su rol:
        /// Cliente ve las propias, Comercio/Admin ve las de sus comercios.
        /// </summary>
        /// <returns>Lista de reservas visibles ordenadas por fecha descendente.</returns>
        public List<Reserva> ListarVisibles()
        {
            var usuario = UsuarioActual();
            if (usuario.Rol == "Cliente")
                return ListarMisReservas();

            ExpirarVencidas();
            var comercios = usuario.Rol == "Admin"
                ? _comercioRepo.GetAll()
                : _comercioRepo.GetByUsuario(usuario.IdUsuario);

            var resultado = new List<Reserva>();
            foreach (var c in comercios)
                resultado.AddRange(_repo.GetByComercio(c.IdComercio, null, null, null));
            return resultado.OrderByDescending(r => r.FechaReserva).ToList();
        }

        /// <summary>
        /// Determina si un usuario puede gestionar (ver/confirmar/cancelar) una reserva.
        /// </summary>
        /// <param name="usuario">Usuario a evaluar.</param>
        /// <param name="reserva">Reserva a gestionar.</param>
        /// <returns>True si es Admin, el cliente de la reserva o el dueño del comercio.</returns>
        public static bool PuedeGestionar(Services.DomainModel.Composite.Usuario usuario, Reserva reserva)
        {
            if (usuario.Rol == "Admin" || reserva.IdUsuario == usuario.IdUsuario)
                return true;
            var comercio = reserva.Disponibilidad?.Elemento?.Sucursal?.Comercio;
            return comercio != null && comercio.IdUsuario == usuario.IdUsuario;
        }

        /// <summary>
        /// Obtiene una reserva por su identificador, validando permisos del usuario actual.
        /// </summary>
        /// <param name="idReserva">Identificador de la reserva.</param>
        /// <returns>La reserva encontrada.</returns>
        /// <exception cref="Exception">Si no existe o no tiene permisos.</exception>
        public Reserva ObtenerPorId(int idReserva)
        {
            var usuario = UsuarioActual();
            var reserva = _repo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");
            if (!PuedeGestionar(usuario, reserva))
                throw new Exception("No tiene permisos sobre esta reserva.");
            return reserva;
        }

        /// <summary>
        /// Obtiene las reservas de un comercio con filtros opcionales. Solo para Admin o dueño del comercio.
        /// </summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="estado">Filtro por estado (null para todos).</param>
        /// <param name="fecha">Filtro por fecha (null para todas).</param>
        /// <param name="idElemento">Filtro por elemento (null para todos).</param>
        /// <returns>Lista de reservas filtradas del comercio.</returns>
        /// <exception cref="Exception">Si el comercio no existe o no tiene permisos.</exception>
        public List<Reserva> ListarDelComercio(int idComercio, string estado = null,
            DateTime? fecha = null, int? idElemento = null)
        {
            var usuario = UsuarioActual();
            var comercio = _comercioRepo.GetById(idComercio)
                ?? throw new Exception("El comercio no existe.");

            if (usuario.Rol != "Admin" && comercio.IdUsuario != usuario.IdUsuario)
                throw new Exception("No tiene permisos sobre este comercio.");

            ExpirarVencidas();
            return _repo.GetByComercio(idComercio, estado, fecha, idElemento);
        }

        /// <summary>
        /// Confirma una reserva pendiente. Solo puede ser ejecutado por Comercio o Admin.
        /// </summary>
        /// <param name="idReserva">Identificador de la reserva a confirmar.</param>
        /// <exception cref="Exception">Si es Cliente, la reserva no existe, no tiene permisos o cambió de estado.</exception>
        public void Confirmar(int idReserva)
        {
            var usuario = UsuarioActual();
            if (usuario.Rol == "Cliente")
                throw new Exception("Solo el comercio o un administrador puede confirmar una reserva.");

            var reserva = _repo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");
            if (!PuedeGestionar(usuario, reserva))
                throw new Exception("No tiene permisos sobre esta reserva.");

            EstadoReservaState.De(reserva.Estado).ValidarConfirmar();

            if (!_repo.CambiarEstado(idReserva, EstadoReserva.Pendiente, EstadoReserva.Confirmada,
                    liberarSlot: false))
                throw new Exception("La reserva cambió de estado. Intentá de nuevo.");

            BitacoraService.Registrar("Confirmación", "Reserva",
                $"Reserva ID {idReserva} confirmada por {usuario.Nombre}", idReserva);
        }

        /// <summary>
        /// Cancela una reserva aplicando la política de cancelación para determinar si corresponde reembolso.
        /// Libera el slot y notifica al primero de la lista de espera.
        /// </summary>
        /// <param name="idReserva">Identificador de la reserva a cancelar.</param>
        /// <param name="motivo">Motivo de la cancelación.</param>
        /// <returns>Resultado indicando si había pago y si fue reembolsado.</returns>
        /// <exception cref="Exception">Si el motivo está vacío, la reserva no existe, no tiene permisos o cambió de estado.</exception>
        public ResultadoCancelacion Cancelar(int idReserva, string motivo)
        {
            var usuario = UsuarioActual();
            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("Debe ingresar el motivo de la cancelación.");

            var reserva = _repo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");

            if (!PuedeGestionar(usuario, reserva))
                throw new Exception("No tiene permisos sobre esta reserva.");

            EstadoReservaState.De(reserva.Estado).ValidarCancelar();

            var slot = reserva.Disponibilidad;
            int idComercio = slot.Elemento.Sucursal.IdComercio;
            int horasGratis = _reglas.ObtenerEntero(idComercio, ReglaNegocioBLL.CancelacionGratisHs);
            IPoliticaCancelacion politica = new PoliticaCancelacionPorPlazo(horasGratis);

            bool reembolso = politica.CorrespondeReembolso(slot.Fecha + slot.HoraInicio, DateTime.Now);

            if (!_repo.CambiarEstado(idReserva, reserva.Estado, EstadoReserva.Cancelada,
                    liberarSlot: true, motivo: motivo.Trim()))
                throw new Exception("La reserva cambió de estado. Intentá de nuevo.");

            var resultado = new ResultadoCancelacion();
            var pago = _pagoRepo.GetAprobadoPorReserva(idReserva);
            if (pago != null)
            {
                resultado.TeniaPago = true;
                resultado.Reembolsado = reembolso;
                _pagoRepo.CambiarEstado(pago.IdPago, reembolso ? EstadoPago.Reembolsado : EstadoPago.Retenido);
            }

            _listaEspera.NotificarPrimero(reserva.IdDisponibilidad);

            BitacoraService.Registrar("Cancelación", "Reserva",
                $"Reserva ID {idReserva} cancelada. Motivo: {motivo.Trim()}" +
                (resultado.TeniaPago ? (reembolso ? " (pago reembolsado)" : " (pago retenido)") : ""),
                idReserva);

            return resultado;
        }

        /// <summary>
        /// Expira las reservas pendientes que superaron su tiempo límite de pago.
        /// Libera los slots y notifica a la lista de espera por cada una.
        /// </summary>
        /// <returns>Lista de reservas que fueron expiradas en esta ejecución.</returns>
        public List<Reserva> ExpirarVencidas()
        {
            var expiradas = new List<Reserva>();
            foreach (var reserva in _repo.GetPendientesVencidas(DateTime.Now))
            {
                EstadoReservaState.De(reserva.Estado).ValidarExpirar();

                if (!_repo.CambiarEstado(reserva.IdReserva, EstadoReserva.Pendiente,
                        EstadoReserva.Expirada, liberarSlot: true))
                    continue;

                _listaEspera.NotificarPrimero(reserva.IdDisponibilidad);

                BitacoraService.Registrar("Expiración", "Reserva",
                    $"Reserva ID {reserva.IdReserva} expirada por falta de pago", reserva.IdReserva);

                expiradas.Add(reserva);
            }
            return expiradas;
        }

        /// <summary>
        /// Verifica si existen reservas pendientes de pago en el sistema.
        /// </summary>
        /// <returns>True si hay al menos una reserva pendiente.</returns>
        public bool HayPendientes()
        {
            return _repo.ExistenPendientes();
        }

        /// <summary>
        /// Calcula el lunes y domingo de la semana que contiene la fecha indicada.
        /// </summary>
        /// <param name="fecha">Fecha de referencia.</param>
        /// <returns>Tupla con el lunes y domingo de esa semana.</returns>
        private static (DateTime lunes, DateTime domingo) SemanaDe(DateTime fecha)
        {
            int desfase = ((int)fecha.DayOfWeek + 6) % 7;
            var lunes = fecha.Date.AddDays(-desfase);
            return (lunes, lunes.AddDays(6));
        }
    }

    /// <summary>
    /// Excepción lanzada cuando un slot ya fue tomado por otro cliente de forma concurrente.
    /// </summary>
    public class SlotNoDisponibleException : Exception
    {
        /// <summary>
        /// Inicializa la excepción con un mensaje descriptivo.
        /// </summary>
        /// <param name="mensaje">Mensaje de error.</param>
        public SlotNoDisponibleException(string mensaje) : base(mensaje) { }
    }
}
