
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
    /// Repositorio de la relación padre-hijo entre familias de permisos.
    /// </summary>
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia, Familia>
    {
        /// <summary>Asocia una familia hija a una familia padre.</summary>
        /// <param name="hijo">Familia hija.</param>
        /// <param name="padre">Familia padre.</param>
        public void Add(Familia hijo, Familia padre)
        {
            SqlHelper.ExecuteNonQuery("INSERT INTO FamiliaFamilia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@IdFamiliaPadre, @IdFamiliaHijo)",
                CommandType.Text,
                new SqlParameter("@IdFamiliaPadre", padre.Id),
                new SqlParameter("@IdFamiliaHijo", hijo.Id));
        }

        /// <summary>Asocia múltiples familias hijas a un padre. No implementado.</summary>
        public void AddRange(List<Familia> children, Familia parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>Elimina una relación de familia. No implementado.</summary>
        public void Delete(Familia obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Obtiene las familias hijas de una familia padre.</summary>
        /// <param name="obj">Familia padre.</param>
        /// <returns>Lista de familias hijas.</returns>
        public List<Familia> GetByObject(Familia obj)
        {
            List<Familia> familias = new List<Familia>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdFamiliaHijo FROM FamiliaFamilia WHERE IdFamiliaPadre = @IdFamiliaPadre",
                CommandType.Text,
                new SqlParameter("@IdFamiliaPadre", obj.Id)))
            {
                while (dataReader.Read())
                {
                    Guid idFamilia = dataReader.GetGuid(0);

                    familias.Add(new FamiliaRepository().GetById(idFamilia));
                }
            }

            return familias;
        }
    }
}
