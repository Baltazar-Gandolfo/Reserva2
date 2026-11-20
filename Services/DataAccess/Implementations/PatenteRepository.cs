using Services.Dal.Implementations.Adapters;
using Services.DomainModel.Composite;
using Services.DataAccess.Interfaces;
using Services.DataAccess.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de acceso a datos para patentes (permisos individuales).
    /// </summary>
    internal class PatenteRepository : IPatenteRepository
    {
        /// <summary>Inserta una nueva patente.</summary>
        /// <param name="entity">Patente a insertar.</param>
        public void Add(Patente entity)
        {
            entity.Id = Guid.NewGuid();
            string commandText = "INSERT INTO Patente (IdPatente, DataKey, TipoAcceso) VALUES (@IdPatente, @DataKey, @TipoAcceso)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdPatente", entity.Id),
                new SqlParameter("@DataKey", entity.DataKey),
                new SqlParameter("@TipoAcceso", (int)entity.TipoAcceso));
        }

        /// <summary>Obtiene todas las patentes del sistema.</summary>
        /// <returns>Lista de patentes.</returns>
        public List<Patente> GetAll()
        {
            List<Patente> patentes = new List<Patente>();
            string commandText = "SELECT IdPatente, DataKey, TipoAcceso FROM Patente";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    patentes.Add(PatenteAdapter.Current.Get(data));
                }
            }
            return patentes;
        }

        /// <summary>Elimina una patente y todas sus relaciones asociadas.</summary>
        /// <param name="id">Identificador de la patente.</param>
        public void Delete(Guid id)
        {
            SqlHelper.ExecuteNonQuery("DELETE FROM FamiliaPatente WHERE IdPatente = @Id", CommandType.Text, new SqlParameter("@Id", id));
            SqlHelper.ExecuteNonQuery("DELETE FROM UsuarioPatente WHERE IdPatente = @Id", CommandType.Text, new SqlParameter("@Id", id));
            SqlHelper.ExecuteNonQuery("DELETE FROM Patente WHERE IdPatente = @Id", CommandType.Text, new SqlParameter("@Id", id));
        }

        /// <summary>Obtiene una patente por su identificador.</summary>
        /// <param name="id">Identificador de la patente.</param>
        /// <returns>Patente encontrada o null.</returns>
        public Patente GetById(Guid id)
        {
            string SelectByIdStatement = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                     CommandType.Text,
                                                     new SqlParameter[] { new SqlParameter("@IdPatente", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return PatenteAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>Actualiza una patente. No implementado.</summary>
        /// <param name="entity">Patente a actualizar.</param>
        public void Update(Patente entity)
        {
            throw new NotImplementedException();
        }
    }
}
