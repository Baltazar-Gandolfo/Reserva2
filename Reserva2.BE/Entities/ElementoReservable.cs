using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class ElementoReservable
    {
        public int IdElemento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public decimal PrecioBase { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdComercio { get; set; }
    }
}