using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Regla de disponibilidad que define los horarios semanales de un elemento reservable.
    /// </summary>
    public class ReglaDisponibilidad
    {
        /// <summary>Identificador único de la regla.</summary>
        public int IdReglaDisponibilidad { get; set; }
        /// <summary>Día de la semana al que aplica la regla.</summary>
        public string DiaSemana { get; set; }
        /// <summary>Hora de inicio del bloque horario.</summary>
        public TimeSpan HoraInicio { get; set; }
        /// <summary>Hora de fin del bloque horario.</summary>
        public TimeSpan HoraFin { get; set; }
        /// <summary>Duración de cada slot en minutos.</summary>
        public int DuracionSlot { get; set; }
        /// <summary>Precio especial que sobreescribe el precio base del elemento.</summary>
        public decimal? PrecioOverride { get; set; }
        /// <summary>Indica si la regla está activa.</summary>
        public bool Activo { get; set; }
        /// <summary>Identificador del elemento reservable asociado.</summary>
        public int IdElemento { get; set; }
    }
}