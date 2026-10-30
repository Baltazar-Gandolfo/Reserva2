using Reserva2.BE.Entities;
using System;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ListaEsperaRepository
    {
        // Marca como notificado al primero de la cola (menor posición) y registra la notificación.
        // Devuelve el usuario notificado o null si nadie esperaba el slot.
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
