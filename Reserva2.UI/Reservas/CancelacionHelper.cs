using Microsoft.VisualBasic;
using Reserva2.BLL.Logic.Reservas;
using System;
using System.Windows.Forms;

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
                "Ingresá el motivo de la cancelación:", "Cancelar reserva");

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show(owner, "Debe ingresar el motivo de la cancelación.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                var resultado = new ReservaBLL().Cancelar(idReserva, motivo);

                string mensaje = "La reserva fue cancelada y el turno quedó libre.";
                if (resultado.TeniaPago)
                    mensaje += resultado.Reembolsado
                        ? "\nSe reembolsará el pago total (cancelación dentro del plazo gratuito)."
                        : "\nSe retiene el pago (cancelación fuera del plazo gratuito).";

                MessageBox.Show(owner, mensaje, "Reserva cancelada",
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
