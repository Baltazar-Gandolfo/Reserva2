using Microsoft.VisualBasic;
using Reserva2.BLL.Logic.Reservas;
using System;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI.Reservas
{
    // Flujo de UI de CU.Cli.004 (pedir motivo, confirmar y mostrar qué pasó con el pago).
    // Lo comparten la grilla de reservas y el detalle de la reserva.
    public static class CancelacionHelper
    {
        // Devuelve true si la reserva quedó cancelada.
        public static bool Cancelar(IWin32Window owner, int idReserva)
        {
            string motivo = Interaction.InputBox(
                T("Ingresá el motivo de la cancelación") + ":", T("Cancelar reserva"));

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show(owner, T("Debe ingresar el motivo de la cancelación."),
                    T("Validación"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                var resultado = new ReservaBLL().Cancelar(idReserva, motivo);

                string mensaje = T("La reserva fue cancelada y el turno quedó libre.");
                if (resultado.TeniaPago)
                    mensaje += resultado.Reembolsado
                        ? "\n" + T("Se reembolsará el pago total (cancelación dentro del plazo gratuito).")
                        : "\n" + T("Se retiene el pago (cancelación fuera del plazo gratuito).");

                MessageBox.Show(owner, mensaje, T("Reserva cancelada"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
