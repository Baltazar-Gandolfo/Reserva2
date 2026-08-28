using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public string Estado { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? ConfirmadaEn { get; set; }
        public DateTime? CanceladaEn { get; set; }
        public DateTime? ExpiraEn { get; set; }
        public string MotivoCancelacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdDisponibilidad { get; set; }
    }
}