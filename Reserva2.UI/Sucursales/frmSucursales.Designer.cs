namespace Reserva2.UI.Sucursales
{
    partial class frmSucursales
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
            chkVerInactivos = new CheckBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            btnBaja = new Button();
            btnAlta = new Button();
            btnEditar = new Button();
            Comercio = new DataGridViewTextBoxColumn();
            Ubicacion = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // chkVerInactivos
            // 
            chkVerInactivos.AutoSize = true;
            chkVerInactivos.ForeColor = Color.White;
            chkVerInactivos.Location = new Point(631, 18);
            chkVerInactivos.Name = "chkVerInactivos";
            chkVerInactivos.Size = new Size(91, 19);
            chkVerInactivos.TabIndex = 18;
            chkVerInactivos.Text = "Ver inactivas";
            chkVerInactivos.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(15, 226);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 17;
            label2.Text = "9 sucursales - 1 inactiva";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Comercio, Ubicacion, Estado });
            dataGridView1.Location = new Point(15, 55);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(708, 150);
            dataGridView1.TabIndex = 16;
            // 
            // btnBaja
            // 
            btnBaja.BackColor = Color.RosyBrown;
            btnBaja.ForeColor = Color.Firebrick;
            btnBaja.Location = new Point(177, 13);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(75, 24);
            btnBaja.TabIndex = 15;
            btnBaja.Text = "Dar de baja";
            btnBaja.UseVisualStyleBackColor = false;
            // 
            // btnAlta
            // 
            btnAlta.BackColor = Color.LightGreen;
            btnAlta.ForeColor = Color.ForestGreen;
            btnAlta.Location = new Point(15, 12);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(75, 24);
            btnAlta.TabIndex = 14;
            btnAlta.Text = "Nuevo";
            btnAlta.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(96, 12);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 24);
            btnEditar.TabIndex = 13;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
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
            // frmSucursales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(742, 251);
            Controls.Add(chkVerInactivos);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(btnBaja);
            Controls.Add(btnAlta);
            Controls.Add(btnEditar);
            Name = "frmSucursales";
            Text = "frmSucursales";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkVerInactivos;
        private Label label2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Comercio;
        private DataGridViewTextBoxColumn Ubicacion;
        private DataGridViewTextBoxColumn Estado;
        private Button btnBaja;
        private Button btnAlta;
        private Button btnEditar;
    }
}