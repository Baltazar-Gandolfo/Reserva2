using Services.Dal.Implementations;
using Services.Dal.Implementations;
using Services.DataAccess.Interfaces;
using Services.DomainModel.Composite;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL.Implementations.Adapters
{
    /// <summary>
    /// Adaptador Singleton que convierte datos SQL en objetos Usuario con sus privilegios.
    /// </summary>
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        /// <summary>Obtiene la instancia única del adaptador.</summary>
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

        /// <summary>Convierte un arreglo de valores SQL en un objeto Usuario con privilegios.</summary>
        /// <param name="values">Valores de la fila de la tabla Usuario.</param>
        /// <returns>Objeto Usuario hidratado.</returns>
        public Usuario Get(object[] values)
        {
            string dniRaw = values[1].ToString();
            string dniValor;
            try
            {
                dniValor = CryptographyService.Decrypt(dniRaw);
            }
            catch
            {
                dniValor = dniRaw;
            }

            Usuario usuario = new Usuario
            {
                IdUsuario = Guid.Parse(values[0].ToString()),
                DNI = dniValor,
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