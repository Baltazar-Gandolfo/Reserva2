using Services.Facade;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Reserva2.UI.Auditoria
{
    public partial class frmAuditoria : Form
    {
        public frmAuditoria()
        {
            InitializeComponent();
            this.Load += frmAuditoria_Load;
            btnFiltrar.Click += btnFiltrar_Click;
            btnExportar.Click += btnExportar_Click;
        }

        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            cboAccion.Items.Clear();
            cboAccion.Items.Add("");
            cboAccion.Items.Add("Login");
            cboAccion.Items.Add("Logout");
            cboAccion.Items.Add("Alta");
            cboAccion.Items.Add("Baja");
            cboAccion.Items.Add("Modificación");
            cboAccion.Items.Add("Asignación");
            cboAccion.SelectedIndex = 0;

            cmoEntidad.Items.Clear();
            cmoEntidad.Items.Add("");
            cmoEntidad.Items.Add("Usuario");
            cmoEntidad.Items.Add("Comercio");
            cmoEntidad.Items.Add("Reserva");
            cmoEntidad.Items.Add("Familia");
            cmoEntidad.Items.Add("Patente");
            cmoEntidad.SelectedIndex = 0;

            cboUsuario.Items.Clear();
            cboUsuario.Items.Add("");
            try
            {
                var usuarios = UsuarioService.GetAll();
                foreach (var u in usuarios)
                    cboUsuario.Items.Add(u.Nombre);
            }
            catch { }
            cboUsuario.SelectedIndex = 0;

            CargarGrilla();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                string accion = cboAccion.SelectedItem?.ToString();
                string entidad = cmoEntidad.SelectedItem?.ToString();

                List<LogEntry> registros = BitacoraService.Listar(
                    dtpDesde.Value,
                    dtpHasta.Value,
                    string.IsNullOrEmpty(accion) ? null : accion,
                    string.IsNullOrEmpty(entidad) ? null : entidad
                );

                dgvAuditoria.Rows.Clear();

                foreach (var r in registros)
                {
                    dgvAuditoria.Rows.Add(
                        r.FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss"),
                        r.NombreUsuario ?? "Sistema",
                        r.Accion,
                        r.Entidad,
                        r.Detalle
                    );
                }
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmAuditoria");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = $"Auditoria_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Fecha y Hora;Usuario;Acción;Entidad;Detalle");

                    foreach (DataGridViewRow row in dgvAuditoria.Rows)
                    {
                        if (row.IsNewRow) continue;
                        sb.AppendLine(string.Join(";",
                            row.Cells[0].Value,
                            row.Cells[1].Value,
                            row.Cells[2].Value,
                            row.Cells[3].Value,
                            row.Cells[4].Value
                        ));
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exportado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ErrorService.Registrar(ex, "frmAuditoria");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAuditoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}