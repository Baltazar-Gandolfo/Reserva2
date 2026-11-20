using System.Configuration;

namespace Reserva2.DAL
{
    /// <summary>
    /// Factory que centraliza la creación del contexto EF de la base de Negocio.
    /// </summary>
    public static class NegocioContextFactory
    {
        /// <summary>Crea una nueva instancia del contexto de base de datos de negocio.</summary>
        /// <returns>Instancia de <see cref="NegocioDbContext"/>.</returns>
        public static NegocioDbContext Crear()
        {
            return new NegocioDbContext(
                ConfigurationManager.ConnectionStrings["NegocioDB"].ConnectionString);
        }
    }
}
