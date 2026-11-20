using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Adaptador Singleton que convierte datos SQL en objetos Patente.
    /// </summary>
    internal class PatenteAdapter : IAdapter<Patente>
    {
        #region Singleton
        private readonly static PatenteAdapter _instance = new PatenteAdapter();

        /// <summary>Obtiene la instancia única del adaptador.</summary>
        public static PatenteAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private PatenteAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion
        /// <summary>Convierte un arreglo de valores SQL en un objeto Patente.</summary>
        /// <param name="values">Valores de la fila de la tabla Patente.</param>
        /// <returns>Objeto Patente hidratado.</returns>
        public Patente Get(object[] values)
        {
            Patente patente = new Patente();
            patente.Id = Guid.Parse(values[0].ToString());
            patente.DataKey = values[1].ToString();
            patente.TipoAcceso = (TipoAcceso)Enum.Parse(typeof(TipoAcceso), values[2].ToString());
            return patente;
        }
    }
}
