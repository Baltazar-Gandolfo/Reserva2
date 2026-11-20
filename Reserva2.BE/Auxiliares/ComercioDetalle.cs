using Reserva2.BE.Entities;
using System.Collections.Generic;

namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Vista detallada de un comercio con toda su información asociada.
    /// </summary>
    public class ComercioDetalle
    {
        /// <summary>Datos del comercio.</summary>
        public Comercio Comercio { get; set; }
        /// <summary>Nombre del usuario propietario del comercio.</summary>
        public string Owner { get; set; }
        /// <summary>Lista de elementos reservables del comercio.</summary>
        public List<ElementoReservable> Elementos { get; set; }
        /// <summary>Reglas de negocio configuradas.</summary>
        public List<ReglaNegocio> Reglas { get; set; }
        /// <summary>Resumen de métricas del mes actual.</summary>
        public ResumenComercio ResumenMes { get; set; }
    }
}
