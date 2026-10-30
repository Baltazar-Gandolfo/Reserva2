using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Reserva2.UI.Sucursales;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Comercios
{
    public partial class frmComercios : Form
    {
        private ComercioBLL _bll = new ComercioBLL();
        private List<Comercio> _comercios;

        public frmComercios()
        {
            InitializeComponent();
            this.Load += frmComercios_Load;
            btnAlta.Click += btnAlta_Click;
            btnEditar.Click += btnEditar_Click;
            btnBaja.Click += btnBaja_Click;
            btnSucursales.Click += btnSucursales_Click;
            chkVerInactivos.CheckedChanged += chkVerInactivos_CheckedChanged;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        // Doble clic: el Admin ve el detalle (CU.Adm.002); el dueño edita su comercio.
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var com = ObtenerSeleccionado();
            if (e.RowIndex < 0 || com == null) return;

            if (SessionManager.GetInstance().GetRol() == "Admin")
            {
                using (var frm = new frmComercioDetalle(com.IdComercio))
                    frm.ShowDialog(this);
                CargarGrilla();
            }
            else
            {
                btnEditar_Click(sender, e);
            }
        }

        private void frmComercios_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            dataGridView1.Rows.Clear();

            var rol = SessionManager.GetInstance().UsuarioActual.Rol;
            if (rol == "Admin")
                _comercios = _bll.ListarTodos();
            else
                _comercios = _bll.ListarPorOwner();

            bool verInactivos = chkVerInactivos.Checked;
            int total = _comercios.Count;
            int inactivos = _comercios.Count(c => !c.Activo);

            var filtrados = verInactivos
                ? _comercios
                : _comercios.Where(c => c.Activo).ToList();

            foreach (var c in filtrados)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    c.Nombre,
                    c.Ubicacion ?? "",
                    c.Activo ? "Activo" : "Inactivo"
                );
                dataGridView1.Rows[rowIndex].Tag = c;

                if (!c.Activo)
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            }

            label2.Text = inactivos > 0
                ? $"{total} comercios · {inactivos} inactivo{(inactivos > 1 ? "s" : "")}"
                : $"{total} comercios";

            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            bool hay = dataGridView1.CurrentRow != null
                    && dataGridView1.CurrentRow.Tag is Comercio;
            btnEditar.Enabled = hay;
            btnBaja.Enabled = hay;
            btnSucursales.Enabled = hay;
        }

        private Comercio ObtenerSeleccionado()
        {
            if (dataGridView1.CurrentRow?.Tag is Comercio com)
                return com;
            return null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            var frm = new frmComercioABM(0);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var com = ObtenerSeleccionado();
            if (com == null)
            {
                MessageBox.Show("Seleccioná un comercio.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new frmComercioABM(com.IdComercio);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            var com = ObtenerSeleccionado();
            if (com == null)
            {
                MessageBox.Show("Seleccioná un comercio.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accion = com.Activo ? "dar de baja" : "reactivar";
            var result = MessageBox.Show(
                $"¿Está seguro que desea {accion} el comercio \"{com.Nombre}\"?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _bll.CambiarEstado(com.IdComercio, !com.Activo);
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSucursales_Click(object sender, EventArgs e)
        {
            var com = ObtenerSeleccionado();
            if (com == null)
            {
                MessageBox.Show("Seleccioná un comercio.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new frmSucursales(com.IdComercio, com.Nombre);
            frm.ShowDialog();
        }

        private void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}