using Reserva2.BE.Entities;
using System;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para la lista de espera de slots.
    /// </summary>
    public class ListaEsperaRepository
    {
        /// <summary>Notifica al primero en la cola de espera de un slot y registra la notificación.</summary>
        /// <param name="idDisponibilidad">Identificador del slot liberado.</param>
        /// <returns>IdUsuario notificado o null si nadie esperaba.</returns>
        public Guid? NotificarPrimero(int idDisponibilidad)
        {
            using (var ctx = NegocioContextFactory.Crear())
            using (var tx = ctx.Database.BeginTransaction())
            {
                var primero = ctx.ListasEspera
                    .Where(l => l.IdDisponibilidad == idDisponibilidad && l.Estado == "Esperando")
                    .OrderBy(l => l.Posicion)
                    .FirstOrDefault();

                if (primero == null)
                {
                    tx.Rollback();
                    return null;
                }

                var ahora = DateTime.Now;
                primero.Estado = "Notificado";
                primero.NotificadoEn = ahora;

                ctx.Notificaciones.Add(new Notificacion
                {
                    Canal = "Sistema",
                    Estado = "Enviada",
                    Tipo = "SlotLiberado",
                    EnviadaEn = ahora,
                    FechaCreacion = ahora,
                    IdUsuario = primero.IdUsuario
                });

                ctx.SaveChanges();
                tx.Commit();
                return primero.IdUsuario;
            }
        }
    }
}
