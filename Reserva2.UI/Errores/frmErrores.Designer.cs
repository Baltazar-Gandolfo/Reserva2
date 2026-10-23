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
            dataGridView1 = new DataGridView();
            cboNivel = new ComboBox();
            FechaHora = new DataGridViewTextBoxColumn();
            Nivel = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Pantalla = new DataGridViewTextBoxColumn();
            Mensaje = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(794, 53);
            btnExportar.Margin = new Padding(3, 4, 3, 4);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(110, 31);
            btnExportar.TabIndex = 21;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(158, 21);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 20;
            label2.Text = "Hasta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(16, 21);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 19;
            label1.Text = "Desde";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(482, 49);
            btnFiltrar.Margin = new Padding(3, 4, 3, 4);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(86, 31);
            btnFiltrar.TabIndex = 18;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(158, 53);
            dtpHasta.Margin = new Padding(3, 4, 3, 4);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(134, 27);
            dtpHasta.TabIndex = 17;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(17, 53);
            dtpDesde.Margin = new Padding(3, 4, 3, 4);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(134, 27);
            dtpDesde.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(306, 21);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 22;
            label3.Text = "Nivel";
            // 
            // btnCopiar
            // 
            btnCopiar.Location = new Point(16, 519);
            btnCopiar.Margin = new Padding(3, 4, 3, 4);
            btnCopiar.Name = "btnCopiar";
            btnCopiar.Size = new Size(161, 31);
            btnCopiar.TabIndex = 23;
            btnCopiar.Text = "Copiar detalle";
            btnCopiar.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FechaHora, Nivel, Usuario, Pantalla, Mensaje });
            dataGridView1.Location = new Point(17, 100);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(887, 201);
            dataGridView1.TabIndex = 24;
            // 
            // cboNivel
            // 
            cboNivel.FormattingEnabled = true;
            cboNivel.Location = new Point(306, 51);
            cboNivel.Name = "cboNivel";
            cboNivel.Size = new Size(151, 28);
            cboNivel.TabIndex = 25;
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
            // textBox1
            // 
            textBox1.Location = new Point(16, 345);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(888, 167);
            textBox1.TabIndex = 26;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(17, 322);
            label4.Name = "label4";
            label4.Size = new Size(139, 20);
            label4.TabIndex = 27;
            label4.Text = "Detalle / StrakTrace";
            // 
            // frmErrores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(916, 563);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(cboNivel);
            Controls.Add(dataGridView1);
            Controls.Add(btnCopiar);
            Controls.Add(label3);
            Controls.Add(btnExportar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnFiltrar);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Name = "frmErrores";
            Text = "Errores del sistema";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn FechaHora;
        private DataGridViewTextBoxColumn Nivel;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Pantalla;
        private DataGridViewTextBoxColumn Mensaje;
        private ComboBox cboNivel;
        private TextBox textBox1;
        private Label label4;
    }
}