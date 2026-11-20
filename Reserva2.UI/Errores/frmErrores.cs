using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI.Errores
{
    /// <summary>
    /// Formulario de visualización y exportación de errores del sistema.
    /// </summary>
    public partial class frmErrores : Form
    {
        /// <summary>Lista en memoria de errores cargados desde la base de datos.</summary>
        private List<Dictionary<string, string>> _errores = new();

        /// <summary>
        /// Inicializa el formulario y conecta los eventos de los controles.
        /// </summary>
        public frmErrores()
        {
            InitializeComponent();
            this.Load += frmErrores_Load;
            btnFiltrar.Click += (s, e) => Filtrar();
            dgvErrores.SelectionChanged += dgvErrores_SelectionChanged;
            btnCopiar.Click += btnCopiar_Click;
            btnExportar.Click += btnExportar_Click;
        }

        /// <summary>Configura los controles y ejecuta el filtro inicial al cargar el formulario.</summary>
        private void frmErrores_Load(object sender, EventArgs e)
        {
            TraducirControles(this);
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

        /// <summary>Consulta los errores por rango de fechas y nivel, y los muestra en la grilla.</summary>
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
            if (dgvErrores.Rows.Count > 0)
                dgvErrores_SelectionChanged(dgvErrores, EventArgs.Empty);
        }

        /// <summary>Clasifica un error como Critico, Advertencia o Error según el tipo de excepción.</summary>
        private static string ClasificarNivel(Dictionary<string, string> err)
        {
            string tipo = ObtenerValor(err, "TipoExcepcion").ToLower();
            if (tipo.Contains("critical") || tipo.Contains("fatal") || tipo.Contains("outofmemory") || tipo.Contains("stackoverflow"))
                return "Critico";
            if (tipo.Contains("warning") || tipo.Contains("timeout") || tipo.Contains("io"))
                return "Advertencia";
            return "Error";
        }

        /// <summary>Obtiene un valor del diccionario de forma segura, devolviendo cadena vacía si no existe.</summary>
        private static string ObtenerValor(Dictionary<string, string> dict, string clave)
        {
            return dict.TryGetValue(clave, out var v) ? v ?? "" : "";
        }

        /// <summary>Muestra el detalle del error seleccionado en el cuadro de texto.</summary>
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

        /// <summary>Copia el detalle del error al portapapeles.</summary>
        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDetalle.Text))
            {
                Clipboard.SetText(txtDetalle.Text);
                MessageBox.Show(T("Detalle copiado al portapapeles."), T("Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>Exporta los errores filtrados a un archivo CSV.</summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvErrores.Rows.Count == 0)
            {
                MessageBox.Show(T("No hay datos para exportar."), T("Info"),
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
                MessageBox.Show(T("Exportado."), T("OK"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Escapa caracteres conflictivos para formato CSV.</summary>
        private static string Escapar(string valor)
        {
            return valor.Replace(";", ",").Replace("\r", " ").Replace("\n", " ");
        }
    }
}
