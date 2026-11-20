using Services.Facade;
using Services.DomainModel.Composite;
using System;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI.Controles
{
    /// <summary>
    /// Formulario de registro de nuevos usuarios. Valida datos, crea la cuenta y asigna permisos según el rol.
    /// </summary>
    public partial class frmRegistro : Form
    {
        /// <summary>Inicializa el formulario y configura validaciones de teclado para DNI y teléfono.</summary>
        public frmRegistro()
        {
            InitializeComponent();
            this.Load += frmRegistro_Load;
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

        /// <summary>Traduce los controles al idioma activo.</summary>
        private void frmRegistro_Load(object sender, EventArgs e)
        {
            TraducirControles(this);
        }

        /// <summary>Valida los campos, encripta DNI y contraseña, registra el usuario y asigna la familia de permisos.</summary>
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, T("El nombre es obligatorio."));
                return;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errorProvider1.SetError(txtApellido, T("El apellido es obligatorio."));
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                errorProvider1.SetError(txtDni, T("El DNI es obligatorio."));
                return;
            }
            if (!txtDni.Text.Trim().All(char.IsDigit))
            {
                errorProvider1.SetError(txtDni, T("El DNI solo puede contener números."));
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !txtTelefono.Text.Trim().All(char.IsDigit))
            {
                errorProvider1.SetError(txtTelefono, T("El teléfono solo puede contener números."));
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                errorProvider1.SetError(txtEmail, T("Ingresá un email válido."));
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPass.Text) || txtPass.Text.Length < 6)
            {
                errorProvider1.SetError(txtPass, T("La contraseña debe tener al menos 6 caracteres."));
                return;
            }
            if (txtPass.Text != txtPass2.Text)
            {
                errorProvider1.SetError(txtPass2, T("Las contraseñas no coinciden."));
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

                MessageBox.Show(T("Cuenta creada correctamente. Ya podés iniciar sesión."),
                    T("Registro exitoso"), MessageBoxButtons.OK, MessageBoxIcon.Information);

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