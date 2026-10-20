using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ReservaRepository
    {
        private static IQueryable<Reserva> ConDetalle(NegocioDbContext ctx)
        {
            return ctx.Reservas
                .Include(r => r.Disponibilidad)
                    .ThenInclude(d => d.Elemento)
                        .ThenInclude(e => e.Sucursal)
                            .ThenInclude(s => s.Comercio);
        }

        public Reserva GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ConDetalle(ctx).FirstOrDefault(r => r.IdReserva == id);
            }
        }

        public List<Reserva> GetByUsuario(Guid idUsuario)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ConDetalle(ctx)
                    .Where(r => r.IdUsuario == idUsuario)
                    .OrderByDescending(r => r.FechaReserva)
                    .ToList();
            }
        }

        public List<Reserva> GetByComercio(int idComercio, string estado, DateTime? fecha, int? idElemento)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var q = ConDetalle(ctx)
                    .Where(r => r.Disponibilidad.Elemento.Sucursal.IdComercio == idComercio);

                if (!string.IsNullOrEmpty(estado))
                    q = q.Where(r => r.Estado == estado);
                if (fecha.HasValue)
                    q = q.Where(r => r.Disponibilidad.Fecha == fecha.Value.Date);
                if (idElemento.HasValue)
                    q = q.Where(r => r.Disponibilidad.IdElemento == idElemento.Value);

                return q.OrderByDescending(r => r.FechaReserva).ToList();
            }
        }

        public List<Reserva> GetPendientesVencidas(DateTime ahora)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ConDetalle(ctx)
                    .Where(r => r.Estado == EstadoReserva.Pendiente
                             && r.ExpiraEn != null && r.ExpiraEn < ahora)
                    .ToList();
            }
        }

        public bool ExistenPendientes()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Reservas.Any(r => r.Estado == EstadoReserva.Pendiente);
            }
        }

        // Reservas Pendiente/Confirmada del cliente en un comercio, cuyo slot cae entre desde y hasta.
        public int ContarActivas(Guid idUsuario, int idComercio, DateTime desde, DateTime hasta)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Reservas.Count(r =>
                    r.IdUsuario == idUsuario
                    && (r.Estado == EstadoReserva.Pendiente || r.Estado == EstadoReserva.Confirmada)
                    && r.Disponibilidad.Elemento.Sucursal.IdComercio == idComercio
                    && r.Disponibilidad.Fecha >= desde.Date
                    && r.Disponibilidad.Fecha <= hasta.Date);
            }
        }

        // Bloquea el slot y crea la reserva en una sola transacción.
        // Devuelve false si el slot dejó de estar Libre (otro cliente lo tomó antes).
        public bool CrearPendiente(Reserva reserva)
        {
            using (var ctx = NegocioContextFactory.Crear())
            using (var tx = ctx.Database.BeginTransaction())
            {
                int bloqueados = ctx.Disponibilidades
                    .Where(d => d.IdDisponibilidad == reserva.IdDisponibilidad
                             && d.Estado == EstadoSlot.Libre)
                    .ExecuteUpdate(s => s.SetProperty(d => d.Estado, EstadoSlot.Reservada));

                if (bloqueados == 0)
                {
                    tx.Rollback();
                    return false;
                }

                reserva.Disponibilidad = null;
                ctx.Reservas.Add(reserva);
                ctx.SaveChanges();
                tx.Commit();
                return true;
            }
        }

        // Transición de estado condicionada al estado actual. Opcionalmente libera el slot.
        public bool CambiarEstado(int idReserva, string estadoEsperado, string estadoNuevo,
            bool liberarSlot, string motivo = null)
        {
            using (var ctx = NegocioContextFactory.Crear())
            using (var tx = ctx.Database.BeginTransaction())
            {
                var reserva = ctx.Reservas.FirstOrDefault(r =>
                    r.IdReserva == idReserva && r.Estado == estadoEsperado);
                if (reserva == null)
                {
                    tx.Rollback();
                    return false;
                }

                var ahora = DateTime.Now;
                reserva.Estado = estadoNuevo;
                if (estadoNuevo == EstadoReserva.Confirmada) reserva.ConfirmadaEn = ahora;
                if (estadoNuevo == EstadoReserva.Cancelada)
                {
                    reserva.CanceladaEn = ahora;
                    reserva.MotivoCancelacion = motivo;
                }

                if (liberarSlot)
                {
                    var slot = ctx.Disponibilidades.Find(reserva.IdDisponibilidad);
                    if (slot != null) slot.Estado = EstadoSlot.Libre;
                }

                ctx.SaveChanges();
                tx.Commit();
                return true;
            }
        }
    }
}
