using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public string Canal { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public DateTime? EnviadaEn { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdReserva { get; set; }
    }
}