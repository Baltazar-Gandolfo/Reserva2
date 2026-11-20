using Services.DomainModel.Composite;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para la gestión de familias (roles compuestos del patrón Composite).
    /// </summary>
    public static class FamiliaService
    {
        /// <summary>
        /// Crea una nueva familia en el sistema.
        /// </summary>
        /// <param name="familia">Familia a crear.</param>
        public static void Add(Familia familia)
        {
            Logic.FamiliaLogic.Add(familia);
        }

        /// <summary>
        /// Agrega una patente a una familia existente.
        /// </summary>
        /// <param name="patente">Patente a agregar.</param>
        /// <param name="familia">Familia destino.</param>
        public static void AgregarPatente(Patente patente, Familia familia)
        {
            Logic.FamiliaLogic.AgregarPatente(patente, familia);
        }

        /// <summary>
        /// Agrega una familia hija dentro de una familia padre.
        /// </summary>
        /// <param name="hijo">Familia hija.</param>
        /// <param name="padre">Familia padre.</param>
        public static void AgregarFamilia(Familia hijo, Familia padre)
        {
            Logic.FamiliaLogic.AgregarFamilia(hijo, padre);
        }

        /// <summary>
        /// Obtiene una familia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la familia.</param>
        /// <returns>Familia encontrada.</returns>
        public static Familia GetById(Guid id)
        {
            return Logic.FamiliaLogic.GetById(id);
        }

        /// <summary>
        /// Obtiene todas las familias del sistema.
        /// </summary>
        /// <returns>Lista de familias.</returns>
        public static List<Familia> GetAll()
        {
            return Logic.FamiliaLogic.GetAll();
        }

        /// <summary>
        /// Elimina una familia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la familia a eliminar.</param>
        public static void Delete(Guid id)
        {
            Logic.FamiliaLogic.Delete(id);
        }
    }
}
