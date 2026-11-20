using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para reservas.
    /// </summary>
    public class ReservaRepository
    {
        /// <summary>Consulta base que incluye disponibilidad, elemento, sucursal y comercio.</summary>
        private static IQueryable<Reserva> ConDetalle(NegocioDbContext ctx)
        {
            return ctx.Reservas
                .Include(r => r.Disponibilidad)
                    .ThenInclude(d => d.Elemento)
                        .ThenInclude(e => e.Sucursal)
                            .ThenInclude(s => s.Comercio);
        }

        /// <summary>Obtiene una reserva por su identificador con detalle completo.</summary>
        /// <param name="id">Identificador de la reserva.</param>
        /// <returns>Reserva encontrada o null.</returns>
        public Reserva GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ConDetalle(ctx).FirstOrDefault(r => r.IdReserva == id);
            }
        }

        /// <summary>Obtiene las reservas de un usuario ordenadas por fecha descendente.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <returns>Lista de reservas del usuario.</returns>
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

        /// <summary>Obtiene las reservas de un comercio con filtros opcionales.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="estado">Filtro por estado (opcional).</param>
        /// <param name="fecha">Filtro por fecha del turno (opcional).</param>
        /// <param name="idElemento">Filtro por elemento reservable (opcional).</param>
        /// <returns>Lista de reservas filtradas.</returns>
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

        /// <summary>Obtiene las reservas pendientes cuya expiración ya pasó.</summary>
        /// <param name="ahora">Fecha y hora actual.</param>
        /// <returns>Lista de reservas vencidas.</returns>
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

        /// <summary>Verifica si existen reservas en estado Pendiente.</summary>
        /// <returns><c>true</c> si hay al menos una reserva pendiente.</returns>
        public bool ExistenPendientes()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Reservas.Any(r => r.Estado == EstadoReserva.Pendiente);
            }
        }

        /// <summary>Cuenta las reservas activas (Pendiente/Confirmada) de un cliente en un comercio y rango de fechas.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="desde">Fecha desde.</param>
        /// <param name="hasta">Fecha hasta.</param>
        /// <returns>Cantidad de reservas activas.</returns>
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

        /// <summary>Bloquea el slot y crea la reserva en una sola transacción atómica.</summary>
        /// <param name="reserva">Reserva a crear.</param>
        /// <returns><c>false</c> si el slot dejó de estar Libre.</returns>
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

        /// <summary>Transición de estado condicionada al estado actual, opcionalmente liberando el slot.</summary>
        /// <param name="idReserva">Identificador de la reserva.</param>
        /// <param name="estadoEsperado">Estado que debe tener para aplicar el cambio.</param>
        /// <param name="estadoNuevo">Nuevo estado a asignar.</param>
        /// <param name="liberarSlot">Si es <c>true</c>, libera el slot asociado.</param>
        /// <param name="motivo">Motivo de cancelación (opcional).</param>
        /// <returns><c>false</c> si la reserva no estaba en el estado esperado.</returns>
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
