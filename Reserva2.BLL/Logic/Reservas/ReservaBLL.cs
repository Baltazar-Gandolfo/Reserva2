using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic.Reservas
{
    public class ResultadoCancelacion
    {
        public bool TeniaPago { get; set; }
        public bool Reembolsado { get; set; }
    }

    public class ReservaBLL
    {
        private readonly ReservaRepository _repo = new ReservaRepository();
        private readonly DisponibilidadRepository _dispRepo = new DisponibilidadRepository();
        private readonly PagoRepository _pagoRepo = new PagoRepository();
        private readonly ComercioRepository _comercioRepo = new ComercioRepository();
        private readonly ReglaNegocioBLL _reglas = new ReglaNegocioBLL();
        private readonly ListaEsperaBLL _listaEspera = new ListaEsperaBLL();

        private static Services.DomainModel.Composite.Usuario UsuarioActual()
        {
            return SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
        }

        // CU.Cli.001 — Realizar Reserva
        public Reserva Reservar(int idDisponibilidad)
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
                IdDisponibilidad = idDisponibilidad
            };

            // FA 7.1: el slot lo tomó otro cliente
            if (!_repo.CrearPendiente(reserva))
                throw new SlotNoDisponibleException("El slot ya fue reservado por otro cliente.");

            BitacoraService.Registrar("Alta", "Reserva",
                $"Reserva pendiente de {elemento.Nombre} el {slot.Fecha:dd/MM/yyyy} {slot.HoraInicio:hh\\:mm}",
                reserva.IdReserva);

            // El contador de expiración arranca recién cuando el cliente empieza a reservar.
            ExpiracionReservasService.Instancia.Iniciar();

            return reserva;
        }

        // CU.Cli.002 — Consultar Mis Reservas
        public List<Reserva> ListarMisReservas()
        {
            var usuario = UsuarioActual();
            ExpirarVencidas(); // consistencia aunque el timer no esté corriendo
            return _repo.GetByUsuario(usuario.IdUsuario);
        }

        // Reservas que el usuario actual puede ver: las propias (Cliente) o las de sus comercios (Comercio/Admin).
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

        // Una reserva la gestiona su cliente, el dueño del comercio o el Admin.
        public static bool PuedeGestionar(Services.DomainModel.Composite.Usuario usuario, Reserva reserva)
        {
            if (usuario.Rol == "Admin" || reserva.IdUsuario == usuario.IdUsuario)
                return true;
            var comercio = reserva.Disponibilidad?.Elemento?.Sucursal?.Comercio;
            return comercio != null && comercio.IdUsuario == usuario.IdUsuario;
        }

        public Reserva ObtenerPorId(int idReserva)
        {
            var usuario = UsuarioActual();
            var reserva = _repo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");
            if (!PuedeGestionar(usuario, reserva))
                throw new Exception("No tiene permisos sobre esta reserva.");
            return reserva;
        }

        // CU.Com.015 — Consultar Reservas del Comercio
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

        // CU.Cli.004 — Cancelar Reserva
        public ResultadoCancelacion Cancelar(int idReserva, string motivo)
        {
            var usuario = UsuarioActual();
            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("Debe ingresar el motivo de la cancelación.");

            var reserva = _repo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");

            if (usuario.Rol != "Admin" && reserva.IdUsuario != usuario.IdUsuario)
                throw new Exception("No puede cancelar una reserva de otro cliente.");

            EstadoReservaState.De(reserva.Estado).ValidarCancelar();

            var slot = reserva.Disponibilidad;
            int idComercio = slot.Elemento.Sucursal.IdComercio;
            int horasGratis = _reglas.ObtenerEntero(idComercio, ReglaNegocioBLL.CancelacionGratisHs);
            IPoliticaCancelacion politica = new PoliticaCancelacionPorPlazo(horasGratis);

            bool reembolso = politica.CorrespondeReembolso(slot.Fecha + slot.HoraInicio, DateTime.Now);

            if (!_repo.CambiarEstado(idReserva, EstadoReserva.Confirmada, EstadoReserva.Cancelada,
                    liberarSlot: true, motivo: motivo.Trim()))
                throw new Exception("La reserva ya no está confirmada.");

            var resultado = new ResultadoCancelacion();
            var pago = _pagoRepo.GetAprobadoPorReserva(idReserva);
            if (pago != null)
            {
                resultado.TeniaPago = true;
                resultado.Reembolsado = reembolso; // FA 6.1 reembolso total / FA 6.2 se retiene
                _pagoRepo.CambiarEstado(pago.IdPago, reembolso ? EstadoPago.Reembolsado : EstadoPago.Retenido);
            }

            _listaEspera.NotificarPrimero(reserva.IdDisponibilidad);

            BitacoraService.Registrar("Cancelación", "Reserva",
                $"Reserva ID {idReserva} cancelada. Motivo: {motivo.Trim()}" +
                (resultado.TeniaPago ? (reembolso ? " (pago reembolsado)" : " (pago retenido)") : ""),
                idReserva);

            return resultado;
        }

        // CU.Sis.004 — Expirar Reserva. Devuelve las reservas que pasaron a Expirada.
        public List<Reserva> ExpirarVencidas()
        {
            var expiradas = new List<Reserva>();
            foreach (var reserva in _repo.GetPendientesVencidas(DateTime.Now))
            {
                EstadoReservaState.De(reserva.Estado).ValidarExpirar();

                if (!_repo.CambiarEstado(reserva.IdReserva, EstadoReserva.Pendiente,
                        EstadoReserva.Expirada, liberarSlot: true))
                    continue; // otro proceso la confirmó o expiró primero

                _listaEspera.NotificarPrimero(reserva.IdDisponibilidad);

                BitacoraService.Registrar("Expiración", "Reserva",
                    $"Reserva ID {reserva.IdReserva} expirada por falta de pago", reserva.IdReserva);

                expiradas.Add(reserva);
            }
            return expiradas;
        }

        public bool HayPendientes()
        {
            return _repo.ExistenPendientes();
        }

        private static (DateTime lunes, DateTime domingo) SemanaDe(DateTime fecha)
        {
            int desfase = ((int)fecha.DayOfWeek + 6) % 7; // lunes = 0
            var lunes = fecha.Date.AddDays(-desfase);
            return (lunes, lunes.AddDays(6));
        }
    }

    public class SlotNoDisponibleException : Exception
    {
        public SlotNoDisponibleException(string mensaje) : base(mensaje) { }
    }
}
