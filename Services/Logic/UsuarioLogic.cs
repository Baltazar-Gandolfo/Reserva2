using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using System;
using Services.Facade;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de usuarios, autenticación y permisos.
    /// </summary>
    internal static class UsuarioLogic
    {
        private static IUsuarioRepository _usuarioRepository;

        static UsuarioLogic()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="user">Email del usuario.</param>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Usuario autenticado con sus privilegios.</returns>
        public static Usuario ValidarCredenciales(string user, string password)
        {
            password = CryptographyService.HashMd5(password);

            Usuario usuario = _usuarioRepository.GetByCredentials(user, password);

            if (usuario == null)
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }
            else if (!usuario.Habilitado)
            {
                throw new Exception("Usuario no habilitado.");
            }

            return usuario;
        }

        /// <summary>
        /// Registra un nuevo usuario verificando unicidad de DNI y recalculando dígitos verificadores.
        /// </summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public static void RegistrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");

            string dniParaBD = CryptographyService.Encrypt(usuario.DNI);
            if (DigitoVerificadorLogic.ExisteDNI(dniParaBD))
                throw new Exception("Ya existe un usuario registrado con ese DNI.");

            _usuarioRepository.Add(usuario);

            if (usuario.IdUsuario == Guid.Empty)
                throw new Exception("El usuario no pudo ser registrado.");

            DigitoVerificadorLogic.RecalcularTodo();
        }

        /// <summary>
        /// Asigna una familia de permisos a un usuario.
        /// </summary>
        /// <param name="familia">Familia a asignar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public static void AgregarFamilia(Familia familia, Usuario usuario)
        {
            new UsuarioFamiliaRepository().Add(familia, usuario);
        }

        /// <summary>
        /// Asigna una patente individual a un usuario.
        /// </summary>
        /// <param name="patente">Patente a asignar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public static void AgregarPatente(Patente patente, Usuario usuario)
        {
            new UsuarioPatenteRepository().Add(patente, usuario);
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public static List<Usuario> GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        /// <summary>
        /// Quita una familia de permisos de un usuario.
        /// </summary>
        /// <param name="familia">Familia a quitar.</param>
        /// <param name="usuario">Usuario afectado.</param>
        public static void QuitarFamilia(Familia familia, Usuario usuario)
        {
            new UsuarioFamiliaRepository().Delete(familia, usuario);
        }

        /// <summary>
        /// Cambia la contraseña de un usuario y recalcula los dígitos verificadores.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoHash">Nuevo hash de la contraseña.</param>
        public static void CambiarPassword(Guid idUsuario, string nuevoHash)
        {
            _usuarioRepository.CambiarPassword(idUsuario, nuevoHash);
            DigitoVerificadorLogic.RecalcularTodo();
        }

        /// <summary>
        /// Cambia el estado de un usuario y recalcula los dígitos verificadores.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoEstado">Nuevo estado (Activo, Inactivo, Bloqueado).</param>
        public static void CambiarEstado(Guid idUsuario, string nuevoEstado)
        {
            _usuarioRepository.CambiarEstado(idUsuario, nuevoEstado);
            DigitoVerificadorLogic.RecalcularTodo();
        }

        /// <summary>
        /// Busca un usuario por su dirección de email.
        /// </summary>
        /// <param name="email">Email a buscar.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public static Usuario BuscarPorEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }
    }
}
