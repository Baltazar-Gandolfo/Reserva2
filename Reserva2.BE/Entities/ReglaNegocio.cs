using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class ReglaNegocio
    {
        public int IdReglaNegocio { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public int IdComercio { get; set; }
    }
}