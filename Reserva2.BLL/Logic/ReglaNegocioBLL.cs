using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System.Collections.Generic;
using System.Globalization;

namespace Reserva2.BLL.Logic
{
    public class ReglaNegocioBLL
    {
        public const string MinutosExpiracion = "minutos_expiracion";
        public const string MaxReservasSemana = "max_reservas_semana";
        public const string CancelacionGratisHs = "cancelacion_gratis_hs";

        // Valores por defecto: se siembran al crear el comercio y sirven de respaldo si falta la regla.
        private static readonly Dictionary<string, (string Valor, string Descripcion)> _defaults = new()
        {
            [MinutosExpiracion] = ("10", "Minutos que se bloquea un slot hasta que se paga la reserva"),
            [MaxReservasSemana] = ("3", "Máximo de reservas pendientes/confirmadas por cliente por semana"),
            [CancelacionGratisHs] = ("24", "Horas de anticipación para cancelar sin perder el pago")
        };

        private readonly ReglaNegocioRepository _repo = new ReglaNegocioRepository();

        public int ObtenerEntero(int idComercio, string clave)
        {
            var regla = _repo.GetByClave(idComercio, clave);
            string valor = regla?.Valor ?? _defaults[clave].Valor;
            return int.Parse(valor, CultureInfo.InvariantCulture);
        }

        public void CrearReglasPorDefecto(int idComercio)
        {
            var reglas = new List<ReglaNegocio>();
            foreach (var kv in _defaults)
            {
                reglas.Add(new ReglaNegocio
                {
                    Clave = kv.Key,
                    Valor = kv.Value.Valor,
                    Descripcion = kv.Value.Descripcion,
                    IdComercio = idComercio
                });
            }
            _repo.Insertar(reglas);
        }
    }
}
