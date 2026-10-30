using Reserva2.BE.Entities;
using Reserva2.BLL.Logic.Reservas;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Pagos
{
    // CU.Sis.002 — Registrar Pago Aprobado (MVP: el pago se asienta directamente, sin pasarela externa)
    public partial class frmPago : Form
    {
        private readonly int _idReserva;
        private readonly ReservaBLL _reservaBll = new ReservaBLL();
        private readonly PagoBLL _pagoBll = new PagoBLL();

        public frmPago(int idReserva)
        {
            InitializeComponent();
            _idReserva = idReserva;
            btnRegistrar.Click += btnRegistrar_Click;
            btnCancelar.Click += (s, e) => this.Close();
        }

        private void frmPago_Load(object sender, EventArgs e)
        {
            // El MVP no maneja comprobante, observaciones ni recibo impreso
            foreach (Control c in new Control[] { label1, txtComprobante, label7, txtObservaciones, chkImprimir })
                c.Visible = false;

            try
            {
                var reserva = _reservaBll.ObtenerPorId(_idReserva);
                var slot = reserva.Disponibilidad;
                decimal monto = _pagoBll.CalcularMonto(reserva);
                decimal pct = slot.Elemento.PorcentajeSeña;

                lblReserva.Text =
                    $"RES-{reserva.IdReserva:00000} - {slot.Elemento.Nombre} - " +
                    $"{slot.Fecha:dd/MM} {slot.HoraInicio:hh\\:mm} - total {slot.Precio:C2}";

                cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTipo.Items.Clear();
                cmbTipo.Items.Add(pct > 0 ? "Seña" : "Pago total");
                cmbTipo.SelectedIndex = 0;
                cmbTipo.Enabled = false;

                numMonto.DecimalPlaces = 2;
                numMonto.Maximum = 100000000;
                numMonto.Value = monto;
                numMonto.Enabled = false;

                cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbMetodo.Items.AddRange(MetodoPago.Todos);
                cmbMetodo.SelectedIndex = 0;

                dtpFecha.Value = DateTime.Now;
                dtpFecha.Enabled = false;

                if (reserva.Estado != EstadoReserva.Pendiente)
                {
                    btnRegistrar.Enabled = false;
                    MessageBox.Show($"La reserva está {reserva.Estado.ToLower()}: no admite pagos.",
                        "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRegistrar.Enabled = false;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cmbMetodo.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná un método de pago.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _pagoBll.RegistrarPago(_idReserva, cmbMetodo.SelectedItem.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
