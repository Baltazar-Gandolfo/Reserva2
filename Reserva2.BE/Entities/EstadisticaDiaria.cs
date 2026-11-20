using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una estadística diaria de un elemento reservable.
    /// </summary>
    public class EstadisticaDiaria
    {
        /// <summary>Identificador único de la estadística.</summary>
        public int IdEstadistica { get; set; }
        /// <summary>Fecha del día estadístico.</summary>
        public DateTime Fecha { get; set; }
        /// <summary>Nombre del día de la semana.</summary>
        public string DiaSemana { get; set; }
        /// <summary>Total de reservas del día.</summary>
        public int TotalReservas { get; set; }
        /// <summary>Cantidad de reservas confirmadas.</summary>
        public int Confirmadas { get; set; }
        /// <summary>Cantidad de reservas canceladas.</summary>
        public int Canceladas { get; set; }
        /// <summary>Cantidad de reservas expiradas.</summary>
        public int Expiradas { get; set; }
        /// <summary>Ingreso total recaudado en el día.</summary>
        public decimal IngresosTotal { get; set; }
        /// <summary>Porcentaje de ocupación del día.</summary>
        public decimal OcupacionPct { get; set; }
        /// <summary>Identificador del elemento reservable asociado.</summary>
        public int IdElemento { get; set; }
    }
}
