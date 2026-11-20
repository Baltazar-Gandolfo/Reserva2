
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
    /// Repositorio de la relación entre usuarios y patentes individuales.
    /// </summary>
    internal class UsuarioPatenteRepository : IJoinRepository<Patente, Usuario>
    {
        /// <summary>Asocia una patente a un usuario.</summary>
        /// <param name="patente">Patente a asociar.</param>
        /// <param name="usuario">Usuario destino.</param>
        public void Add(Patente patente, Usuario usuario)
        {
            SqlHelper.ExecuteNonQuery("INSERT INTO UsuarioPatente (IdUsuario, IdPatente) VALUES (@IdUsuario, @IdPatente)",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@IdPatente", patente.Id));
        }

        /// <summary>Asocia múltiples patentes a un usuario. No implementado.</summary>
        public void AddRange(List<Patente> children, Usuario parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>Elimina una relación de patente. No implementado.</summary>
        public void Delete(Patente obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Obtiene las patentes individuales asignadas a un usuario.</summary>
        /// <param name="obj">Usuario a consultar.</param>
        /// <returns>Lista de patentes del usuario.</returns>
        public List<Patente> GetByObject(Usuario obj)
        {
            List<Patente> patentes = new List<Patente>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdPatente FROM UsuarioPatente WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario)))
            {
                while (dataReader.Read())
                {
                    Guid idPatente = dataReader.GetGuid(0);

                    patentes.Add(new PatenteRepository().GetById(idPatente));
                }
            }

            return patentes;
        }
    }
}
