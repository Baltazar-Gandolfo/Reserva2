using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    /// <summary>
    /// Representa una entrada de la bitácora de auditoría.
    /// </summary>
    public class LogEntry
    {
        /// <summary>Identificador de la entrada de bitácora.</summary>
        public int IdBitacora { get; set; }
        /// <summary>Tipo de acción realizada (ej: Login, Alta, Baja, Modificación).</summary>
        public string Accion { get; set; }
        /// <summary>Nombre de la entidad afectada.</summary>
        public string Entidad { get; set; }
        /// <summary>Descripción detallada de la operación.</summary>
        public string Detalle { get; set; }
        /// <summary>Identificador opcional de la entidad afectada.</summary>
        public int? IdEntidad { get; set; }
        /// <summary>Valor anterior a la modificación.</summary>
        public string ValorAnterior { get; set; }
        /// <summary>Valor posterior a la modificación.</summary>
        public string ValorNuevo { get; set; }
        /// <summary>Origen de la operación (nombre de la aplicación).</summary>
        public string Origen { get; set; }
        /// <summary>Dirección IP desde donde se originó la operación.</summary>
        public string IpOrigen { get; set; }
        /// <summary>Fecha y hora en que se registró la operación.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Identificador del usuario que realizó la operación.</summary>
        public Guid? IdUsuario { get; set; }
        /// <summary>Nombre del usuario que realizó la operación.</summary>
        public string NombreUsuario { get; set; }
    }
}