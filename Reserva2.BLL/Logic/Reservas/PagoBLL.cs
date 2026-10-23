using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic.Reservas
{
    // CU.Sis.002 — Registrar Pago Aprobado.
    // En el MVP no hay pasarela externa: el pago se asienta directamente como aprobado.
    // La confirmación queda a cargo del comercio (ReservaBLL.Confirmar).
    public class PagoBLL
    {
        private readonly ReservaRepository _reservaRepo = new ReservaRepository();
        private readonly PagoRepository _pagoRepo = new PagoRepository();

        // Reservas del cliente que todavía esperan pago y no vencieron.
        public List<Reserva> ListarPendientesDePago()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
            var ahora = DateTime.Now;
            return _reservaRepo.GetByUsuario(usuario.IdUsuario)
                .Where(r => r.Estado == EstadoReserva.Pendiente && r.ExpiraEn > ahora)
                .ToList();
        }

        public List<Pago> ListarPagos(int idReserva)
        {
            return _pagoRepo.GetPorReserva(idReserva);
        }

        public Dictionary<int, List<Pago>> ListarPorReservas(IEnumerable<int> idsReserva)
        {
            return _pagoRepo.GetPorReservas(idsReserva);
        }

        // Total efectivamente cobrado (pagos aprobados) de una reserva.
        public static decimal TotalPagado(IEnumerable<Pago> pagos)
        {
            return pagos == null ? 0m : pagos.Where(p => p.Estado == EstadoPago.Aprobado).Sum(p => p.Monto);
        }

        // Importe a abonar: la seña (porcentaje del elemento) o el total si no tiene seña.
        public decimal CalcularMonto(Reserva reserva)
        {
            var slot = reserva.Disponibilidad;
            return CalcularMonto(slot.Precio, slot.Elemento.PorcentajeSeña);
        }

        public static decimal CalcularMonto(decimal precio, decimal porcentajeSeña)
        {
            return porcentajeSeña > 0 ? Math.Round(precio * porcentajeSeña / 100m, 2) : precio;
        }

        public Pago RegistrarPago(int idReserva, string metodoPago)
        {
            if (!MetodoPago.Todos.Contains(metodoPago))
                throw new Exception("Seleccione un método de pago.");

            var reserva = _reservaRepo.GetById(idReserva)
                ?? throw new Exception("La reserva no existe."); // FA 2.1

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
