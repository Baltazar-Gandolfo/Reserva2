namespace Reserva2.UI.Comercios
{
    partial class frmComercios
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
            txtBuscar = new TextBox();
            comboBox1 = new ComboBox();
            btnBuscar = new Button();
            btnVer = new Button();
            btnAlta = new Button();
            btnBaja = new Button();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            label2 = new Label();
            Comercio = new DataGridViewTextBoxColumn();
            Rubro = new DataGridViewTextBoxColumn();
            Elementos = new DataGridViewTextBoxColumn();
            Recaudado_mes = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(12, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(196, 23);
            txtBuscar.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(214, 27);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(107, 23);
            comboBox1.TabIndex = 2;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(327, 28);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.RightToLeft = RightToLeft.No;
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnVer
            // 
            btnVer.Location = new Point(483, 28);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(75, 24);
            btnVer.TabIndex = 4;
            btnVer.Text = "Ver detalle";
            btnVer.UseVisualStyleBackColor = true;
            // 
            // btnAlta
            // 
            btnAlta.BackColor = Color.LightGreen;
            btnAlta.ForeColor = Color.ForestGreen;
            btnAlta.Location = new Point(564, 28);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(75, 24);
            btnAlta.TabIndex = 5;
            btnAlta.Text = "Nuevo";
            btnAlta.UseVisualStyleBackColor = false;
            // 
            // btnBaja
            // 
            btnBaja.BackColor = Color.RosyBrown;
            btnBaja.ForeColor = Color.Firebrick;
            btnBaja.Location = new Point(645, 28);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(75, 24);
            btnBaja.TabIndex = 6;
            btnBaja.Text = "Dar de baja";
            btnBaja.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Comercio, Rubro, Elementos, Recaudado_mes, Estado });
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(708, 150);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(214, 9);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 9;
            label3.Text = "Estado";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 241);
            label2.Name = "label2";
            label2.Size = new Size(133, 15);
            label2.TabIndex = 10;
            label2.Text = "9 comercios - 1 inactivo";
            // 
            // Comercio
            // 
            Comercio.HeaderText = "Comercio";
            Comercio.Name = "Comercio";
            Comercio.Width = 185;
            // 
            // Rubro
            // 
            Rubro.HeaderText = "Rubro";
            Rubro.Name = "Rubro";
            Rubro.Width = 120;
            // 
            // Elementos
            // 
            Elementos.HeaderText = "Elementos";
            Elementos.Name = "Elementos";
            // 
            // Recaudado_mes
            // 
            Recaudado_mes.HeaderText = "Recaudado mes";
            Recaudado_mes.Name = "Recaudado_mes";
            Recaudado_mes.Width = 160;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            // 
            // frmComercios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(732, 264);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(btnBaja);
            Controls.Add(btnAlta);
            Controls.Add(btnVer);
            Controls.Add(btnBuscar);
            Controls.Add(comboBox1);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            ForeColor = Color.Black;
            Name = "frmComercios";
            Text = "frmComercios";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private ComboBox comboBox1;
        private Button btnBuscar;
        private Button btnVer;
        private Button btnAlta;
        private Button btnBaja;
        private DataGridView dataGridView1;
        private Label label3;
        private Label label2;
        private DataGridViewTextBoxColumn Comercio;
        private DataGridViewTextBoxColumn Rubro;
        private DataGridViewTextBoxColumn Elementos;
        private DataGridViewTextBoxColumn Recaudado_mes;
        private DataGridViewTextBoxColumn Estado;
    }
}