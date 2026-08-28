using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    public class LogAuditoria
    {
        public int IdLog { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
        public int? IdEntidad { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string Origen { get; set; }
        public string IpOrigen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
    }
}