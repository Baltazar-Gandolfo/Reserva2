namespace Reserva2.UI.Comercios
{
    partial class frmComercios
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
            btnSucursales = new Button();
            btnEditar = new Button();
            btnAlta = new Button();
            btnBaja = new Button();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            chkVerInactivos = new CheckBox();
            Comercio = new DataGridViewTextBoxColumn();
            Ubicacion = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnSucursales
            // 
            btnSucursales.Location = new Point(301, 29);
            btnSucursales.Name = "btnSucursales";
            btnSucursales.RightToLeft = RightToLeft.No;
            btnSucursales.Size = new Size(93, 23);
            btnSucursales.TabIndex = 3;
            btnSucursales.Text = "Sucursales...";
            btnSucursales.UseVisualStyleBackColor = true;
            //btnSucursales.Click += btnBuscar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(93, 27);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 24);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnAlta
            // 
            btnAlta.BackColor = Color.LightGreen;
            btnAlta.ForeColor = Color.ForestGreen;
            btnAlta.Location = new Point(12, 27);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(75, 24);
            btnAlta.TabIndex = 5;
            btnAlta.Text = "Nuevo";
            btnAlta.UseVisualStyleBackColor = false;
            // 
            // btnBaja
            // 
            btnBaja.BackColor = Color.RosyBrown;
            btnBaja.ForeColor = Color.Firebrick;
            btnBaja.Location = new Point(174, 28);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(75, 24);
            btnBaja.TabIndex = 6;
            btnBaja.Text = "Dar de baja";
            btnBaja.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Comercio, Ubicacion, Estado });
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(708, 150);
            dataGridView1.TabIndex = 7;
            //dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 241);
            label2.Name = "label2";
            label2.Size = new Size(133, 15);
            label2.TabIndex = 10;
            label2.Text = "9 comercios - 1 inactivo";
            // 
            // chkVerInactivos
            // 
            chkVerInactivos.AutoSize = true;
            chkVerInactivos.ForeColor = Color.White;
            chkVerInactivos.Location = new Point(628, 33);
            chkVerInactivos.Name = "chkVerInactivos";
            chkVerInactivos.Size = new Size(92, 19);
            chkVerInactivos.TabIndex = 11;
            chkVerInactivos.Text = "Ver inactivos";
            chkVerInactivos.UseVisualStyleBackColor = true;
            // 
            // Comercio
            // 
            Comercio.HeaderText = "Nombre de Comercio";
            Comercio.Name = "Comercio";
            Comercio.Width = 245;
            // 
            // Ubicacion
            // 
            Ubicacion.HeaderText = "Ubicación";
            Ubicacion.Name = "Ubicacion";
            Ubicacion.Width = 320;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            // 
            // frmComercios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(732, 264);
            Controls.Add(chkVerInactivos);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(btnBaja);
            Controls.Add(btnAlta);
            Controls.Add(btnEditar);
            Controls.Add(btnSucursales);
            ForeColor = Color.Black;
            Name = "frmComercios";
            Text = "frmComercios";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private ComboBox comboBox1;
        private Button btnSucursales;
        private Button btnEditar;
        private Button btnAlta;
        private Button btnBaja;
        private DataGridView dataGridView1;
        private Label label3;
        private Label label2;
        private CheckBox chkVerInactivos;
        private DataGridViewTextBoxColumn Comercio;
        private DataGridViewTextBoxColumn Ubicacion;
        private DataGridViewTextBoxColumn Estado;
    }
}