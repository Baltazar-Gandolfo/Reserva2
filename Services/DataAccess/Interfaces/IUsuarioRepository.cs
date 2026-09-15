using DataAccess.Interfaces;
using Services.DomainModel.Composite;

namespace Services.DataAccess.Interfaces
{
    internal interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        //Un camino es en el acceso es comparar los hash en
        //el where del sql server
        Usuario GetByCredentials(string user, string password);
        Usuario GetByEmail(string email);

        //Camino 2: Traer el usuario por nombre y luego comparar el
        //hash en la capa de servicio
        void CambiarPassword(Guid idUsuario, string nuevoHash);
        void CambiarEstado(Guid idUsuario, string nuevoEstado);
    }
}