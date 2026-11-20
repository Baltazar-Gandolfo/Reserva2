using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una notificación enviada a un usuario del sistema.
    /// </summary>
    public class Notificacion
    {
        /// <summary>Identificador único de la notificación.</summary>
        public int IdNotificacion { get; set; }
        /// <summary>Canal de envío (Email, SMS, Push).</summary>
        public string Canal { get; set; }
        /// <summary>Estado de la notificación (Pendiente, Enviada, Fallida).</summary>
        public string Estado { get; set; }
        /// <summary>Tipo de notificación (Confirmación, Recordatorio, etc.).</summary>
        public string? Tipo { get; set; }
        /// <summary>Fecha y hora en que se envió la notificación.</summary>
        public DateTime? EnviadaEn { get; set; }
        /// <summary>Fecha de creación del registro de notificación.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del usuario destinatario.</summary>
        public Guid IdUsuario { get; set; }
        /// <summary>Identificador de la reserva relacionada.</summary>
        public int? IdReserva { get; set; }
    }
}