using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using System;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI
{
    /// <summary>
    /// Formulario rápido para crear un comercio/sucursal desde otros formularios.
    /// </summary>
    public class frmComercioRapido : Form
    {
        private TextBox txtNombre;
        private TextBox txtUbicacion;
        private Button btnCrear;
        private Button btnCancelar;

        /// <summary>Id del comercio recién creado, disponible tras DialogResult.OK.</summary>
        public int IdComercioCreado { get; private set; }

        /// <summary>
        /// Inicializa el formulario con los controles de creación rápida.
        /// </summary>
        public frmComercioRapido()
        {
            this.Text = "Crear Comercio / Sucursal";
            this.Size = new System.Drawing.Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblNombre = new Label { Text = "Nombre *", Location = new System.Drawing.Point(15, 20), AutoSize = true };
            txtNombre = new TextBox { Location = new System.Drawing.Point(120, 17), Width = 240 };

            var lblUbicacion = new Label { Text = "Ubicación", Location = new System.Drawing.Point(15, 55), AutoSize = true };
            txtUbicacion = new TextBox { Location = new System.Drawing.Point(120, 52), Width = 240 };

            btnCrear = new Button { Text = "Crear", Location = new System.Drawing.Point(195, 100), Width = 80 };
            btnCrear.Click += BtnCrear_Click;

            btnCancelar = new Button { Text = "Cancelar", Location = new System.Drawing.Point(280, 100), Width = 80 };
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lblNombre, txtNombre, lblUbicacion, txtUbicacion, btnCrear, btnCancelar });
            this.Load += (s, e) => TraducirControles(this);
        }

        /// <summary>Valida los datos y crea el comercio en la base de datos.</summary>
        private void BtnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show(T("El nombre es obligatorio."),
                        T("Validación"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                var comercio = new Comercio
                {
                    Nombre = txtNombre.Text.Trim(),
                    Ubicacion = txtUbicacion.Text.Trim()
                };

                var bll = new ComercioBLL();
                bll.Crear(comercio);

                IdComercioCreado = comercio.IdComercio;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {

        }
    }
}