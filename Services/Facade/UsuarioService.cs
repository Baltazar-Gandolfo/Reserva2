using Services.DomainModel.Composite;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para la gestión de usuarios del sistema.
    /// </summary>
    public static class UsuarioService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public static void RegistrarUsuario(Usuario usuario)
        {
            Logic.UsuarioLogic.RegistrarUsuario(usuario);
        }

        /// <summary>
        /// Asigna una familia (rol compuesto) a un usuario.
        /// </summary>
        /// <param name="familia">Familia a asignar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public static void AgregarFamilia(Familia familia, Usuario usuario)
        {
            Logic.UsuarioLogic.AgregarFamilia(familia, usuario);
        }

        /// <summary>
        /// Asigna una patente (permiso) a un usuario.
        /// </summary>
        /// <param name="patente">Patente a asignar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public static void AgregarPatente(Patente patente, Usuario usuario)
        {
            Logic.UsuarioLogic.AgregarPatente(patente, usuario);
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public static List<Usuario> GetAll()
        {
            return Logic.UsuarioLogic.GetAll();
        }

        /// <summary>
        /// Quita una familia (rol compuesto) de un usuario.
        /// </summary>
        /// <param name="familia">Familia a quitar.</param>
        /// <param name="usuario">Usuario afectado.</param>
        public static void QuitarFamilia(Familia familia, Usuario usuario)
        {
            Logic.UsuarioLogic.QuitarFamilia(familia, usuario);
        }

        /// <summary>
        /// Cambia la contraseña de un usuario.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoHash">Nuevo hash de contraseña.</param>
        public static void CambiarPassword(Guid idUsuario, string nuevoHash)
        {
            Logic.UsuarioLogic.CambiarPassword(idUsuario, nuevoHash);
        }

        /// <summary>
        /// Cambia el estado de un usuario (Activo, Bloqueado, etc.).
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoEstado">Nuevo estado a asignar.</param>
        public static void CambiarEstado(Guid idUsuario, string nuevoEstado)
        {
            Logic.UsuarioLogic.CambiarEstado(idUsuario, nuevoEstado);
        }

        /// <summary>
        /// Busca un usuario por su dirección de email.
        /// </summary>
        /// <param name="email">Email del usuario a buscar.</param>
        /// <returns>Usuario encontrado o null si no existe.</returns>
        public static Usuario BuscarPorEmail(string email)
        {
            return Logic.UsuarioLogic.BuscarPorEmail(email);
        }
    }
}
