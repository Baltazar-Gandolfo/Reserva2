using Services.Dal.Implementations;
using Services.DomainModel;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Services.Logic
{
    internal static class BitacoraLogic
    {
        private static readonly BitacoraRepository _repo = new BitacoraRepository();

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

        public static List<LogEntry> Listar(DateTime? desde, DateTime? hasta, string accion, string entidad)
        {
            return _repo.Listar(desde, hasta, accion, entidad);
        }
    }
}