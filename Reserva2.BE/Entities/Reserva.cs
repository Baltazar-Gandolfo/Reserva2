using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una reserva realizada por un usuario sobre un slot de disponibilidad.
    /// </summary>
    public class Reserva
    {
        /// <summary>Identificador único de la reserva.</summary>
        public int IdReserva { get; set; }
        /// <summary>Estado de la reserva (Pendiente, Confirmada, Cancelada, Expirada).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha y hora en que se creó la reserva.</summary>
        public DateTime FechaReserva { get; set; }
        /// <summary>Fecha y hora en que se confirmó la reserva.</summary>
        public DateTime? ConfirmadaEn { get; set; }
        /// <summary>Fecha y hora en que se canceló la reserva.</summary>
        public DateTime? CanceladaEn { get; set; }
        /// <summary>Fecha y hora en que expira la reserva si no se confirma.</summary>
        public DateTime? ExpiraEn { get; set; }
        /// <summary>Motivo de cancelación de la reserva.</summary>
        public string? MotivoCancelacion { get; set; }
        /// <summary>Nombre del cliente que realizó la reserva.</summary>
        public string? NombreCliente { get; set; }
        /// <summary>Identificador del usuario que realizó la reserva.</summary>
        public Guid IdUsuario { get; set; }
        /// <summary>Identificador del slot de disponibilidad reservado.</summary>
        public int IdDisponibilidad { get; set; }
        /// <summary>Referencia de navegación al slot de disponibilidad.</summary>
        public Disponibilidad? Disponibilidad { get; set; }
    }
}