using System;

namespace Reserva2.BE.Auxiliares
{
    public class BloqueHorario
    {
        public string DiaSemana { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fin { get; set; }
    }
}
