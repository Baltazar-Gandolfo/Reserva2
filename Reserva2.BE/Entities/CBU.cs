using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa una Clave Bancaria Uniforme asociada a un comercio para recibir pagos.
    /// </summary>
    public class CBU
    {
        /// <summary>Identificador único del registro CBU.</summary>
        public int IdCBU { get; set; }
        /// <summary>Alias bancario asociado al CBU.</summary>
        public string? Alias { get; set; }
        /// <summary>Número de CBU (22 dígitos).</summary>
        public string Cbu { get; set; }
        /// <summary>Indica si el CBU está activo para recibir pagos.</summary>
        public bool Activo { get; set; }
        /// <summary>Identificador del comercio propietario del CBU.</summary>
        public int IdComercio { get; set; }
    }
}