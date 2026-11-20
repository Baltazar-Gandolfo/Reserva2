using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa un elemento que puede ser reservado (cancha, sala, mesa, etc.).
    /// </summary>
    public class ElementoReservable
    {
        /// <summary>Identificador único del elemento.</summary>
        public int IdElemento { get; set; }
        /// <summary>Nombre del elemento reservable.</summary>
        public string Nombre { get; set; }
        /// <summary>Descripción opcional del elemento.</summary>
        public string? Descripcion { get; set; }
        /// <summary>Tipo del elemento (Cancha, Sala, Mesa, etc.).</summary>
        public string Tipo { get; set; }
        /// <summary>Precio base por slot.</summary>
        public decimal PrecioBase { get; set; }
        /// <summary>Capacidad máxima de personas.</summary>
        public int Capacidad { get; set; }
        /// <summary>Duración de cada slot en minutos.</summary>
        public int DuracionSlot { get; set; }
        /// <summary>Porcentaje del precio que se cobra como seña.</summary>
        public decimal PorcentajeSeña { get; set; }
        /// <summary>Estado del elemento (Activo, Inactivo).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha de creación del elemento.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador de la sucursal a la que pertenece.</summary>
        public int IdSucursal { get; set; }
        /// <summary>Referencia de navegación a la sucursal.</summary>
        public Sucursal? Sucursal { get; set; }
    }
}