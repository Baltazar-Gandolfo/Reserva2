using Services.DataAccess.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de acceso a datos para dígitos verificadores horizontales (DVH) y verticales (DVV).
    /// </summary>
    internal class DigitoVerificadorRepository
    {
        /// <summary>Obtiene la cadena de conexión del servicio.</summary>
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ServicioDB"].ConnectionString;
        }

        /// <summary>Obtiene todas las filas de Usuario como diccionarios clave-valor incluyendo DVH.</summary>
        /// <returns>Lista de diccionarios con los campos de cada fila.</returns>
        public List<Dictionary<string, string>> ObtenerUsuariosRaw()
        {
            var lista = new List<Dictionary<string, string>>();
            string sql = "SELECT IdUsuario, DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado, FechaRegistro, DVH FROM Usuario";
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fila = new Dictionary<string, string>();
                        fila["IdUsuario"] = reader["IdUsuario"].ToString();
                        fila["DNI"] = reader["DNI"]?.ToString() ?? "";
                        fila["Nombre"] = reader["Nombre"]?.ToString() ?? "";
                        fila["Email"] = reader["Email"]?.ToString() ?? "";
                        fila["PasswordHash"] = reader["PasswordHash"]?.ToString() ?? "";
                        fila["Rol"] = reader["Rol"]?.ToString() ?? "";
                        fila["Telefono"] = reader["Telefono"]?.ToString() ?? "";
                        fila["Estado"] = reader["Estado"]?.ToString() ?? "";
                        fila["FechaRegistro"] = Convert.ToDateTime(reader["FechaRegistro"]).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        object dvh = reader["DVH"];
                        fila["DVH"] = (dvh == null || dvh == DBNull.Value) ? "" : dvh.ToString();
                        lista.Add(fila);
                    }
                }
            }
            return lista;
        }

        /// <summary>Actualiza el DVH de un usuario específico.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="dvh">Nuevo valor del DVH.</param>
        public void ActualizarDVH(Guid idUsuario, string dvh)
        {
            string sql = "UPDATE Usuario SET DVH = @DVH WHERE IdUsuario = @Id";
            SqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@DVH", dvh),
                new SqlParameter("@Id", idUsuario));
        }

        /// <summary>Obtiene el DVV almacenado para una entidad.</summary>
        /// <param name="nombreEntidad">Nombre de la entidad (ej: "Usuario").</param>
        /// <returns>Valor del DVV o null si no existe.</returns>
        public string ObtenerDVV(string nombreEntidad)
        {
            string sql = "SELECT ValorDVV FROM DigitoVerificadorVertical WHERE NombreEntidad = @Entidad";
            object result = SqlHelper.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@Entidad", nombreEntidad));
            if (result == null || result == DBNull.Value)
                return null;
            return result.ToString();
        }

        /// <summary>Inserta o actualiza el DVV de una entidad.</summary>
        /// <param name="nombreEntidad">Nombre de la entidad.</param>
        /// <param name="valorDVV">Nuevo valor del DVV.</param>
        public void ActualizarDVV(string nombreEntidad, string valorDVV)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sqlCheck = "SELECT COUNT(*) FROM DigitoVerificadorVertical WHERE NombreEntidad = @Entidad";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@Entidad", nombreEntidad);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        string sqlUpdate = "UPDATE DigitoVerificadorVertical SET ValorDVV = @DVV WHERE NombreEntidad = @Entidad";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@DVV", valorDVV);
                            cmdUpdate.Parameters.AddWithValue("@Entidad", nombreEntidad);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sqlInsert = "INSERT INTO DigitoVerificadorVertical (NombreEntidad, ValorDVV) VALUES (@Entidad, @DVV)";
                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@Entidad", nombreEntidad);
                            cmdInsert.Parameters.AddWithValue("@DVV", valorDVV);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>Verifica si existe un usuario con el DNI encriptado indicado.</summary>
        /// <param name="dniEncriptado">DNI encriptado a buscar.</param>
        /// <returns><c>true</c> si el DNI ya existe en la base de datos.</returns>
        public bool ExisteDNI(string dniEncriptado)
        {
            string sql = "SELECT COUNT(*) FROM Usuario WHERE DNI = @DNI";
            object result = SqlHelper.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@DNI", dniEncriptado));
            return Convert.ToInt32(result) > 0;
        }
    }
}
