using Services.Facade;
using System;
using System.IO;
using System.Windows.Forms;
using static Reserva2.UI.I18n.TraducirHelper;

namespace Reserva2.UI.Config
{
    /// <summary>
    /// Formulario de backup y restauración de bases de datos SQL Server.
    /// </summary>
    public partial class frmBackup : Form
    {
        /// <summary>Nombres de las bases de datos a respaldar.</summary>
        private string[] basesDeDatos = { "Reserva2_Servicio", "Reserva2_Negocio" };

        /// <summary>Inicializa el formulario y conecta los eventos de los botones.</summary>
        public frmBackup()
        {
            InitializeComponent();
            this.Load += frmBackup_Load;
            btnExaminar.Click += btnExaminar_Click;
            btnCrearBackup.Click += btnCrearBackup_Click;
            btnExaminarRestore.Click += btnExaminarRestore_Click;
            btnRestaurar.Click += btnRestaurar_Click;
        }

        /// <summary>Establece la ruta por defecto y carga los backups existentes al abrir el formulario.</summary>
        private void frmBackup_Load(object sender, EventArgs e)
        {
            TraducirControles(this);
            string carpetaServidor = null;
            try { carpetaServidor = BackupService.ObtenerCarpetaPorDefecto(); }
            catch { }
            txtRutaBackup.Text = !string.IsNullOrWhiteSpace(carpetaServidor)
                ? carpetaServidor
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reserva2_Backups");
            lblEstado.Text = "";
            progressBar1.Value = 0;
            CargarBackupsExistentes();
        }

        /// <summary>Abre el diálogo para seleccionar la carpeta de destino del backup.</summary>
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccioná la carpeta de destino";
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtRutaBackup.Text = fbd.SelectedPath;
            }
        }

        /// <summary>Crea un backup de ambas bases de datos en la carpeta seleccionada.</summary>
        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaBackup.Text))
            {
                MessageBox.Show(T("Seleccioná una ruta de destino."), T("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show(T("Backup de ambas bases creado correctamente."), T("OK"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarBackupsExistentes();
            }
            catch (Exception ex)
            {
                progressBar1.Value = 0;
                lblEstado.Text = "Error";
                ErrorService.Registrar(ex, "frmBackup");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Abre el diálogo para seleccionar el archivo .bak a restaurar.</summary>
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

        /// <summary>Restaura una base de datos desde el archivo .bak seleccionado, previa confirmación.</summary>
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaStore.Text))
            {
                MessageBox.Show(T("Seleccioná un archivo .bak"), T("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    MessageBox.Show(T("Base restaurada correctamente.") + "\n" + T("Se recomienda reiniciar la aplicación."),
                        T("OK"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    progressBar1.Value = 0;
                    lblEstado.Text = "Error";
                    ErrorService.Registrar(ex, "frmBackup");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>Lista los archivos .bak de la carpeta configurada en la grilla de backups.</summary>
        private void CargarBackupsExistentes()
        {
            dgvBackups.Rows.Clear();
            string carpeta = txtRutaBackup.Text.Trim();

            string[] archivos;
            try
            {
                if (!Directory.Exists(carpeta)) return;
                archivos = Directory.GetFiles(carpeta, "*.bak");
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
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

        private void btnCrearBackup_Click_1(object sender, EventArgs e)
        {

        }

        private void txtRutaBackup_TextChanged(object sender, EventArgs e)
        {

        }
    }
}