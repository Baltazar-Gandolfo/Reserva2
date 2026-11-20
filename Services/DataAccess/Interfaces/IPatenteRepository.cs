using DataAccess.Interfaces;
using Services.DomainModel.Composite;

namespace Services.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio específico para la entidad Patente.
    /// </summary>
    internal interface IPatenteRepository : IGenericRepository<Patente>
    {
    }
}
