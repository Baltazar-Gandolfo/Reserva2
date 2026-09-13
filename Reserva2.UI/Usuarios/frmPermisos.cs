using Services.Facade;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reserva2.UI.Usuarios
{
    public partial class frmPermisos : Form
    {
        private List<Familia> familias;
        private List<Patente> patentes;
        private List<Usuario> usuarios;

        public frmPermisos()
        {
            InitializeComponent();
            this.Load += frmPermisos_Load;
            btnCrearFamilia.Click += btnCrearFamilia_Click;
            btnEditarFamilia.Click += btnEditarFamilia_Click;
            btnEliminarFamilia.Click += btnEliminarFamilia_Click;
            btnCrearPatente.Click += btnCrearPatente_Click;
            btnAsignarFamilia.Click += btnAsignarFamilia_Click;
            btnQuitarFamilia.Click += btnQuitarFamilia_Click;
            treeView1.AfterSelect += treeView1_AfterSelect;
            btnEditarPatente.Click += btnEditarPatente_Click;
            //btnEditarPatente.Click += (s, ev) => throw new Exception("Error de prueba para validar LogError");
            btnEliminarPatente.Click += btnEliminarPatente_Click;
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {
            CargarArbol();
            CargarUsuarios();
        }

        private void CargarArbol()
        {
            treeView1.Nodes.Clear();

            familias = FamiliaService.GetAll();
            patentes = PatenteService.GetAll();

            TreeNode nodeFamilias = new TreeNode("Familias");
            foreach (var familia in familias)
            {
                TreeNode nodeFam = new TreeNode(familia.Nombre);
                nodeFam.Tag = familia;

                foreach (var hijo in familia.GetHijos())
                {
                    if (hijo is Patente pat)
                    {
                        TreeNode nodePat = new TreeNode(pat.DataKey);
                        nodePat.Tag = pat;
                        nodeFam.Nodes.Add(nodePat);
                    }
                    else if (hijo is Familia subFam)
                    {
                        TreeNode nodeSub = new TreeNode(subFam.Nombre);
                        nodeSub.Tag = subFam;
                        nodeFam.Nodes.Add(nodeSub);
                    }
                }

                nodeFamilias.Nodes.Add(nodeFam);
            }

            TreeNode nodePatentes = new TreeNode("Patentes");
            foreach (var patente in patentes)
            {
                TreeNode nodePat = new TreeNode(patente.DataKey);
                nodePat.Tag = patente;
                nodePatentes.Nodes.Add(nodePat);
            }

            treeView1.Nodes.Add(nodeFamilias);
            treeView1.Nodes.Add(nodePatentes);
            treeView1.ExpandAll();
        }

        private void CargarUsuarios()
        {
            usuarios = UsuarioService.GetAll();
            cboUsuarios.DataSource = null;
            cboUsuarios.DataSource = usuarios;
            cboUsuarios.DisplayMember = "Nombre";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Familia fam)
            {
                txtFamiliaNombre.Text = fam.Nombre;
                txtFamiliaDescripcion.Text = "";
            }
            else if (e.Node.Tag is Patente pat)
            {
                textBox2.Text = pat.DataKey;
                textBox1.Text = "";
            }
        }

        private void btnCrearFamilia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFamiliaNombre.Text))
            {
                MessageBox.Show("Ingresá el nombre de la familia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Familia nueva = new Familia();
                nueva.Nombre = txtFamiliaNombre.Text.Trim();
                FamiliaService.Add(nueva);

                // Si hay una patente seleccionada en el árbol, asignarla a la familia
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Patente patSel)
                {
                    FamiliaService.AgregarPatente(patSel, nueva);
                }

                MessageBox.Show("Familia creada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BitacoraService.Registrar("Alta", "Familia", $"Se creó la familia: {nueva.Nombre}");
                txtFamiliaNombre.Clear();
                txtFamiliaDescripcion.Clear();
                CargarArbol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarFamilia_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia fam))
            {
                MessageBox.Show("Seleccioná una familia del árbol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Funcionalidad de edición pendiente de implementar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia fam))
            {
                MessageBox.Show("Seleccioná una familia del árbol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"¿Eliminar la familia '{fam.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    FamiliaService.Delete(fam.Id);
                    MessageBox.Show("Familia eliminada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BitacoraService.Registrar("Baja", "Familia", $"Se eliminó la familia: {fam.Nombre}");
                    CargarArbol();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCrearPatente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Ingresá la clave de la patente (ej: frmNuevaPantalla).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Patente nueva = new Patente();
                nueva.DataKey = textBox2.Text.Trim();
                PatenteService.Add(nueva);

                MessageBox.Show("Patente creada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BitacoraService.Registrar("Alta", "Patente", $"Se creó la patente: {nueva.DataKey}");
                textBox2.Clear();
                textBox1.Clear();
                CargarArbol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarFamilia_Click(object sender, EventArgs e)
        {
            if (cboUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia fam))
            {
                MessageBox.Show("Seleccioná una familia del árbol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Usuario usuario = (Usuario)cboUsuarios.SelectedItem;
                UsuarioService.AgregarFamilia(fam, usuario);
                MessageBox.Show($"Familia '{fam.Nombre}' asignada a {usuario.Nombre}.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BitacoraService.Registrar("Asignación", "Familia", $"Familia '{fam.Nombre}' asignada a {usuario.Nombre}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarFamilia_Click(object sender, EventArgs e)
        {
            if (cboUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia fam))
            {
                MessageBox.Show("Seleccioná una familia del árbol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Usuario usuario = (Usuario)cboUsuarios.SelectedItem;
                UsuarioService.QuitarFamilia(fam, usuario);
                MessageBox.Show($"Familia '{fam.Nombre}' quitada de {usuario.Nombre}.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BitacoraService.Registrar("Baja", "Familia", $"Familia '{fam.Nombre}' quitada de {usuario.Nombre}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearFamilia_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEditarPatente_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad pendiente de implementar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarPatente_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad pendiente de implementar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}