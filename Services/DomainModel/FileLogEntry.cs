using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    /// <summary>
    /// Representa una entrada de log destinada a escritura en archivo.
    /// </summary>
    internal class FileLogEntry
    {
        /// <summary>Marca temporal del evento.</summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>Nivel de severidad del mensaje.</summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>Texto descriptivo del evento.</summary>
        public string Message  { get; set; }

        /// <summary>Excepción asociada al evento, si la hay.</summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Genera la representación formateada de la entrada para escritura en archivo.
        /// </summary>
        /// <returns>Cadena con formato [fecha] [nivel] mensaje [detalles].</returns>
        public override string ToString()
        {
            //Generamos formato del mensaje de log, por ejemplo: [2024-06-01 14:30:00] [Error] Ocurrió un error al procesar la solicitud. Detalles: System.NullReferenceException: Object reference not set to an instance of an object.
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{TimeStamp:yyyy-MM-dd HH:mm:ss}] ");
            sb.Append($"[{LogLevel}] ");
            sb.Append($"{Message}");

            if (Exception != null)
            {
                sb.Append($" Detalles: {Exception.Message}");
                sb.Append($" StackTrace: {Exception.StackTrace}");
            }

            return sb.ToString();
        }
    }
}
