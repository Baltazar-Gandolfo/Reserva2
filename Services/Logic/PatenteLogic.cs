using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using System;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de patentes (permisos atómicos).
    /// </summary>
    /// <summary>
    /// Lógica de negocio para la gestión de patentes (permisos atómicos).
    /// </summary>
    internal static class PatenteLogic
    {
        private static readonly IPatenteRepository _repo = new PatenteRepository();

        /// <summary>
        /// Agrega una nueva patente al sistema.
        /// </summary>
        /// <param name="patente">Patente a crear.</param>
        public static void Add(Patente patente)
        {
            _repo.Add(patente);
        }

        /// <summary>
        /// Obtiene una patente por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la patente.</param>
        /// <returns>Patente encontrada.</returns>
        public static Patente GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Obtiene todas las patentes del sistema.
        /// </summary>
        /// <returns>Lista de patentes.</returns>
        public static List<Patente> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
