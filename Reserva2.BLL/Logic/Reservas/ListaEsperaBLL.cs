using Reserva2.DAL.Repositories;
using Services.Facade;

namespace Reserva2.BLL.Logic.Reservas
{
    public class ListaEsperaBLL
    {
        private readonly ListaEsperaRepository _repo = new ListaEsperaRepository();

        // Notifica al primero de la cola cuando se libera un slot (si hay alguien esperando).
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
