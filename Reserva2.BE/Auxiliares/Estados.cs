namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Constantes de estado para los slots de disponibilidad.
    /// </summary>
    public static class EstadoSlot
    {
        /// <summary>El slot está libre para ser reservado.</summary>
        public const string Libre = "Libre";
        /// <summary>El slot ya fue reservado.</summary>
        public const string Reservada = "Reservada";
        /// <summary>El slot está bloqueado manualmente.</summary>
        public const string Bloqueada = "Bloqueada";
    }

    /// <summary>
    /// Constantes de estado para las reservas.
    /// </summary>
    public static class EstadoReserva
    {
        /// <summary>La reserva está pendiente de confirmación.</summary>
        public const string Pendiente = "Pendiente";
        /// <summary>La reserva fue confirmada.</summary>
        public const string Confirmada = "Confirmada";
        /// <summary>La reserva fue cancelada.</summary>
        public const string Cancelada = "Cancelada";
        /// <summary>La reserva expiró sin ser confirmada.</summary>
        public const string Expirada = "Expirada";
    }

    /// <summary>
    /// Constantes de estado para los pagos.
    /// </summary>
    public static class EstadoPago
    {
        /// <summary>El pago fue aprobado.</summary>
        public const string Aprobado = "Aprobado";
        /// <summary>El pago fue reembolsado al cliente.</summary>
        public const string Reembolsado = "Reembolsado";
        /// <summary>El pago está retenido pendiente de acreditación.</summary>
        public const string Retenido = "Retenido";
    }

    /// <summary>
    /// Métodos de pago disponibles en el sistema.
    /// </summary>
    public static class MetodoPago
    {
        /// <summary>Lista de todos los métodos de pago soportados.</summary>
        public static readonly string[] Todos = { "Efectivo", "Transferencia", "Tarjeta" };
    }
}
