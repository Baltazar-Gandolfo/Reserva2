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
    /// Lógica de negocio para el registro y consulta de pagos.
    /// Implementa CU.Sis.002 — Registrar Pago Aprobado.
    /// </summary>
    public class PagoBLL
    {
        private readonly ReservaRepository _reservaRepo = new ReservaRepository();
        private readonly PagoRepository _pagoRepo = new PagoRepository();

        /// <summary>
        /// Obtiene las reservas del cliente actual que están pendientes de pago y no vencieron.
        /// </summary>
        /// <returns>Lista de reservas pendientes de pago.</returns>
        /// <exception cref="Exception">Si no hay sesión iniciada.</exception>
        public List<Reserva> ListarPendientesDePago()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
            var ahora = DateTime.Now;
            return _reservaRepo.GetByUsuario(usuario.IdUsuario)
                .Where(r => r.Estado == EstadoReserva.Pendiente && r.ExpiraEn > ahora)
                .ToList();
        }

        /// <summary>
        /// Obtiene los pagos asociados a una reserva.
        /// </summary>
        /// <param name="idReserva">Identificador de la reserva.</param>
        /// <returns>Lista de pagos de la reserva.</returns>
        public List<Pago> ListarPagos(int idReserva)
        {
            return _pagoRepo.GetPorReserva(idReserva);
        }

        /// <summary>
        /// Obtiene los pagos agrupados por reserva para un conjunto de reservas.
        /// </summary>
        /// <param name="idsReserva">Identificadores de las reservas.</param>
        /// <returns>Diccionario con los pagos de cada reserva.</returns>
        public Dictionary<int, List<Pago>> ListarPorReservas(IEnumerable<int> idsReserva)
        {
            return _pagoRepo.GetPorReservas(idsReserva);
        }

        /// <summary>
        /// Calcula el total efectivamente cobrado (pagos aprobados) de una lista de pagos.
        /// </summary>
        /// <param name="pagos">Lista de pagos a sumar.</param>
        /// <returns>Monto total de pagos aprobados.</returns>
        public static decimal TotalPagado(IEnumerable<Pago> pagos)
        {
            return pagos == null ? 0m : pagos.Where(p => p.Estado == EstadoPago.Aprobado).Sum(p => p.Monto);
        }

        /// <summary>
        /// Calcula el importe a abonar para una reserva: la seña o el total según el porcentaje del elemento.
        /// </summary>
        /// <param name="reserva">Reserva sobre la cual calcular.</param>
        /// <returns>Monto a abonar.</returns>
        public decimal CalcularMonto(Reserva reserva)
        {
            var slot = reserva.Disponibilidad;
            return CalcularMonto(slot.Precio, slot.Elemento.PorcentajeSeña);
        }

        /// <summary>
        /// Calcula el importe a abonar dado un precio y un porcentaje de seña.
        /// </summary>
        /// <param name="precio">Precio del slot.</param>
        /// <param name="porcentajeSeña">Porcentaje de seña (0 = pago total).</param>
        /// <returns>Monto redondeado a dos decimales.</returns>
        public static decimal CalcularMonto(decimal precio, decimal porcentajeSeña)
        {
            return porcentajeSeña > 0 ? Math.Round(precio * porcentajeSeña / 100m, 2) : precio;
        }

        /// <summary>
        /// Registra un pago aprobado para una reserva pendiente. Valida permisos, estado y expiración.
        /// </summary>
        /// <param name="idReserva">Identificador de la reserva a pagar.</param>
        /// <param name="metodoPago">Método de pago utilizado.</param>
        /// <returns>El pago registrado con estado Aprobado.</returns>
        /// <exception cref="Exception">Si el método es inválido, la reserva no existe, no tiene permisos, no está pendiente o expiró.</exception>
        public Pago RegistrarPago(int idReserva, string metodoPago)
        {
            if (!MetodoPago.Todos.Contains(metodoPago))
                throw new Exception("Seleccione un método de pago.");

            var reserva = _reservaRepo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe.");

            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
            if (!ReservaBLL.PuedeGestionar(usuario, reserva))
                throw new Exception("No tiene permisos sobre esta reserva.");

            if (reserva.Estado != EstadoReserva.Pendiente)
                throw new Exception("La reserva ya no está pendiente.");

            if (reserva.ExpiraEn.HasValue && reserva.ExpiraEn < DateTime.Now)
                throw new Exception("La reserva expiró. Realizá una nueva reserva.");

            var pago = new Pago
            {
                Estado = EstadoPago.Aprobado,
                FechaPago = DateTime.Now,
                Monto = CalcularMonto(reserva),
                MetodoPago = metodoPago,
                IdReserva = idReserva
            };

            if (!_pagoRepo.RegistrarAprobado(pago))
                throw new Exception("La reserva ya no está pendiente.");

            BitacoraService.Registrar("Alta", "Pago",
                $"Pago aprobado de ${pago.Monto:N2} ({metodoPago}) para la reserva ID {idReserva}", pago.IdPago);

            return pago;
        }
    }
}
