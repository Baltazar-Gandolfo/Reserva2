using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    public class LogEntry
    {
        public int IdBitacora { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
        public int? IdEntidad { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string Origen { get; set; }
        public string IpOrigen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }
}