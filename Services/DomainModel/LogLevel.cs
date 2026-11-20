using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    /// <summary>
    /// Niveles de severidad para los mensajes de log.
    /// </summary>
    internal enum LogLevel
    {
        /// <summary>Mensajes de depuración para desarrollo.</summary>
        Debug,
        /// <summary>Mensajes informativos de operación normal.</summary>
        Info,
        /// <summary>Advertencias que no impiden el funcionamiento.</summary>
        Warn,
        /// <summary>Errores recuperables del sistema.</summary>
        Error,
        /// <summary>Errores fatales que impiden continuar.</summary>
        Fatal
    }
}
