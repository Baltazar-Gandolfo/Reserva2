using Services.Facade;
using Services.DomainModel.Composite;
using Reserva2.UI.Controles;
using System;
using System.Windows.Forms;

namespace Reserva2.UI
{
    public partial class frmLogin : Form
    {
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Completá todos los campos.";
                lblError.Visible = true;
                return;
            }

            try
            {
                Usuario usuario = LoginService.ValidarCredenciales(txtEmail.Text.Trim(), txtPassword.Text);

                SessionManager.GetInstance().IniciarSesion(usuario);

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
                lblError.Text = ex.Message;
                lblError.Visible = true;
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnIngresar_Click(sender, e);
            }
        }

        private void lnkRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperarPass frm = new frmRecuperarPass();
            frm.ShowDialog();
        }

        private void lknRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegistro frm = new frmRegistro();
            frm.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}