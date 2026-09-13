using Services.DataAccess.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    internal class ErrorRepository
    {
        public void Insertar(string mensaje, string tipoExcepcion, string stackTrace, string modulo, Guid? idUsuario)
        {
            string sql = @"INSERT INTO LogError 
                (Mensaje, TipoExcepcion, StackTrace, Modulo, FechaCreacion, IdUsuario) 
                VALUES 
                (@Mensaje, @TipoExcepcion, @StackTrace, @Modulo, @FechaCreacion, @IdUsuario)";

            SqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Mensaje", mensaje),
                new SqlParameter("@TipoExcepcion", (object)tipoExcepcion ?? DBNull.Value),
                new SqlParameter("@StackTrace", (object)stackTrace ?? DBNull.Value),
                new SqlParameter("@Modulo", (object)modulo ?? DBNull.Value),
                new SqlParameter("@FechaCreacion", DateTime.Now),
                new SqlParameter("@IdUsuario", (object)idUsuario ?? DBNull.Value));
        }

        public List<Dictionary<string, string>> Listar(DateTime? desde, DateTime? hasta)
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            string sql = @"SELECT e.IdError, e.Mensaje, e.TipoExcepcion, e.StackTrace, e.Modulo, 
                           e.FechaCreacion, u.Nombre as NombreUsuario
                           FROM LogError e 
                           LEFT JOIN Usuario u ON e.IdUsuario = u.IdUsuario
                           WHERE 1=1";

            List<SqlParameter> parametros = new List<SqlParameter>();

            if (desde.HasValue)
            {
                sql += " AND e.FechaCreacion >= @Desde";
                parametros.Add(new SqlParameter("@Desde", desde.Value));
            }
            if (hasta.HasValue)
            {
                sql += " AND e.FechaCreacion <= @Hasta";
                parametros.Add(new SqlParameter("@Hasta", hasta.Value.AddDays(1)));
            }

            sql += " ORDER BY e.FechaCreacion DESC";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, parametros.ToArray()))
            {
                while (reader.Read())
                {
                    var item = new Dictionary<string, string>();
                    item["IdError"] = reader["IdError"].ToString();
                    item["Mensaje"] = reader["Mensaje"].ToString();
                    item["TipoExcepcion"] = reader["TipoExcepcion"].ToString();
                    item["StackTrace"] = reader["StackTrace"].ToString();
                    item["Modulo"] = reader["Modulo"].ToString();
                    item["FechaCreacion"] = Convert.ToDateTime(reader["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm:ss");
                    item["NombreUsuario"] = reader["NombreUsuario"].ToString();
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}