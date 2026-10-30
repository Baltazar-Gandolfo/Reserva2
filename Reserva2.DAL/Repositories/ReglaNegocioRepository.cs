using Reserva2.BE.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ReglaNegocioRepository
    {
        public List<ReglaNegocio> GetByComercio(int idComercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasNegocio.Where(r => r.IdComercio == idComercio).ToList();
            }
        }

        public ReglaNegocio GetByClave(int idComercio, string clave)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.ReglasNegocio
                    .FirstOrDefault(r => r.IdComercio == idComercio && r.Clave == clave);
            }
        }

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
