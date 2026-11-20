using Services.Facade;
using Services.DomainModel;
using Services.DomainModel.Composite;
using Reserva2.UI.Controles;
using Reserva2.UI.Config;
using System;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI
{
    /// <summary>
    /// Formulario de inicio de sesión. Valida credenciales y verifica integridad de datos antes de ingresar.
    /// </summary>
    public partial class frmLogin : Form
    {
        /// <summary>
        /// Inicializa el formulario y conecta los eventos de los controles.
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            btnIngresar.Click += btnIngresar_Click;
            lnkRecuperar.LinkClicked += lnkRecuperar_LinkClicked;
            lknRegistro.LinkClicked += lknRegistro_LinkClicked;
            txtEmail.KeyDown += txt_KeyDown;
            txtPassword.KeyDown += txt_KeyDown;
        }

        /// <summary>
        /// Valida las credenciales, verifica integridad DVH/DVV e inicia la sesión del usuario.
        /// </summary>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = T("Completá todos los campos.");
                lblError.Visible = true;
                return;
            }

            try
            {
                Usuario usuario = LoginService.ValidarCredenciales(txtEmail.Text.Trim(), txtPassword.Text);

                ResultadoIntegridad integridad = LoginService.VerificarIntegridad();

                if (!integridad.EsValido)
                {
                    if (usuario.Rol == "Admin")
                    {
                        var frm = new frmIntegridadDVH(integridad);
                        DialogResult resultado = frm.ShowDialog();

                        if (resultado == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        MessageBox.Show(
                            T("El sistema no está disponible, contacte con un Administrador."),
                            T("Error de integridad"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                SessionManager.GetInstance().IniciarSesion(usuario);
                BitacoraService.Registrar("Login", "Usuario", $"Ingresó al sistema: {usuario.Email}");

                frmPrincipal principal = new frmPrincipal();
                principal.FormClosed += (s, args) =>
                {
                    if (!SessionManager.GetInstance().EstaAutenticado())
                    {
                        txtPassword.Clear();
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                };
                principal.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmLogin");
                lblError.Text = ex.Message;
                lblError.Visible = true;
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// Permite iniciar sesión con la tecla Enter.
        /// </summary>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnIngresar_Click(sender, e);
            }
        }

        /// <summary>
        /// Abre el formulario de recuperación de contraseña.
        /// </summary>
        private void lnkRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperarPass frm = new frmRecuperarPass();
            frm.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario de registro de nueva cuenta.
        /// </summary>
        private void lknRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegistro frm = new frmRegistro();
            frm.ShowDialog();
        }

        /// <summary>
        /// Traduce los controles del formulario al idioma actual.
        /// </summary>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            TraducirControles(this);
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {

        }
    }
}