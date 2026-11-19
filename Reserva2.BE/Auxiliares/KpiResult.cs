using System;
using System.Collections.Generic;

namespace Reserva2.BE.Auxiliares
{
    public class KpiResult
    {
        public decimal IngresosCobrados { get; set; }
        public decimal IngresosAnterior { get; set; }
        public decimal OcupacionPct { get; set; }
        public decimal OcupacionAnteriorPct { get; set; }
        public decimal CancelacionPct { get; set; }
        public decimal CancelacionAnteriorPct { get; set; }
        public decimal RecurrentesPct { get; set; }
        public decimal RecurrentesAnteriorPct { get; set; }
        public DateTime PeriodoAnteriorDesde { get; set; }
        public DateTime PeriodoAnteriorHasta { get; set; }
        public List<DetalleElemento> Detalle { get; set; } = new();
    }

    public class DetalleElemento
    {
        public string Elemento { get; set; } = "";
        public string Sucursal { get; set; } = "";
        public int TotalReservas { get; set; }
        public decimal OcupacionPct { get; set; }
        public decimal Ingresos { get; set; }
        public decimal CancelacionPct { get; set; }
    }
}
