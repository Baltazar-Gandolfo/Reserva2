using System;

namespace Reserva2.BLL.Logic.Reservas
{
    /// <summary>
    /// Interfaz del patrón Strategy para la política de cancelación de reservas.
    /// Determina si corresponde reembolso al cancelar.
    /// </summary>
    public interface IPoliticaCancelacion
    {
        /// <summary>
        /// Evalúa si corresponde reembolso total al cancelar.
        /// </summary>
        /// <param name="inicioSlot">Fecha y hora de inicio del turno reservado.</param>
        /// <param name="ahora">Momento actual de la cancelación.</param>
        /// <returns>True si corresponde reembolso total, false si se retiene el pago.</returns>
        bool CorrespondeReembolso(DateTime inicioSlot, DateTime ahora);
    }

    /// <summary>
    /// Política de cancelación basada en plazo: reembolso total si se cancela
    /// con al menos N horas de anticipación (regla cancelacion_gratis_hs del comercio).
    /// </summary>
    public class PoliticaCancelacionPorPlazo : IPoliticaCancelacion
    {
        private readonly int _horasGratis;

        /// <summary>
        /// Inicializa la política con la cantidad de horas de gracia.
        /// </summary>
        /// <param name="horasGratis">Horas mínimas de anticipación para obtener reembolso.</param>
        public PoliticaCancelacionPorPlazo(int horasGratis)
        {
            _horasGratis = horasGratis;
        }

        /// <inheritdoc/>
        public bool CorrespondeReembolso(DateTime inicioSlot, DateTime ahora)
        {
            return (inicioSlot - ahora).TotalHours >= _horasGratis;
        }
    }
}
