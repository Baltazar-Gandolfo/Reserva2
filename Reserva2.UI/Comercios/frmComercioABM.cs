using System;
using System.Windows.Forms;
using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;

namespace Reserva2.UI.Comercios
{
    public partial class frmComercioABM : Form
    {
        private readonly int _idComercio;
        private readonly ComercioBLL _bll = new ComercioBLL();

        public frmComercioABM(int idComercio)
        {
            InitializeComponent();
            _idComercio = idComercio;

            this.Load += frmComercioABM_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void frmComercioABM_Load(object sender, EventArgs e)
        {
            if (_idComercio > 0)
            {
                this.Text = "Editar comercio";
                var com = _bll.ObtenerPorId(_idComercio);
                if (com != null)
                {
                    txtNombre.Text = com.Nombre;
                    txtUbicacion.Text = com.Ubicacion ?? "";
                    chkActivo.Checked = com.Activo;
                }
            }
            else
            {
                this.Text = "Nuevo comercio";
                chkActivo.Checked = true;
                chkActivo.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                if (_idComercio > 0)
                {
                    var com = _bll.ObtenerPorId(_idComercio);
                    com.Nombre = txtNombre.Text.Trim();
                    com.Ubicacion = txtUbicacion.Text.Trim();
                    com.Activo = chkActivo.Checked;
                    _bll.Actualizar(com);
                }
                else
                {
                    var com = new Comercio
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Ubicacion = txtUbicacion.Text.Trim()
                    };
                    _bll.Crear(com);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}