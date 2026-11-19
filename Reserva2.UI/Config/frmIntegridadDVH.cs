using Services.DomainModel;
using Services.Facade;
using System;
using System.Windows.Forms;

namespace Reserva2.UI.Config
{
    public partial class frmIntegridadDVH : Form
    {
        private ResultadoIntegridad _resultado;

        public frmIntegridadDVH(ResultadoIntegridad resultado)
        {
            InitializeComponent();
            _resultado = resultado;
            this.Load += frmIntegridadDVH_Load;
            btnRecalcular.Click += btnRecalcular_Click;
            btnRestaurar.Click += btnRestaurar_Click;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void frmIntegridadDVH_Load(object sender, EventArgs e)
        {
            dgvIntegridadDVH.Rows.Clear();
            foreach (var fila in _resultado.FilasConFalla)
            {
                dgvIntegridadDVH.Rows.Add(fila.Tabla, fila.ClavePrimaria, fila.CamposConFalla);
            }
        }

        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                DigitoVerificadorService.RecalcularTodo();
                BitacoraService.Registrar("Modificación", "Sistema", "Se recalcularon los dígitos verificadores de Usuario");
                MessageBox.Show("Dígitos verificadores recalculados correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmIntegridadDVH");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            var frm = new frmBackup();
            frm.ShowDialog();
            MessageBox.Show("Después de restaurar, reiniciá la aplicación e ingresá de nuevo.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
