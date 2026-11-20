using System;
using System.Collections.Generic;

namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Resultado de indicadores clave de rendimiento (KPI) del dashboard.
    /// </summary>
    public class KpiResult
    {
        /// <summary>Ingresos cobrados en el período actual.</summary>
        public decimal IngresosCobrados { get; set; }
        /// <summary>Ingresos cobrados en el período anterior (para comparación).</summary>
        public decimal IngresosAnterior { get; set; }
        /// <summary>Porcentaje de ocupación actual.</summary>
        public decimal OcupacionPct { get; set; }
        /// <summary>Porcentaje de ocupación del período anterior.</summary>
        public decimal OcupacionAnteriorPct { get; set; }
        /// <summary>Porcentaje de cancelación actual.</summary>
        public decimal CancelacionPct { get; set; }
        /// <summary>Porcentaje de cancelación del período anterior.</summary>
        public decimal CancelacionAnteriorPct { get; set; }
        /// <summary>Porcentaje de clientes recurrentes actual.</summary>
        public decimal RecurrentesPct { get; set; }
        /// <summary>Porcentaje de clientes recurrentes del período anterior.</summary>
        public decimal RecurrentesAnteriorPct { get; set; }
        /// <summary>Fecha de inicio del período anterior.</summary>
        public DateTime PeriodoAnteriorDesde { get; set; }
        /// <summary>Fecha de fin del período anterior.</summary>
        public DateTime PeriodoAnteriorHasta { get; set; }
        /// <summary>Detalle de KPI desglosado por elemento reservable.</summary>
        public List<DetalleElemento> Detalle { get; set; } = new();
    }

    /// <summary>
    /// Detalle de métricas por elemento reservable dentro del KPI.
    /// </summary>
    public class DetalleElemento
    {
        /// <summary>Nombre del elemento reservable.</summary>
        public string Elemento { get; set; } = "";
        /// <summary>Nombre de la sucursal.</summary>
        public string Sucursal { get; set; } = "";
        /// <summary>Total de reservas del elemento.</summary>
        public int TotalReservas { get; set; }
        /// <summary>Porcentaje de ocupación del elemento.</summary>
        public decimal OcupacionPct { get; set; }
        /// <summary>Ingresos generados por el elemento.</summary>
        public decimal Ingresos { get; set; }
        /// <summary>Porcentaje de cancelación del elemento.</summary>
        public decimal CancelacionPct { get; set; }
    }
}
