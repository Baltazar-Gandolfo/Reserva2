namespace Reserva2.UI
{
    partial class frmLogin
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
            txtEmail = new TextBox();
            label4 = new Label();
            txtPassword = new TextBox();
            chkRecordarme = new CheckBox();
            lnkRecuperar = new LinkLabel();
            lblError = new Label();
            btnIngresar = new Button();
            label6 = new Label();
            lknRegistro = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Narrow", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HighlightText;
            label1.Location = new Point(168, 37);
            label1.Name = "label1";
            label1.Size = new Size(111, 31);
            label1.TabIndex = 0;
            label1.Text = "Reserva2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(67, 86);
            label2.Name = "label2";
            label2.Size = new Size(212, 15);
            label2.TabIndex = 1;
            label2.Text = "Ingresá tus credenciales para continuar";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(67, 114);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 2;
            label3.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(67, 143);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(329, 23);
            txtEmail.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(67, 184);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 4;
            label4.Text = "Contraseña";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(67, 212);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(329, 23);
            txtPassword.TabIndex = 5;
            // 
            // chkRecordarme
            // 
            chkRecordarme.AutoSize = true;
            chkRecordarme.ForeColor = Color.Transparent;
            chkRecordarme.Location = new Point(67, 255);
            chkRecordarme.Name = "chkRecordarme";
            chkRecordarme.Size = new Size(90, 19);
            chkRecordarme.TabIndex = 6;
            chkRecordarme.Text = "Recordarme";
            chkRecordarme.UseVisualStyleBackColor = true;
            // 
            // lnkRecuperar
            // 
            lnkRecuperar.AutoSize = true;
            lnkRecuperar.LinkColor = Color.Thistle;
            lnkRecuperar.Location = new Point(255, 256);
            lnkRecuperar.Name = "lnkRecuperar";
            lnkRecuperar.Size = new Size(141, 15);
            lnkRecuperar.TabIndex = 7;
            lnkRecuperar.TabStop = true;
            lnkRecuperar.Text = "¿Olvidaste tu contraseña?";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.BackColor = Color.Transparent;
            lblError.ForeColor = Color.IndianRed;
            lblError.Location = new Point(67, 286);
            lblError.Name = "lblError";
            lblError.Size = new Size(164, 15);
            lblError.TabIndex = 8;
            lblError.Text = "Email o contraseña incorrecto";
            lblError.Visible = false;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(67, 316);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(329, 31);
            btnIngresar.TabIndex = 9;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Transparent;
            label6.Location = new Point(128, 363);
            label6.Name = "label6";
            label6.Size = new Size(103, 15);
            label6.TabIndex = 10;
            label6.Text = "¿No tenés cuenta?";
            // 
            // lknRegistro
            // 
            lknRegistro.AutoSize = true;
            lknRegistro.LinkColor = Color.Thistle;
            lknRegistro.Location = new Point(255, 363);
            lknRegistro.Name = "lknRegistro";
            lknRegistro.Size = new Size(59, 15);
            lknRegistro.TabIndex = 11;
            lknRegistro.TabStop = true;
            lknRegistro.Text = "Registrate";
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(457, 413);
            Controls.Add(lknRegistro);
            Controls.Add(label6);
            Controls.Add(btnIngresar);
            Controls.Add(lblError);
            Controls.Add(lnkRecuperar);
            Controls.Add(chkRecordarme);
            Controls.Add(txtPassword);
            Controls.Add(label4);
            Controls.Add(txtEmail);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmLogin";
            Text = "frmLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtPassword;
        private CheckBox chkRecordarme;
        private LinkLabel lnkRecuperar;
        private Label lblError;
        private Button btnIngresar;
        private Label label6;
        private LinkLabel lknRegistro;
    }
}