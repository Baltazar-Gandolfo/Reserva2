using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    internal class BackupRepository
    {
        private string GetMasterConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
        }

        public void CrearBackup(string nombreBD, string rutaArchivo)
        {
            string sql = $"BACKUP DATABASE [{nombreBD}] TO DISK = @Ruta WITH FORMAT, INIT";
            using (SqlConnection conn = new SqlConnection(GetMasterConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Ruta", rutaArchivo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

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