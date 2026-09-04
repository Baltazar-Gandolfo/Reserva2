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
            lblEstado = new Label();
            btnEnviar = new Button();
            btnCerrar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Pink;
            label1.Location = new Point(24, 24);
            label1.Name = "label1";
            label1.Size = new Size(301, 15);
            label1.TabIndex = 0;
            label1.Text = "Ingresá tu email y te enviamos un link para restablecerla";
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
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.SpringGreen;
            lblEstado.Location = new Point(28, 121);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(272, 15);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Listo. Revisá tu casilla, el link vence en 30 minutos.";
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(199, 157);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(76, 27);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.RosyBrown;
            btnCerrar.ForeColor = Color.Firebrick;
            btnCerrar.Location = new Point(281, 157);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(76, 27);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            // 
            // frmRecuperarPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(386, 196);
            Controls.Add(btnCerrar);
            Controls.Add(btnEnviar);
            Controls.Add(lblEstado);
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
        private Label lblEstado;
        private Button btnEnviar;
        private Button btnCerrar;
    }
}