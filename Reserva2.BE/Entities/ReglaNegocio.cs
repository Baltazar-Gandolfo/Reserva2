using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Regla de negocio configurable por comercio (clave-valor).
    /// </summary>
    public class ReglaNegocio
    {
        /// <summary>Identificador único de la regla.</summary>
        public int IdReglaNegocio { get; set; }
        /// <summary>Clave identificadora de la regla.</summary>
        public string Clave { get; set; }
        /// <summary>Descripción de la regla.</summary>
        public string? Descripcion { get; set; }
        /// <summary>Valor de la regla.</summary>
        public string Valor { get; set; }
        /// <summary>Identificador del comercio al que pertenece la regla.</summary>
        public int IdComercio { get; set; }
    }
}