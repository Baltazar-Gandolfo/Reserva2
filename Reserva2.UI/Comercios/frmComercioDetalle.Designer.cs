namespace Reserva2.UI.Comercios
{
    partial class frmComercioDetalle
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
            tabDetalle = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtRazonSocial = new TextBox();
            txtRubro = new TextBox();
            txtDireccion = new TextBox();
            txtOwner = new TextBox();
            checkBox1 = new CheckBox();
            label6 = new Label();
            label7 = new Label();
            txtCUIT = new TextBox();
            txtTelefono = new TextBox();
            groupBox1 = new GroupBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            lblRecaudado = new Label();
            lblReservas = new Label();
            lblOcupacion = new Label();
            lblCanceladas = new Label();
            btnCerrar = new Button();
            btnBaja = new Button();
            tabDetalle.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabDetalle
            // 
            tabDetalle.Controls.Add(tabPage1);
            tabDetalle.Controls.Add(tabPage2);
            tabDetalle.Controls.Add(tabPage3);
            tabDetalle.Location = new Point(12, 12);
            tabDetalle.Name = "tabDetalle";
            tabDetalle.SelectedIndex = 0;
            tabDetalle.Size = new Size(700, 292);
            tabDetalle.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.MediumSlateBlue;
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(txtTelefono);
            tabPage1.Controls.Add(txtCUIT);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Controls.Add(txtOwner);
            tabPage1.Controls.Add(txtDireccion);
            tabPage1.Controls.Add(txtRubro);
            tabPage1.Controls.Add(txtRazonSocial);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(692, 264);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Datos";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(692, 264);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Elementos";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(692, 264);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Estadísticas";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(16, 15);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 0;
            label1.Text = "Razón Social";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(16, 44);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 1;
            label2.Text = "Rubro";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(16, 73);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 2;
            label3.Text = "Dirección";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(16, 102);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 3;
            label4.Text = "Owner";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(16, 131);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 4;
            label5.Text = "Estado";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.ForeColor = Color.White;
            txtRazonSocial.Location = new Point(93, 12);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(246, 23);
            txtRazonSocial.TabIndex = 5;
            // 
            // txtRubro
            // 
            txtRubro.ForeColor = Color.White;
            txtRubro.Location = new Point(93, 41);
            txtRubro.Name = "txtRubro";
            txtRubro.Size = new Size(246, 23);
            txtRubro.TabIndex = 6;
            // 
            // txtDireccion
            // 
            txtDireccion.ForeColor = Color.White;
            txtDireccion.Location = new Point(93, 70);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(573, 23);
            txtDireccion.TabIndex = 7;
            // 
            // txtOwner
            // 
            txtOwner.ForeColor = Color.White;
            txtOwner.Location = new Point(93, 99);
            txtOwner.Name = "txtOwner";
            txtOwner.Size = new Size(573, 23);
            txtOwner.TabIndex = 8;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(93, 131);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(60, 19);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(345, 15);
            label6.Name = "label6";
            label6.Size = new Size(33, 15);
            label6.TabIndex = 10;
            label6.Text = "CUIT";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(345, 44);
            label7.Name = "label7";
            label7.Size = new Size(53, 15);
            label7.TabIndex = 11;
            label7.Text = "Teléfono";
            // 
            // txtCUIT
            // 
            txtCUIT.ForeColor = Color.White;
            txtCUIT.Location = new Point(420, 12);
            txtCUIT.Name = "txtCUIT";
            txtCUIT.Size = new Size(246, 23);
            txtCUIT.TabIndex = 12;
            // 
            // txtTelefono
            // 
            txtTelefono.ForeColor = Color.White;
            txtTelefono.Location = new Point(420, 41);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(246, 23);
            txtTelefono.TabIndex = 13;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblCanceladas);
            groupBox1.Controls.Add(lblOcupacion);
            groupBox1.Controls.Add(lblReservas);
            groupBox1.Controls.Add(lblRecaudado);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(16, 156);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(650, 89);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Resumen del mes";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 28);
            label8.Name = "label8";
            label8.Size = new Size(66, 15);
            label8.TabIndex = 1;
            label8.Text = "Recaudado";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(172, 28);
            label9.Name = "label9";
            label9.Size = new Size(52, 15);
            label9.TabIndex = 2;
            label9.Text = "Reservas";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(329, 28);
            label10.Name = "label10";
            label10.Size = new Size(100, 15);
            label10.TabIndex = 3;
            label10.Text = "Ocupación prom.";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(482, 28);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 4;
            label11.Text = "Canceladas";
            // 
            // lblRecaudado
            // 
            lblRecaudado.AutoSize = true;
            lblRecaudado.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRecaudado.Location = new Point(6, 43);
            lblRecaudado.Name = "lblRecaudado";
            lblRecaudado.Size = new Size(82, 21);
            lblRecaudado.TabIndex = 1;
            lblRecaudado.Text = "$1000000";
            // 
            // lblReservas
            // 
            lblReservas.AutoSize = true;
            lblReservas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblReservas.Location = new Point(172, 44);
            lblReservas.Name = "lblReservas";
            lblReservas.Size = new Size(37, 21);
            lblReservas.TabIndex = 5;
            lblReservas.Text = "146";
            lblReservas.Click += label13_Click;
            // 
            // lblOcupacion
            // 
            lblOcupacion.AutoSize = true;
            lblOcupacion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOcupacion.Location = new Point(329, 43);
            lblOcupacion.Name = "lblOcupacion";
            lblOcupacion.Size = new Size(42, 21);
            lblOcupacion.TabIndex = 6;
            lblOcupacion.Text = "80%";
            // 
            // lblCanceladas
            // 
            lblCanceladas.AutoSize = true;
            lblCanceladas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCanceladas.Location = new Point(482, 43);
            lblCanceladas.Name = "lblCanceladas";
            lblCanceladas.Size = new Size(28, 21);
            lblCanceladas.TabIndex = 7;
            lblCanceladas.Text = "11";
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(636, 310);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnBaja
            // 
            btnBaja.BackColor = Color.RosyBrown;
            btnBaja.ForeColor = Color.Firebrick;
            btnBaja.Location = new Point(555, 310);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(75, 24);
            btnBaja.TabIndex = 7;
            btnBaja.Text = "Dar de baja";
            btnBaja.UseVisualStyleBackColor = false;
            // 
            // frmComercioDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(723, 344);
            Controls.Add(btnBaja);
            Controls.Add(btnCerrar);
            Controls.Add(tabDetalle);
            Name = "frmComercioDetalle";
            Text = "frmComercioDetalle";
            tabDetalle.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabDetalle;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtOwner;
        private TextBox txtDireccion;
        private TextBox txtRubro;
        private TextBox txtRazonSocial;
        private Label label5;
        private GroupBox groupBox1;
        private Label lblCanceladas;
        private Label lblOcupacion;
        private Label lblReservas;
        private Label lblRecaudado;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private TextBox txtTelefono;
        private TextBox txtCUIT;
        private Label label7;
        private Label label6;
        private CheckBox checkBox1;
        private Button btnCerrar;
        private Button btnBaja;
    }
}