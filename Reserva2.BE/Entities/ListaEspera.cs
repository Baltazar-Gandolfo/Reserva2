using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Entrada en la lista de espera de un slot de disponibilidad.
    /// </summary>
    public class ListaEspera
    {
        /// <summary>Identificador único de la entrada en la lista de espera.</summary>
        public int IdListaEspera { get; set; }
        /// <summary>Estado de la entrada (Esperando, Notificado, Expirado).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha de creación de la entrada.</summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>Fecha en que se notificó al usuario sobre la disponibilidad.</summary>
        public DateTime? NotificadoEn { get; set; }
        /// <summary>Posición del usuario en la lista de espera.</summary>
        public int Posicion { get; set; }
        /// <summary>Identificador del usuario en espera.</summary>
        public Guid IdUsuario { get; set; }
        /// <summary>Identificador del slot de disponibilidad solicitado.</summary>
        public int IdDisponibilidad { get; set; }
    }
}