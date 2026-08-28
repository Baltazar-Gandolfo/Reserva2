using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class Pago
    {
        public int IdPago { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaAcreditacion { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MpPaymentId { get; set; }
        public string MpPreferenceId { get; set; }
        public int IdReserva { get; set; }
    }
}
