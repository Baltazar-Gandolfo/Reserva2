using Reserva2.DAL.Repositories;
using Services.Facade;

namespace Reserva2.BLL.Logic.Reservas
{
    /// <summary>
    /// Lógica de negocio para la gestión de la lista de espera de slots.
    /// </summary>
    public class ListaEsperaBLL
    {
        private readonly ListaEsperaRepository _repo = new ListaEsperaRepository();

        /// <summary>
        /// Notifica al primer usuario en la cola de espera cuando se libera un slot.
        /// </summary>
        /// <param name="idDisponibilidad">Identificador del slot liberado.</param>
        /// <returns>True si se notificó a alguien, false si la cola estaba vacía.</returns>
        public bool NotificarPrimero(int idDisponibilidad)
        {
            var usuario = _repo.NotificarPrimero(idDisponibilidad);
            if (usuario == null)
                return false;

            BitacoraService.Registrar("Notificación", "ListaEspera",
                $"Se notificó al usuario {usuario} por la liberación del slot ID {idDisponibilidad}",
                idDisponibilidad);
            return true;
        }
    }
}
