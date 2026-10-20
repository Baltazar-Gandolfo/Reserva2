using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Services.Facade;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Comercios
{
    // CU.Adm.002 — Ver Detalle de Comercio (solo lectura). Desde acá el Admin puede dar de baja o reactivar.
    public partial class frmComercioDetalle : Form
    {
        private readonly int _idComercio;
        private readonly ComercioBLL _bll = new ComercioBLL();
        private Comercio _comercio;

        private DataGridView _gridElementos;
        private DataGridView _gridReglas;

        public frmComercioDetalle(int idComercio)
        {
            InitializeComponent();
            _idComercio = idComercio;
            this.Load += (s, e) => Cargar();
            btnBaja.Click += btnBaja_Click;
        }

        private void Cargar()
        {
            PrepararControles();

            try
            {
                var detalle = _bll.ObtenerDetalle(_idComercio);
                _comercio = detalle.Comercio;

                this.Text = $"Comercio — {_comercio.Nombre}";
                txtRazonSocial.Text = _comercio.Nombre;
                txtRubro.Text = _comercio.Slug;
                txtDireccion.Text = _comercio.Ubicacion;
                txtOwner.Text = detalle.Owner;
                checkBox1.Checked = _comercio.Activo;
                btnBaja.Text = _comercio.Activo ? "Dar de baja" : "Reactivar";

                var mes = DateTime.Today.ToString("MMMM yyyy");
                groupBox1.Text = $"Resumen de {mes}";
                lblRecaudado.Text = detalle.ResumenMes.Recaudado.ToString("C2");
                lblReservas.Text = detalle.ResumenMes.Reservas.ToString();
                lblOcupacion.Text = $"{detalle.ResumenMes.OcupacionPct:0.#}%";
                lblCanceladas.Text = detalle.ResumenMes.Canceladas.ToString();

                _gridElementos.Rows.Clear();
                foreach (var el in detalle.Elementos.OrderBy(x => x.Sucursal.Nombre).ThenBy(x => x.Nombre))
                    _gridElementos.Rows.Add(el.Nombre, el.Sucursal.Nombre, el.Tipo,
                        el.PrecioBase.ToString("N2"), $"{el.PorcentajeSeña:0.##}%", el.Estado);

                _gridReglas.Rows.Clear();
                foreach (var r in detalle.Reglas.OrderBy(x => x.Clave))
                    _gridReglas.Rows.Add(r.Clave, r.Valor, r.Descripcion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnBaja.Enabled = false;
            }
        }

        // Pantalla de solo lectura; los campos que el MVP no maneja (CUIT, teléfono) se ocultan.
        private void PrepararControles()
        {
            foreach (var tb in new[] { txtRazonSocial, txtRubro, txtDireccion, txtOwner })
                tb.ReadOnly = true;
            checkBox1.AutoCheck = false;

            foreach (Control c in new Control[] { label6, txtCUIT, label7, txtTelefono })
                c.Visible = false;
            label2.Text = "Slug";

            tabPage3.Text = "Configuración";
            if (_gridElementos == null)
            {
                _gridElementos = ObtenerGrilla(tabPage2,
                    "Nombre", "Sucursal", "Tipo", "Precio base", "Seña", "Estado");
                _gridReglas = ObtenerGrilla(tabPage3, "Regla", "Valor", "Descripción");
            }
        }

        // Usa la grilla que ya exista en la pestaña o, si no hay, crea una de solo lectura.
        private static DataGridView ObtenerGrilla(TabPage pagina, params string[] columnas)
        {
            var grilla = pagina.Controls.OfType<DataGridView>().FirstOrDefault();
            if (grilla == null)
            {
                grilla = new DataGridView { Dock = DockStyle.Fill };
                pagina.Controls.Add(grilla);
            }

            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.RowHeadersVisible = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (grilla.Columns.Count == 0)
                foreach (var nombre in columnas)
                    grilla.Columns.Add(nombre, nombre);

            return grilla;
        }

        // Conectado desde el Designer
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (_comercio == null) return;

            string accion = _comercio.Activo ? "dar de baja" : "reactivar";
            var resp = MessageBox.Show($"¿Está seguro que desea {accion} el comercio \"{_comercio.Nombre}\"?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            try
            {
                _bll.CambiarEstado(_comercio.IdComercio, !_comercio.Activo);
                Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
