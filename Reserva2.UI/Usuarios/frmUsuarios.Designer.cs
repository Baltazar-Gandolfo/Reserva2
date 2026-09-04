namespace Reserva2.UI.Usuarios
{
    partial class frmUsuarios
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
            txtBuscar = new TextBox();
            cmbRol = new ComboBox();
            chkSoloActivos = new CheckBox();
            btnExportar = new Button();
            dgvUsuarios = new DataGridView();
            label3 = new Label();
            Nombre = new DataGridViewTextBoxColumn();
            DNI = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            Rol = new DataGridViewTextBoxColumn();
            Activo = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Buscar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(231, 9);
            label2.Name = "label2";
            label2.Size = new Size(24, 15);
            label2.TabIndex = 1;
            label2.Text = "Rol";
            // 
            // txtBuscar
            // 
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Location = new Point(12, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(201, 23);
            txtBuscar.TabIndex = 2;
            // 
            // cmbRol
            // 
            cmbRol.ForeColor = Color.White;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(231, 27);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(121, 23);
            cmbRol.TabIndex = 3;
            // 
            // chkSoloActivos
            // 
            chkSoloActivos.AutoSize = true;
            chkSoloActivos.ForeColor = Color.White;
            chkSoloActivos.Location = new Point(367, 29);
            chkSoloActivos.Name = "chkSoloActivos";
            chkSoloActivos.Size = new Size(89, 19);
            chkSoloActivos.TabIndex = 4;
            chkSoloActivos.Text = "Solo activos";
            chkSoloActivos.UseVisualStyleBackColor = true;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(636, 25);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(75, 23);
            btnExportar.TabIndex = 5;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { Nombre, DNI, Email, Rol, Activo });
            dgvUsuarios.Location = new Point(12, 67);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(698, 150);
            dgvUsuarios.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 229);
            label3.Name = "label3";
            label3.Size = new Size(148, 15);
            label3.TabIndex = 11;
            label3.Text = "312 usuarios - 4 mostrados";
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            Nombre.Width = 180;
            // 
            // DNI
            // 
            DNI.HeaderText = "DNI";
            DNI.Name = "DNI";
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.Name = "Email";
            Email.Width = 175;
            // 
            // Rol
            // 
            Rol.HeaderText = "Rol";
            Rol.Name = "Rol";
            // 
            // Activo
            // 
            Activo.HeaderText = "Activo";
            Activo.Name = "Activo";
            Activo.Resizable = DataGridViewTriState.True;
            Activo.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // frmUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(722, 253);
            Controls.Add(label3);
            Controls.Add(dgvUsuarios);
            Controls.Add(btnExportar);
            Controls.Add(chkSoloActivos);
            Controls.Add(cmbRol);
            Controls.Add(txtBuscar);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmUsuarios";
            Text = "frmUsuarios";
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtBuscar;
        private ComboBox cmbRol;
        private CheckBox chkSoloActivos;
        private Button btnExportar;
        private DataGridView dgvUsuarios;
        private Label label3;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn DNI;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Rol;
        private DataGridViewCheckBoxColumn Activo;
    }
}