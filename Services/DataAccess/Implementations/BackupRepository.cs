using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de operaciones de backup y restauración de la base de datos.
    /// </summary>
    internal class BackupRepository
    {
        /// <summary>Obtiene la cadena de conexión a la base master.</summary>
        private string GetMasterConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
        }

        /// <summary>Obtiene la carpeta de backup por defecto del servidor SQL.</summary>
        /// <returns>Ruta de la carpeta de backup o null.</returns>
        public string ObtenerCarpetaPorDefecto()
        {
            using (SqlConnection conn = new SqlConnection(GetMasterConnectionString()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS nvarchar(512))", conn))
            {
                conn.Open();
                object r = cmd.ExecuteScalar();
                return r == null || r == DBNull.Value ? null : r.ToString();
            }
        }

        /// <summary>Crea un backup completo de la base de datos en la ruta indicada.</summary>
        /// <param name="nombreBD">Nombre de la base de datos.</param>
        /// <param name="rutaArchivo">Ruta del archivo de backup.</param>
        public void CrearBackup(string nombreBD, string rutaArchivo)
        {
            string sql = $"BACKUP DATABASE [{nombreBD}] TO DISK = @Ruta WITH FORMAT, INIT";
            try
            {
                using (SqlConnection conn = new SqlConnection(GetMasterConnectionString()))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ruta", rutaArchivo);
                    cmd.CommandTimeout = 120;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 3201)
            {
                throw new Exception(
                    "El servidor SQL no tiene permiso para escribir en esa carpeta. " +
                    "Usá la carpeta por defecto del servidor o una carpeta con permisos para el servicio de SQL Server.", ex);
            }
        }

        /// <summary>Restaura la base de datos desde un archivo de backup.</summary>
        /// <param name="nombreBD">Nombre de la base de datos a restaurar.</param>
        /// <param name="rutaArchivo">Ruta del archivo de backup.</param>
        public void Restaurar(string nombreBD, string rutaArchivo)
        {
            string connStr = GetMasterConnectionString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    SqlCommand cmdKick = new SqlCommand(
                        $"ALTER DATABASE [{nombreBD}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", conn);
                    cmdKick.ExecuteNonQuery();

                    SqlCommand cmdRestore = new SqlCommand(
                        $"RESTORE DATABASE [{nombreBD}] FROM DISK = @Ruta WITH REPLACE;", conn);
                    cmdRestore.Parameters.AddWithValue("@Ruta", rutaArchivo);
                    cmdRestore.CommandTimeout = 120;
                    cmdRestore.ExecuteNonQuery();
                }
                finally
                {
                    SqlCommand cmdMulti = new SqlCommand(
                        $"ALTER DATABASE [{nombreBD}] SET MULTI_USER;", conn);
                    cmdMulti.ExecuteNonQuery();
                }
            }
        }
    }
}