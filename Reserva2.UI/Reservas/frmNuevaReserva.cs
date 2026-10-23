using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Reserva2.BLL.Logic.Reservas;
using Reserva2.UI.Controles;
using Reserva2.UI.Pagos;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SlotDisp = Reserva2.BE.Entities.Disponibilidad;

namespace Reserva2.UI.Reservas
{
    // CU.Cli.001 — Realizar Reserva
    public partial class frmNuevaReserva : Form
    {
        private readonly ComercioBLL _comercioBll = new ComercioBLL();
        private readonly ElementoReservableBLL _elementoBll = new ElementoReservableBLL();
        private readonly DisponibilidadBLL _dispBll = new DisponibilidadBLL();
        private readonly ReglaNegocioBLL _reglasBll = new ReglaNegocioBLL();
        private readonly ReservaBLL _reservaBll = new ReservaBLL();

        private List<SlotDisp> _libres = new List<SlotDisp>();
        private ElementoReservable _elemento;
        private SlotDisp _slot;
        private Reserva _reserva;
        private bool _armando;
        private readonly System.Windows.Forms.Timer _timerCuenta = new System.Windows.Forms.Timer { Interval = 1000 };

        // comboBox1 = Comercio, comboBox2 = Elemento, comboBox3 = Fecha, listBox1 = Turnos libres
        public frmNuevaReserva()
        {
            InitializeComponent();
            this.Load += frmNuevaReserva_Load;
            this.FormClosed += (s, e) => _timerCuenta.Dispose();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            btnConfirmar.Click += btnConfirmar_Click;
            btnCancelar.Click += (s, e) => this.Close();
            _timerCuenta.Tick += timerCuenta_Tick;
        }

        private void frmNuevaReserva_Load(object sender, EventArgs e)
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;
            bool esCliente = usuario.Rol == "Cliente";
            textBox1.Text = usuario.Nombre;
            textBox1.ReadOnly = esCliente;
            button1.Visible = false;
            if (!esCliente)
                textBox1.PlaceholderText = "Nombre del cliente (presencial)";

            foreach (var combo in new[] { comboBox1, comboBox2, comboBox3 })
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DisplayMember = comboBox2.DisplayMember = comboBox3.DisplayMember =
                nameof(ItemCombo<int>.Texto);

            LimpiarResumen();
            btnConfirmar.Enabled = false;

            var comercios = _comercioBll.ListarActivos();
            comboBox1.DataSource = comercios
                .Select(c => new ItemCombo<int>(c.IdComercio, c.Nombre)).ToList();

            if (comercios.Count == 0)
                MessageBox.Show("No hay comercios disponibles para reservar.", "Reservas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_armando || comboBox1.SelectedItem is not ItemCombo<int> comercio) return;

            var elementos = _elementoBll.ListarPorComercio(comercio.Valor)
                .Where(x => x.Estado == "Activo" && x.Sucursal.Activo).ToList();

            comboBox2.DataSource = elementos
                .Select(x => new ItemCombo<ElementoReservable>(x, $"{x.Sucursal.Nombre} — {x.Nombre}"))
                .ToList();

            int minutos = _reglasBll.ObtenerEntero(comercio.Valor, ReglaNegocioBLL.MinutosExpiracion);
            label12.Text = $"La reserva queda Pendiente hasta registrar el pago.\r\nVence a los {minutos} minutos.";

            if (elementos.Count == 0)
            {
                comboBox2.DataSource = null;
                CargarFechas(null);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_armando) return;
            _elemento = (comboBox2.SelectedItem as ItemCombo<ElementoReservable>)?.Valor;
            CargarFechas(_elemento);
        }

