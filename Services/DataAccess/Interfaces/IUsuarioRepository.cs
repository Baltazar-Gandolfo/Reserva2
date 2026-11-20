using DataAccess.Interfaces;
using Services.DomainModel.Composite;

namespace Services.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio específico para la entidad Usuario con operaciones de autenticación.
    /// </summary>
    internal interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        /// <summary>Obtiene un usuario validando sus credenciales.</summary>
        Usuario GetByCredentials(string user, string password);
        /// <summary>Busca un usuario por su email.</summary>
        Usuario GetByEmail(string email);
        /// <summary>Cambia la contraseña de un usuario.</summary>
        void CambiarPassword(Guid idUsuario, string nuevoHash);
        /// <summary>Cambia el estado de un usuario.</summary>
        void CambiarEstado(Guid idUsuario, string nuevoEstado);
    }
}