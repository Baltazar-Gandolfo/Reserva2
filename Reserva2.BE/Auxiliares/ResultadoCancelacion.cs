namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Resultado de la operación de cancelación de una reserva.
    /// </summary>
    public class ResultadoCancelacion
    {
        /// <summary>Indica si la reserva tenía un pago asociado.</summary>
        public bool TeniaPago { get; set; }
        /// <summary>Indica si se realizó el reembolso del pago.</summary>
        public bool Reembolsado { get; set; }
    }
}
