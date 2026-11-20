using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa un pago asociado a una reserva.
    /// </summary>
    public class Pago
    {
        /// <summary>Identificador único del pago.</summary>
        public int IdPago { get; set; }
        /// <summary>Estado del pago (Pendiente, Acreditado, Rechazado).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha en que se realizó el pago.</summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>Monto total del pago.</summary>
        public decimal Monto { get; set; }
        /// <summary>Método de pago utilizado (Efectivo, Transferencia, etc.).</summary>
        public string MetodoPago { get; set; }
        /// <summary>Identificador de la reserva asociada.</summary>
        public int IdReserva { get; set; }
        /// <summary>Referencia de navegación a la reserva.</summary>
        public Reserva? Reserva { get; set; }
    }
}
