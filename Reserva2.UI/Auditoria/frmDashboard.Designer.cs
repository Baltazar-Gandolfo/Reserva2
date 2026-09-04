namespace Reserva2.UI.Auditoria
{
    partial class frmDashboard
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
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            lblReservas = new Label();
            lblIngresos = new Label();
            lblOcupacion = new Label();
            lblSeñas = new Label();
            label5 = new Label();
            dataGridView1 = new DataGridView();
            Hora = new DataGridViewTextBoxColumn();
            Elemento = new DataGridViewTextBoxColumn();
            Cliente = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            Saldo = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lblReservas);
            panel1.Controls.Add(label1);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(176, 86);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 12);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 0;
            label1.Text = "Reservas hoy";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblIngresos);
            panel2.Controls.Add(label2);
            panel2.ForeColor = Color.White;
            panel2.Location = new Point(209, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(176, 86);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblOcupacion);
            panel3.Controls.Add(label3);
            panel3.ForeColor = Color.White;
            panel3.Location = new Point(406, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(176, 86);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(lblSeñas);
            panel4.Controls.Add(label4);
            panel4.ForeColor = Color.White;
            panel4.Location = new Point(603, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(176, 86);
            panel4.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 12);
            label2.Name = "label2";
            label2.Size = new Size(130, 21);
            label2.TabIndex = 1;
            label2.Text = "Ingresos del día";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(14, 12);
            label3.Name = "label3";
            label3.Size = new Size(92, 21);
            label3.TabIndex = 2;
            label3.Text = "Ocupación";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(13, 12);
            label4.Name = "label4";
            label4.Size = new Size(143, 21);
            label4.TabIndex = 3;
            label4.Text = "Señas pendientes";
            // 
            // lblReservas
            // 
            lblReservas.AutoSize = true;
            lblReservas.Location = new Point(13, 54);
            lblReservas.Name = "lblReservas";
            lblReservas.Size = new Size(19, 15);
            lblReservas.TabIndex = 1;
            lblReservas.Text = "14";
            // 
            // lblIngresos
            // 
            lblIngresos.AutoSize = true;
            lblIngresos.Location = new Point(13, 54);
            lblIngresos.Name = "lblIngresos";
            lblIngresos.Size = new Size(19, 15);
            lblIngresos.TabIndex = 2;
            lblIngresos.Text = "14";
            // 
            // lblOcupacion
            // 
            lblOcupacion.AutoSize = true;
            lblOcupacion.Location = new Point(14, 54);
            lblOcupacion.Name = "lblOcupacion";
            lblOcupacion.Size = new Size(19, 15);
            lblOcupacion.TabIndex = 3;
            lblOcupacion.Text = "14";
            // 
            // lblSeñas
            // 
            lblSeñas.AutoSize = true;
            lblSeñas.Location = new Point(13, 54);
            lblSeñas.Name = "lblSeñas";
            lblSeñas.Size = new Size(19, 15);
            lblSeñas.TabIndex = 4;
            lblSeñas.Text = "14";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(12, 126);
            label5.Name = "label5";
            label5.Size = new Size(139, 15);
            label5.TabIndex = 4;
            label5.Text = "Próximas reservas de hoy";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Hora, Elemento, Cliente, Estado, Saldo });
            dataGridView1.Location = new Point(12, 157);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(767, 123);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Hora
            // 
            Hora.HeaderText = "Hora";
            Hora.MinimumWidth = 10;
            Hora.Name = "Hora";
            Hora.ReadOnly = true;
            // 
            // Elemento
            // 
            Elemento.HeaderText = "Elemento";
            Elemento.Name = "Elemento";
            Elemento.ReadOnly = true;
            // 
            // Cliente
            // 
            Cliente.HeaderText = "Cliente";
            Cliente.Name = "Cliente";
            Cliente.ReadOnly = true;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            Estado.ReadOnly = true;
            // 
            // Saldo
            // 
            Saldo.HeaderText = "Saldo";
            Saldo.Name = "Saldo";
            Saldo.ReadOnly = true;
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(796, 304);
            Controls.Add(dataGridView1);
            Controls.Add(label5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmDashboard";
            Text = "frmDashboard";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private Panel panel3;
        private Label label3;
        private Panel panel4;
        private Label label4;
        private Label lblReservas;
        private Label lblIngresos;
        private Label lblOcupacion;
        private Label lblSeñas;
        private Label label5;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Hora;
        private DataGridViewTextBoxColumn Elemento;
        private DataGridViewTextBoxColumn Cliente;
        private DataGridViewTextBoxColumn Estado;
        private DataGridViewTextBoxColumn Saldo;
    }
}