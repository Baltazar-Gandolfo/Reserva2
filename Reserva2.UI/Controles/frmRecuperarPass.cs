using Services.Facade;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Controles
{
    public partial class frmRecuperarPass : Form
    {
        public frmRecuperarPass()
        {
            InitializeComponent();
            txtNuevaPass.PasswordChar = '*';
            txtConfirmarPass.PasswordChar = '*';
            lblEstado.Visible = false;
            btnEnviar.Click += btnEnviar_Click;
            btnCerrar.Click += (s, e) => this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            lblEstado.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                lblEstado.Text = "Ingresá un email válido.";
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNuevaPass.Text) || txtNuevaPass.Text.Length < 6)
            {
                lblEstado.Text = "La contraseña debe tener al menos 6 caracteres.";
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Visible = true;
                return;
            }

            if (txtNuevaPass.Text != txtConfirmarPass.Text)
            {
                lblEstado.Text = "Las contraseñas no coinciden.";
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Visible = true;
                return;
            }

            try
            {
                var usuario = UsuarioService.BuscarPorEmail(txtEmail.Text.Trim());

                if (usuario == null)
                {
                    lblEstado.Text = "No existe un usuario con ese email.";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Visible = true;
                    return;
                }

                string nuevoHash = CryptographyService.HashMd5(txtNuevaPass.Text);
                UsuarioService.CambiarPassword(usuario.IdUsuario, nuevoHash);

                BitacoraService.Registrar("Modificación", "Usuario",
                    $"Contraseña reseteada para: {txtEmail.Text.Trim()}");

                MessageBox.Show("Contraseña cambiada correctamente. Ya podés iniciar sesión.",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmRecuperarPass");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}