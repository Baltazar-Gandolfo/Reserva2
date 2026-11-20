using Reserva2.BE.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para reglas de negocio configurables.
    /// </summary>
    public class ReglaNegocioRepository
    {
        /// <summary>Obtiene las reglas de negocio de un comercio.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <returns>Lista de reglas del comercio.</returns>
        public List<ReglaNegocio> GetByComercio(int idComercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasNegocio.Where(r => r.IdComercio == idComercio).ToList();
            }
        }

        /// <summary>Obtiene una regla de negocio por clave dentro de un comercio.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="clave">Clave de la regla.</param>
        /// <returns>Regla encontrada o null.</returns>
        public ReglaNegocio GetByClave(int idComercio, string clave)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasNegocio
                    .FirstOrDefault(r => r.IdComercio == idComercio && r.Clave == clave);
            }
        }

        /// <summary>Inserta un conjunto de reglas de negocio.</summary>
        /// <param name="reglas">Reglas a insertar.</param>
        public void Insertar(IEnumerable<ReglaNegocio> reglas)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                ctx.ReglasNegocio.AddRange(reglas);
                ctx.SaveChanges();
            }
        }
    }
}
