using Services.Dal.Implementations.Adapters;
using Services.DataAccess;
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using Services.DataAccess.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
    internal class UsuarioRepository : IUsuarioRepository
    {

        public Usuario GetByCredentials(string email, string password)
        {
            string commandText = "SELECT IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro FROM Usuario WHERE Email = @Email AND PasswordHash = @Password AND Estado = 'Activo'";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)))
            {
                if (dataReader.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = dataReader.GetGuid(dataReader.GetOrdinal("IdUsuario")),
                        DNI = dataReader["DNI"].ToString(),
                        Nombre = dataReader["Nombre"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        Password = dataReader["PasswordHash"].ToString(),
                        Rol = dataReader["Rol"].ToString(),
                        Telefono = dataReader["Telefono"].ToString(),
                        Estado = dataReader["Estado"].ToString(),
                        FechaRegistro = Convert.ToDateTime(dataReader["FechaRegistro"])
                    };
                }
                return null;
            }
        }

        public void Add(Usuario usuario)
        {
            usuario.IdUsuario = Guid.NewGuid();
            string commandText = @"INSERT INTO Usuario 
        (IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro) 
        VALUES 
        (@IdUsuario, @DNI, @Nombre, @Email, @PasswordHash, @Rol, @Telefono, @Estado, @FechaRegistro)";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@DNI", usuario.DNI),
                new SqlParameter("@Nombre", usuario.Nombre),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@PasswordHash", usuario.Password),
                new SqlParameter("@Rol", usuario.Rol),
                new SqlParameter("@Telefono", usuario.Telefono),
                new SqlParameter("@Estado", usuario.Estado),
                new SqlParameter("@FechaRegistro", usuario.FechaRegistro));
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
