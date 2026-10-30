using System;
using System.Windows.Forms;
using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;

namespace Reserva2.UI.Sucursales
{
    public partial class frmSucursalABM : Form
    {
        private readonly int _idSucursal;
        private readonly int _idComercio;
        private readonly SucursalBLL _bll = new SucursalBLL();

        public frmSucursalABM(int idSucursal, int idComercio, string nombreComercio)
        {
            InitializeComponent();
            _idSucursal = idSucursal;
            _idComercio = idComercio;

            lblComercio.Text = $"Comercio: {nombreComercio}";

            this.Load += frmSucursalABM_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void frmSucursalABM_Load(object sender, EventArgs e)
        {
            if (_idSucursal > 0)
            {
                this.Text = "Editar sucursal";
                var suc = _bll.ObtenerPorId(_idSucursal);
                if (suc != null)
                {
                    txtNombre.Text = suc.Nombre;
                    txtUbicacion.Text = suc.Ubicacion ?? "";
                    chkActivo.Checked = suc.Activo;
                }
            }
            else
            {
                this.Text = "Nueva sucursal";
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
                if (_idSucursal > 0)
                {
                    var suc = _bll.ObtenerPorId(_idSucursal);
                    suc.Nombre = txtNombre.Text.Trim();
                    suc.Ubicacion = txtUbicacion.Text.Trim();
                    suc.Activo = chkActivo.Checked;
                    _bll.Actualizar(suc);
                }
                else
                {
                    var suc = new Sucursal
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Ubicacion = txtUbicacion.Text.Trim(),
                        IdComercio = _idComercio
                    };
                    _bll.Crear(suc);
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