using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class Sena
    {
        public int IdSena { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaSena { get; set; }
        public decimal MontoSena { get; set; }
        public int IdPago { get; set; }
    }
}
