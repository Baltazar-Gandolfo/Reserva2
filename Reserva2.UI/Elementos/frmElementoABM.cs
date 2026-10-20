using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Services.Facade;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Elementos
{
    public partial class frmElementoABM : Form
    {
        private readonly ElementoReservableBLL _bll = new ElementoReservableBLL();
        private readonly ComercioBLL _comercioBll = new ComercioBLL();
        private readonly SucursalBLL _sucursalBll = new SucursalBLL();
        private int _idElemento;
        private int _idComercio;

        public frmElementoABM(int idElemento, int idComercio)
        {
            InitializeComponent();
            _idElemento = idElemento;
            _idComercio = idComercio;
            this.Load += frmElementoABM_Load;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void frmElementoABM_Load(object sender, EventArgs e)
        {
            // Cargar combo Tipo
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.Clear();
            cmbTipo.Items.AddRange(new object[]
            {
                "Cancha de fútbol",
                "Cancha de pádel",
                "Cancha de tenis",
                "Cancha de básquet",
                "Cancha de vóley",
                "Pileta",
                "Quincho",
                "Salón de eventos",
                "Turno pelu/uñas/etc.",
                "Otro"
            });
            cmbTipo.SelectedIndex = 0;

            // Cargar combo Duración (en minutos)
            cboDuracion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDuracion.Items.Clear();
            cboDuracion.Items.AddRange(new object[]
            {
                "30", "60", "90", "120", "180", "240", "300", "360"
            });
            cboDuracion.SelectedIndex = 1; // 60 por defecto

            // Config numéricos
            numCapacidad.Minimum = 1;
            numCapacidad.Maximum = 10000;
            numCapacidad.Value = 1;

            numPrecio.Minimum = 0;
            numPrecio.Maximum = 10000000;
            numPrecio.DecimalPlaces = 2;

            numSena.Minimum = 0;
            numSena.Maximum = 100;
            numSena.DecimalPlaces = 0;

            chkActivo.Checked = true;

            // comboBox1 = Comercio, comboBox2 = Sucursal
            // Con comercio indicado queda fijo; en un alta sin comercio se elige acá.
            var rol = SessionManager.GetInstance().GetRol();
            var comercios = (rol == "Admin" ? _comercioBll.ListarTodos() : _comercioBll.ListarPorOwner())
                .Where(c => c.Activo || c.IdComercio == _idComercio).ToList();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DisplayMember = nameof(Comercio.Nombre);
            comboBox1.ValueMember = nameof(Comercio.IdComercio);
            comboBox1.DataSource = comercios;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DisplayMember = nameof(Sucursal.Nombre);
            comboBox2.ValueMember = nameof(Sucursal.IdSucursal);

            if (_idComercio > 0)
            {
                comboBox1.SelectedValue = _idComercio;
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.SelectedIndexChanged += (s2, e2) => CargarSucursales();
            }

            if (comercios.Count == 0)
            {
                MessageBox.Show("No tenés comercios registrados. Creá uno desde 'Mis comercios'.",
                    "Sin comercios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGuardar.Enabled = false;
            }
            else
            {
                CargarSucursales();
            }

            // Si es edición, cargar datos
            if (_idElemento > 0)
            {
                this.Text = "Editar Elemento";
                CargarElemento();
            }
            else
            {
                this.Text = "Nuevo Elemento";
            }
        }

        private void CargarSucursales()
        {
            if (comboBox1.SelectedValue is not int idComercio)
                return;

            var sucursales = _sucursalBll.ListarPorComercio(idComercio)
                .Where(x => x.Activo).ToList();
            comboBox2.DataSource = sucursales;

            bool hay = sucursales.Count > 0;
            btnGuardar.Enabled = hay;
            if (!hay)
            {
                MessageBox.Show("El comercio no tiene sucursales activas. Creá una sucursal antes de agregar elementos.",
                    "Sin sucursales", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CargarElemento()
        {
            try
            {
                var elem = _bll.ObtenerPorId(_idElemento);
                if (elem == null)
                {
                    MessageBox.Show("No se encontró el elemento.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtNombre.Text = elem.Nombre;
                comboBox2.SelectedValue = elem.IdSucursal;
                cmbTipo.SelectedItem = elem.Tipo;
                numCapacidad.Value = elem.Capacidad;
                numPrecio.Value = elem.PrecioBase;
                numSena.Value = elem.PorcentajeSeña;
                txtObservaciones.Text = elem.Descripcion ?? "";
                chkActivo.Checked = elem.Estado == "Activo";

                // Seleccionar duración
                string dur = elem.DuracionSlot.ToString();
                int idx = cboDuracion.Items.IndexOf(dur);
                cboDuracion.SelectedIndex = idx >= 0 ? idx : 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (comboBox2.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una sucursal.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox2.Focus();
                    return;
                }

                if (cmbTipo.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un tipo.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTipo.Focus();
                    return;
                }

                if (numPrecio.Value <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a cero.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numPrecio.Focus();
                    return;
                }

                var elemento = new ElementoReservable
                {
                    IdElemento = _idElemento,
                    Nombre = txtNombre.Text.Trim(),
                    Tipo = cmbTipo.SelectedItem.ToString(),
                    Capacidad = (int)numCapacidad.Value,
                    PrecioBase = numPrecio.Value,
                    PorcentajeSeña = numSena.Value,
                    DuracionSlot = int.Parse(cboDuracion.SelectedItem.ToString()),
                    Descripcion = txtObservaciones.Text.Trim(),
                    Estado = chkActivo.Checked ? "Activo" : "Inactivo",
                    IdSucursal = (int)comboBox2.SelectedValue
                };

                if (_idElemento == 0)
                    _bll.Crear(elemento);
                else
                    _bll.Actualizar(elemento);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}