using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Registro de error del sistema almacenado en la tabla LogError.
    /// </summary>
    public class LogError
    {
        /// <summary>Identificador único del error.</summary>
        public int IdError { get; set; }
        /// <summary>Mensaje descriptivo del error.</summary>
        public string Mensaje { get; set; }
        /// <summary>Tipo de excepción (nombre completo de la clase).</summary>
        public string TipoExcepcion { get; set; }
        /// <summary>Traza de la pila de ejecución al momento del error.</summary>
        public string StackTrace { get; set; }
        /// <summary>Módulo o pantalla donde se originó el error.</summary>
        public string Modulo { get; set; }
        /// <summary>Fecha y hora en que se registró el error.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del usuario activo al momento del error.</summary>
        public int? IdUsuario { get; set; }
    }
}