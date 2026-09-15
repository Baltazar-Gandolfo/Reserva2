using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Services.Facade;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Comercios
{
    public partial class frmComercioDetalle : Form
    {
        private ComercioBLL _bll = new ComercioBLL();
        private Comercio _comercio;
        private bool _esNuevo;

        public frmComercioDetalle(Comercio comercio)
        {
            InitializeComponent();
            _comercio = comercio;
            _esNuevo = (comercio == null);
            this.Load += frmComercioDetalle_Load;
            btnCerrar.Click += btnCerrar_Click;
        }

        private void frmComercioDetalle_Load(object sender, EventArgs e)
        {
            if (_esNuevo)
            {
                this.Text = "Nuevo comercio";
                txtOwner.Text = SessionManager.GetInstance().UsuarioActual.Nombre;
                txtOwner.ReadOnly = true;
                checkBox1.Checked = true;
                checkBox1.Enabled = false;

                btnCerrar.Text = "Guardar";
            }
            else
            {
                this.Text = $"Comercio — {_comercio.Nombre}";
                txtRazonSocial.Text = _comercio.Nombre;
                txtDireccion.Text = _comercio.Ubicacion;
                txtRubro.Text = _comercio.Slug;
                txtOwner.Text = SessionManager.GetInstance().UsuarioActual.Nombre;
                txtOwner.ReadOnly = true;
                checkBox1.Checked = _comercio.Activo;

                btnCerrar.Text = "Guardar cambios";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("El nombre del comercio es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_esNuevo)
                {
                    Comercio nuevo = new Comercio
                    {
                        Nombre = txtRazonSocial.Text.Trim(),
                        Ubicacion = txtDireccion.Text.Trim(),
                        LogoUrl = ""
                    };

                    _bll.Crear(nuevo);
                    MessageBox.Show("Comercio creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _comercio.Nombre = txtRazonSocial.Text.Trim();
                    _comercio.Ubicacion = txtDireccion.Text.Trim();
                    _comercio.Activo = checkBox1.Checked;

                    _bll.Actualizar(_comercio);
                    MessageBox.Show("Comercio actualizado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmComercioDetalle");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {

        }
    }
}