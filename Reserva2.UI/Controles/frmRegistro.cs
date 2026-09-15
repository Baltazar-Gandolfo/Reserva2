using Services.Facade;
using Services.DomainModel.Composite;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Controles
{
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
            btnCancelar.Click += (s, e) => this.Close();
            rdbCliente.Checked = true;
            txtDni.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
            txtTelefono.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "El nombre es obligatorio.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errorProvider1.SetError(txtApellido, "El apellido es obligatorio.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                errorProvider1.SetError(txtDni, "El DNI es obligatorio.");
                return;
            }
            if (!txtDni.Text.Trim().All(char.IsDigit))
            {
                errorProvider1.SetError(txtDni, "El DNI solo puede contener números.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !txtTelefono.Text.Trim().All(char.IsDigit))
            {
                errorProvider1.SetError(txtTelefono, "El teléfono solo puede contener números.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                errorProvider1.SetError(txtEmail, "Ingresá un email válido.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPass.Text) || txtPass.Text.Length < 6)
            {
                errorProvider1.SetError(txtPass, "La contraseña debe tener al menos 6 caracteres.");
                return;
            }
            if (txtPass.Text != txtPass2.Text)
            {
                errorProvider1.SetError(txtPass2, "Las contraseñas no coinciden.");
                return;
            }

            try
            {
                string rol = rdbOwner.Checked ? "Comercio" : "Cliente";

                string passwordHash = CryptographyService.HashMd5(txtPass.Text);

                string dniEncriptado = CryptographyService.Encrypt(txtDni.Text.Trim());

                Usuario nuevoUsuario = new Usuario
                {
                    DNI = dniEncriptado,
                    Nombre = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}",
                    Email = txtEmail.Text.Trim(),
                    Password = passwordHash,
                    Telefono = txtTelefono.Text.Trim(),
                    Rol = rol,
                    Estado = "Activo",
                    FechaRegistro = DateTime.Now
                };

                UsuarioService.RegistrarUsuario(nuevoUsuario);

                // Asignar la familia correspondiente al rol
                var familias = FamiliaService.GetAll();
                Familia familiaRol = null;
                foreach (var f in familias)
                {
                    if ((rol == "Comercio" && f.Nombre == "Comercio") ||
                        (rol == "Cliente" && f.Nombre == "Cliente"))
                    {
                        familiaRol = f;
                        break;
                    }
                }

                if (familiaRol != null)
                {
                    UsuarioService.AgregarFamilia(familiaRol, nuevoUsuario);
                }

                BitacoraService.Registrar("Alta", "Usuario", $"Se registró: {nuevoUsuario.Email} como {rol}");

                MessageBox.Show("Cuenta creada correctamente. Ya podés iniciar sesión.",
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmRegistro");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}