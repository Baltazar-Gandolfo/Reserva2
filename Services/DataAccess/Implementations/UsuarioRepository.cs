using Services.Dal.Implementations.Adapters;
using Services.DAL.Implementations.Adapters;
using Services.DataAccess;
using Services.DataAccess.Interfaces;
using Services.DataAccess.Tools;
using Services.DomainModel.Composite;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de acceso a datos para la entidad Usuario.
    /// </summary>
    internal class UsuarioRepository : IUsuarioRepository
    {
        /// <summary>Busca un usuario activo por email y contraseña hasheada.</summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Hash MD5 de la contraseña.</param>
        /// <returns>Usuario autenticado o null si no coincide.</returns>
        public Usuario GetByCredentials(string email, string password)
        {
            string commandText = "SELECT IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro FROM Usuario WHERE Email = @Email AND PasswordHash = @Password AND Estado = 'Activo'";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        /// <summary>Inserta un nuevo usuario en la base de datos con DNI encriptado.</summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public void Add(Usuario usuario)
        {
            usuario.IdUsuario = Guid.NewGuid();
            string dniEncriptado = CryptographyService.Encrypt(usuario.DNI);

            string commandText = @"INSERT INTO Usuario
        (IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro)
        VALUES
        (@IdUsuario, @DNI, @Nombre, @Email, @PasswordHash, @Rol, @Telefono, @Estado, @FechaRegistro)";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@DNI", dniEncriptado),
                new SqlParameter("@Nombre", usuario.Nombre),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@PasswordHash", usuario.Password),
                new SqlParameter("@Rol", usuario.Rol),
                new SqlParameter("@Telefono", usuario.Telefono),
                new SqlParameter("@Estado", usuario.Estado),
                new SqlParameter("@FechaRegistro", usuario.FechaRegistro));
        }

        /// <summary>Actualiza un usuario existente. No implementado.</summary>
        /// <param name="entity">Usuario a actualizar.</param>
        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>Elimina un usuario por su identificador. No implementado.</summary>
        /// <param name="id">Identificador del usuario.</param>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>Obtiene un usuario por su identificador. No implementado.</summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <returns>Usuario encontrado.</returns>
        public Usuario GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>Obtiene todos los usuarios del sistema.</summary>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string commandText = "SELECT IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro FROM Usuario";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    usuarios.Add(UsuarioAdapter.Current.Get(data));
                }
            }
            return usuarios;
        } 

        /// <summary>Cambia la contraseña de un usuario.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoHash">Nuevo hash de la contraseña.</param>
        public void CambiarPassword(Guid idUsuario, string nuevoHash)
        {
            string sql = "UPDATE Usuario SET PasswordHash = @Hash WHERE IdUsuario = @Id";
            SqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Hash", nuevoHash),
                new SqlParameter("@Id", idUsuario));
        }

        /// <summary>Cambia el estado de un usuario.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="nuevoEstado">Nuevo estado (Activo, Inactivo, Bloqueado).</param>
        public void CambiarEstado(Guid idUsuario, string nuevoEstado)
        {
            string sql = "UPDATE Usuario SET Estado = @Estado WHERE IdUsuario = @Id";
            SqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Estado", nuevoEstado),
                new SqlParameter("@Id", idUsuario));
        }

        /// <summary>Busca un usuario por su dirección de email.</summary>
        /// <param name="email">Email a buscar.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public Usuario GetByEmail(string email)
        {
            string sql = "SELECT IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro FROM Usuario WHERE Email = @Email";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@Email", email)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }
    }
}
