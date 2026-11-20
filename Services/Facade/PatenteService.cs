using Services.DomainModel.Composite;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para la gestión de patentes (permisos atómicos del patrón Composite).
    /// </summary>
    public static class PatenteService
    {
        /// <summary>
        /// Crea una nueva patente en el sistema.
        /// </summary>
        /// <param name="patente">Patente a crear.</param>
        public static void Add(Patente patente)
        {
            Logic.PatenteLogic.Add(patente);
        }

        /// <summary>
        /// Obtiene una patente por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la patente.</param>
        /// <returns>Patente encontrada.</returns>
        public static Patente GetById(Guid id)
        {
            return Logic.PatenteLogic.GetById(id);
        }

        /// <summary>
        /// Obtiene todas las patentes del sistema.
        /// </summary>
        /// <returns>Lista de patentes.</returns>
        public static List<Patente> GetAll()
        {
            return Logic.PatenteLogic.GetAll();
        }
    }
}
