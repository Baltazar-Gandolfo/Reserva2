using System;

namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Bloque horario genérico definido por día de la semana y rango de horas.
    /// </summary>
    public class BloqueHorario
    {
        /// <summary>Día de la semana del bloque.</summary>
        public string DiaSemana { get; set; }
        /// <summary>Hora de inicio del bloque.</summary>
        public TimeSpan Inicio { get; set; }
        /// <summary>Hora de fin del bloque.</summary>
        public TimeSpan Fin { get; set; }
    }
}
