namespace Reserva2.UI.Config
{
    partial class frmIntegridadDVH
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
            dgvIntegridadDVH = new DataGridView();
            Tabla = new DataGridViewTextBoxColumn();
            Fila = new DataGridViewTextBoxColumn();
            Falla = new DataGridViewTextBoxColumn();
            btnRestaurar = new Button();
            btnRecalcular = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvIntegridadDVH).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(560, 15);
            label1.TabIndex = 0;
            label1.Text = "Se detectó una inconsistencia en los datos(posible manipulación fuera del sistema). Elija cómo resolverla:";
            // 
            // dgvIntegridadDVH
            // 
            dgvIntegridadDVH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIntegridadDVH.Columns.AddRange(new DataGridViewColumn[] { Tabla, Fila, Falla });
            dgvIntegridadDVH.Location = new Point(12, 52);
            dgvIntegridadDVH.Name = "dgvIntegridadDVH";
            dgvIntegridadDVH.Size = new Size(643, 189);
            dgvIntegridadDVH.TabIndex = 1;
            // 
            // Tabla
            // 
            Tabla.HeaderText = "Tabla";
            Tabla.Name = "Tabla";
            // 
            // Fila
            // 
            Fila.HeaderText = "Fila(clave primaria)";
            Fila.Name = "Fila";
            Fila.Width = 400;
            // 
            // Falla
            // 
            Falla.HeaderText = "Campos con falla";
            Falla.Name = "Falla";
            // 
            // btnRestaurar
            // 
            btnRestaurar.Location = new Point(12, 247);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(202, 37);
            btnRestaurar.TabIndex = 2;
            btnRestaurar.Text = "Restaurar desde Backup";
            btnRestaurar.UseVisualStyleBackColor = true;
            // 
            // btnRecalcular
            // 
            btnRecalcular.Location = new Point(229, 247);
            btnRecalcular.Name = "btnRecalcular";
            btnRecalcular.Size = new Size(202, 37);
            btnRecalcular.TabIndex = 3;
            btnRecalcular.Text = "Recalcular dígitos verificadores";
            btnRecalcular.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.RosyBrown;
            btnCancelar.ForeColor = Color.Red;
            btnCancelar.Location = new Point(557, 247);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(98, 37);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // frmIntegridadDVH
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(671, 292);
            Controls.Add(btnCancelar);
            Controls.Add(btnRecalcular);
            Controls.Add(btnRestaurar);
            Controls.Add(dgvIntegridadDVH);
            Controls.Add(label1);
            Name = "frmIntegridadDVH";
            Text = "frmIntegridadDVH";
            ((System.ComponentModel.ISupportInitialize)dgvIntegridadDVH).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvIntegridadDVH;
        private DataGridViewTextBoxColumn Tabla;
        private DataGridViewTextBoxColumn Fila;
        private DataGridViewTextBoxColumn Falla;
        private Button btnRestaurar;
        private Button btnRecalcular;
        private Button btnCancelar;
    }
}