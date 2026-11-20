using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para pagos de reservas.
    /// </summary>
    public class PagoRepository
    {
        /// <summary>Obtiene el pago aprobado de una reserva.</summary>
        /// <param name="idReserva">Identificador de la reserva.</param>
        /// <returns>Pago aprobado o null.</returns>
        public Pago GetAprobadoPorReserva(int idReserva)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Pagos.FirstOrDefault(p =>
                    p.IdReserva == idReserva && p.Estado == EstadoPago.Aprobado);
            }
        }

        /// <summary>Obtiene los pagos agrupados por reserva para un conjunto de reservas.</summary>
        /// <param name="idsReserva">Identificadores de las reservas.</param>
        /// <returns>Diccionario de IdReserva a lista de pagos ordenados por fecha.</returns>
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

        /// <summary>Obtiene todos los pagos de una reserva.</summary>
        /// <param name="idReserva">Identificador de la reserva.</param>
        /// <returns>Lista de pagos.</returns>
        public List<Pago> GetPorReserva(int idReserva)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Pagos.Where(p => p.IdReserva == idReserva).ToList();
            }
        }

        /// <summary>Registra un pago aprobado y desactiva la expiración de la reserva Pendiente.</summary>
        /// <param name="pago">Pago a registrar.</param>
        /// <returns><c>false</c> si la reserva ya no está Pendiente.</returns>
        public bool RegistrarAprobado(Pago pago)
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

                reserva.ExpiraEn = null;

                pago.Reserva = null;
                ctx.Pagos.Add(pago);
                ctx.SaveChanges();
                tx.Commit();
                return true;
            }
        }

        /// <summary>Cambia el estado de un pago.</summary>
        /// <param name="idPago">Identificador del pago.</param>
        /// <param name="estado">Nuevo estado.</param>
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
