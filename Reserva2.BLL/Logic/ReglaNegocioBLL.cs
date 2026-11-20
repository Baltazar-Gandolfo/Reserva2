using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System.Collections.Generic;
using System.Globalization;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Lógica de negocio para las reglas configurables de cada comercio.
    /// </summary>
    public class ReglaNegocioBLL
    {
        /// <summary>
        /// Clave de la regla que define los minutos de expiración para reservas pendientes de pago.
        /// </summary>
        public const string MinutosExpiracion = "minutos_expiracion";

        /// <summary>
        /// Clave de la regla que limita la cantidad máxima de reservas por cliente por semana.
        /// </summary>
        public const string MaxReservasSemana = "max_reservas_semana";

        /// <summary>
        /// Clave de la regla que define las horas de anticipación para cancelar con reembolso total.
        /// </summary>
        public const string CancelacionGratisHs = "cancelacion_gratis_hs";

        private static readonly Dictionary<string, (string Valor, string Descripcion)> _defaults = new()
        {
            [MinutosExpiracion] = ("10", "Minutos que se bloquea un slot hasta que se paga la reserva"),
            [MaxReservasSemana] = ("3", "Máximo de reservas pendientes/confirmadas por cliente por semana"),
            [CancelacionGratisHs] = ("24", "Horas de anticipación para cancelar sin perder el pago")
        };

        private readonly ReglaNegocioRepository _repo = new ReglaNegocioRepository();

        /// <summary>
        /// Obtiene el valor entero de una regla de negocio, usando el valor por defecto si no existe en la base.
        /// </summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="clave">Clave de la regla a consultar.</param>
        /// <returns>Valor entero de la regla.</returns>
        public int ObtenerEntero(int idComercio, string clave)
        {
            var regla = _repo.GetByClave(idComercio, clave);
            string valor = regla?.Valor ?? _defaults[clave].Valor;
            return int.Parse(valor, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Crea las reglas de negocio por defecto para un comercio recién creado.
        /// </summary>
        /// <param name="idComercio">Identificador del comercio.</param>
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
