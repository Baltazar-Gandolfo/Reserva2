using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class PagoRepository
    {
        public Pago GetAprobadoPorReserva(int idReserva)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Pagos.FirstOrDefault(p =>
                    p.IdReserva == idReserva && p.Estado == EstadoPago.Aprobado);
            }
        }

        public Dictionary<int, List<Pago>> GetPorReservas(IEnumerable<int> idsReserva)
        {
            var ids = idsReserva.ToList();
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Pagos
                    .Where(p => ids.Contains(p.IdReserva))
                    .ToList()
                    .GroupBy(p => p.IdReserva)
                    .ToDictionary(g => g.Key, g => g.OrderBy(p => p.FechaPago).ToList());
            }
        }

        public List<Pago> GetPorReserva(int idReserva)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Pagos.Where(p => p.IdReserva == idReserva).ToList();
            }
        }

        // Asienta el pago y confirma la reserva Pendiente en una sola transacción.
        // Devuelve false si la reserva ya no está Pendiente.
        public bool RegistrarAprobadoYConfirmar(Pago pago)
        {
            using (var ctx = NegocioContextFactory.Crear())
            using (var tx = ctx.Database.BeginTransaction())
            {
                var reserva = ctx.Reservas.FirstOrDefault(r =>
                    r.IdReserva == pago.IdReserva && r.Estado == EstadoReserva.Pendiente);
                if (reserva == null)
                {
                    tx.Rollback();
                    return false;
                }

                reserva.Estado = EstadoReserva.Confirmada;
                reserva.ConfirmadaEn = DateTime.Now;

                pago.Reserva = null;
                ctx.Pagos.Add(pago);
                ctx.SaveChanges();
                tx.Commit();
                return true;
            }
        }

        public void CambiarEstado(int idPago, string estado)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var pago = ctx.Pagos.Find(idPago);
                if (pago != null)
                {
                    pago.Estado = estado;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
