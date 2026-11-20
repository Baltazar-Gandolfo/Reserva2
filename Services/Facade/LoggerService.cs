using Services.DataAccess.Implementations;
using Services.DomainModel;
using Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para el servicio de logging a archivo.
    /// </summary>
    public static class LoggerService
    {
        private static readonly FileLogger fileLogger = new FileLogger();

        /// <summary>
        /// Registra un mensaje de depuración.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        public static void Debug(string message)
        {
            fileLogger.Debug(message);
        }

        /// <summary>
        /// Registra un mensaje de depuración con excepción.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        /// <param name="exception">Excepción asociada.</param>
        public static void Debug(string message, Exception exception)
        {
            fileLogger.Debug(message, exception);
        }

        /// <summary>
        /// Registra un mensaje de error.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        public static void Error(string message)
        {
            fileLogger.Error(message);
        }

        /// <summary>
        /// Registra un mensaje de error con excepción.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        /// <param name="exception">Excepción asociada.</param>
        public static void Error(string message, Exception exception)
        {
            fileLogger.Error(message, exception);
        }

        /// <summary>
        /// Registra un mensaje de error fatal.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        public static void Fatal(string message)
        {
            fileLogger.Fatal(message);
        }

        /// <summary>
        /// Registra un mensaje de error fatal con excepción.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        /// <param name="exception">Excepción asociada.</param>
        public static void Fatal(string message, Exception exception)
        {
            fileLogger.Fatal(message, exception);
        }

        /// <summary>
        /// Registra un mensaje informativo.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        public static void Info(string message)
        {
            fileLogger.Info(message);
        }

        /// <summary>
        /// Registra un mensaje informativo con excepción.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        /// <param name="exception">Excepción asociada.</param>
        public static void Info(string message, Exception exception)
        {
            fileLogger.Info(message, exception);
        }

        /// <summary>
        /// Registra un mensaje de advertencia.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        public static void Warn(string message)
        {
            fileLogger.Warn(message);
        }

        /// <summary>
        /// Registra un mensaje de advertencia con excepción.
        /// </summary>
        /// <param name="message">Texto del mensaje.</param>
        /// <param name="exception">Excepción asociada.</param>
        public static void Warn(string message, Exception exception)
        {
            fileLogger.Warn(message, exception);
        }
    }
}
