using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class ReglaDisponibilidad
    {
        public int IdReglaDisponibilidad { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int DuracionSlot { get; set; }
        public decimal? PrecioOverride { get; set; }
        public bool Activo { get; set; }
        public int IdElemento { get; set; }
    }
}