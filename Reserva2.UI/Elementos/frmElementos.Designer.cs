namespace Reserva2.UI.Elementos
{
    partial class frmElementos
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
            btnEditar = new Button();
            btnDisponibilidad = new Button();
            dgvElementos = new DataGridView();
            chkVerInactivos = new CheckBox();
            btnBaja = new Button();
            btnNuevo = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            lblComercio = new Label();
            lblSucursal = new Label();
            Nombre = new DataGridViewTextBoxColumn();
            Sucursal = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            Capacidad = new DataGridViewTextBoxColumn();
            Precio_hora = new DataGridViewTextBoxColumn();
            Seña = new DataGridViewTextBoxColumn();
            Activo = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvElementos).BeginInit();
            SuspendLayout();
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(97, 68);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(79, 28);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnDisponibilidad
            // 
            btnDisponibilidad.Location = new Point(267, 68);
            btnDisponibilidad.Name = "btnDisponibilidad";
            btnDisponibilidad.Size = new Size(112, 28);
            btnDisponibilidad.TabIndex = 3;
            btnDisponibilidad.Text = "Disponibilidad...";
            btnDisponibilidad.UseVisualStyleBackColor = true;
            // 
            // dgvElementos
            // 
            dgvElementos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvElementos.Columns.AddRange(new DataGridViewColumn[] { Nombre, Sucursal, Tipo, Capacidad, Precio_hora, Seña, Activo });
            dgvElementos.Location = new Point(12, 102);
            dgvElementos.Name = "dgvElementos";
            dgvElementos.Size = new Size(885, 349);
            dgvElementos.TabIndex = 4;
            // 
            // chkVerInactivos
            // 
            chkVerInactivos.AutoSize = true;
            chkVerInactivos.ForeColor = Color.White;
            chkVerInactivos.Location = new Point(805, 73);
            chkVerInactivos.Name = "chkVerInactivos";
            chkVerInactivos.Size = new Size(92, 19);
            chkVerInactivos.TabIndex = 5;
            chkVerInactivos.Text = "Ver inactivos";
            chkVerInactivos.UseVisualStyleBackColor = true;
            // 
            // btnBaja
            // 
            btnBaja.BackColor = Color.RosyBrown;
            btnBaja.ForeColor = Color.Firebrick;
            btnBaja.Location = new Point(182, 67);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(79, 29);
            btnBaja.TabIndex = 8;
            btnBaja.Text = "Dar de baja";
            btnBaja.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.LightGreen;
            btnNuevo.ForeColor = Color.ForestGreen;
            btnNuevo.Location = new Point(12, 68);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(79, 29);
            btnNuevo.TabIndex = 7;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(887, 15);
            label1.TabIndex = 9;
            label1.Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(202, 23);
            comboBox1.TabIndex = 10;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(238, 23);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(202, 23);
            comboBox2.TabIndex = 11;
            // 
            // lblComercio
            // 
            lblComercio.AutoSize = true;
            lblComercio.ForeColor = Color.White;
            lblComercio.Location = new Point(12, 5);
            lblComercio.Name = "lblComercio";
            lblComercio.Size = new Size(59, 15);
            lblComercio.TabIndex = 12;
            lblComercio.Text = "Comercio";
            // 
            // lblSucursal
            // 
            lblSucursal.AutoSize = true;
            lblSucursal.ForeColor = Color.White;
            lblSucursal.Location = new Point(238, 5);
            lblSucursal.Name = "lblSucursal";
            lblSucursal.Size = new Size(51, 15);
            lblSucursal.TabIndex = 13;
            lblSucursal.Text = "Sucursal";
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            Nombre.Width = 182;
            // 
            // Sucursal
            // 
            Sucursal.HeaderText = "Sucursal";
            Sucursal.Name = "Sucursal";
            Sucursal.Width = 140;
            // 
            // Tipo
            // 
            Tipo.HeaderText = "Tipo";
            Tipo.Name = "Tipo";
            Tipo.Width = 120;
            // 
            // Capacidad
            // 
            Capacidad.HeaderText = "Capacidad";
            Capacidad.Name = "Capacidad";
            // 
            // Precio_hora
            // 
            Precio_hora.HeaderText = "Precio/hora";
            Precio_hora.Name = "Precio_hora";
            // 
            // Seña
            // 
            Seña.HeaderText = "Seña";
            Seña.Name = "Seña";
            // 
            // Activo
            // 
            Activo.HeaderText = "Activo";
            Activo.Name = "Activo";
            // 
            // frmElementos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(909, 463);
            Controls.Add(lblSucursal);
            Controls.Add(lblComercio);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(btnBaja);
            Controls.Add(btnNuevo);
            Controls.Add(chkVerInactivos);
            Controls.Add(dgvElementos);
            Controls.Add(btnDisponibilidad);
            Controls.Add(btnEditar);
            Name = "frmElementos";
            Text = "frmElementos";
            ((System.ComponentModel.ISupportInitialize)dgvElementos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnEditar;
        private Button btnDisponibilidad;
        private DataGridView dgvElementos;
        private CheckBox chkVerInactivos;
        private Button btnBaja;
        private Button btnNuevo;
        private Label label1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Sucursal;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn Capacidad;
        private DataGridViewTextBoxColumn Precio_hora;
        private DataGridViewTextBoxColumn Seña;
        private DataGridViewTextBoxColumn Activo;
        private Label lblComercio;
        private Label lblSucursal;
    }
}