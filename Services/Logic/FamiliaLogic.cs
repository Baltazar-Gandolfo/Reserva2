using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using System;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de familias (roles compuestos).
    /// </summary>
    internal static class FamiliaLogic
    {
        private static readonly IFamiliaRepository _repo = new FamiliaRepository();

        /// <summary>
        /// Agrega una nueva familia al sistema.
        /// </summary>
        /// <param name="familia">Familia a crear.</param>
        public static void Add(Familia familia)
        {
            _repo.Add(familia);
        }

        /// <summary>
        /// Agrega una patente como hija de una familia.
        /// </summary>
        /// <param name="patente">Patente a agregar.</param>
        /// <param name="familia">Familia padre.</param>
        public static void AgregarPatente(Patente patente, Familia familia)
        {
            new FamiliaPatenteRepository().Add(patente, familia);
        }

        /// <summary>
        /// Agrega una familia como hija de otra familia.
        /// </summary>
        /// <param name="hijo">Familia hija.</param>
        /// <param name="padre">Familia padre.</param>
        public static void AgregarFamilia(Familia hijo, Familia padre)
        {
            new FamiliaFamiliaRepository().Add(hijo, padre);
        }

        /// <summary>
        /// Obtiene una familia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la familia.</param>
        /// <returns>Familia encontrada.</returns>
        public static Familia GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Obtiene todas las familias del sistema.
        /// </summary>
        /// <returns>Lista de familias.</returns>
        public static List<Familia> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Elimina una familia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la familia a eliminar.</param>
        public static void Delete(Guid id)
        {
            _repo.Delete(id);
        }
    }
}
