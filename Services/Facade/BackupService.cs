using Services.Dal.Implementations;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para el servicio de backup y restauración de base de datos.
    /// </summary>
    public static class BackupService
    {
        private static readonly BackupRepository _repo = new BackupRepository();

        /// <summary>
        /// Obtiene la carpeta por defecto de SQL Server para almacenar backups.
        /// </summary>
        /// <returns>Ruta absoluta de la carpeta de backup.</returns>
        public static string ObtenerCarpetaPorDefecto()
        {
            return _repo.ObtenerCarpetaPorDefecto();
        }

        /// <summary>
        /// Crea un backup completo de la base de datos.
        /// </summary>
        /// <param name="nombreBD">Nombre de la base de datos.</param>
        /// <param name="rutaArchivo">Ruta completa del archivo .bak destino.</param>
        public static void CrearBackup(string nombreBD, string rutaArchivo)
        {
            _repo.CrearBackup(nombreBD, rutaArchivo);
        }

        /// <summary>
        /// Restaura una base de datos desde un archivo de backup.
        /// </summary>
        /// <param name="nombreBD">Nombre de la base de datos a restaurar.</param>
        /// <param name="rutaArchivo">Ruta completa del archivo .bak origen.</param>
        public static void Restaurar(string nombreBD, string rutaArchivo)
        {
            _repo.Restaurar(nombreBD, rutaArchivo);
        }
    }
}