using Services.Dal.Implementations;
using Services.DomainModel;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de negocio para el registro de auditoría (bitácora) del sistema.
    /// </summary>
    internal static class BitacoraLogic
    {
        private static readonly BitacoraRepository _repo = new BitacoraRepository();

        /// <summary>
        /// Registra una entrada en la bitácora de auditoría.
        /// </summary>
        /// <param name="accion">Tipo de acción (Login, Alta, Baja, Modificación).</param>
        /// <param name="entidad">Nombre de la entidad afectada.</param>
        /// <param name="detalle">Descripción detallada de la operación.</param>
        /// <param name="idEntidad">Identificador de la entidad afectada.</param>
        /// <param name="valorAnterior">Valor anterior a la modificación.</param>
        /// <param name="valorNuevo">Valor posterior a la modificación.</param>
        public static void Registrar(string accion, string entidad, string detalle,
            int? idEntidad = null, string valorAnterior = null, string valorNuevo = null)
        {
            var sesion = SessionManager.GetInstance();
            var entry = new LogEntry
            {
                Accion = accion,
                Entidad = entidad,
                Detalle = detalle,
                IdEntidad = idEntidad,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                Origen = "Reserva2",
                IpOrigen = Environment.MachineName,
                FechaCreacion = DateTime.Now,
                IdUsuario = sesion.UsuarioActual?.IdUsuario
            };

            _repo.Insertar(entry);
        }

        /// <summary>
        /// Lista entradas de la bitácora filtradas por rango de fechas, acción y entidad.
        /// </summary>
        /// <param name="desde">Fecha desde.</param>
        /// <param name="hasta">Fecha hasta.</param>
        /// <param name="accion">Filtro por tipo de acción.</param>
        /// <param name="entidad">Filtro por entidad.</param>
        /// <returns>Lista de entradas de bitácora.</returns>
        public static List<LogEntry> Listar(DateTime? desde, DateTime? hasta, string accion, string entidad)
        {
            return _repo.Listar(desde, hasta, accion, entidad);
        }
    }
}