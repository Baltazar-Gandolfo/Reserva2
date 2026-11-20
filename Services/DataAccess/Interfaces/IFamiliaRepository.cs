using DataAccess.Interfaces;
using Services.DomainModel.Composite;
using System;

namespace Services.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio específico para la entidad Familia.
    /// </summary>
    internal interface IFamiliaRepository : IGenericRepository<Familia>
    {
    }
}
