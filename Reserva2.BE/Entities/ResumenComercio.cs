namespace Reserva2.BE.Entities
{
    // Resumen calculado (no persistido) de la actividad de un comercio en un período.
    public class ResumenComercio
    {
        public decimal Recaudado { get; set; }
        public int Reservas { get; set; }
        public int Canceladas { get; set; }
        public decimal OcupacionPct { get; set; }
    }
}
