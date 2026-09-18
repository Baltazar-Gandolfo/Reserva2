using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Elementos
{
    public partial class frmElementoABM : Form
    {
        private readonly ElementoReservableBLL _bll = new ElementoReservableBLL();
        private int _idElemento;
        private int _idComercio;

        public frmElementoABM(int idElemento, int idComercio)
        {
            InitializeComponent();
            _idElemento = idElemento;
            _idComercio = idComercio;
            this.Load += frmElementoABM_Load;
            btnGuardar.Click += btnGuardar_Click;
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
                    IdComercio = _idComercio
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