using Services.DomainModel.Composite;

namespace Services.Facade
{
    public static class UsuarioService
    {
        public static void RegistrarUsuario(Usuario usuario)
        {
            Logic.UsuarioLogic.RegistrarUsuario(usuario);
        }

        public static void AgregarFamilia(Familia familia, Usuario usuario)
        {
            Logic.UsuarioLogic.AgregarFamilia(familia, usuario);
        }

        public static void AgregarPatente(Patente patente, Usuario usuario)
        {
            Logic.UsuarioLogic.AgregarPatente(patente, usuario);
        }

        public static List<Usuario> GetAll()
        {
            return Logic.UsuarioLogic.GetAll();
        }

        public static void QuitarFamilia(Familia familia, Usuario usuario)
        {
            Logic.UsuarioLogic.QuitarFamilia(familia, usuario);
        }
        public static void CambiarPassword(Guid idUsuario, string nuevoHash)
        {
            Logic.UsuarioLogic.CambiarPassword(idUsuario, nuevoHash);
        }

        public static void CambiarEstado(Guid idUsuario, string nuevoEstado)
        {
            Logic.UsuarioLogic.CambiarEstado(idUsuario, nuevoEstado);
        }

        public static Usuario BuscarPorEmail(string email)
        {
            return Logic.UsuarioLogic.BuscarPorEmail(email);
        }
    }
}
