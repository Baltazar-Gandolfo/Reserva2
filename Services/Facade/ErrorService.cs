using Services.Dal.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Services.Facade
{
    public static class ErrorService
    {
        private static readonly ErrorRepository _repo = new ErrorRepository();

        public static void Registrar(Exception ex, string modulo = null)
        {
            Guid? idUsuario = SessionManager.GetInstance().UsuarioActual?.IdUsuario;

            try
            {
                _repo.Insertar(
                    ex.Message,
                    ex.GetType().FullName,
                    ex.StackTrace,
                    modulo,
                    idUsuario);
            }
            catch
            {
                GuardarEnArchivo(ex, modulo);
            }
        }

        public static List<Dictionary<string, string>> Listar(DateTime? desde = null, DateTime? hasta = null)
        {
            return _repo.Listar(desde, hasta);
        }

        private static void GuardarEnArchivo(Exception ex, string modulo)
        {
            try
            {
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "errors");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string archivo = Path.Combine(carpeta, $"error_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                var data = new
                {
                    Mensaje = ex.Message,
                    Tipo = ex.GetType().FullName,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message,
                    Modulo = modulo,
                    Fecha = DateTime.Now
                };

                File.WriteAllText(archivo, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }
    }
}