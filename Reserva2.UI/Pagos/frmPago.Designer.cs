namespace Reserva2.UI.Pagos
{
    partial class frmPago
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
            btnCancelar = new Button();
            btnRegistrar = new Button();
            chkImprimir = new CheckBox();
            txtObservaciones = new TextBox();
            numMonto = new NumericUpDown();
            cmbTipo = new ComboBox();
            txtComprobante = new TextBox();
            label7 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblReserva = new Label();
            cmbMetodo = new ComboBox();
            label4 = new Label();
            dtpFecha = new DateTimePicker();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)numMonto).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(395, 390);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(76, 27);
            btnCancelar.TabIndex = 33;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(313, 390);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(76, 27);
            btnRegistrar.TabIndex = 32;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // chkImprimir
            // 
            chkImprimir.AutoSize = true;
            chkImprimir.ForeColor = Color.White;
            chkImprimir.Location = new Point(158, 370);
            chkImprimir.Name = "chkImprimir";
            chkImprimir.Size = new Size(108, 19);
            chkImprimir.TabIndex = 31;
            chkImprimir.Text = "Imprimir recibo";
            chkImprimir.UseVisualStyleBackColor = true;
            // 
            // txtObservaciones
            // 
            txtObservaciones.ForeColor = Color.White;
            txtObservaciones.Location = new Point(158, 310);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(313, 54);
            txtObservaciones.TabIndex = 30;
            // 
            // numMonto
            // 
            numMonto.ForeColor = Color.White;
            numMonto.Location = new Point(158, 104);
            numMonto.Name = "numMonto";
            numMonto.Size = new Size(119, 23);
            numMonto.TabIndex = 26;
            // 
            // cmbTipo
            // 
            cmbTipo.ForeColor = Color.White;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(158, 56);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(313, 23);
            cmbTipo.TabIndex = 25;
            // 
            // txtComprobante
            // 
            txtComprobante.ForeColor = Color.White;
            txtComprobante.Location = new Point(158, 248);
            txtComprobante.Name = "txtComprobante";
            txtComprobante.Size = new Size(313, 23);
            txtComprobante.TabIndex = 24;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(29, 313);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 23;
            label7.Text = "Observaciones";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(29, 106);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 19;
            label3.Text = "Monto *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(29, 59);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 18;
            label2.Text = "Tipo *";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 251);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 17;
            label1.Text = "N° comprobante";
            // 
            // lblReserva
            // 
            lblReserva.AutoSize = true;
            lblReserva.BackColor = Color.Plum;
            lblReserva.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReserva.Location = new Point(29, 21);
            lblReserva.Name = "lblReserva";
            lblReserva.Size = new Size(439, 21);
            lblReserva.TabIndex = 34;
            lblReserva.Text = "RES-00284 - Fútbol -- Cancha A - 03/08 18:00 - saldo $12600";
            // 
            // cmbMetodo
            // 
            cmbMetodo.ForeColor = Color.White;
            cmbMetodo.FormattingEnabled = true;
            cmbMetodo.Location = new Point(158, 153);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(313, 23);
            cmbMetodo.TabIndex = 36;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(29, 156);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 35;
            label4.Text = "Método *";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(158, 200);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(119, 23);
            dtpFecha.TabIndex = 37;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(29, 206);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 38;
            label5.Text = "Fecha";
            // 
            // frmPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(490, 435);
            Controls.Add(label5);
            Controls.Add(dtpFecha);
            Controls.Add(cmbMetodo);
            Controls.Add(label4);
            Controls.Add(lblReserva);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrar);
            Controls.Add(chkImprimir);
            Controls.Add(txtObservaciones);
            Controls.Add(numMonto);
            Controls.Add(cmbTipo);
            Controls.Add(txtComprobante);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmPago";
            Text = "frmPago";
            Load += frmPago_Load;
            ((System.ComponentModel.ISupportInitialize)numMonto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnRegistrar;
        private CheckBox chkImprimir;
        private TextBox txtObservaciones;
        private NumericUpDown numMonto;
        private ComboBox cmbTipo;
        private TextBox txtComprobante;
        private Label label7;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblReserva;
        private ComboBox cmbMetodo;
        private Label label4;
        private DateTimePicker dtpFecha;
        private Label label5;
    }
}