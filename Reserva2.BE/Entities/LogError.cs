using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class LogError
    {
        public int IdError { get; set; }
        public string Mensaje { get; set; }
        public string TipoExcepcion { get; set; }
        public string StackTrace { get; set; }
        public string Modulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
    }
}