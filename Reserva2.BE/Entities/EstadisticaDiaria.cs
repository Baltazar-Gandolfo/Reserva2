using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class EstadisticaDiaria
    {
        public int IdEstadistica { get; set; }
        public DateTime Fecha { get; set; }
        public string DiaSemana { get; set; }
        public int TotalReservas { get; set; }
        public int Confirmadas { get; set; }
        public int Canceladas { get; set; }
        public int Expiradas { get; set; }
        public decimal IngresosTotal { get; set; }
        public decimal OcupacionPct { get; set; }
        public int IdElemento { get; set; }
    }
}
