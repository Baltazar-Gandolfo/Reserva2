namespace Reserva2.UI.Reservas
{
    partial class frmReservas
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
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            btnFiltrar = new Button();
            btnDetalle = new Button();
            btnRegistrarPago = new Button();
            btnCancelar = new Button();
            btnImprimir = new Button();
            statusStrip1 = new StatusStrip();
            lblCantidad = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lblFacturado = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            lblPendiente = new ToolStripStatusLabel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            Codigo = new DataGridViewTextBoxColumn();
            FechaHora = new DataGridViewTextBoxColumn();
            Elemento = new DataGridViewTextBoxColumn();
            Cliente = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            Saldo = new DataGridViewTextBoxColumn();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.ImeMode = ImeMode.NoControl;
            dateTimePicker1.Location = new Point(12, 31);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(133, 23);
            dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Location = new Point(164, 31);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(133, 23);
            dateTimePicker2.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(315, 31);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.Silver;
            textBox1.Location = new Point(457, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(170, 23);
            textBox1.TabIndex = 3;
            textBox1.Text = "Cliente, código o cancha\r\n";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(633, 31);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 4;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            // 
            // btnDetalle
            // 
            btnDetalle.Location = new Point(12, 300);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(102, 23);
            btnDetalle.TabIndex = 5;
            btnDetalle.Text = "Ver detalle";
            btnDetalle.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(133, 300);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(102, 23);
            btnRegistrarPago.TabIndex = 6;
            btnRegistrarPago.Text = "Registrar pago...";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(255, 300);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(102, 23);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar reserva";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(381, 300);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(151, 23);
            btnImprimir.TabIndex = 8;
            btnImprimir.Text = "Imprimir comprobante";
            btnImprimir.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.DarkSlateBlue;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblCantidad, toolStripStatusLabel1, lblFacturado, toolStripStatusLabel2, lblPendiente });
            statusStrip1.Location = new Point(0, 336);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblCantidad
            // 
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(58, 17);
            lblCantidad.Text = "5 reservas";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(10, 17);
            toolStripStatusLabel1.Text = "|";
            // 
            // lblFacturado
            // 
            lblFacturado.ForeColor = Color.White;
            lblFacturado.Name = "lblFacturado";
            lblFacturado.Size = new Size(102, 17);
            lblFacturado.Text = "Facturado: $79000";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.ForeColor = Color.White;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(10, 17);
            toolStripStatusLabel2.Text = "|";
            // 
            // lblPendiente
            // 
            lblPendiente.ForeColor = Color.White;
            lblPendiente.Name = "lblPendiente";
            lblPendiente.Size = new Size(152, 17);
            lblPendiente.Text = "Pendiente de cobro: $37600";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 10;
            label1.Text = "Desde";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(164, 13);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 11;
            label2.Text = "Hasta";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(315, 13);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 12;
            label3.Text = "Estado";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(457, 13);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 13;
            label4.Text = "Buscar";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Codigo, FechaHora, Elemento, Cliente, Estado, Total, Saldo });
            dataGridView1.Location = new Point(12, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(743, 225);
            dataGridView1.TabIndex = 14;
            // 
            // Codigo
            // 
            Codigo.HeaderText = "Código";
            Codigo.Name = "Codigo";
            // 
            // FechaHora
            // 
            FechaHora.HeaderText = "Fecha y hora";
            FechaHora.Name = "FechaHora";
            // 
            // Elemento
            // 
            Elemento.HeaderText = "Elemento";
            Elemento.Name = "Elemento";
            // 
            // Cliente
            // 
            Cliente.HeaderText = "Cliente";
            Cliente.Name = "Cliente";
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            // 
            // Total
            // 
            Total.HeaderText = "Total";
            Total.Name = "Total";
            // 
            // Saldo
            // 
            Saldo.HeaderText = "Saldo";
            Saldo.Name = "Saldo";
            // 
            // frmReservas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(800, 358);
            Controls.Add(dataGridView1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Controls.Add(btnImprimir);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrarPago);
            Controls.Add(btnDetalle);
            Controls.Add(btnFiltrar);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Name = "frmReservas";
            Text = "frmReservas";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Button btnFiltrar;
        private Button btnDetalle;
        private Button btnRegistrarPago;
        private Button btnCancelar;
        private Button btnImprimir;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblCantidad;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel lblFacturado;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel lblPendiente;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Codigo;
        private DataGridViewTextBoxColumn FechaHora;
        private DataGridViewTextBoxColumn Elemento;
        private DataGridViewTextBoxColumn Cliente;
        private DataGridViewTextBoxColumn Estado;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewTextBoxColumn Saldo;
    }
}