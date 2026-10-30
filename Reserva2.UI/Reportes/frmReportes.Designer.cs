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
            lblCompIngresos = new Label();
            lblValorIngresos = new Label();
            lblTituloIngresos = new Label();
            pnlOcupacion = new Panel();
            lblCompOcupacion = new Label();
            lblValorOcupacion = new Label();
            lblTituloOcupacion = new Label();
            pnlCancelacion = new Panel();
            lblCompCancelacion = new Label();
            lblValorCancelacion = new Label();
            lblTituloCancelacion = new Label();
            pnlRecurrentes = new Panel();
            lblCompRecurrentes = new Label();
            lblValorRecurrentes = new Label();
            lblTituloRecurrentes = new Label();
            lblPeriodoAnterior = new Label();
            label3 = new Label();
            dgvDetalle = new DataGridView();
            Elemento = new DataGridViewTextBoxColumn();
            Sucursal = new DataGridViewTextBoxColumn();
            Reservas = new DataGridViewTextBoxColumn();
            Ocupacion = new DataGridViewTextBoxColumn();
            Ingresos = new DataGridViewTextBoxColumn();
            Cancelacion = new DataGridViewTextBoxColumn();
            pnlIngresos.SuspendLayout();
            pnlOcupacion.SuspendLayout();
            pnlCancelacion.SuspendLayout();
            pnlRecurrentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
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
            pnlIngresos.Controls.Add(lblCompIngresos);
            pnlIngresos.Controls.Add(lblValorIngresos);
            pnlIngresos.Controls.Add(lblTituloIngresos);
            pnlIngresos.Location = new Point(12, 79);
            pnlIngresos.Name = "pnlIngresos";
            pnlIngresos.Size = new Size(178, 100);
            pnlIngresos.TabIndex = 18;
            // 
            // lblCompIngresos
            // 
            lblCompIngresos.AutoSize = true;
            lblCompIngresos.ForeColor = Color.White;
            lblCompIngresos.Location = new Point(13, 79);
            lblCompIngresos.Name = "lblCompIngresos";
            lblCompIngresos.Size = new Size(38, 15);
            lblCompIngresos.TabIndex = 26;
            lblCompIngresos.Text = "label6";
            // 
            // lblValorIngresos
            // 
            lblValorIngresos.AutoSize = true;
            lblValorIngresos.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValorIngresos.ForeColor = Color.White;
            lblValorIngresos.Location = new Point(13, 36);
            lblValorIngresos.Name = "lblValorIngresos";
            lblValorIngresos.Size = new Size(78, 32);
            lblValorIngresos.TabIndex = 25;
            lblValorIngresos.Text = "label5";
            // 
            // lblTituloIngresos
            // 
            lblTituloIngresos.AutoSize = true;
            lblTituloIngresos.ForeColor = Color.White;
            lblTituloIngresos.Location = new Point(13, 8);
            lblTituloIngresos.Name = "lblTituloIngresos";
            lblTituloIngresos.Size = new Size(103, 15);
            lblTituloIngresos.TabIndex = 24;
            lblTituloIngresos.Text = "Ingresos cobrados";
            // 
            // pnlOcupacion
            // 
            pnlOcupacion.Controls.Add(lblCompOcupacion);
            pnlOcupacion.Controls.Add(lblValorOcupacion);
            pnlOcupacion.Controls.Add(lblTituloOcupacion);
            pnlOcupacion.Location = new Point(205, 79);
            pnlOcupacion.Name = "pnlOcupacion";
            pnlOcupacion.Size = new Size(178, 100);
            pnlOcupacion.TabIndex = 19;
            // 
            // lblCompOcupacion
            // 
            lblCompOcupacion.AutoSize = true;
            lblCompOcupacion.ForeColor = Color.White;
            lblCompOcupacion.Location = new Point(17, 77);
            lblCompOcupacion.Name = "lblCompOcupacion";
            lblCompOcupacion.Size = new Size(38, 15);
            lblCompOcupacion.TabIndex = 29;
            lblCompOcupacion.Text = "label7";
            // 
            // lblValorOcupacion
            // 
            lblValorOcupacion.AutoSize = true;
            lblValorOcupacion.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValorOcupacion.ForeColor = Color.White;
            lblValorOcupacion.Location = new Point(17, 34);
            lblValorOcupacion.Name = "lblValorOcupacion";
            lblValorOcupacion.Size = new Size(78, 32);
            lblValorOcupacion.TabIndex = 28;
            lblValorOcupacion.Text = "label8";
            // 
            // lblTituloOcupacion
            // 
            lblTituloOcupacion.AutoSize = true;
            lblTituloOcupacion.ForeColor = Color.White;
            lblTituloOcupacion.Location = new Point(17, 8);
            lblTituloOcupacion.Name = "lblTituloOcupacion";
            lblTituloOcupacion.Size = new Size(118, 15);
            lblTituloOcupacion.TabIndex = 27;
            lblTituloOcupacion.Text = "Ocupación de turnos";
            // 
            // pnlCancelacion
            // 
            pnlCancelacion.Controls.Add(lblCompCancelacion);
            pnlCancelacion.Controls.Add(lblValorCancelacion);
            pnlCancelacion.Controls.Add(lblTituloCancelacion);
            pnlCancelacion.Location = new Point(399, 79);
            pnlCancelacion.Name = "pnlCancelacion";
            pnlCancelacion.Size = new Size(178, 100);
            pnlCancelacion.TabIndex = 19;
            // 
            // lblCompCancelacion
            // 
            lblCompCancelacion.AutoSize = true;
            lblCompCancelacion.ForeColor = Color.White;
            lblCompCancelacion.Location = new Point(21, 77);
            lblCompCancelacion.Name = "lblCompCancelacion";
            lblCompCancelacion.Size = new Size(44, 15);
            lblCompCancelacion.TabIndex = 29;
            lblCompCancelacion.Text = "label10";
            // 
            // lblValorCancelacion
            // 
            lblValorCancelacion.AutoSize = true;
            lblValorCancelacion.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValorCancelacion.ForeColor = Color.White;
            lblValorCancelacion.Location = new Point(17, 34);
            lblValorCancelacion.Name = "lblValorCancelacion";
            lblValorCancelacion.Size = new Size(91, 32);
            lblValorCancelacion.TabIndex = 28;
            lblValorCancelacion.Text = "label11";
            // 
            // lblTituloCancelacion
            // 
            lblTituloCancelacion.AutoSize = true;
            lblTituloCancelacion.ForeColor = Color.White;
            lblTituloCancelacion.Location = new Point(21, 8);
            lblTituloCancelacion.Name = "lblTituloCancelacion";
            lblTituloCancelacion.Size = new Size(144, 15);
            lblTituloCancelacion.TabIndex = 27;
            lblTituloCancelacion.Text = "Cancelaciones y expiradas";
            // 
            // pnlRecurrentes
            // 
            pnlRecurrentes.Controls.Add(lblCompRecurrentes);
            pnlRecurrentes.Controls.Add(lblValorRecurrentes);
            pnlRecurrentes.Controls.Add(lblTituloRecurrentes);
            pnlRecurrentes.Location = new Point(592, 79);
            pnlRecurrentes.Name = "pnlRecurrentes";
            pnlRecurrentes.Size = new Size(178, 100);
            pnlRecurrentes.TabIndex = 20;
            // 
            // lblCompRecurrentes
            // 
            lblCompRecurrentes.AutoSize = true;
            lblCompRecurrentes.ForeColor = Color.White;
            lblCompRecurrentes.Location = new Point(24, 77);
            lblCompRecurrentes.Name = "lblCompRecurrentes";
            lblCompRecurrentes.Size = new Size(44, 15);
            lblCompRecurrentes.TabIndex = 29;
            lblCompRecurrentes.Text = "label13";
            // 
            // lblValorRecurrentes
            // 
            lblValorRecurrentes.AutoSize = true;
            lblValorRecurrentes.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValorRecurrentes.ForeColor = Color.White;
            lblValorRecurrentes.Location = new Point(24, 34);
            lblValorRecurrentes.Name = "lblValorRecurrentes";
            lblValorRecurrentes.Size = new Size(91, 32);
            lblValorRecurrentes.TabIndex = 28;
            lblValorRecurrentes.Text = "label14";
            // 
            // lblTituloRecurrentes
            // 
            lblTituloRecurrentes.AutoSize = true;
            lblTituloRecurrentes.ForeColor = Color.White;
            lblTituloRecurrentes.Location = new Point(24, 8);
            lblTituloRecurrentes.Name = "lblTituloRecurrentes";
            lblTituloRecurrentes.Size = new Size(111, 15);
            lblTituloRecurrentes.TabIndex = 27;
            lblTituloRecurrentes.Text = "Clientes recurrentes";
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
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { Elemento, Sucursal, Reservas, Ocupacion, Ingresos, Cancelacion });
            dgvDetalle.Location = new Point(12, 262);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.Size = new Size(766, 165);
            dgvDetalle.TabIndex = 23;
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
            // frmReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(797, 450);
            Controls.Add(dgvDetalle);
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
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
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
        private DataGridView dgvDetalle;
        private DataGridViewTextBoxColumn Elemento;
        private DataGridViewTextBoxColumn Sucursal;
        private DataGridViewTextBoxColumn Reservas;
        private DataGridViewTextBoxColumn Ocupacion;
        private DataGridViewTextBoxColumn Ingresos;
        private DataGridViewTextBoxColumn Cancelacion;
        private Label lblTituloIngresos;
        private Label lblValorIngresos;
        private Label lblCompIngresos;
        private Label lblCompOcupacion;
        private Label lblValorOcupacion;
        private Label lblTituloOcupacion;
        private Label lblCompCancelacion;
        private Label lblValorCancelacion;
        private Label lblTituloCancelacion;
        private Label lblCompRecurrentes;
        private Label lblValorRecurrentes;
        private Label lblTituloRecurrentes;
    }
}