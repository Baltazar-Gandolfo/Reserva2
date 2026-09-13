using Services.Dal.Implementations;
using System;

namespace Services.Facade
{
    public static class BackupService
    {
        private static readonly BackupRepository _repo = new BackupRepository();

        public static void CrearBackup(string nombreBD, string rutaArchivo)
        {
            _repo.CrearBackup(nombreBD, rutaArchivo);
        }

        public static void Restaurar(string nombreBD, string rutaArchivo)
        {
            _repo.Restaurar(nombreBD, rutaArchivo);
        }
    }
}