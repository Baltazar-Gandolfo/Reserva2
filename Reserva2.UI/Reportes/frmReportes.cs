using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Reserva2.UI.Controles;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reserva2.UI.Reportes
{
    public partial class frmReportes : Form
    {
        private readonly ReporteBLL _reporteBll = new();
        private bool _esAdmin;
        private bool _cargando;

        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            _esAdmin = SessionManager.GetInstance().GetRol() == "Admin";

            cboComercio.Visible = _esAdmin;
            lblComercio.Visible = _esAdmin;

            cboComercio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSucursal.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComercio.DisplayMember = nameof(ItemCombo<int>.Texto);
            cboSucursal.DisplayMember = nameof(ItemCombo<int>.Texto);

            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.ReadOnly = true;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.RowHeadersVisible = false;

            ConfigurarPaneles();
            CargarComercios();

            btnGenerar.Click += (s, ev) => Generar();
            btnExportar.Click += btnExportar_Click;
            cboComercio.SelectedIndexChanged += (s, ev) => CargarSucursales();
        }

        private void ConfigurarPaneles()
        {
            pnlIngresos.BackColor = Color.FromArgb(46, 125, 50);
            pnlOcupacion.BackColor = Color.FromArgb(21, 101, 192);
            pnlCancelacion.BackColor = Color.FromArgb(198, 40, 40);
            pnlRecurrentes.BackColor = Color.FromArgb(106, 27, 154);

            lblTituloIngresos.Text = "Ingresos cobrados";
            lblTituloOcupacion.Text = "Ocupación de turnos";
            lblTituloCancelacion.Text = "Cancelaciones + Expiradas";
            lblTituloRecurrentes.Text = "Clientes recurrentes";

            LimpiarKpis();
        }

        private void LimpiarKpis()
        {
            lblValorIngresos.Text = lblValorOcupacion.Text = lblValorCancelacion.Text = lblValorRecurrentes.Text = "—";
            lblCompIngresos.Text = lblCompOcupacion.Text = lblCompCancelacion.Text = lblCompRecurrentes.Text = "";
            lblPeriodoAnterior.Text = "";
        }

        private void CargarComercios()
        {
            _cargando = true;
            try
            {
                var comercios = _reporteBll.ListarComerciosVisibles();
                var items = comercios
                    .Select(c => new ItemCombo<int>(c.IdComercio, c.Nombre)).ToList();

                if (_esAdmin)
                    items.Insert(0, new ItemCombo<int>(0, "Todos los comercios"));

                cboComercio.DataSource = items;
                if (!_esAdmin && items.Count > 0)
                    cboComercio.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmReportes.CargarComercios");
            }
            _cargando = false;
            CargarSucursales();
        }

        private void CargarSucursales()
        {
            if (_cargando) return;
            _cargando = true;
            try
            {
                int idComercio = (cboComercio.SelectedItem as ItemCombo<int>)?.Valor ?? 0;
                var items = new List<ItemCombo<int>> { new ItemCombo<int>(0, "Todas") };

                if (idComercio > 0)
                {
                    var sucursales = _reporteBll.ListarSucursales(idComercio);
                    items.AddRange(sucursales.Select(s => new ItemCombo<int>(s.IdSucursal, s.Nombre)));
                }

                cboSucursal.DataSource = items;
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmReportes.CargarSucursales");
            }
            _cargando = false;
        }

        private void Generar()
        {
            try
            {
                int idComercio = (cboComercio.SelectedItem as ItemCombo<int>)?.Valor ?? 0;
                int idSucursal = (cboSucursal.SelectedItem as ItemCombo<int>)?.Valor ?? 0;

                var kpis = _reporteBll.ObtenerKpis(
                    idComercio > 0 ? idComercio : null,
                    idSucursal > 0 ? idSucursal : null,
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date);

                lblValorIngresos.Text = kpis.IngresosCobrados.ToString("C2");
                lblCompIngresos.Text = Comparacion(kpis.IngresosCobrados, kpis.IngresosAnterior, true);
                lblCompIngresos.ForeColor = ColorComparacion(kpis.IngresosCobrados, kpis.IngresosAnterior, true);

                lblValorOcupacion.Text = $"{kpis.OcupacionPct}%";
                lblCompOcupacion.Text = Comparacion(kpis.OcupacionPct, kpis.OcupacionAnteriorPct, true);
                lblCompOcupacion.ForeColor = ColorComparacion(kpis.OcupacionPct, kpis.OcupacionAnteriorPct, true);

                lblValorCancelacion.Text = $"{kpis.CancelacionPct}%";
                lblCompCancelacion.Text = Comparacion(kpis.CancelacionPct, kpis.CancelacionAnteriorPct, false);
                lblCompCancelacion.ForeColor = ColorComparacion(kpis.CancelacionPct, kpis.CancelacionAnteriorPct, false);
                if (kpis.CancelacionPct >= 12)
                    lblValorCancelacion.ForeColor = Color.Red;
                else
                    lblValorCancelacion.ForeColor = Color.White;

                lblValorRecurrentes.Text = $"{kpis.RecurrentesPct}%";
                lblCompRecurrentes.Text = Comparacion(kpis.RecurrentesPct, kpis.RecurrentesAnteriorPct, true);
                lblCompRecurrentes.ForeColor = ColorComparacion(kpis.RecurrentesPct, kpis.RecurrentesAnteriorPct, true);

                lblPeriodoAnterior.Text = $"Comparado contra {kpis.PeriodoAnteriorDesde:dd/MM/yyyy} — " +
                    $"{kpis.PeriodoAnteriorHasta:dd/MM/yyyy} (misma cantidad de días)";

                dgvDetalle.Rows.Clear();
                foreach (var d in kpis.Detalle)
                {
                    int idx = dgvDetalle.Rows.Add(
                        d.Elemento, d.Sucursal, d.TotalReservas,
                        $"{d.OcupacionPct}%", d.Ingresos.ToString("N2"),
                        $"{d.CancelacionPct}%");

                    if (d.CancelacionPct >= 12)
                        dgvDetalle.Rows[idx].Cells["Cancelacion"].Style.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmReportes.Generar");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Comparacion(decimal actual, decimal anterior, bool mayorEsMejor)
        {
            if (anterior == 0) return actual > 0 ? "Nuevo" : "";
            decimal diff = actual - anterior;
            decimal pct = Math.Round(100m * diff / anterior, 1);
            string signo = pct >= 0 ? "+" : "";
            return $"{signo}{pct}% vs anterior";
        }

        private static Color ColorComparacion(decimal actual, decimal anterior, bool mayorEsMejor)
        {
            decimal diff = actual - anterior;
            bool positivo = mayorEsMejor ? diff >= 0 : diff <= 0;
            return positivo ? Color.LightGreen : Color.Salmon;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Generá el reporte primero.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"Reporte_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Periodo;{dtpDesde.Value:dd/MM/yyyy};{dtpHasta.Value:dd/MM/yyyy}");
                sb.AppendLine($"Ingresos;{lblValorIngresos.Text}");
                sb.AppendLine($"Ocupacion;{lblValorOcupacion.Text}");
                sb.AppendLine($"Cancelaciones;{lblValorCancelacion.Text}");
                sb.AppendLine($"Recurrentes;{lblValorRecurrentes.Text}");
                sb.AppendLine();
                sb.AppendLine("Elemento;Sucursal;Reservas;Ocupacion;Ingresos;Cancelacion");

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    sb.AppendLine(string.Join(";",
                        row.Cells[0].Value, row.Cells[1].Value,
                        row.Cells[2].Value, row.Cells[3].Value,
                        row.Cells[4].Value, row.Cells[5].Value));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exportado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
