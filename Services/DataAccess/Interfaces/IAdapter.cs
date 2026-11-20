using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataAccess.Interfaces
{
    /// <summary>
    /// Interfaz para adaptar un array de valores de base de datos a una entidad de dominio.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a construir.</typeparam>
    internal interface IAdapter<T> where T : class
    {
        /// <summary>Construye una instancia de <typeparamref name="T"/> a partir de los valores.</summary>
        T Get(object[] values);
    }
}
