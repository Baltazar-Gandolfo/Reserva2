using System;

namespace Reserva2.BLL.Logic.Reservas
{
    // Patrón Strategy: política que decide qué pasa con el pago al cancelar.
    public interface IPoliticaCancelacion
    {
        // true = reembolso total; false = se retiene el pago.
        bool CorrespondeReembolso(DateTime inicioSlot, DateTime ahora);
    }

    // Reembolso total si se cancela con al menos N horas de anticipación (regla cancelacion_gratis_hs).
    public class PoliticaCancelacionPorPlazo : IPoliticaCancelacion
    {
        private readonly int _horasGratis;

        public PoliticaCancelacionPorPlazo(int horasGratis)
        {
            _horasGratis = horasGratis;
        }

        public bool CorrespondeReembolso(DateTime inicioSlot, DateTime ahora)
        {
            return (inicioSlot - ahora).TotalHours >= _horasGratis;
        }
    }
}
