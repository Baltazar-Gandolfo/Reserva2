using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa un slot de disponibilidad horaria de un elemento reservable.
    /// </summary>
    public class Disponibilidad
    {
        /// <summary>Identificador único de la disponibilidad.</summary>
        public int IdDisponibilidad { get; set; }
        /// <summary>Fecha del slot de disponibilidad.</summary>
        public DateTime Fecha { get; set; }
        /// <summary>Hora de inicio del slot.</summary>
        public TimeSpan HoraInicio { get; set; }
        /// <summary>Hora de fin del slot.</summary>
        public TimeSpan HoraFin { get; set; }
        /// <summary>Precio del slot.</summary>
        public decimal Precio { get; set; }
        /// <summary>Estado del slot (Disponible, Reservado, Bloqueado).</summary>
        public string Estado { get; set; }
        /// <summary>Identificador del elemento reservable asociado.</summary>
        public int IdElemento { get; set; }
        /// <summary>Referencia de navegación al elemento reservable.</summary>
        public ElementoReservable? Elemento { get; set; }
    }
}