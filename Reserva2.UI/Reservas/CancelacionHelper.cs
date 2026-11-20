using Microsoft.VisualBasic;
using Reserva2.BLL.Logic.Reservas;
using System;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI.Reservas
{
    /// <summary>
    /// Helper de cancelación de reservas. Pide motivo, ejecuta la cancelación e informa sobre el reembolso.
    /// </summary>
    public static class CancelacionHelper
    {
        /// <summary>
        /// Ejecuta el flujo de cancelación de una reserva: pide motivo, cancela y muestra resultado.
        /// </summary>
        /// <param name="owner">Ventana padre para los diálogos.</param>
        /// <param name="idReserva">Id de la reserva a cancelar.</param>
        /// <returns><c>true</c> si la reserva fue cancelada exitosamente.</returns>
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
