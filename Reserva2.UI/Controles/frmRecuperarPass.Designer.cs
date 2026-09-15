namespace Reserva2.UI.Controles
{
    partial class frmRecuperarPass
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
            txtEmail = new TextBox();
            btnEnviar = new Button();
            btnCerrar = new Button();
            txtNuevaPass = new TextBox();
            label3 = new Label();
            txtConfirmarPass = new TextBox();
            label4 = new Label();
            lblEstado = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Pink;
            label1.Location = new Point(24, 24);
            label1.Name = "label1";
            label1.Size = new Size(208, 15);
            label1.TabIndex = 0;
            label1.Text = "Ingresá tu email y la nueva contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;
            label2.Location = new Point(24, 55);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 1;
            label2.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(24, 82);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(335, 23);
            txtEmail.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(201, 313);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(76, 27);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.RosyBrown;
            btnCerrar.ForeColor = Color.Firebrick;
            btnCerrar.Location = new Point(283, 313);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(76, 27);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            // 
            // txtNuevaPass
            // 
            txtNuevaPass.Location = new Point(24, 149);
            txtNuevaPass.Name = "txtNuevaPass";
            txtNuevaPass.Size = new Size(335, 23);
            txtNuevaPass.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            label3.Location = new Point(24, 122);
            label3.Name = "label3";
            label3.Size = new Size(102, 15);
            label3.TabIndex = 6;
            label3.Text = "Nueva contraseña";
            // 
            // txtConfirmarPass
            // 
            txtConfirmarPass.Location = new Point(24, 219);
            txtConfirmarPass.Name = "txtConfirmarPass";
            txtConfirmarPass.Size = new Size(335, 23);
            txtConfirmarPass.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.ForeColor = Color.White;
            label4.Location = new Point(24, 192);
            label4.Name = "label4";
            label4.Size = new Size(105, 15);
            label4.TabIndex = 8;
            label4.Text = "Repetir contraseña";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.BackColor = Color.Transparent;
            lblEstado.ForeColor = Color.Lime;
            lblEstado.Location = new Point(24, 265);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(272, 15);
            lblEstado.TabIndex = 10;
            lblEstado.Text = "Listo. Revisa tu casilla, el link vence en 30 minutos.";
            lblEstado.Click += label5_Click;
            // 
            // frmRecuperarPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(380, 371);
            Controls.Add(lblEstado);
            Controls.Add(txtConfirmarPass);
            Controls.Add(label4);
            Controls.Add(txtNuevaPass);
            Controls.Add(label3);
            Controls.Add(btnCerrar);
            Controls.Add(btnEnviar);
            Controls.Add(txtEmail);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmRecuperarPass";
            Text = "frmRecuperarPass";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtEmail;
        private Button btnEnviar;
        private Button btnCerrar;
        private TextBox txtNuevaPass;
        private Label label3;
        private TextBox txtConfirmarPass;
        private Label label4;
        private Label lblEstado;
    }
}