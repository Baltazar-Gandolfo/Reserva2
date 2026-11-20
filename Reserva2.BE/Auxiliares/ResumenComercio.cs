namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Resumen mensual de métricas de un comercio.
    /// </summary>
    public class ResumenComercio
    {
        /// <summary>Monto total recaudado en el período.</summary>
        public decimal Recaudado { get; set; }
        /// <summary>Cantidad total de reservas.</summary>
        public int Reservas { get; set; }
        /// <summary>Cantidad de reservas canceladas.</summary>
        public int Canceladas { get; set; }
        /// <summary>Porcentaje de ocupación.</summary>
        public decimal OcupacionPct { get; set; }
    }
}
