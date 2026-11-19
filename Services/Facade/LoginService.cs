using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Logic;
using System;

namespace Services.Facade
{
    public static class LoginService
    {
        public static Usuario ValidarCredenciales(string user, string password)
        {
            return UsuarioLogic.ValidarCredenciales(user, password);
        }

        public static void RegistrarUsuario(Usuario usuario)
        {
            UsuarioLogic.RegistrarUsuario(usuario);
        }

        public static ResultadoIntegridad VerificarIntegridad()
        {
            return DigitoVerificadorLogic.Verificar();
        }
    }
}
