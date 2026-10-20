namespace Reserva2.BE.Entities
{
    public static class EstadoSlot
    {
        public const string Libre = "Libre";
        public const string Reservada = "Reservada";
        public const string Bloqueada = "Bloqueada";
    }

    public static class EstadoReserva
    {
        public const string Pendiente = "Pendiente";
        public const string Confirmada = "Confirmada";
        public const string Cancelada = "Cancelada";
        public const string Expirada = "Expirada";
    }

    public static class EstadoPago
    {
        public const string Aprobado = "Aprobado";
        public const string Reembolsado = "Reembolsado";
        public const string Retenido = "Retenido";
    }

    public static class MetodoPago
    {
        public static readonly string[] Todos = { "Efectivo", "Transferencia", "Tarjeta" };
    }
}
