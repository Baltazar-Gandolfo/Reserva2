using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para slots de disponibilidad.
    /// </summary>
    public class DisponibilidadRepository
    {
        /// <summary>Obtiene los slots de un elemento en un rango de fechas.</summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <param name="desde">Fecha desde.</param>
        /// <param name="hasta">Fecha hasta.</param>
        /// <returns>Lista de slots ordenados por fecha y hora.</returns>
        public List<Disponibilidad> GetByElemento(int idElemento, DateTime desde, DateTime hasta)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Disponibilidades
                    .Where(d => d.IdElemento == idElemento
                             && d.Fecha >= desde.Date && d.Fecha <= hasta.Date)
                    .OrderBy(d => d.Fecha).ThenBy(d => d.HoraInicio)
                    .ToList();
            }
        }

        /// <summary>Obtiene un slot por su identificador con elemento y sucursal incluidos.</summary>
        /// <param name="id">Identificador del slot.</param>
        /// <returns>Slot encontrado o null.</returns>
        public Disponibilidad GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Disponibilidades
                    .Include(d => d.Elemento).ThenInclude(e => e.Sucursal)
                    .FirstOrDefault(d => d.IdDisponibilidad == id);
            }
        }

        /// <summary>Inserta solo los slots que todavía no existen (mismo elemento, fecha y hora de inicio).</summary>
        /// <param name="slots">Slots a insertar.</param>
        /// <returns>Cantidad de slots nuevos insertados.</returns>
        public int InsertarNuevos(IEnumerable<Disponibilidad> slots)
        {
            var lista = slots.ToList();
            if (lista.Count == 0) return 0;

            int idElemento = lista[0].IdElemento;
            DateTime desde = lista.Min(s => s.Fecha);
            DateTime hasta = lista.Max(s => s.Fecha);

            using (var ctx = NegocioContextFactory.Crear())
            {
                var existentes = ctx.Disponibilidades
                    .Where(d => d.IdElemento == idElemento && d.Fecha >= desde && d.Fecha <= hasta)
                    .Select(d => new { d.Fecha, d.HoraInicio })
                    .ToList()
                    .Select(d => (d.Fecha, d.HoraInicio))
                    .ToHashSet();

                var nuevos = lista.Where(s => !existentes.Contains((s.Fecha, s.HoraInicio))).ToList();
                ctx.Disponibilidades.AddRange(nuevos);
                ctx.SaveChanges();
                return nuevos.Count;
            }
        }

        /// <summary>Obtiene las reservas vigentes (Pendiente/Confirmada) por slot de un elemento en un rango.</summary>
        /// <param name="idElemento">Identificador del elemento.</param>
        /// <param name="desde">Fecha desde.</param>
        /// <param name="hasta">Fecha hasta.</param>
        /// <returns>Diccionario de IdDisponibilidad a reserva vigente más reciente.</returns>
        public Dictionary<int, Reserva> GetReservasVigentes(int idElemento, DateTime desde, DateTime hasta)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Reservas
                    .Where(r => r.Disponibilidad.IdElemento == idElemento
                             && r.Disponibilidad.Fecha >= desde.Date && r.Disponibilidad.Fecha <= hasta.Date
                             && (r.Estado == EstadoReserva.Pendiente || r.Estado == EstadoReserva.Confirmada))
                    .ToList()
                    .GroupBy(r => r.IdDisponibilidad)
                    .ToDictionary(g => g.Key, g => g.OrderByDescending(r => r.FechaReserva).First());
            }
        }

        /// <summary>Alterna un slot entre Libre y Bloqueada. No aplica a slots reservados.</summary>
        /// <param name="id">Identificador del slot.</param>
        /// <returns>Estado resultante o null si el slot está reservado.</returns>
        public string AlternarBloqueo(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var slot = ctx.Disponibilidades.Find(id);
                if (slot == null || slot.Estado == EstadoSlot.Reservada)
                    return null;

                slot.Estado = slot.Estado == EstadoSlot.Libre ? EstadoSlot.Bloqueada : EstadoSlot.Libre;
                ctx.SaveChanges();
                return slot.Estado;
            }
        }

        /// <summary>Cambia el estado de un slot de disponibilidad.</summary>
        /// <param name="id">Identificador del slot.</param>
        /// <param name="estado">Nuevo estado.</param>
        public void CambiarEstado(int id, string estado)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var slot = ctx.Disponibilidades.Find(id);
                if (slot != null)
                {
                    slot.Estado = estado;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
