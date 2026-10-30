namespace Reserva2.UI.Errores
{
    partial class frmErrores
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
            btnExportar = new Button();
            label2 = new Label();
            label1 = new Label();
            btnFiltrar = new Button();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            label3 = new Label();
            btnCopiar = new Button();
            dgvErrores = new DataGridView();
            FechaHora = new DataGridViewTextBoxColumn();
            Nivel = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Pantalla = new DataGridViewTextBoxColumn();
            Mensaje = new DataGridViewTextBoxColumn();
            cboNivel = new ComboBox();
            txtDetalle = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvErrores).BeginInit();
            SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(695, 40);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(96, 23);
            btnExportar.TabIndex = 21;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(138, 16);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 20;
            label2.Text = "Hasta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(14, 16);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 19;
            label1.Text = "Desde";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(422, 37);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 18;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(138, 40);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(118, 23);
            dtpHasta.TabIndex = 17;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(15, 40);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(118, 23);
            dtpDesde.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(268, 16);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 22;
            label3.Text = "Nivel";
            // 
            // btnCopiar
            // 
            btnCopiar.Location = new Point(14, 389);
            btnCopiar.Name = "btnCopiar";
            btnCopiar.Size = new Size(141, 23);
            btnCopiar.TabIndex = 23;
            btnCopiar.Text = "Copiar detalle";
            btnCopiar.UseVisualStyleBackColor = true;
            // 
            // dgvErrores
            // 
            dgvErrores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvErrores.Columns.AddRange(new DataGridViewColumn[] { FechaHora, Nivel, Usuario, Pantalla, Mensaje });
            dgvErrores.Location = new Point(15, 75);
            dgvErrores.Margin = new Padding(3, 2, 3, 2);
            dgvErrores.Name = "dgvErrores";
            dgvErrores.RowHeadersWidth = 51;
            dgvErrores.Size = new Size(776, 151);
            dgvErrores.TabIndex = 24;
            // 
            // FechaHora
            // 
            FechaHora.HeaderText = "Fecha y Hora";
            FechaHora.MinimumWidth = 6;
            FechaHora.Name = "FechaHora";
            FechaHora.Width = 160;
            // 
            // Nivel
            // 
            Nivel.HeaderText = "Nivel";
            Nivel.MinimumWidth = 6;
            Nivel.Name = "Nivel";
            Nivel.Width = 125;
            // 
            // Usuario
            // 
            Usuario.HeaderText = "Usuario";
            Usuario.MinimumWidth = 6;
            Usuario.Name = "Usuario";
            Usuario.Width = 160;
            // 
            // Pantalla
            // 
            Pantalla.HeaderText = "Pantalla";
            Pantalla.MinimumWidth = 6;
            Pantalla.Name = "Pantalla";
            Pantalla.Width = 160;
            // 
            // Mensaje
            // 
            Mensaje.HeaderText = "Mensaje";
            Mensaje.MinimumWidth = 6;
            Mensaje.Name = "Mensaje";
            Mensaje.Width = 228;
            // 
            // cboNivel
            // 
            cboNivel.FormattingEnabled = true;
            cboNivel.Location = new Point(268, 38);
            cboNivel.Margin = new Padding(3, 2, 3, 2);
            cboNivel.Name = "cboNivel";
            cboNivel.Size = new Size(133, 23);
            cboNivel.TabIndex = 25;
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(14, 259);
            txtDetalle.Margin = new Padding(3, 2, 3, 2);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(778, 126);
            txtDetalle.TabIndex = 26;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(15, 242);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 27;
            label4.Text = "Detalle / StrakTrace";
            // 
            // frmErrores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(802, 422);
            Controls.Add(label4);
            Controls.Add(txtDetalle);
            Controls.Add(cboNivel);
            Controls.Add(dgvErrores);
            Controls.Add(btnCopiar);
            Controls.Add(label3);
            Controls.Add(btnExportar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnFiltrar);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmErrores";
            Text = "Errores del sistema";
            ((System.ComponentModel.ISupportInitialize)dgvErrores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExportar;
        private Label label2;
        private Label label1;
        private Button btnFiltrar;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
        private Label label3;
        private Button btnCopiar;
        private DataGridView dgvErrores;
        private DataGridViewTextBoxColumn FechaHora;
        private DataGridViewTextBoxColumn Nivel;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Pantalla;
        private DataGridViewTextBoxColumn Mensaje;
        private ComboBox cboNivel;
        private TextBox txtDetalle;
        private Label label4;
    }
}