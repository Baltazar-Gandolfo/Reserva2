using Services.DomainModel.Composite;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Singleton que gestiona la sesión del usuario autenticado.
    /// </summary>
    public sealed class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Usuario actualmente autenticado en la sesión.
        /// </summary>
        public Usuario UsuarioActual { get; private set; }

        /// <summary>
        /// Fecha y hora en que se inició la sesión actual.
        /// </summary>
        public DateTime? FechaLogin { get; private set; }

        private SessionManager() { }

        /// <summary>
        /// Obtiene la instancia única del administrador de sesión.
        /// </summary>
        /// <returns>Instancia de <see cref="SessionManager"/>.</returns>
        public static SessionManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new SessionManager();
                }
            }
            return _instance;
        }

        /// <summary>
        /// Inicia una nueva sesión para el usuario indicado.
        /// </summary>
        /// <param name="usuario">Usuario que inicia sesión.</param>
        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
            FechaLogin = DateTime.Now;
        }

        /// <summary>
        /// Cierra la sesión actual y libera los datos del usuario.
        /// </summary>
        public void CerrarSesion()
        {
            UsuarioActual = null;
            FechaLogin = null;
        }

        /// <summary>
        /// Indica si hay un usuario autenticado en la sesión.
        /// </summary>
        /// <returns><c>true</c> si hay sesión activa; de lo contrario, <c>false</c>.</returns>
        public bool EstaAutenticado()
        {
            return UsuarioActual != null;
        }

        /// <summary>
        /// Obtiene el rol del usuario autenticado.
        /// </summary>
        /// <returns>Nombre del rol o cadena vacía si no hay sesión.</returns>
        public string GetRol()
        {
            return UsuarioActual?.Rol ?? "";
        }
    }
}