        private void CargarFechas(ElementoReservable elemento)
        {
            _libres = elemento == null
                ? new List<SlotDisp>()
                : _dispBll.ListarLibres(elemento.IdElemento);

            var fechas = _libres.Select(s => s.Fecha).Distinct().OrderBy(f => f).ToList();
            comboBox3.DataSource = fechas
                .Select(f => new ItemCombo<DateTime>(f, f.ToString("dddd dd/MM/yyyy"))).ToList();

            if (fechas.Count == 0)
            {
                listBox1.DataSource = null;
                _slot = null;
                LimpiarResumen();
                btnConfirmar.Enabled = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_armando) return;
            if (comboBox3.SelectedItem is not ItemCombo<DateTime> fecha) return;

            listBox1.DisplayMember = nameof(ItemCombo<int>.Texto);
            listBox1.DataSource = _libres.Where(s => s.Fecha == fecha.Valor)
                .OrderBy(s => s.HoraInicio)
                .Select(s => new ItemCombo<SlotDisp>(s,
                    $"{s.HoraInicio:hh\\:mm} – {s.HoraFin:hh\\:mm}     ${s.Precio:N2}"))
                .ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _slot = (listBox1.SelectedItem as ItemCombo<SlotDisp>)?.Valor;
            if (_slot == null || _elemento == null)
            {
                LimpiarResumen();
                btnConfirmar.Enabled = false;
                return;
            }

            decimal pct = _elemento.PorcentajeSeña;
            decimal aPagar = PagoBLL.CalcularMonto(_slot.Precio, pct);

            lblElemento.Text = _elemento.Nombre;
            lblFecha.Text = _slot.Fecha.ToString("dd/MM/yyyy");
            lblTurno.Text = $"{_slot.HoraInicio:hh\\:mm} – {_slot.HoraFin:hh\\:mm}";
            lblPrecio.Text = _slot.Precio.ToString("C2");
            label10.Text = pct > 0 ? $"Seña ({pct:0.##}%)" : "A pagar ahora";
            lblSeña.Text = aPagar.ToString("C2");
            lblSaldo.Text = (_slot.Precio - aPagar).ToString("C2");
            btnConfirmar.Enabled = _reserva == null;
        }

        private void LimpiarResumen()
        {
            lblElemento.Text = lblFecha.Text = lblTurno.Text = "—";
            lblPrecio.Text = lblSeña.Text = lblSaldo.Text = "—";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Segunda etapa: la reserva ya está creada, ahora se paga
            if (_reserva != null)
            {
                IrAPagar();
                return;
            }

            if (_slot == null)
            {
                MessageBox.Show("Seleccioná un turno libre.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nombre = textBox1.ReadOnly ? null : textBox1.Text.Trim();
                _reserva = _reservaBll.Reservar(_slot.IdDisponibilidad, nombre);
                IniciarCuentaRegresiva();
            }
            catch (SlotNoDisponibleException ex)
            {
                // FA 7.1: el turno ya no está libre; se vuelve a elegir
                MessageBox.Show(ex.Message, "Turno no disponible",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarFechas(_elemento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IniciarCuentaRegresiva()
        {
            foreach (Control c in new Control[] { comboBox1, comboBox2, comboBox3, listBox1 })
                c.Enabled = false;

            btnConfirmar.Text = "Pagar ahora";
            btnCancelar.Text = "Cerrar";
            ActualizarCuenta();
            _timerCuenta.Start();
        }

        private void timerCuenta_Tick(object sender, EventArgs e)
        {
            ActualizarCuenta();
        }

        private void ActualizarCuenta()
        {
            var restante = _reserva.ExpiraEn.Value - DateTime.Now;
            if (restante <= TimeSpan.Zero)
            {
                _timerCuenta.Stop();
                label12.Text = "La reserva expiró y el turno fue liberado.";
                btnConfirmar.Enabled = false;
                return;
            }

            label12.Text = $"Reserva Pendiente. Vence en {restante:mm\\:ss}.\r\nRegistrá el pago de seña.";
        }

        private void IrAPagar()
        {
            using (var frm = new frmPago(_reserva.IdReserva))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _timerCuenta.Stop();
                    MessageBox.Show("¡Pago registrado!\r\nLa reserva queda pendiente de confirmación por el comercio.",
                        "Reservas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
