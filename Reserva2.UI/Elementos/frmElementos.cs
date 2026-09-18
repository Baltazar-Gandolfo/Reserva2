using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Elementos
{
    public partial class frmElementos : Form
    {
        private readonly ElementoReservableBLL _bll = new ElementoReservableBLL();
        private readonly ComercioBLL _comercioBll = new ComercioBLL();
        private int _idComercio; // 0 = todos (modo Admin)

        public frmElementos(int idComercio)
        {
            InitializeComponent();
            _idComercio = idComercio;
            this.Load += frmElementos_Load;
            dgvElementos.SelectionChanged += dgvElementos_SelectionChanged;
            btnNuevo.Click += btnNuevo_Click;
            btnEditar.Click += btnEditar_Click;
            btnBaja.Click += btnBaja_Click;
            btnDisponibilidad.Click += btnDisponibilidad_Click;
            chkVerInactivos.CheckedChanged += chkVerInactivos_CheckedChanged;
        }

        private void frmElementos_Load(object sender, EventArgs e)
        {
            if (_idComercio > 0)
            {
                var comercio = _comercioBll.ObtenerPorId(_idComercio);
                if (comercio != null)
                    this.Text = $"Elementos — {comercio.Nombre}";
            }
            else
            {
                this.Text = "Elementos — Todos los comercios";
            }

            CargarGrilla();

            btnEditar.Enabled = false;
            btnBaja.Enabled = false;
            btnDisponibilidad.Enabled = false;
        }

        private void CargarGrilla()
        {
            try
            {
                List<ElementoReservable> lista;

                if (_idComercio > 0)
                    lista = _bll.ListarPorComercio(_idComercio);
                else
                    lista = _bll.ListarTodos();

                if (!chkVerInactivos.Checked)
                    lista = lista.Where(e => e.Estado == "Activo").ToList();

                dgvElementos.Rows.Clear();

                foreach (var elem in lista)
                {
                    int rowIndex = dgvElementos.Rows.Add(
                        elem.Nombre ?? "",
                        elem.Tipo ?? "",
                        elem.Capacidad,
                        elem.PrecioBase.ToString("N2"),
                        elem.PorcentajeSeña.ToString("N0") + "%",
                        elem.Estado ?? ""
                    );
                    dgvElementos.Rows[rowIndex].Tag = elem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar elementos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvElementos_SelectionChanged(object sender, EventArgs e)
        {
            bool haySeleccion = dgvElementos.CurrentRow != null
                && dgvElementos.CurrentRow.Tag is ElementoReservable;

            btnEditar.Enabled = haySeleccion;
            btnBaja.Enabled = haySeleccion;
            btnDisponibilidad.Enabled = haySeleccion;

            if (haySeleccion)
            {
                var elem = (ElementoReservable)dgvElementos.CurrentRow.Tag;
                btnBaja.Text = elem.Estado == "Activo" ? "Dar de baja" : "Reactivar";
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            int idComercioParaAlta = _idComercio;

            // Si es Admin viendo todos, tiene que elegir comercio
            if (idComercioParaAlta == 0)
            {
                var comercios = _comercioBll.ListarTodos();
                if (comercios.Count == 0)
                {
                    MessageBox.Show("No hay comercios registrados.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var sel = new frmSeleccionarComercio(comercios);
                if (sel.ShowDialog() != DialogResult.OK)
                    return;
                idComercioParaAlta = sel.IdComercioSeleccionado;
            }

            var frm = new frmElementoABM(0, idComercioParaAlta);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvElementos.CurrentRow?.Tag is ElementoReservable elem)
            {
                var frm = new frmElementoABM(elem.IdElemento, elem.IdComercio);
                if (frm.ShowDialog() == DialogResult.OK)
                    CargarGrilla();
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgvElementos.CurrentRow?.Tag is ElementoReservable elem)
            {
                string accion = elem.Estado == "Activo" ? "dar de baja" : "reactivar";
                var resp = MessageBox.Show(
                    $"¿Está seguro que desea {accion} \"{elem.Nombre}\"?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        string nuevoEstado = elem.Estado == "Activo" ? "Inactivo" : "Activo";
                        _bll.CambiarEstado(elem.IdElemento, nuevoEstado);
                        CargarGrilla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDisponibilidad_Click(object sender, EventArgs e)
        {
            // TODO: Tarea 2
            MessageBox.Show("Próximamente...", "Disponibilidad",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}