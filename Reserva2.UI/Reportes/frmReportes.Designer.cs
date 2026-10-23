namespace Reserva2.UI.Reportes
{
    partial class frmReportes
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
            lblComercio = new Label();
            label2 = new Label();
            label1 = new Label();
            btnGenerar = new Button();
            cboComercio = new ComboBox();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            lblSucursal = new Label();
            cboSucursal = new ComboBox();
            pnlIngresos = new Panel();
            pnlOcupacion = new Panel();
            pnlCancelacion = new Panel();
            pnlRecurrentes = new Panel();
            lblPeriodoAnterior = new Label();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            Elemento = new DataGridViewTextBoxColumn();
            Sucursal = new DataGridViewTextBoxColumn();
            Reservas = new DataGridViewTextBoxColumn();
            Ocupacion = new DataGridViewTextBoxColumn();
            Ingresos = new DataGridViewTextBoxColumn();
            Cancelacion = new DataGridViewTextBoxColumn();
            label4 = new Label();
            lblIngresosValor = new Label();
            lblIngresosVar = new Label();
            lblOcupacionVar = new Label();
            lblOcupacionValor = new Label();
            label9 = new Label();
            lblCancelacionVar = new Label();
            lblCancelacionValor = new Label();
            label12 = new Label();
            lblRecurrentesVar = new Label();
            lblRecurrentesValor = new Label();
            label15 = new Label();
            pnlIngresos.SuspendLayout();
            pnlOcupacion.SuspendLayout();
            pnlCancelacion.SuspendLayout();
            pnlRecurrentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(695, 33);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(75, 23);
            btnExportar.TabIndex = 15;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // lblComercio
            // 
            lblComercio.AutoSize = true;
            lblComercio.ForeColor = Color.White;
            lblComercio.Location = new Point(12, 8);
            lblComercio.Name = "lblComercio";
            lblComercio.Size = new Size(131, 15);
            lblComercio.TabIndex = 14;
            lblComercio.Text = "Comercio (solo Admin)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(389, 9);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 13;
            label2.Text = "Hasta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(265, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 12;
            label1.Text = "Desde";
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(522, 33);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(75, 23);
            btnGenerar.TabIndex = 11;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            // 
            // cboComercio
            // 
            cboComercio.FormattingEnabled = true;
            cboComercio.Location = new Point(12, 34);
            cboComercio.Name = "cboComercio";
            cboComercio.Size = new Size(121, 23);
            cboComercio.TabIndex = 10;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(389, 33);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(118, 23);
            dtpHasta.TabIndex = 9;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(266, 33);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(118, 23);
            dtpDesde.TabIndex = 8;
            // 
            // lblSucursal
            // 
            lblSucursal.AutoSize = true;
            lblSucursal.ForeColor = Color.White;
            lblSucursal.Location = new Point(139, 8);
            lblSucursal.Name = "lblSucursal";
            lblSucursal.Size = new Size(51, 15);
            lblSucursal.TabIndex = 17;
            lblSucursal.Text = "Sucursal";
            // 
            // cboSucursal
            // 
            cboSucursal.FormattingEnabled = true;
            cboSucursal.Location = new Point(139, 34);
            cboSucursal.Name = "cboSucursal";
            cboSucursal.Size = new Size(121, 23);
            cboSucursal.TabIndex = 16;
            // 
            // pnlIngresos
            // 
            pnlIngresos.Controls.Add(lblIngresosVar);
            pnlIngresos.Controls.Add(lblIngresosValor);
            pnlIngresos.Controls.Add(label4);
            pnlIngresos.Location = new Point(12, 79);
            pnlIngresos.Name = "pnlIngresos";
            pnlIngresos.Size = new Size(178, 100);
            pnlIngresos.TabIndex = 18;
            // 
            // pnlOcupacion
            // 
            pnlOcupacion.Controls.Add(lblOcupacionVar);
            pnlOcupacion.Controls.Add(lblOcupacionValor);
            pnlOcupacion.Controls.Add(label9);
            pnlOcupacion.Location = new Point(205, 79);
            pnlOcupacion.Name = "pnlOcupacion";
            pnlOcupacion.Size = new Size(178, 100);
            pnlOcupacion.TabIndex = 19;
            // 
            // pnlCancelacion
            // 
            pnlCancelacion.Controls.Add(lblCancelacionVar);
            pnlCancelacion.Controls.Add(lblCancelacionValor);
            pnlCancelacion.Controls.Add(label12);
            pnlCancelacion.Location = new Point(399, 79);
            pnlCancelacion.Name = "pnlCancelacion";
            pnlCancelacion.Size = new Size(178, 100);
            pnlCancelacion.TabIndex = 19;
            // 
            // pnlRecurrentes
            // 
            pnlRecurrentes.Controls.Add(lblRecurrentesVar);
            pnlRecurrentes.Controls.Add(lblRecurrentesValor);
            pnlRecurrentes.Controls.Add(label15);
            pnlRecurrentes.Location = new Point(592, 79);
            pnlRecurrentes.Name = "pnlRecurrentes";
            pnlRecurrentes.Size = new Size(178, 100);
            pnlRecurrentes.TabIndex = 20;
            // 
            // lblPeriodoAnterior
            // 
            lblPeriodoAnterior.AutoSize = true;
            lblPeriodoAnterior.ForeColor = Color.White;
            lblPeriodoAnterior.Location = new Point(12, 193);
            lblPeriodoAnterior.Name = "lblPeriodoAnterior";
            lblPeriodoAnterior.Size = new Size(380, 15);
            lblPeriodoAnterior.TabIndex = 21;
            lblPeriodoAnterior.Text = "Comparado contra 01/09/2026 — 23/09/2026 (misma cantidad de días)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 234);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 22;
            label3.Text = "Detalle por elemento";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Elemento, Sucursal, Reservas, Ocupacion, Ingresos, Cancelacion });
            dataGridView1.Location = new Point(12, 262);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(766, 165);
            dataGridView1.TabIndex = 23;
            // 
            // Elemento
            // 
            Elemento.HeaderText = "Elemento";
            Elemento.Name = "Elemento";
            Elemento.Width = 200;
            // 
            // Sucursal
            // 
            Sucursal.HeaderText = "Sucursal";
            Sucursal.Name = "Sucursal";
            Sucursal.Width = 150;
            // 
            // Reservas
            // 
            Reservas.HeaderText = "Reservas";
            Reservas.Name = "Reservas";
            Reservas.Width = 80;
            // 
            // Ocupacion
            // 
            Ocupacion.HeaderText = "Ocupación";
            Ocupacion.Name = "Ocupacion";
            Ocupacion.Width = 98;
            // 
            // Ingresos
            // 
            Ingresos.HeaderText = "Ingresos";
            Ingresos.Name = "Ingresos";
            // 
            // Cancelacion
            // 
            Cancelacion.HeaderText = "Cancelac.";
            Cancelacion.Name = "Cancelacion";
            Cancelacion.Width = 95;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(13, 8);
            label4.Name = "label4";
            label4.Size = new Size(103, 15);
            label4.TabIndex = 24;
            label4.Text = "Ingresos cobrados";
            // 
            // lblIngresosValor
            // 
            lblIngresosValor.AutoSize = true;
            lblIngresosValor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIngresosValor.ForeColor = Color.White;
            lblIngresosValor.Location = new Point(13, 36);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Size = new Size(78, 32);
            lblIngresosValor.TabIndex = 25;
            lblIngresosValor.Text = "label5";
            // 
            // lblIngresosVar
            // 
            lblIngresosVar.AutoSize = true;
            lblIngresosVar.ForeColor = Color.White;
            lblIngresosVar.Location = new Point(13, 79);
            lblIngresosVar.Name = "lblIngresosVar";
            lblIngresosVar.Size = new Size(38, 15);
            lblIngresosVar.TabIndex = 26;
            lblIngresosVar.Text = "label6";
            // 
            // lblOcupacionVar
            // 
            lblOcupacionVar.AutoSize = true;
            lblOcupacionVar.ForeColor = Color.White;
            lblOcupacionVar.Location = new Point(17, 77);
            lblOcupacionVar.Name = "lblOcupacionVar";
            lblOcupacionVar.Size = new Size(38, 15);
            lblOcupacionVar.TabIndex = 29;
            lblOcupacionVar.Text = "label7";
            // 
            // lblOcupacionValor
            // 
            lblOcupacionValor.AutoSize = true;
            lblOcupacionValor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOcupacionValor.ForeColor = Color.White;
            lblOcupacionValor.Location = new Point(17, 34);
            lblOcupacionValor.Name = "lblOcupacionValor";
            lblOcupacionValor.Size = new Size(78, 32);
            lblOcupacionValor.TabIndex = 28;
            lblOcupacionValor.Text = "label8";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(17, 8);
            label9.Name = "label9";
            label9.Size = new Size(118, 15);
            label9.TabIndex = 27;
            label9.Text = "Ocupación de turnos";
            // 
            // lblCancelacionVar
            // 
            lblCancelacionVar.AutoSize = true;
            lblCancelacionVar.ForeColor = Color.White;
            lblCancelacionVar.Location = new Point(21, 77);
            lblCancelacionVar.Name = "lblCancelacionVar";
            lblCancelacionVar.Size = new Size(44, 15);
            lblCancelacionVar.TabIndex = 29;
            lblCancelacionVar.Text = "label10";
            // 
            // lblCancelacionValor
            // 
            lblCancelacionValor.AutoSize = true;
            lblCancelacionValor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCancelacionValor.ForeColor = Color.White;
            lblCancelacionValor.Location = new Point(17, 34);
            lblCancelacionValor.Name = "lblCancelacionValor";
            lblCancelacionValor.Size = new Size(91, 32);
            lblCancelacionValor.TabIndex = 28;
            lblCancelacionValor.Text = "label11";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(21, 8);
            label12.Name = "label12";
            label12.Size = new Size(144, 15);
            label12.TabIndex = 27;
            label12.Text = "Cancelaciones y expiradas";
            // 
            // lblRecurrentesVar
            // 
            lblRecurrentesVar.AutoSize = true;
            lblRecurrentesVar.ForeColor = Color.White;
            lblRecurrentesVar.Location = new Point(24, 77);
            lblRecurrentesVar.Name = "lblRecurrentesVar";
            lblRecurrentesVar.Size = new Size(44, 15);
            lblRecurrentesVar.TabIndex = 29;
            lblRecurrentesVar.Text = "label13";
            // 
            // lblRecurrentesValor
            // 
            lblRecurrentesValor.AutoSize = true;
            lblRecurrentesValor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecurrentesValor.ForeColor = Color.White;
            lblRecurrentesValor.Location = new Point(24, 34);
            lblRecurrentesValor.Name = "lblRecurrentesValor";
            lblRecurrentesValor.Size = new Size(91, 32);
            lblRecurrentesValor.TabIndex = 28;
            lblRecurrentesValor.Text = "label14";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(24, 8);
            label15.Name = "label15";
            label15.Size = new Size(111, 15);
            label15.TabIndex = 27;
            label15.Text = "Clientes recurrentes";
            // 
            // frmReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(797, 450);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(lblPeriodoAnterior);
            Controls.Add(pnlRecurrentes);
            Controls.Add(pnlCancelacion);
            Controls.Add(pnlOcupacion);
            Controls.Add(pnlIngresos);
            Controls.Add(lblSucursal);
            Controls.Add(cboSucursal);
            Controls.Add(btnExportar);
            Controls.Add(lblComercio);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGenerar);
            Controls.Add(cboComercio);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Name = "frmReportes";
            Text = "frmReportes";
            Load += frmReportes_Load;
            pnlIngresos.ResumeLayout(false);
            pnlIngresos.PerformLayout();
            pnlOcupacion.ResumeLayout(false);
            pnlOcupacion.PerformLayout();
            pnlCancelacion.ResumeLayout(false);
            pnlCancelacion.PerformLayout();
            pnlRecurrentes.ResumeLayout(false);
            pnlRecurrentes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExportar;
        private Label lblComercio;
        private Label label2;
        private Label label1;
        private Button btnGenerar;
        private ComboBox cboComercio;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
        private Label lblSucursal;
        private ComboBox cboSucursal;
        private Panel pnlIngresos;
        private Panel pnlOcupacion;
        private Panel pnlCancelacion;
        private Panel pnlRecurrentes;
        private Label lblPeriodoAnterior;
        private Label label3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Elemento;
        private DataGridViewTextBoxColumn Sucursal;
        private DataGridViewTextBoxColumn Reservas;
        private DataGridViewTextBoxColumn Ocupacion;
        private DataGridViewTextBoxColumn Ingresos;
        private DataGridViewTextBoxColumn Cancelacion;
        private Label label4;
        private Label lblIngresosValor;
        private Label lblIngresosVar;
        private Label lblOcupacionVar;
        private Label lblOcupacionValor;
        private Label label9;
        private Label lblCancelacionVar;
        private Label lblCancelacionValor;
        private Label label12;
        private Label lblRecurrentesVar;
        private Label lblRecurrentesValor;
        private Label label15;
    }
}