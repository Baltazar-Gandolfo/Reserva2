using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class Comercio
    {
        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Ubicacion { get; set; }
        public string LogoUrl { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
    }
}