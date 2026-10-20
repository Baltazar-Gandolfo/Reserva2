using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;

namespace Reserva2.UI.Sucursales
{
    public partial class frmSucursales : Form
    {
        private readonly int _idComercio;
        private readonly string _nombreComercio;
        private readonly SucursalBLL _bll = new SucursalBLL();

        public frmSucursales(int idComercio, string nombreComercio)
        {
            InitializeComponent();
            _idComercio = idComercio;
            _nombreComercio = nombreComercio;

            this.Text = $"Sucursales — {_nombreComercio}";

            this.Load += frmSucursales_Load;
            btnAlta.Click += btnAlta_Click;
            btnEditar.Click += btnEditar_Click;
            btnBaja.Click += btnBaja_Click;
            chkVerInactivos.CheckedChanged += chkVerInactivos_CheckedChanged;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void frmSucursales_Load(object sender, EventArgs e)
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

            var lista = _bll.ListarPorComercio(_idComercio);
            bool verInactivos = chkVerInactivos.Checked;

            int total = lista.Count;
            int inactivas = lista.Count(s => !s.Activo);

            var filtrada = verInactivos
                ? lista
                : lista.Where(s => s.Activo).ToList();

            foreach (var suc in filtrada)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    suc.Nombre,
                    suc.Ubicacion ?? "",
                    suc.Activo ? "Activa" : "Inactiva"
                );
                dataGridView1.Rows[rowIndex].Tag = suc;

                if (!suc.Activo)
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            }

            label2.Text = inactivas > 0
                ? $"{total} sucursales · {inactivas} inactiva{(inactivas > 1 ? "s" : "")}"
                : $"{total} sucursales";

            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            bool hay = dataGridView1.CurrentRow != null
                    && dataGridView1.CurrentRow.Tag is Sucursal;
            btnEditar.Enabled = hay;
            btnBaja.Enabled = hay;
        }

        private Sucursal ObtenerSeleccionada()
        {
            if (dataGridView1.CurrentRow?.Tag is Sucursal suc)
                return suc;
            return null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            var frm = new frmSucursalABM(0, _idComercio, _nombreComercio);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var suc = ObtenerSeleccionada();
            if (suc == null) return;

            var frm = new frmSucursalABM(suc.IdSucursal, _idComercio, _nombreComercio);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            var suc = ObtenerSeleccionada();
            if (suc == null) return;

            string accion = suc.Activo ? "dar de baja" : "reactivar";
            var result = MessageBox.Show(
                $"¿Está seguro que desea {accion} la sucursal \"{suc.Nombre}\"?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _bll.CambiarEstado(suc.IdSucursal, !suc.Activo);
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}