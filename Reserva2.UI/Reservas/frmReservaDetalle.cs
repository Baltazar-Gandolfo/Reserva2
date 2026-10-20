using Reserva2.BE.Entities;
using Reserva2.BLL.Logic.Reservas;
using Reserva2.UI.Pagos;
using Services.Facade;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Reservas
{
    // Detalle de una reserva: datos, pagos asentados e historial.
    public partial class frmReservaDetalle : Form
    {
        private readonly int _idReserva;
        private readonly ReservaBLL _reservaBll = new ReservaBLL();
        private readonly PagoBLL _pagoBll = new PagoBLL();
        private Reserva _reserva;

        public frmReservaDetalle(int idReserva)
        {
            InitializeComponent();
            _idReserva = idReserva;
            this.Load += (s, e) => Cargar();
            btnCerrar.Click += (s, e) => this.Close();
            btnRegistrarPago.Click += btnRegistrarPago_Click;
            btnCancelarReserva.Click += btnCancelarReserva_Click;
        }

        private void Cargar()
        {
            btnImprimir.Visible = false; // fuera del alcance del MVP
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.ReadOnly = true;
            dgvPagos.RowHeadersVisible = false;
            lstActividad.View = View.List;

            try
            {
                _reserva = _reservaBll.ObtenerPorId(_idReserva);
                var slot = _reserva.Disponibilidad;
                var elemento = slot.Elemento;
                var pagos = _pagoBll.ListarPagos(_idReserva);
                decimal pagado = PagoBLL.TotalPagado(pagos);
                bool vigente = _reserva.Estado == EstadoReserva.Pendiente || _reserva.Estado == EstadoReserva.Confirmada;

                var cliente = UsuarioService.GetAll().FirstOrDefault(u => u.IdUsuario == _reserva.IdUsuario);

                this.Text = $"Reserva RES-{_reserva.IdReserva:00000}";
                lblElemento.Text = $"{elemento.Nombre} — {elemento.Sucursal.Nombre}";
                lblCliente.Text = cliente == null ? _reserva.IdUsuario.ToString() : $"{cliente.Nombre} — {cliente.DNI}";
                lblTurno.Text = $"{slot.Fecha:dd/MM/yyyy} {slot.HoraInicio:hh\\:mm} – {slot.HoraFin:hh\\:mm}";
                lblTotal.Text = slot.Precio.ToString("C2");
                lblPagado.Text = pagado.ToString("C2");
                lblSaldo.Text = (vigente ? Math.Max(0, slot.Precio - pagado) : 0).ToString("C2");
                lblEstado.Text = _reserva.Estado;
                lblCreada.Text = _reserva.FechaReserva.ToString("dd/MM/yyyy HH:mm");

                dgvPagos.Rows.Clear();
                foreach (var p in pagos)
                {
                    dgvPagos.Rows.Add(
                        p.FechaPago?.ToString("dd/MM/yyyy HH:mm") ?? "",
                        elemento.PorcentajeSeña > 0 ? "Seña" : "Pago total",
                        p.MetodoPago,
                        p.Monto.ToString("N2"),
                        p.Estado);
                }

                lstActividad.Items.Clear();
                lstActividad.Items.Add($"{_reserva.FechaReserva:dd/MM HH:mm} — Reserva creada (Pendiente)");
                foreach (var p in pagos)
                    lstActividad.Items.Add($"{p.FechaPago:dd/MM HH:mm} — Pago {p.Estado.ToLower()} por {p.Monto:C2} ({p.MetodoPago})");
                if (_reserva.ConfirmadaEn.HasValue)
                    lstActividad.Items.Add($"{_reserva.ConfirmadaEn:dd/MM HH:mm} — Reserva confirmada");
                if (_reserva.CanceladaEn.HasValue)
                    lstActividad.Items.Add($"{_reserva.CanceladaEn:dd/MM HH:mm} — Reserva cancelada. Motivo: {_reserva.MotivoCancelacion}");
                if (_reserva.Estado == EstadoReserva.Expirada && _reserva.ExpiraEn.HasValue)
                    lstActividad.Items.Add($"{_reserva.ExpiraEn:dd/MM HH:mm} — Reserva expirada por falta de pago");

                var rol = SessionManager.GetInstance().GetRol();
                btnRegistrarPago.Enabled = _reserva.Estado == EstadoReserva.Pendiente;
                btnCancelarReserva.Enabled = _reserva.Estado == EstadoReserva.Confirmada
                    && (rol == "Cliente" || rol == "Admin");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRegistrarPago.Enabled = btnCancelarReserva.Enabled = false;
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPago(_idReserva))
                if (frm.ShowDialog(this) == DialogResult.OK)
                    Cargar();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (CancelacionHelper.Cancelar(this, _idReserva))
                Cargar();
        }
    }
}
