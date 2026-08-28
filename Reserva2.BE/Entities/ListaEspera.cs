using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class ListaEspera
    {
        public int IdListaEspera { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? NotificadoEn { get; set; }
        public int Posicion { get; set; }
        public int IdUsuario { get; set; }
        public int IdDisponibilidad { get; set; }
    }
}