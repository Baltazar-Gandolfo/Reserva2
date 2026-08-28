using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataAccess.Implementations.Adapters
{
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        public static UsuarioAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private UsuarioAdapter()
        {
        }
        #endregion

        public Usuario Get(object[] values)
        {
            // Columnas esperadas del SELECT:
            // [0] IdUsuario, [1] DNI, [2] Nombre, [3] Email,
            // [4] PasswordHash, [5] Rol, [6] Telefono,
            // [7] Estado, [8] FechaRegistro

            Usuario usuario = new Usuario
            {
                IdUsuario = Guid.Parse(values[0].ToString()),
                DNI = values[1].ToString(),
                Nombre = values[2].ToString(),
                Email = values[3].ToString(),
                Password = values[4].ToString(),
                Rol = values[5].ToString(),
                Telefono = values[6].ToString(),
                Estado = values[7].ToString(),
                FechaRegistro = Convert.ToDateTime(values[8])
            };

            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
    }
}