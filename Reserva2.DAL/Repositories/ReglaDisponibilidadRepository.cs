using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ReglaDisponibilidadRepository
    {
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

        public ReglaDisponibilidad GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasDisponibilidad.FirstOrDefault(r => r.IdReglaDisponibilidad == id);
            }
        }

        // Una regla activa del mismo elemento y día se superpone si los rangos horarios se cruzan.
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

        // Desactiva las reglas vigentes, inserta las nuevas y limpia los slots libres futuros
        // que ya no entran en el horario (se borran, o se bloquean si tienen historial de reservas).
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
