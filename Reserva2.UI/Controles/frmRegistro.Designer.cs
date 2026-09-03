namespace Reserva2.UI.Controles
{
    partial class frmRegistro
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtDni = new TextBox();
            txtTelefono = new TextBox();
            txtEmail = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            txtPass = new TextBox();
            txtPass2 = new TextBox();
            grpRol = new GroupBox();
            rdbCliente = new RadioButton();
            rdbOwner = new RadioButton();
            label8 = new Label();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            grpRol.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(49, 40);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(49, 123);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 1;
            label2.Text = "DNI *";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(49, 203);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 2;
            label3.Text = "Email *";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(49, 288);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 3;
            label4.Text = "Contraseña *";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Transparent;
            label5.Location = new Point(316, 40);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 4;
            label5.Text = "Apellido *";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.DarkSlateBlue;
            label6.Cursor = Cursors.Cross;
            label6.ForeColor = Color.Transparent;
            label6.Location = new Point(316, 123);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 5;
            label6.Text = "Teléfono";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Transparent;
            label7.Location = new Point(316, 288);
            label7.Name = "label7";
            label7.Size = new Size(69, 15);
            label7.TabIndex = 6;
            label7.Text = "Confirmar *";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(49, 70);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(235, 23);
            txtNombre.TabIndex = 7;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(316, 70);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(235, 23);
            txtApellido.TabIndex = 8;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(49, 151);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(235, 23);
            txtDni.TabIndex = 9;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(316, 151);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(235, 23);
            txtTelefono.TabIndex = 10;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(49, 232);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(468, 23);
            txtEmail.TabIndex = 11;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(49, 318);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(235, 23);
            txtPass.TabIndex = 12;
            // 
            // txtPass2
            // 
            txtPass2.Location = new Point(316, 318);
            txtPass2.Name = "txtPass2";
            txtPass2.Size = new Size(235, 23);
            txtPass2.TabIndex = 13;
            // 
            // grpRol
            // 
            grpRol.Controls.Add(label8);
            grpRol.Controls.Add(rdbOwner);
            grpRol.Controls.Add(rdbCliente);
            grpRol.ForeColor = Color.White;
            grpRol.Location = new Point(49, 369);
            grpRol.Name = "grpRol";
            grpRol.Size = new Size(502, 90);
            grpRol.TabIndex = 14;
            grpRol.TabStop = false;
            grpRol.Text = "Tipo de cuenta";
            // 
            // rdbCliente
            // 
            rdbCliente.AutoSize = true;
            rdbCliente.Location = new Point(17, 31);
            rdbCliente.Name = "rdbCliente";
            rdbCliente.Size = new Size(62, 19);
            rdbCliente.TabIndex = 0;
            rdbCliente.TabStop = true;
            rdbCliente.Text = "Cliente";
            rdbCliente.UseVisualStyleBackColor = true;
            // 
            // rdbOwner
            // 
            rdbOwner.AutoSize = true;
            rdbOwner.Location = new Point(180, 31);
            rdbOwner.Name = "rdbOwner";
            rdbOwner.Size = new Size(174, 19);
            rdbOwner.TabIndex = 1;
            rdbOwner.TabStop = true;
            rdbOwner.Text = "Owner (dueño de comercio)";
            rdbOwner.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 62);
            label8.Name = "label8";
            label8.Size = new Size(351, 15);
            label8.TabIndex = 2;
            label8.Text = "Los perfiles de Admin se crean desde la base, no por autoregistro.";
            // 
            // button1
            // 
            button1.Location = new Point(313, 476);
            button1.Name = "button1";
            button1.Size = new Size(134, 29);
            button1.TabIndex = 15;
            button1.Text = "Crear cuenta";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackColor = Color.RosyBrown;
            button2.Cursor = Cursors.No;
            button2.ForeColor = Color.Firebrick;
            button2.Location = new Point(453, 476);
            button2.Margin = new Padding(0, 0, 0, 0);
            button2.Name = "button2";
            button2.Size = new Size(98, 29);
            button2.TabIndex = 16;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = false;
            // 
            // frmRegistro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(603, 517);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(grpRol);
            Controls.Add(txtPass2);
            Controls.Add(txtPass);
            Controls.Add(txtEmail);
            Controls.Add(txtTelefono);
            Controls.Add(txtDni);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmRegistro";
            Text = "frmRegistro";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            grpRol.ResumeLayout(false);
            grpRol.PerformLayout();
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
        private TextBox txtApellido;
        private TextBox txtDni;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private ErrorProvider errorProvider1;
        private TextBox txtPass;
        private Button button2;
        private Button button1;
        private GroupBox grpRol;
        private Label label8;
        private RadioButton rdbOwner;
        private RadioButton rdbCliente;
        private TextBox txtPass2;
    }
}