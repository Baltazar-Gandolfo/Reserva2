
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
    /// Adaptador Singleton que convierte datos SQL en objetos Familia con sus hijos.
    /// </summary>
    internal class FamiliaAdapter : IAdapter<Familia>
    {
        #region Singleton
        private readonly static FamiliaAdapter _instance = new FamiliaAdapter();

        /// <summary>Obtiene la instancia única del adaptador.</summary>
        public static FamiliaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private FamiliaAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion
        /// <summary>Convierte un arreglo de valores SQL en un objeto Familia con sus hijos.</summary>
        /// <param name="values">Valores de la fila de la tabla Familia.</param>
        /// <returns>Objeto Familia hidratado con familias y patentes hijas.</returns>
        public Familia Get(object[] values)
        {
            Familia familia = new Familia();
            //Hidratación de los datos primitivos del objeto
            familia.Id = Guid.Parse(values[0].ToString());
            familia.Nombre = values[1].ToString();

            //Hidratación de los objetos hijos

            familia.AddRange(new FamiliaFamiliaRepository().GetByObject(familia));

            familia.AddRange(new FamiliaPatenteRepository().GetByObject(familia));

            return familia;
        }
    }
}
