namespace Reserva2.UI.Config
{
    partial class frmBackup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtRutaBackup = new TextBox();
            btnExaminar = new Button();
            btnCrearBackup = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnRestaurar = new Button();
            label2 = new Label();
            btnExaminarRestore = new Button();
            txtRutaStore = new TextBox();
            label3 = new Label();
            dgvBackups = new DataGridView();
            progressBar1 = new ProgressBar();
            lblEstado = new Label();
            Fecha = new DataGridViewTextBoxColumn();
            BaseDeDatos = new DataGridViewTextBoxColumn();
            Archivo = new DataGridViewTextBoxColumn();
            Tamaño = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBackups).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 29);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Ruta de destino";
            label1.Click += label1_Click;
            // 
            // txtRutaBackup
            // 
            txtRutaBackup.Location = new Point(6, 47);
            txtRutaBackup.Name = "txtRutaBackup";
            txtRutaBackup.Size = new Size(491, 23);
            txtRutaBackup.TabIndex = 1;
            // 
            // btnExaminar
            // 
            btnExaminar.ForeColor = Color.Black;
            btnExaminar.Location = new Point(503, 46);
            btnExaminar.Name = "btnExaminar";
            btnExaminar.Size = new Size(75, 23);
            btnExaminar.TabIndex = 2;
            btnExaminar.Text = "Examinar...";
            btnExaminar.UseVisualStyleBackColor = true;
            // 
            // btnCrearBackup
            // 
            btnCrearBackup.ForeColor = Color.Black;
            btnCrearBackup.Location = new Point(648, 45);
            btnCrearBackup.Name = "btnCrearBackup";
            btnCrearBackup.Size = new Size(95, 24);
            btnCrearBackup.TabIndex = 3;
            btnCrearBackup.Text = "Crear backup";
            btnCrearBackup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCrearBackup);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnExaminar);
            groupBox1.Controls.Add(txtRutaBackup);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(770, 100);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Backup";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnRestaurar);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnExaminarRestore);
            groupBox2.Controls.Add(txtRutaStore);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(12, 136);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(770, 100);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Restaurar";
            // 
            // btnRestaurar
            // 
            btnRestaurar.ForeColor = Color.Black;
            btnRestaurar.Location = new Point(648, 48);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(95, 24);
            btnRestaurar.TabIndex = 3;
            btnRestaurar.Text = "Restaurar";
            btnRestaurar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 32);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 0;
            label2.Text = "Archivo de backup";
            // 
            // btnExaminarRestore
            // 
            btnExaminarRestore.ForeColor = Color.Black;
            btnExaminarRestore.Location = new Point(503, 49);
            btnExaminarRestore.Name = "btnExaminarRestore";
            btnExaminarRestore.Size = new Size(75, 23);
            btnExaminarRestore.TabIndex = 2;
            btnExaminarRestore.Text = "Examinar...";
            btnExaminarRestore.UseVisualStyleBackColor = true;
            // 
            // txtRutaStore
            // 
            txtRutaStore.Location = new Point(6, 50);
            txtRutaStore.Name = "txtRutaStore";
            txtRutaStore.Size = new Size(491, 23);
            txtRutaStore.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 258);
            label3.Name = "label3";
            label3.Size = new Size(105, 15);
            label3.TabIndex = 6;
            label3.Text = "Backups existentes";
            // 
            // dgvBackups
            // 
            dgvBackups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBackups.Columns.AddRange(new DataGridViewColumn[] { Fecha, BaseDeDatos, Archivo, Tamaño });
            dgvBackups.Location = new Point(12, 286);
            dgvBackups.Name = "dgvBackups";
            dgvBackups.Size = new Size(770, 166);
            dgvBackups.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = Color.Lime;
            progressBar1.Location = new Point(12, 458);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(598, 23);
            progressBar1.TabIndex = 8;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.White;
            lblEstado.Location = new Point(616, 466);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(146, 15);
            lblEstado.TabIndex = 9;
            lblEstado.Text = "Backup en progreso... 62%";
            // 
            // Fecha
            // 
            Fecha.HeaderText = "Fecha";
            Fecha.Name = "Fecha";
            Fecha.Width = 140;
            // 
            // BaseDeDatos
            // 
            BaseDeDatos.HeaderText = "Base de datos";
            BaseDeDatos.Name = "BaseDeDatos";
            BaseDeDatos.Width = 127;
            // 
            // Archivo
            // 
            Archivo.HeaderText = "Archivo";
            Archivo.Name = "Archivo";
            Archivo.Width = 360;
            // 
            // Tamaño
            // 
            Tamaño.HeaderText = "Tamaño";
            Tamaño.Name = "Tamaño";
            // 
            // frmBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(800, 493);
            Controls.Add(lblEstado);
            Controls.Add(progressBar1);
            Controls.Add(dgvBackups);
            Controls.Add(label3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmBackup";
            Text = "frmBackup";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBackups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtRutaBackup;
        private Button btnExaminar;
        private Button btnCrearBackup;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnRestaurar;
        private Label label2;
        private Button btnExaminarRestore;
        private TextBox txtRutaStore;
        private Label label3;
        private DataGridView dgvBackups;
        private ProgressBar progressBar1;
        private Label lblEstado;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn BaseDeDatos;
        private DataGridViewTextBoxColumn Archivo;
        private DataGridViewTextBoxColumn Tamaño;
    }
}