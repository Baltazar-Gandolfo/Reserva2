using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Services.Facade;
using System;
using System.Collections.Generic;
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
            btnBuscar.Click += btnBuscar_Click;
            btnAlta.Click += btnAlta_Click;
            btnVer.Click += btnVer_Click;
            btnBaja.Click += btnBaja_Click;
        }

        private void frmComercios_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Todos");
            comboBox1.Items.Add("Activo");
            comboBox1.Items.Add("Inactivo");
            comboBox1.SelectedIndex = 0;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
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

            var filtrados = _comercios.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                filtrados = filtrados.Where(c => c.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()));

            string filtroEstado = comboBox1.SelectedItem?.ToString();
            if (filtroEstado == "Activo")
                filtrados = filtrados.Where(c => c.Activo);
            else if (filtroEstado == "Inactivo")
                filtrados = filtrados.Where(c => !c.Activo);

            foreach (var c in filtrados)
            {
                dataGridView1.Rows.Add(
                    c.Nombre,
                    c.Ubicacion ?? "",
                    "",
                    "",
                    c.Activo ? "Activo" : "Inactivo"
                );
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = c;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmComercioDetalle frm = new frmComercioDetalle(null);
            frm.MdiParent = this.MdiParent;
            frm.FormClosed += (s, args) => CargarGrilla();
            frm.Show();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Tag == null)
            {
                MessageBox.Show("Seleccioná un comercio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Comercio seleccionado = (Comercio)dataGridView1.CurrentRow.Tag;
            frmComercioDetalle frm = new frmComercioDetalle(seleccionado);
            frm.MdiParent = this.MdiParent;
            frm.FormClosed += (s, args) => CargarGrilla();
            frm.Show();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Tag == null)
            {
                MessageBox.Show("Seleccioná un comercio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Comercio seleccionado = (Comercio)dataGridView1.CurrentRow.Tag;
            string accion = seleccionado.Activo ? "deshabilitar" : "habilitar";

            var result = MessageBox.Show(
                $"¿{accion.ToUpper()} el comercio '{seleccionado.Nombre}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _bll.CambiarEstado(seleccionado.IdComercio, !seleccionado.Activo);
                    MessageBox.Show($"Comercio {accion}do.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}