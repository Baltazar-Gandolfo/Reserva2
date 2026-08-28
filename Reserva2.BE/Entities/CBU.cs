using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class CBU
    {
        public int IdCBU { get; set; }
        public string Alias { get; set; }
        public string Cbu { get; set; }
        public bool Activo { get; set; }
        public int IdComercio { get; set; }
    }
}