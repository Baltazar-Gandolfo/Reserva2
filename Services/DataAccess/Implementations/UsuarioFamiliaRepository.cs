
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using Services.DataAccess.Tools;
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
    /// Repositorio de la relación entre usuarios y familias de permisos.
    /// </summary>
    internal class UsuarioFamiliaRepository : IJoinRepository<Familia, Usuario>
    {
        /// <summary>Asocia una familia de permisos a un usuario.</summary>
        /// <param name="familia">Familia a asociar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public void Add(Familia familia, Usuario usuario)
        {
            SqlHelper.ExecuteNonQuery("INSERT INTO UsuarioFamilia (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@IdFamilia", familia.Id));
        }

        /// <summary>Asocia múltiples familias a un usuario. No implementado.</summary>
        public void AddRange(List<Familia> children, Usuario parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>Elimina una relación de familia. No implementado.</summary>
        public void Delete(Familia obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Obtiene las familias de permisos asignadas a un usuario.</summary>
        /// <param name="obj">Usuario a consultar.</param>
        /// <returns>Lista de familias del usuario.</returns>
        public List<Familia> GetByObject(Usuario obj)
        {
            List<Familia> familias = new List<Familia>();

            using(SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdFamilia FROM UsuarioFamilia WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario)))
            {
                while (dataReader.Read())
                {
                    Guid idFamilia = dataReader.GetGuid(0);

                    familias.Add(new FamiliaRepository().GetById(idFamilia));
                }
            }

            return familias;
        }

        /// <summary>Elimina la asociación entre una familia y un usuario.</summary>
        /// <param name="familia">Familia a desasociar.</param>
        /// <param name="usuario">Usuario afectado.</param>
        public void Delete(Familia familia, Usuario usuario)
        {
            string commandText = "DELETE FROM UsuarioFamilia WHERE IdUsuario = @IdUsuario AND IdFamilia = @IdFamilia";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@IdFamilia", familia.Id));
        }
    }
}
