using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio para tablas de relación muchos-a-muchos.
    /// </summary>
    /// <typeparam name="T">Tipo hijo de la relación.</typeparam>
    /// <typeparam name="Y">Tipo padre de la relación.</typeparam>
    internal interface IJoinRepository <T, Y>
    {
        /// <summary>Obtiene los hijos asociados a un padre.</summary>
        List<T> GetByObject(Y obj);
        /// <summary>Elimina una relación.</summary>
        void Delete(T obj);
        /// <summary>Agrega una relación hijo-padre.</summary>
        void Add(T obj, Y parent);
        /// <summary>Agrega múltiples hijos a un padre.</summary>
        void AddRange(List<T> children, Y parent);
    }
}
