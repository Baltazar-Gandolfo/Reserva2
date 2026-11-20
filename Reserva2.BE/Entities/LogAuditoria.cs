using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Registro de auditoría (bitácora) del sistema.
    /// </summary>
    public class LogAuditoria
    {
        /// <summary>Identificador único del registro.</summary>
        public int IdLog { get; set; }
        /// <summary>Acción realizada (Login, Alta, Baja, etc.).</summary>
        public string Accion { get; set; }
        /// <summary>Entidad afectada (Usuario, Comercio, Reserva, etc.).</summary>
        public string Entidad { get; set; }
        /// <summary>Descripción detallada de la acción.</summary>
        public string Detalle { get; set; }
        /// <summary>Identificador de la entidad afectada.</summary>
        public int? IdEntidad { get; set; }
        /// <summary>Valor anterior del campo modificado.</summary>
        public string ValorAnterior { get; set; }
        /// <summary>Valor nuevo del campo modificado.</summary>
        public string ValorNuevo { get; set; }
        /// <summary>Origen de la acción.</summary>
        public string Origen { get; set; }
        /// <summary>Dirección IP desde donde se realizó la acción.</summary>
        public string IpOrigen { get; set; }
        /// <summary>Fecha y hora en que se registró la acción.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del usuario que realizó la acción.</summary>
        public int? IdUsuario { get; set; }
    }
}