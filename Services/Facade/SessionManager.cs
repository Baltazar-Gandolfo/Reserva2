using Services.DomainModel.Composite;
using System;

namespace Services.Facade
{
    public sealed class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();

        public Usuario UsuarioActual { get; private set; }
        public DateTime? FechaLogin { get; private set; }

        private SessionManager() { }

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

        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
            FechaLogin = DateTime.Now;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            FechaLogin = null;
        }

        public bool EstaAutenticado()
        {
            return UsuarioActual != null;
        }

        public string GetRol()
        {
            return UsuarioActual?.Rol ?? "";
        }
    }
}