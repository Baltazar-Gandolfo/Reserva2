using Services.DomainModel;
using System;
using System.Collections.Generic;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para el servicio de bitácora de auditoría.
    /// </summary>
    public static class BitacoraService
    {
        /// <summary>
        /// Registra una acción en la bitácora de auditoría.
        /// </summary>
        /// <param name="accion">Tipo de acción realizada (ej: Login, Alta, Baja).</param>
        /// <param name="entidad">Nombre de la entidad afectada.</param>
        /// <param name="detalle">Descripción detallada de la operación.</param>
        /// <param name="idEntidad">Identificador opcional de la entidad afectada.</param>
        /// <param name="valorAnterior">Valor previo a la modificación.</param>
        /// <param name="valorNuevo">Valor posterior a la modificación.</param>
        public static void Registrar(string accion, string entidad, string detalle,
            int? idEntidad = null, string valorAnterior = null, string valorNuevo = null)
        {
            Logic.BitacoraLogic.Registrar(accion, entidad, detalle, idEntidad, valorAnterior, valorNuevo);
        }

        /// <summary>
        /// Lista los registros de auditoría aplicando filtros opcionales.
        /// </summary>
        /// <param name="desde">Fecha de inicio del rango.</param>
        /// <param name="hasta">Fecha de fin del rango.</param>
        /// <param name="accion">Filtro por tipo de acción.</param>
        /// <param name="entidad">Filtro por nombre de entidad.</param>
        /// <returns>Lista de entradas de bitácora que coinciden con los filtros.</returns>
        public static List<LogEntry> Listar(DateTime? desde = null, DateTime? hasta = null,
            string accion = null, string entidad = null)
        {
            return Logic.BitacoraLogic.Listar(desde, hasta, accion, entidad);
        }
    }
}