using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa un comercio registrado en el sistema de reservas.
    /// </summary>
    public class Comercio
    {
        /// <summary>Identificador único del comercio.</summary>
        public int IdComercio { get; set; }
        /// <summary>Nombre del comercio.</summary>
        public string Nombre { get; set; } = "";
        /// <summary>Slug URL-friendly del comercio.</summary>
        public string? Slug { get; set; }
        /// <summary>Dirección o ubicación física del comercio.</summary>
        public string? Ubicacion { get; set; }
        /// <summary>URL del logotipo del comercio.</summary>
        public string? LogoUrl { get; set; }
        /// <summary>Indica si el comercio está activo.</summary>
        public bool Activo { get; set; }
        /// <summary>Fecha de creación del comercio en el sistema.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del usuario propietario del comercio.</summary>
        public Guid IdUsuario { get; set; }
    }
}