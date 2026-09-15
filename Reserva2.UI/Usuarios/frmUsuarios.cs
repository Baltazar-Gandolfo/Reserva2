using Services.Facade;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reserva2.UI.Usuarios
{
    public partial class frmUsuarios : Form
    {
        private List<Usuario> _usuarios;

        public frmUsuarios()
        {
            InitializeComponent();
            this.Load += frmUsuarios_Load;
            txtBuscar.TextChanged += (s, e) => CargarGrilla();
            cmbRol.SelectedIndexChanged += (s, e) => CargarGrilla();
            chkSoloActivos.CheckedChanged += (s, e) => CargarGrilla();
            btnExportar.Click += btnExportar_Click;
            verFichaToolStripMenuItem.Click += verFichaToolStripMenuItem_Click;
            resetearContraseñaToolStripMenuItem.Click += resetearContraseñaToolStripMenuItem_Click;
            desactivarUsuarioToolStripMenuItem.Click += desactivarUsuarioToolStripMenuItem_Click;
            dgvUsuarios.CellMouseClick += dgvUsuarios_CellMouseClick;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Todos");
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("Comercio");
            cmbRol.Items.Add("Cliente");
            cmbRol.SelectedIndex = 0;

            chkSoloActivos.Checked = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.ContextMenuStrip = contextMenuStrip1;
            dgvUsuarios.Columns["Activo"].ReadOnly = true;

            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                _usuarios = UsuarioService.GetAll();
                dgvUsuarios.Rows.Clear();

                var filtrados = _usuarios.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string buscar = txtBuscar.Text.ToLower();
                    filtrados = filtrados.Where(u =>
                        u.Nombre.ToLower().Contains(buscar) ||
                        u.Email.ToLower().Contains(buscar));
                }

                string rolFiltro = cmbRol.SelectedItem?.ToString();
                if (rolFiltro != "Todos")
                    filtrados = filtrados.Where(u => u.Rol == rolFiltro);

                if (chkSoloActivos.Checked)
                    filtrados = filtrados.Where(u => u.Habilitado);

                foreach (var u in filtrados)
                {
                    string dniDescifrado;
                    try
                    {
                        dniDescifrado = CryptographyService.Decrypt(u.DNI);
                    }
                    catch
                    {
                        dniDescifrado = u.DNI;
                    }

                    int idx = dgvUsuarios.Rows.Add(
                        u.Nombre,
                        dniDescifrado,
                        u.Email,
                        u.Rol,
                        u.Habilitado
                    );
                    dgvUsuarios.Rows[idx].Tag = u;
                }

                label3.Text = $"{_usuarios.Count} usuarios - {dgvUsuarios.Rows.Count} mostrados";
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmUsuarios");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvUsuarios.ClearSelection();
                dgvUsuarios.Rows[e.RowIndex].Selected = true;
                dgvUsuarios.CurrentCell = dgvUsuarios.Rows[e.RowIndex].Cells[0];

                var usuario = (Usuario)dgvUsuarios.Rows[e.RowIndex].Tag;
                desactivarUsuarioToolStripMenuItem.Text = usuario.Habilitado ? "Desactivar usuario" : "Activar usuario";

                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private Usuario ObtenerSeleccionado()
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.Tag == null)
            {
                MessageBox.Show("Seleccioná un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return (Usuario)dgvUsuarios.CurrentRow.Tag;
        }

        private void verFichaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerSeleccionado();
            if (usuario == null) return;

            string dniDescifrado;
            try { dniDescifrado = CryptographyService.Decrypt(usuario.DNI); }
            catch { dniDescifrado = usuario.DNI; }

            string ficha = $"Nombre: {usuario.Nombre}\n" +
                           $"DNI: {dniDescifrado}\n" +
                           $"Email: {usuario.Email}\n" +
                           $"Teléfono: {usuario.Telefono}\n" +
                           $"Rol: {usuario.Rol}\n" +
                           $"Estado: {usuario.Estado}\n" +
                           $"Registrado: {usuario.FechaRegistro:dd/MM/yyyy HH:mm}";

            MessageBox.Show(ficha, $"Ficha de {usuario.Nombre}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void resetearContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerSeleccionado();
            if (usuario == null) return;

            var result = MessageBox.Show(
                $"¿Resetear la contraseña de '{usuario.Nombre}'?\n\nLa nueva contraseña será: reserva123",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string nuevoHash = CryptographyService.HashMd5("reserva123");
                    UsuarioService.CambiarPassword(usuario.IdUsuario, nuevoHash);

                    BitacoraService.Registrar("Modificación", "Usuario",
                        $"Se reseteó la contraseña de: {usuario.Email}");

                    MessageBox.Show("Contraseña reseteada a 'reserva123'.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ErrorService.Registrar(ex, "frmUsuarios");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void desactivarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerSeleccionado();
            if (usuario == null) return;

            var sesion = SessionManager.GetInstance().UsuarioActual;
            if (usuario.IdUsuario == sesion.IdUsuario)
            {
                MessageBox.Show("No podés desactivarte a vos mismo.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accion = usuario.Habilitado ? "desactivar" : "activar";
            var result = MessageBox.Show(
                $"¿{accion.ToUpper()} al usuario '{usuario.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string nuevoEstado = usuario.Habilitado ? "Inactivo" : "Activo";
                    UsuarioService.CambiarEstado(usuario.IdUsuario, nuevoEstado);

                    BitacoraService.Registrar(
                        usuario.Habilitado ? "Baja" : "Alta", "Usuario",
                        $"Usuario {usuario.Email} → {nuevoEstado}");

                    MessageBox.Show($"Usuario {accion}do.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    ErrorService.Registrar(ex, "frmUsuarios");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = $"Usuarios_{DateTime.Now:yyyyMMdd}.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Nombre;DNI;Email;Rol;Activo");

                    foreach (DataGridViewRow row in dgvUsuarios.Rows)
                    {
                        sb.AppendLine(string.Join(";",
                            row.Cells[0].Value,
                            row.Cells[1].Value,
                            row.Cells[2].Value,
                            row.Cells[3].Value,
                            row.Cells[4].Value));
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exportado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ErrorService.Registrar(ex, "frmUsuarios");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}