using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Logic;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para las operaciones de autenticación e inicio de sesión.
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// Valida las credenciales de un usuario contra la base de datos.
        /// </summary>
        /// <param name="user">Email del usuario.</param>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Usuario autenticado si las credenciales son válidas.</returns>
        public static Usuario ValidarCredenciales(string user, string password)
        {
            return UsuarioLogic.ValidarCredenciales(user, password);
        }

        /// <summary>
        /// Registra un nuevo usuario desde el formulario de registro.
        /// </summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public static void RegistrarUsuario(Usuario usuario)
        {
            UsuarioLogic.RegistrarUsuario(usuario);
        }

        /// <summary>
        /// Verifica la integridad de los dígitos verificadores al momento del login.
        /// </summary>
        /// <returns>Resultado de la verificación de integridad.</returns>
        public static ResultadoIntegridad VerificarIntegridad()
        {
            return DigitoVerificadorLogic.Verificar();
        }
    }
}
