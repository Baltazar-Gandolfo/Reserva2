using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una seña (pago parcial anticipado) asociada a un pago.
    /// </summary>
    public class Sena
    {
        /// <summary>Identificador único de la seña.</summary>
        public int IdSena { get; set; }
        /// <summary>Estado actual de la seña (Pendiente, Acreditada, etc.).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha en que se realizó la seña.</summary>
        public DateTime? FechaSena { get; set; }
        /// <summary>Monto de la seña.</summary>
        public decimal MontoSena { get; set; }
        /// <summary>Identificador del pago asociado.</summary>
        public int IdPago { get; set; }
    }
}
