using Services.DataAccess.Tools;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    internal class BitacoraRepository
    {
        public void Insertar(LogEntry entry)
        {
            string sql = @"INSERT INTO Bitacora 
                (Accion, Entidad, Detalle, IdEntidad, ValorAnterior, ValorNuevo, Origen, IpOrigen, FechaCreacion, IdUsuario) 
                VALUES 
                (@Accion, @Entidad, @Detalle, @IdEntidad, @ValorAnterior, @ValorNuevo, @Origen, @IpOrigen, @FechaCreacion, @IdUsuario)";

            SqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Accion", entry.Accion),
                new SqlParameter("@Entidad", (object)entry.Entidad ?? DBNull.Value),
                new SqlParameter("@Detalle", (object)entry.Detalle ?? DBNull.Value),
                new SqlParameter("@IdEntidad", (object)entry.IdEntidad ?? DBNull.Value),
                new SqlParameter("@ValorAnterior", (object)entry.ValorAnterior ?? DBNull.Value),
                new SqlParameter("@ValorNuevo", (object)entry.ValorNuevo ?? DBNull.Value),
                new SqlParameter("@Origen", (object)entry.Origen ?? DBNull.Value),
                new SqlParameter("@IpOrigen", (object)entry.IpOrigen ?? DBNull.Value),
                new SqlParameter("@FechaCreacion", entry.FechaCreacion),
                new SqlParameter("@IdUsuario", (object)entry.IdUsuario ?? DBNull.Value));
        }

        public List<LogEntry> Listar(DateTime? desde, DateTime? hasta, string accion, string entidad)
        {
            List<LogEntry> lista = new List<LogEntry>();
            string sql = @"SELECT b.IdBitacora, b.Accion, b.Entidad, b.Detalle, b.IdEntidad, 
                           b.ValorAnterior, b.ValorNuevo, b.Origen, b.IpOrigen, b.FechaCreacion, 
                           b.IdUsuario, u.Nombre as NombreUsuario
                           FROM Bitacora b 
                           LEFT JOIN Usuario u ON b.IdUsuario = u.IdUsuario
                           WHERE 1=1";

            List<SqlParameter> parametros = new List<SqlParameter>();

            if (desde.HasValue)
            {
                sql += " AND b.FechaCreacion >= @Desde";
                parametros.Add(new SqlParameter("@Desde", desde.Value));
            }
            if (hasta.HasValue)
            {
                sql += " AND b.FechaCreacion <= @Hasta";
                parametros.Add(new SqlParameter("@Hasta", hasta.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(accion))
            {
                sql += " AND b.Accion LIKE @Accion";
                parametros.Add(new SqlParameter("@Accion", "%" + accion + "%"));
            }
            if (!string.IsNullOrEmpty(entidad))
            {
                sql += " AND b.Entidad LIKE @Entidad";
                parametros.Add(new SqlParameter("@Entidad", "%" + entidad + "%"));
            }

            sql += " ORDER BY b.FechaCreacion DESC";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, parametros.ToArray()))
            {
                while (reader.Read())
                {
                    lista.Add(new LogEntry
                    {
                        IdBitacora = Convert.ToInt32(reader["IdBitacora"]),
                        Accion = reader["Accion"].ToString(),
                        Entidad = reader["Entidad"].ToString(),
                        Detalle = reader["Detalle"].ToString(),
                        IdEntidad = reader["IdEntidad"] == DBNull.Value ? null : Convert.ToInt32(reader["IdEntidad"]),
                        ValorAnterior = reader["ValorAnterior"].ToString(),
                        ValorNuevo = reader["ValorNuevo"].ToString(),
                        Origen = reader["Origen"].ToString(),
                        IpOrigen = reader["IpOrigen"].ToString(),
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        IdUsuario = reader["IdUsuario"] == DBNull.Value ? null : (Guid?)Guid.Parse(reader["IdUsuario"].ToString()),
                        NombreUsuario = reader["NombreUsuario"].ToString()
                    });
                }
            }
            return lista;
        }
    }
}