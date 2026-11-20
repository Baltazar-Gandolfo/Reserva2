using Reserva2.BE.Entities;

namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Representa un slot del calendario visual con su reserva asociada.
    /// </summary>
    public class SlotCalendario
    {
        /// <summary>Slot de disponibilidad.</summary>
        public Disponibilidad Slot { get; set; }
        /// <summary>Reserva asociada al slot (null si está libre).</summary>
        public Reserva Reserva { get; set; }
        /// <summary>Estado visual para la UI (Libre, Reservada, Expirada, etc.).</summary>
        public string EstadoVisual { get; set; }
    }
}
