namespace Reserva2.UI.Elementos
{
    partial class frmElementoABM
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtNombre = new TextBox();
            cmbTipo = new ComboBox();
            numCapacidad = new NumericUpDown();
            numPrecio = new NumericUpDown();
            numSena = new NumericUpDown();
            cboDuracion = new ComboBox();
            txtObservaciones = new TextBox();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numCapacidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSena).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(21, 15);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(21, 55);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 1;
            label2.Text = "Tipo *";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(21, 98);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 2;
            label3.Text = "Capacidad";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(21, 135);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 3;
            label4.Text = "Precio por hora *";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(21, 175);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 4;
            label5.Text = "Seña(%)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(21, 215);
            label6.Name = "label6";
            label6.Size = new Size(90, 15);
            label6.TabIndex = 5;
            label6.Text = "Duración Turno";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(21, 267);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 6;
            label7.Text = "Observaciones";
            // 
            // txtNombre
            // 
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(150, 12);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(313, 23);
            txtNombre.TabIndex = 7;
            // 
            // cmbTipo
            // 
            cmbTipo.ForeColor = Color.White;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(150, 52);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(313, 23);
            cmbTipo.TabIndex = 8;
            // 
            // numCapacidad
            // 
            numCapacidad.ForeColor = Color.White;
            numCapacidad.Location = new Point(150, 90);
            numCapacidad.Name = "numCapacidad";
            numCapacidad.Size = new Size(86, 23);
            numCapacidad.TabIndex = 9;
            // 
            // numPrecio
            // 
            numPrecio.ForeColor = Color.White;
            numPrecio.Location = new Point(150, 133);
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(119, 23);
            numPrecio.TabIndex = 10;
            // 
            // numSena
            // 
            numSena.ForeColor = Color.White;
            numSena.Location = new Point(150, 173);
            numSena.Name = "numSena";
            numSena.Size = new Size(86, 23);
            numSena.TabIndex = 11;
            // 
            // cboDuracion
            // 
            cboDuracion.ForeColor = Color.White;
            cboDuracion.FormattingEnabled = true;
            cboDuracion.Location = new Point(150, 212);
            cboDuracion.Name = "cboDuracion";
            cboDuracion.Size = new Size(119, 23);
            cboDuracion.TabIndex = 12;
            // 
            // txtObservaciones
            // 
            txtObservaciones.ForeColor = Color.White;
            txtObservaciones.Location = new Point(150, 264);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(313, 54);
            txtObservaciones.TabIndex = 13;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(150, 324);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 14;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(305, 344);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(76, 27);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(387, 344);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(76, 27);
            btnCancelar.TabIndex = 16;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // frmElementoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(475, 383);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(chkActivo);
            Controls.Add(txtObservaciones);
            Controls.Add(cboDuracion);
            Controls.Add(numSena);
            Controls.Add(numPrecio);
            Controls.Add(numCapacidad);
            Controls.Add(cmbTipo);
            Controls.Add(txtNombre);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmElementoABM";
            Text = "frmElementoABM";
            ((System.ComponentModel.ISupportInitialize)numCapacidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSena).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtNombre;
        private ComboBox cmbTipo;
        private NumericUpDown numCapacidad;
        private NumericUpDown numPrecio;
        private NumericUpDown numSena;
        private ComboBox cboDuracion;
        private TextBox txtObservaciones;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}