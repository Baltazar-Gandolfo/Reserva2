using Services.DomainModel;
using System;
using System.Collections.Generic;

namespace Services.Facade
{
    public static class BitacoraService
    {
        public static void Registrar(string accion, string entidad, string detalle,
            int? idEntidad = null, string valorAnterior = null, string valorNuevo = null)
        {
            Logic.BitacoraLogic.Registrar(accion, entidad, detalle, idEntidad, valorAnterior, valorNuevo);
        }

        public static List<LogEntry> Listar(DateTime? desde = null, DateTime? hasta = null,
            string accion = null, string entidad = null)
        {
            return Logic.BitacoraLogic.Listar(desde, hasta, accion, entidad);
        }
    }
}