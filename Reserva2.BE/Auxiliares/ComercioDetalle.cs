using Reserva2.BE.Entities;
using System.Collections.Generic;

namespace Reserva2.BE.Auxiliares
{
    public class ComercioDetalle
    {
        public Comercio Comercio { get; set; }
        public string Owner { get; set; }
        public List<ElementoReservable> Elementos { get; set; }
        public List<ReglaNegocio> Reglas { get; set; }
        public ResumenComercio ResumenMes { get; set; }
    }
}
