using System.Configuration;

namespace Reserva2.DAL
{
    // Factory: centraliza la creación del contexto EF de la base de Negocio.
    public static class NegocioContextFactory
    {
        public static NegocioDbContext Crear()
        {
            return new NegocioDbContext(
                ConfigurationManager.ConnectionStrings["NegocioDB"].ConnectionString);
        }
    }
}
