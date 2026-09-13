using Services.Facade;
using System;
using System.IO;
using System.Windows.Forms;

namespace Reserva2.UI.Config
{
    public partial class frmBackup : Form
    {
        private string[] basesDeDatos = { "Reserva2_Servicio", "Reserva2_Negocio" };

        public frmBackup()
        {
            InitializeComponent();
            this.Load += frmBackup_Load;
            btnExaminar.Click += btnExaminar_Click;
            btnCrearBackup.Click += btnCrearBackup_Click;
            btnExaminarRestore.Click += btnExaminarRestore_Click;
            btnRestaurar.Click += btnRestaurar_Click;
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            txtRutaBackup.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reserva2_Backups");
            lblEstado.Text = "";
            progressBar1.Value = 0;
            CargarBackupsExistentes();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccioná la carpeta de destino";
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtRutaBackup.Text = fbd.SelectedPath;
            }
        }

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaBackup.Text))
            {
                MessageBox.Show("Seleccioná una ruta de destino.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string carpeta = txtRutaBackup.Text.Trim();
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                progressBar1.Value = 0;
                lblEstado.Text = "Creando backup...";
                this.Refresh();

                int progreso = 0;
                foreach (string bd in basesDeDatos)
                {
                    string archivo = Path.Combine(carpeta, $"{bd}_{timestamp}.bak");
                    BackupService.CrearBackup(bd, archivo);
                    progreso += 50;
                    progressBar1.Value = progreso;
                    this.Refresh();
                }

                progressBar1.Value = 100;
                lblEstado.Text = "Backup completado";

                BitacoraService.Registrar("Backup", "Sistema", $"Backup creado en: {carpeta}");

                MessageBox.Show("Backup de ambas bases creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarBackupsExistentes();
            }
            catch (Exception ex)
            {
                progressBar1.Value = 0;
                lblEstado.Text = "Error";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExaminarRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Backup files (*.bak)|*.bak";
                ofd.Title = "Seleccioná el archivo de backup";
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtRutaStore.Text = ofd.FileName;
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaStore.Text))
            {
                MessageBox.Show("Seleccioná un archivo .bak", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string archivo = txtRutaStore.Text.Trim();
            string nombreArchivo = Path.GetFileNameWithoutExtension(archivo);

            string bdDetectada = "";
            foreach (string bd in basesDeDatos)
            {
                if (nombreArchivo.StartsWith(bd))
                {
                    bdDetectada = bd;
                    break;
                }
            }

            if (string.IsNullOrEmpty(bdDetectada))
            {
                MessageBox.Show("No se pudo detectar la base de datos del archivo.\nEl nombre debe empezar con 'Reserva2_Servicio' o 'Reserva2_Negocio'.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"¿Restaurar la base '{bdDetectada}' desde este archivo?\n\n{archivo}\n\nEsto reemplazará TODOS los datos actuales.",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    progressBar1.Value = 0;
                    lblEstado.Text = $"Restaurando {bdDetectada}...";
                    this.Refresh();

                    BackupService.Restaurar(bdDetectada, archivo);

                    progressBar1.Value = 100;
                    lblEstado.Text = "Restauración completada";

                    BitacoraService.Registrar("Restauración", "Sistema", $"BD {bdDetectada} restaurada desde: {archivo}");

                    MessageBox.Show("Base restaurada correctamente.\nSe recomienda reiniciar la aplicación.",
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    progressBar1.Value = 0;
                    lblEstado.Text = "Error";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarBackupsExistentes()
        {
            dgvBackups.Rows.Clear();
            string carpeta = txtRutaBackup.Text.Trim();

            if (!Directory.Exists(carpeta)) return;

            string[] archivos = Directory.GetFiles(carpeta, "*.bak");
            foreach (string archivo in archivos)
            {
                FileInfo fi = new FileInfo(archivo);
                string bd = "";
                foreach (string b in basesDeDatos)
                {
                    if (fi.Name.StartsWith(b))
                    {
                        bd = b;
                        break;
                    }
                }

                dgvBackups.Rows.Add(
                    fi.LastWriteTime.ToString("dd/MM/yyyy HH:mm"),
                    bd,
                    fi.Name,
                    $"{fi.Length / 1024} KB"
                );
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}