namespace Reserva2.UI.Sucursales
{
    partial class frmSucursalABM
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
            btnGuardar = new Button();
            chkActivo = new CheckBox();
            txtUbicacion = new TextBox();
            txtNombre = new TextBox();
            label2 = new Label();
            label1 = new Label();
            lblComercio = new Label();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(261, 171);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.ForeColor = Color.Black;
            btnGuardar.Location = new Point(172, 171);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(88, 136);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(59, 19);
            chkActivo.TabIndex = 11;
            chkActivo.Text = "Activa";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtUbicacion
            // 
            txtUbicacion.Location = new Point(88, 91);
            txtUbicacion.Name = "txtUbicacion";
            txtUbicacion.Size = new Size(248, 23);
            txtUbicacion.TabIndex = 10;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(88, 50);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(214, 23);
            txtNombre.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(18, 91);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 8;
            label2.Text = "Ubicación";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 53);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 7;
            label1.Text = "Nombre *";
            // 
            // lblComercio
            // 
            lblComercio.AutoSize = true;
            lblComercio.ForeColor = Color.White;
            lblComercio.Location = new Point(18, 18);
            lblComercio.Name = "lblComercio";
            lblComercio.Size = new Size(192, 15);
            lblComercio.TabIndex = 14;
            lblComercio.Text = "Comercio: Complejo Deportivo Sur";
            // 
            // frmSucursalABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(353, 207);
            Controls.Add(lblComercio);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(chkActivo);
            Controls.Add(txtUbicacion);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmSucursalABM";
            Text = "frmSucursalABM";
            //Load += frmSucursalABM_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnGuardar;
        private CheckBox chkActivo;
        private TextBox txtUbicacion;
        private TextBox txtNombre;
        private Label label2;
        private Label label1;
        private Label lblComercio;
    }
}