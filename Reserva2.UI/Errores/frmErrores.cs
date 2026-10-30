using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reserva2.UI.Errores
{
    public partial class frmErrores : Form
    {
        private List<Dictionary<string, string>> _errores = new();

        public frmErrores()
        {
            InitializeComponent();
            this.Load += frmErrores_Load;
            btnFiltrar.Click += (s, e) => Filtrar();
            dgvErrores.SelectionChanged += dgvErrores_SelectionChanged;
            btnCopiar.Click += btnCopiar_Click;
            btnExportar.Click += btnExportar_Click;
        }

        private void frmErrores_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            cboNivel.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNivel.Items.AddRange(new object[] { "Todos", "Error", "Advertencia", "Critico" });
            cboNivel.SelectedIndex = 0;
            cboNivel.SelectedIndexChanged += (s, ev) => Filtrar();

            dgvErrores.AllowUserToAddRows = false;
            dgvErrores.ReadOnly = true;
            dgvErrores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvErrores.RowHeadersVisible = false;
            txtDetalle.ReadOnly = true;
            txtDetalle.ScrollBars = ScrollBars.Vertical;

            Filtrar();
        }

        private void Filtrar()
        {
            try
            {
                _errores = ErrorService.Listar(dtpDesde.Value.Date, dtpHasta.Value.Date.AddDays(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _errores = new List<Dictionary<string, string>>();
            }

            string nivel = cboNivel.SelectedItem?.ToString() ?? "Todos";
            var filtrados = nivel == "Todos"
                ? _errores
                : _errores.Where(err => ClasificarNivel(err) == nivel).ToList();

            filtrados = filtrados.Take(500).ToList();

            dgvErrores.Rows.Clear();
            foreach (var err in filtrados)
            {
                string nivelErr = ClasificarNivel(err);
                int idx = dgvErrores.Rows.Add(
                    ObtenerValor(err, "FechaCreacion"),
                    nivelErr,
                    ObtenerValor(err, "NombreUsuario"),
                    ObtenerValor(err, "Modulo"),
                    ObtenerValor(err, "Mensaje"));

                var fila = dgvErrores.Rows[idx];
                fila.Tag = err;

                if (nivelErr == "Critico")
                    fila.DefaultCellStyle.ForeColor = Color.Red;
                else if (nivelErr == "Advertencia")
                    fila.DefaultCellStyle.ForeColor = Color.Orange;
            }

            txtDetalle.Clear();
        }

        private static string ClasificarNivel(Dictionary<string, string> err)
        {
            string tipo = ObtenerValor(err, "TipoExcepcion").ToLower();
            if (tipo.Contains("critical") || tipo.Contains("fatal") || tipo.Contains("outofmemory") || tipo.Contains("stackoverflow"))
                return "Critico";
            if (tipo.Contains("warning") || tipo.Contains("timeout") || tipo.Contains("io"))
                return "Advertencia";
            return "Error";
        }

        private static string ObtenerValor(Dictionary<string, string> dict, string clave)
        {
            return dict.TryGetValue(clave, out var v) ? v ?? "" : "";
        }

        private void dgvErrores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvErrores.CurrentRow?.Tag is not Dictionary<string, string> err)
            {
                txtDetalle.Clear();
                return;
            }

            txtDetalle.Text = $"Mensaje: {ObtenerValor(err, "Mensaje")}\r\n\r\n" +
                              $"Tipo: {ObtenerValor(err, "TipoExcepcion")}\r\n\r\n" +
                              $"StackTrace:\r\n{ObtenerValor(err, "StackTrace")}";
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDetalle.Text))
            {
                Clipboard.SetText(txtDetalle.Text);
                MessageBox.Show("Detalle copiado al portapapeles.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvErrores.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"Errores_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Fecha;Nivel;Usuario;Pantalla;Mensaje;Tipo;StackTrace");

                foreach (DataGridViewRow row in dgvErrores.Rows)
                {
                    if (row.Tag is not Dictionary<string, string> err) continue;
                    sb.AppendLine(string.Join(";",
                        Escapar(ObtenerValor(err, "FechaCreacion")),
                        Escapar(ClasificarNivel(err)),
                        Escapar(ObtenerValor(err, "NombreUsuario")),
                        Escapar(ObtenerValor(err, "Modulo")),
                        Escapar(ObtenerValor(err, "Mensaje")),
                        Escapar(ObtenerValor(err, "TipoExcepcion")),
                        Escapar(ObtenerValor(err, "StackTrace"))));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exportado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Escapar(string valor)
        {
            return valor.Replace(";", ",").Replace("\r", " ").Replace("\n", " ");
        }
    }
}
