using Services.Facade;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reserva2.UI.Controles
{
    public partial class frmRecuperarPass : Form
    {
        private TextBox txtPassActual;
        private Label lblPassActual;

        public frmRecuperarPass()
        {
            InitializeComponent();

            label1.Text = "Ingresá tu email, la contraseña actual y la nueva";

            lblPassActual = new Label
            {
                Text = "Contraseña actual",
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(24, 122)
            };
            txtPassActual = new TextBox
            {
                PasswordChar = '*',
                Location = new Point(24, 140),
                Size = new System.Drawing.Size(335, 23)
            };
            this.Controls.Add(lblPassActual);
            this.Controls.Add(txtPassActual);

            label3.Text = "Nueva contraseña";
            label3.Location = new Point(24, 175);
            txtNuevaPass.Location = new Point(24, 193);

            label4.Text = "Repetir contraseña";
            label4.Location = new Point(24, 228);
            txtConfirmarPass.Location = new Point(24, 246);

            lblEstado.Location = new Point(24, 282);
            btnEnviar.Location = new Point(201, 320);
            btnCerrar.Location = new Point(283, 320);

            txtNuevaPass.PasswordChar = '*';
            txtConfirmarPass.PasswordChar = '*';
            lblEstado.Visible = false;
            btnCerrar.Click += (s, e) => this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            lblEstado.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MostrarError("Ingresá un email válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassActual.Text))
            {
                MostrarError("Ingresá la contraseña actual.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNuevaPass.Text) || txtNuevaPass.Text.Length < 6)
            {
                MostrarError("La nueva contraseña debe tener al menos 6 caracteres.");
                return;
            }

            if (txtNuevaPass.Text != txtConfirmarPass.Text)
            {
                MostrarError("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                LoginService.ValidarCredenciales(txtEmail.Text.Trim(), txtPassActual.Text);
            }
            catch
            {
                MostrarError("La contraseña actual es incorrecta.");
                return;
            }

            try
            {
                var usuario = UsuarioService.BuscarPorEmail(txtEmail.Text.Trim());

                if (usuario == null)
                {
                    MostrarError("No existe un usuario con ese email.");
                    return;
                }

                string nuevoHash = CryptographyService.HashMd5(txtNuevaPass.Text);
                UsuarioService.CambiarPassword(usuario.IdUsuario, nuevoHash);

                BitacoraService.Registrar("Modificación", "Usuario",
                    $"Contraseña cambiada por el usuario: {txtEmail.Text.Trim()}");

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

        private void MostrarError(string mensaje)
        {
            lblEstado.Text = mensaje;
            lblEstado.ForeColor = Color.Red;
            lblEstado.Visible = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
