using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio genérico con operaciones CRUD básicas.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad del dominio.</typeparam>
    public interface IGenericRepository<T>
    {
        /// <summary>Agrega una nueva entidad.</summary>
        /// <param name="entity">Entidad a agregar.</param>
        void Add(T entity);
        /// <summary>Actualiza una entidad existente.</summary>
        /// <param name="entity">Entidad con los datos actualizados.</param>
        void Update(T entity);
        /// <summary>Elimina una entidad por su identificador.</summary>
        /// <param name="id">Identificador de la entidad a eliminar.</param>
        void Delete(Guid id);
        /// <summary>Obtiene una entidad por su identificador.</summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>La entidad encontrada o null.</returns>
        T GetById(Guid id);
        /// <summary>Obtiene todas las entidades.</summary>
        /// <returns>Lista con todas las entidades.</returns>
        List<T> GetAll();
    }
}
