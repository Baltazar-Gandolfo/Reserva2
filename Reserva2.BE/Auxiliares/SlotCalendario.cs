using Reserva2.BE.Entities;

namespace Reserva2.BE.Auxiliares
{
    public class SlotCalendario
    {
        public Disponibilidad Slot { get; set; }
        public Reserva Reserva { get; set; }
        public string EstadoVisual { get; set; }
    }
}
