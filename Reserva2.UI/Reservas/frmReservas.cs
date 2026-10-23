using Reserva2.BE.Entities;
using Reserva2.BLL.Logic.Reservas;
using Reserva2.UI.Pagos;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Reservas
{
    // CU.Cli.002 — Consultar Mis Reservas, CU.Cli.004 — Cancelar Reserva y CU.Com.015 — Consultar Reservas del Comercio.
    // El Cliente ve sus reservas; Comercio/Admin ven las de sus comercios (ReservaBLL.ListarVisibles).
    public partial class frmReservas : Form
    {
        private const string TextoBusqueda = "Cliente, código o cancha";

        private readonly ReservaBLL _reservaBll = new ReservaBLL();
        private readonly PagoBLL _pagoBll = new PagoBLL();
        private readonly bool _soloPendientes;
        private readonly bool _esCliente;

        private List<Reserva> _todas = new List<Reserva>();
        private Dictionary<int, List<Pago>> _pagos = new Dictionary<int, List<Pago>>();
        private Dictionary<Guid, string> _nombres = new Dictionary<Guid, string>();
        private bool _placeholderActivo = true;
        private bool _cargando;
        private bool _modoConfirmar;

        public frmReservas(bool soloPendientes = false)
        {
            InitializeComponent();
            _soloPendientes = soloPendientes;
            _esCliente = SessionManager.GetInstance().GetRol() == "Cliente";

            this.Load += frmReservas_Load;
            btnFiltrar.Click += (s, e) => Aplicar();
            btnDetalle.Click += btnDetalle_Click;
            btnRegistrarPago.Click += btnRegistrarPago_Click;
            btnCancelar.Click += btnCancelar_Click;
            dataGridView1.SelectionChanged += (s, e) => ActualizarBotones();
            dataGridView1.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) btnDetalle_Click(s, e); };
            comboBox1.SelectedIndexChanged += (s, e) => Aplicar();
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
        }

        private void frmReservas_Load(object sender, EventArgs e)
        {
            this.Text = _esCliente ? "Mis reservas" : "Reservas del comercio";

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Función no incluida en el alcance del MVP
            btnImprimir.Visible = false;
            if (_esCliente) label4.Visible = textBox1.Visible = false;

            dateTimePicker1.Value = DateTime.Today.AddDays(-30);
            dateTimePicker2.Value = DateTime.Today.AddDays(30);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.AddRange(new object[]
            {
                "Todos", EstadoReserva.Pendiente, EstadoReserva.Confirmada,
                EstadoReserva.Cancelada, EstadoReserva.Expirada
            });
            _cargando = true;
            comboBox1.SelectedItem = _soloPendientes ? EstadoReserva.Pendiente : "Todos";
            _cargando = false;

            textBox1.Text = TextoBusqueda;
            textBox1.ForeColor = Color.Gray;

            Recargar();
        }

        private void Recargar()
        {
            try
            {
                _nombres = UsuarioService.GetAll()
                    .GroupBy(u => u.IdUsuario)
                    .ToDictionary(g => g.Key, g => g.First().Nombre);

                _todas = _reservaBll.ListarVisibles();
                _pagos = _pagoBll.ListarPorReservas(_todas.Select(r => r.IdReserva));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _todas = new List<Reserva>();
                _pagos = new Dictionary<int, List<Pago>>();
            }
            Aplicar();
        }

        private string TextoFiltro()
        {
            return _placeholderActivo ? "" : textBox1.Text.Trim();
        }

        private void Aplicar()
        {
            if (_cargando) return;

            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date;
            string estado = comboBox1.SelectedItem?.ToString() ?? "Todos";
            string texto = TextoFiltro().ToLower();

            var filtradas = _todas.Where(r =>
                r.Disponibilidad.Fecha >= desde && r.Disponibilidad.Fecha <= hasta
                && (estado == "Todos" || r.Estado == estado)
                && (texto == "" || Coincide(r, texto))).ToList();

            dataGridView1.Rows.Clear();
            decimal facturado = 0, pendiente = 0;

            foreach (var r in filtradas)
            {
                var slot = r.Disponibilidad;
                _pagos.TryGetValue(r.IdReserva, out var pagosReserva);
                decimal pagado = PagoBLL.TotalPagado(pagosReserva);
                bool vigente = r.Estado == EstadoReserva.Pendiente || r.Estado == EstadoReserva.Confirmada;
                decimal saldo = vigente ? Math.Max(0, slot.Precio - pagado) : 0;

                facturado += pagado;
                pendiente += saldo;

                int idx = dataGridView1.Rows.Add(
                    CodigoReserva(r),
                    $"{slot.Fecha:dd/MM/yyyy} {slot.HoraInicio:hh\\:mm}",
                    $"{slot.Elemento.Nombre} ({slot.Elemento.Sucursal.Nombre})",
                    NombreCliente(r),
                    r.Estado,
                    slot.Precio.ToString("N2"),
                    saldo.ToString("N2"));

                var fila = dataGridView1.Rows[idx];
                fila.Tag = r;
                if (!vigente)
                    fila.DefaultCellStyle.ForeColor = Color.Gray;
            }

            lblCantidad.Text = $"{filtradas.Count} reservas";
            lblFacturado.Text = $"Facturado: {facturado:C2}";
            lblPendiente.Text = $"Pendiente de cobro: {pendiente:C2}";
            ActualizarBotones();
        }

        private bool Coincide(Reserva r, string texto)
        {
            return NombreCliente(r).ToLower().Contains(texto)
                || CodigoReserva(r).ToLower().Contains(texto)
                || r.Disponibilidad.Elemento.Nombre.ToLower().Contains(texto);
        }

        private string NombreCliente(Reserva r)
        {
            if (!string.IsNullOrWhiteSpace(r.NombreCliente))
                return r.NombreCliente;
            return _nombres.TryGetValue(r.IdUsuario, out var n) ? n : r.IdUsuario.ToString();
        }

        private static string CodigoReserva(Reserva r) => $"RES-{r.IdReserva:00000}";

        private Reserva Seleccionada()
        {
            return dataGridView1.CurrentRow?.Tag as Reserva;
        }

        private void ActualizarBotones()
        {
            var r = Seleccionada();
            btnDetalle.Enabled = r != null;

            bool pendiente = r != null && r.Estado == EstadoReserva.Pendiente;
            bool tienePago = false;
            if (r != null && _pagos.TryGetValue(r.IdReserva, out var pp))
                tienePago = pp.Any(p => p.Estado == EstadoPago.Aprobado);

            if (pendiente && tienePago && !_esCliente)
            {
                btnRegistrarPago.Enabled = true;
                btnRegistrarPago.Text = "Confirmar";
                _modoConfirmar = true;
            }
            else
            {
                btnRegistrarPago.Enabled = pendiente && !tienePago;
                btnRegistrarPago.Text = "Registrar pago";
                _modoConfirmar = false;
            }

            btnCancelar.Enabled = r != null
                && (r.Estado == EstadoReserva.Pendiente || r.Estado == EstadoReserva.Confirmada);
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            var r = Seleccionada();
            if (r == null) return;

            using (var frm = new frmReservaDetalle(r.IdReserva))
                frm.ShowDialog(this);
            Recargar();
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            var r = Seleccionada();
            if (r == null) return;

            if (_modoConfirmar)
            {
                try
                {
                    new ReservaBLL().Confirmar(r.IdReserva);
                    MessageBox.Show("Reserva confirmada.", "Confirmación",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Recargar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            using (var frm = new frmPago(r.IdReserva))
                if (frm.ShowDialog(this) == DialogResult.OK)
                    Recargar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var r = Seleccionada();
            if (r == null) return;

            if (CancelacionHelper.Cancelar(this, r.IdReserva))
                Recargar();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (!_placeholderActivo) return;
            _placeholderActivo = false;
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text)) return;
            _placeholderActivo = true;
            textBox1.Text = TextoBusqueda;
            textBox1.ForeColor = Color.Gray;
        }
    }
}
