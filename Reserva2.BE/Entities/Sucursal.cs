using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una sucursal física de un comercio.
    /// </summary>
    public class Sucursal
    {
        /// <summary>Identificador único de la sucursal.</summary>
        public int IdSucursal { get; set; }
        /// <summary>Nombre de la sucursal.</summary>
        public string Nombre { get; set; }
        /// <summary>Dirección o ubicación de la sucursal.</summary>
        public string? Ubicacion { get; set; }
        /// <summary>Indica si la sucursal está activa.</summary>
        public bool Activo { get; set; }
        /// <summary>Fecha de creación de la sucursal.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del comercio propietario.</summary>
        public int IdComercio { get; set; }
        /// <summary>Referencia de navegación al comercio.</summary>
        public Comercio? Comercio { get; set; }
    }
}