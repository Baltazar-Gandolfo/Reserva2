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
    /// Repositorio de acceso a datos para familias de permisos.
    /// </summary>
    internal class FamiliaRepository : IFamiliaRepository
    {
        /// <summary>Inserta una nueva familia de permisos.</summary>
        /// <param name="entity">Familia a insertar.</param>
        public void Add(Familia entity)
        {
            entity.Id = Guid.NewGuid();
            string commandText = "INSERT INTO Familia (IdFamilia, Nombre) VALUES (@IdFamilia, @Nombre)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdFamilia", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre));
        }

        /// <summary>Obtiene todas las familias de permisos.</summary>
        /// <returns>Lista de familias.</returns>
        public List<Familia> GetAll()
        {
            List<Familia> familias = new List<Familia>();
            string commandText = "SELECT IdFamilia, Nombre FROM Familia";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familias.Add(FamiliaAdapter.Current.Get(data));
                }
            }
            return familias;
        }

        /// <summary>Elimina una familia y todas sus relaciones asociadas.</summary>
        /// <param name="id">Identificador de la familia.</param>
        public void Delete(Guid id)
        {
            SqlHelper.ExecuteNonQuery("DELETE FROM FamiliaPatente WHERE IdFamilia = @Id", CommandType.Text, new SqlParameter("@Id", id));
            SqlHelper.ExecuteNonQuery("DELETE FROM FamiliaFamilia WHERE IdFamiliaPadre = @Id OR IdFamiliaHijo = @Id", CommandType.Text, new SqlParameter("@Id", id));
            SqlHelper.ExecuteNonQuery("DELETE FROM UsuarioFamilia WHERE IdFamilia = @Id", CommandType.Text, new SqlParameter("@Id", id));
            SqlHelper.ExecuteNonQuery("DELETE FROM Familia WHERE IdFamilia = @Id", CommandType.Text, new SqlParameter("@Id", id));
        }

        /// <summary>Obtiene una familia por su identificador.</summary>
        /// <param name="id">Identificador de la familia.</param>
        /// <returns>Familia encontrada o null.</returns>
        public Familia GetById(Guid id)
        {
            string SelectByIdStatement = "SELECT IdFamilia, Nombre FROM [dbo].[Familia] WHERE IdFamilia = @IdFamilia";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                     CommandType.Text,
                                                     new SqlParameter[] { new SqlParameter("@IdFamilia", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return FamiliaAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>Actualiza una familia. No implementado.</summary>
        /// <param name="entity">Familia a actualizar.</param>
        public void Update(Familia entity)
        {
            throw new NotImplementedException();
        }
    }
}
