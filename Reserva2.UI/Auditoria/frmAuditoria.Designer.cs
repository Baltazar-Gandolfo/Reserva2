namespace Reserva2.UI.Auditoria
{
    partial class frmAuditoria
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
            label5 = new Label();
            dtpDesde = new DateTimePicker();
            cboUsuario = new ComboBox();
            label4 = new Label();
            label1 = new Label();
            dtpHasta = new DateTimePicker();
            cboAccion = new ComboBox();
            label2 = new Label();
            btnFiltrar = new Button();
            btnExportar = new Button();
            dgvAuditoria = new DataGridView();
            FechaHora = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Accion = new DataGridViewTextBoxColumn();
            Entidad = new DataGridViewTextBoxColumn();
            Detalle = new DataGridViewTextBoxColumn();
            cmoEntidad = new ComboBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 42;
            label5.Text = "Desde";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(12, 27);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(119, 23);
            dtpDesde.TabIndex = 41;
            // 
            // cboUsuario
            // 
            cboUsuario.ForeColor = Color.White;
            cboUsuario.FormattingEnabled = true;
            cboUsuario.Location = new Point(289, 27);
            cboUsuario.Name = "cboUsuario";
            cboUsuario.Size = new Size(141, 23);
            cboUsuario.TabIndex = 40;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(289, 9);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 39;
            label4.Text = "Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(151, 9);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 44;
            label1.Text = "Hasta";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(151, 27);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(119, 23);
            dtpHasta.TabIndex = 43;
            // 
            // cboAccion
            // 
            cboAccion.ForeColor = Color.White;
            cboAccion.FormattingEnabled = true;
            cboAccion.Location = new Point(451, 27);
            cboAccion.Name = "cboAccion";
            cboAccion.Size = new Size(141, 23);
            cboAccion.TabIndex = 46;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(451, 9);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 45;
            label2.Text = "Acción";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(813, 29);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 47;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(793, 309);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(95, 23);
            btnExportar.TabIndex = 48;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // dgvAuditoria
            // 
            dgvAuditoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditoria.Columns.AddRange(new DataGridViewColumn[] { FechaHora, Usuario, Accion, Entidad, Detalle });
            dgvAuditoria.Location = new Point(12, 68);
            dgvAuditoria.Name = "dgvAuditoria";
            dgvAuditoria.Size = new Size(876, 235);
            dgvAuditoria.TabIndex = 49;
            // 
            // FechaHora
            // 
            FechaHora.HeaderText = "Fecha y Hora";
            FechaHora.Name = "FechaHora";
            FechaHora.Width = 140;
            // 
            // Usuario
            // 
            Usuario.HeaderText = "Usuario";
            Usuario.Name = "Usuario";
            Usuario.Width = 210;
            // 
            // Accion
            // 
            Accion.HeaderText = "Acción";
            Accion.Name = "Accion";
            Accion.Width = 153;
            // 
            // Entidad
            // 
            Entidad.HeaderText = "Entidad";
            Entidad.Name = "Entidad";
            Entidad.Width = 120;
            // 
            // Detalle
            // 
            Detalle.HeaderText = "Detalle";
            Detalle.Name = "Detalle";
            Detalle.Width = 210;
            // 
            // cmoEntidad
            // 
            cmoEntidad.ForeColor = Color.White;
            cmoEntidad.FormattingEnabled = true;
            cmoEntidad.Location = new Point(612, 27);
            cmoEntidad.Name = "cmoEntidad";
            cmoEntidad.Size = new Size(141, 23);
            cmoEntidad.TabIndex = 51;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(612, 9);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 50;
            label3.Text = "Entidad";
            // 
            // frmAuditoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(903, 340);
            Controls.Add(cmoEntidad);
            Controls.Add(label3);
            Controls.Add(dgvAuditoria);
            Controls.Add(btnExportar);
            Controls.Add(btnFiltrar);
            Controls.Add(cboAccion);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpHasta);
            Controls.Add(label5);
            Controls.Add(dtpDesde);
            Controls.Add(cboUsuario);
            Controls.Add(label4);
            Name = "frmAuditoria";
            Text = "frmAuditoria";
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private DateTimePicker dtpDesde;
        private ComboBox cboUsuario;
        private Label label4;
        private Label label1;
        private DateTimePicker dtpHasta;
        private ComboBox cboAccion;
        private Label label2;
        private Button btnFiltrar;
        private Button btnExportar;
        private DataGridView dgvAuditoria;
        private DataGridViewTextBoxColumn FechaHora;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Accion;
        private DataGridViewTextBoxColumn Entidad;
        private DataGridViewTextBoxColumn Detalle;
        private ComboBox cmoEntidad;
        private Label label3;
    }
}