using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para reglas de disponibilidad horaria.
    /// </summary>
    public class ReglaDisponibilidadRepository
    {
        /// <summary>Obtiene las reglas de un elemento, opcionalmente solo las activas.</summary>
        /// <param name="idElemento">Identificador del elemento.</param>
        /// <param name="soloActivas">Si es <c>true</c>, filtra solo las reglas activas.</param>
        /// <returns>Lista de reglas ordenadas por día y hora.</returns>
        public List<ReglaDisponibilidad> GetByElemento(int idElemento, bool soloActivas = true)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var q = ctx.ReglasDisponibilidad.Where(r => r.IdElemento == idElemento);
                if (soloActivas)
                    q = q.Where(r => r.Activo);
                return q.OrderBy(r => r.DiaSemana).ThenBy(r => r.HoraInicio).ToList();
            }
        }

        /// <summary>Obtiene una regla por su identificador.</summary>
        /// <param name="id">Identificador de la regla.</param>
        /// <returns>Regla encontrada o null.</returns>
        public ReglaDisponibilidad GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasDisponibilidad.FirstOrDefault(r => r.IdReglaDisponibilidad == id);
            }
        }

        /// <summary>Verifica si una regla activa del mismo elemento y día se superpone con el rango horario.</summary>
        /// <param name="idElemento">Identificador del elemento.</param>
        /// <param name="diaSemana">Día de la semana.</param>
        /// <param name="horaInicio">Hora de inicio del rango.</param>
        /// <param name="horaFin">Hora de fin del rango.</param>
        /// <returns><c>true</c> si existe superposición.</returns>
        public bool ExisteSuperposicion(int idElemento, string diaSemana,
            System.TimeSpan horaInicio, System.TimeSpan horaFin)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasDisponibilidad.Any(r =>
                    r.IdElemento == idElemento
                    && r.Activo
                    && r.DiaSemana == diaSemana
                    && r.HoraInicio < horaFin
                    && horaInicio < r.HoraFin);
            }
        }

        /// <summary>Reemplaza las reglas activas de un elemento por nuevas, limpiando slots futuros obsoletos.</summary>
        /// <param name="idElemento">Identificador del elemento.</param>
        /// <param name="nuevas">Nuevas reglas a insertar.</param>
        /// <param name="entraEnHorario">Función que evalúa si un slot encaja en el nuevo horario.</param>
        public void ReemplazarReglas(int idElemento, List<ReglaDisponibilidad> nuevas,
            System.Func<string, System.TimeSpan, System.TimeSpan, bool> entraEnHorario)
        {
            using (var ctx = NegocioContextFactory.Crear())
            using (var tx = ctx.Database.BeginTransaction())
            {
                foreach (var vieja in ctx.ReglasDisponibilidad.Where(r => r.IdElemento == idElemento && r.Activo))
                    vieja.Activo = false;

                ctx.ReglasDisponibilidad.AddRange(nuevas);

                var hoy = System.DateTime.Today;
                var libres = ctx.Disponibilidades
                    .Where(d => d.IdElemento == idElemento && d.Fecha >= hoy && d.Estado == EstadoSlot.Libre)
                    .ToList();

                foreach (var slot in libres)
                {
                    string dia = Reserva2DiaSemana(slot.Fecha);
                    if (entraEnHorario(dia, slot.HoraInicio, slot.HoraFin))
                        continue;

                    bool conHistorial =
                        ctx.Reservas.Any(r => r.IdDisponibilidad == slot.IdDisponibilidad)
                        || ctx.ListasEspera.Any(l => l.IdDisponibilidad == slot.IdDisponibilidad);

                    if (conHistorial)
                        slot.Estado = EstadoSlot.Bloqueada;
                    else
                        ctx.Disponibilidades.Remove(slot);
                }

                ctx.SaveChanges();
                tx.Commit();
            }
        }

        private static string Reserva2DiaSemana(System.DateTime fecha)
        {
            return fecha.DayOfWeek switch
            {
                System.DayOfWeek.Monday => "Lunes",
                System.DayOfWeek.Tuesday => "Martes",
                System.DayOfWeek.Wednesday => "Miercoles",
                System.DayOfWeek.Thursday => "Jueves",
                System.DayOfWeek.Friday => "Viernes",
                System.DayOfWeek.Saturday => "Sabado",
                _ => "Domingo"
            };
        }

        /// <summary>Inserta una nueva regla de disponibilidad.</summary>
        /// <param name="regla">Regla a insertar.</param>
        public void Insertar(ReglaDisponibilidad regla)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                ctx.ReglasDisponibilidad.Add(regla);
                ctx.SaveChanges();
            }
        }
    }
}
