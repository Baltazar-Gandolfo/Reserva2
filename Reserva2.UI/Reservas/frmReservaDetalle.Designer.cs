namespace Reserva2.UI.Reservas
{
    partial class frmReservaDetalle
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
            lblTurno = new Label();
            lblEstado = new Label();
            lblSaldo = new Label();
            lblTotal = new Label();
            lblCliente = new Label();
            lblElemento = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            grpDatos = new GroupBox();
            lblCreada = new Label();
            lblPagado = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            dgvPagos = new DataGridView();
            label12 = new Label();
            btnRegistrarPago = new Button();
            btnCancelarReserva = new Button();
            btnCerrar = new Button();
            btnImprimir = new Button();
            lstActividad = new ListView();
            Fecha = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            Metodo = new DataGridViewTextBoxColumn();
            Monto = new DataGridViewTextBoxColumn();
            Registro = new DataGridViewTextBoxColumn();
            grpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).BeginInit();
            SuspendLayout();
            // 
            // lblTurno
            // 
            lblTurno.AutoSize = true;
            lblTurno.ForeColor = Color.White;
            lblTurno.Location = new Point(504, 46);
            lblTurno.Name = "lblTurno";
            lblTurno.Size = new Size(138, 15);
            lblTurno.TabIndex = 47;
            lblTurno.Text = "03/08/2026 18:00 -- 19:00";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.LightGreen;
            lblEstado.Location = new Point(504, 19);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(70, 15);
            lblEstado.TabIndex = 46;
            lblEstado.Text = "Confirmada";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.ForeColor = Color.White;
            lblSaldo.Location = new Point(145, 100);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(43, 15);
            lblSaldo.TabIndex = 45;
            lblSaldo.Text = "$12600";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.ForeColor = Color.White;
            lblTotal.Location = new Point(145, 73);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(43, 15);
            lblTotal.TabIndex = 44;
            lblTotal.Text = "$18000";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(145, 46);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(158, 15);
            lblCliente.TabIndex = 43;
            lblCliente.Text = "Micaela Gómez -- 12.345.678";
            // 
            // lblElemento
            // 
            lblElemento.AutoSize = true;
            lblElemento.ForeColor = Color.White;
            lblElemento.Location = new Point(145, 19);
            lblElemento.Name = "lblElemento";
            lblElemento.Size = new Size(117, 15);
            lblElemento.TabIndex = 42;
            lblElemento.Text = "Fútbol 5 -- Cancha A";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(364, 46);
            label11.Name = "label11";
            label11.Size = new Size(39, 15);
            label11.TabIndex = 40;
            label11.Text = "Turno";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(365, 19);
            label10.Name = "label10";
            label10.Size = new Size(42, 15);
            label10.TabIndex = 39;
            label10.Text = "Estado";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(6, 100);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 38;
            label9.Text = "Saldo";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(5, 73);
            label8.Name = "label8";
            label8.Size = new Size(33, 15);
            label8.TabIndex = 37;
            label8.Text = "Total";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(6, 46);
            label7.Name = "label7";
            label7.Size = new Size(44, 15);
            label7.TabIndex = 36;
            label7.Text = "Cliente";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 35;
            label6.Text = "Elemento";
            // 
            // grpDatos
            // 
            grpDatos.Controls.Add(lblCreada);
            grpDatos.Controls.Add(label6);
            grpDatos.Controls.Add(lblPagado);
            grpDatos.Controls.Add(label7);
            grpDatos.Controls.Add(label3);
            grpDatos.Controls.Add(label8);
            grpDatos.Controls.Add(label4);
            grpDatos.Controls.Add(label9);
            grpDatos.Controls.Add(lblTurno);
            grpDatos.Controls.Add(label10);
            grpDatos.Controls.Add(lblEstado);
            grpDatos.Controls.Add(label11);
            grpDatos.Controls.Add(lblSaldo);
            grpDatos.Controls.Add(lblElemento);
            grpDatos.Controls.Add(lblTotal);
            grpDatos.Controls.Add(lblCliente);
            grpDatos.ForeColor = Color.White;
            grpDatos.Location = new Point(12, 12);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(705, 136);
            grpDatos.TabIndex = 48;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos de la reserva";
            // 
            // lblCreada
            // 
            lblCreada.AutoSize = true;
            lblCreada.ForeColor = Color.White;
            lblCreada.Location = new Point(503, 100);
            lblCreada.Name = "lblCreada";
            lblCreada.Size = new Size(135, 15);
            lblCreada.TabIndex = 52;
            lblCreada.Text = "28/07/2026 10:14 -- Web";
            // 
            // lblPagado
            // 
            lblPagado.AutoSize = true;
            lblPagado.ForeColor = Color.White;
            lblPagado.Location = new Point(503, 73);
            lblPagado.Name = "lblPagado";
            lblPagado.Size = new Size(37, 15);
            lblPagado.TabIndex = 51;
            lblPagado.Text = "$5400";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(363, 100);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 50;
            label3.Text = "Creada";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(364, 73);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 49;
            label4.Text = "Pagado";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(12, 162);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 49;
            label5.Text = "Pagos";
            // 
            // dgvPagos
            // 
            dgvPagos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPagos.Columns.AddRange(new DataGridViewColumn[] { Fecha, Tipo, Metodo, Monto, Registro });
            dgvPagos.Location = new Point(12, 180);
            dgvPagos.Name = "dgvPagos";
            dgvPagos.Size = new Size(705, 73);
            dgvPagos.TabIndex = 50;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(12, 270);
            label12.Name = "label12";
            label12.Size = new Size(51, 15);
            label12.TabIndex = 51;
            label12.Text = "Historial";
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(271, 394);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(125, 23);
            btnRegistrarPago.TabIndex = 53;
            btnRegistrarPago.Text = "Registrar pago...";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // btnCancelarReserva
            // 
            btnCancelarReserva.Location = new Point(412, 394);
            btnCancelarReserva.Name = "btnCancelarReserva";
            btnCancelarReserva.Size = new Size(125, 23);
            btnCancelarReserva.TabIndex = 54;
            btnCancelarReserva.Text = "Cancelar reserva";
            btnCancelarReserva.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(642, 394);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 55;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(552, 394);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(75, 23);
            btnImprimir.TabIndex = 56;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = true;
            // 
            // lstActividad
            // 
            lstActividad.Location = new Point(12, 288);
            lstActividad.Name = "lstActividad";
            lstActividad.Size = new Size(705, 97);
            lstActividad.TabIndex = 57;
            lstActividad.UseCompatibleStateImageBehavior = false;
            // 
            // Fecha
            // 
            Fecha.HeaderText = "Fecha";
            Fecha.Name = "Fecha";
            Fecha.Width = 137;
            // 
            // Tipo
            // 
            Tipo.HeaderText = "Tipo";
            Tipo.Name = "Tipo";
            Tipo.Width = 125;
            // 
            // Metodo
            // 
            Metodo.HeaderText = "Método";
            Metodo.Name = "Metodo";
            Metodo.Width = 125;
            // 
            // Monto
            // 
            Monto.HeaderText = "Monto";
            Monto.Name = "Monto";
            Monto.Width = 125;
            // 
            // Registro
            // 
            Registro.HeaderText = "Registro";
            Registro.Name = "Registro";
            Registro.Width = 150;
            // 
            // frmReservaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(730, 431);
            Controls.Add(lstActividad);
            Controls.Add(btnImprimir);
            Controls.Add(btnCerrar);
            Controls.Add(btnCancelarReserva);
            Controls.Add(btnRegistrarPago);
            Controls.Add(label12);
            Controls.Add(dgvPagos);
            Controls.Add(label5);
            Controls.Add(grpDatos);
            Name = "frmReservaDetalle";
            Text = "frmReservaDetalle";
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTurno;
        private Label lblEstado;
        private Label lblSaldo;
        private Label lblTotal;
        private Label lblCliente;
        private Label lblElemento;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private GroupBox grpDatos;
        private Label lblCreada;
        private Label lblPagado;
        private Label label3;
        private Label label4;
        private Label label5;
        private DataGridView dgvPagos;
        private Label label12;
        private Button btnRegistrarPago;
        private Button btnCancelarReserva;
        private Button btnCerrar;
        private Button btnImprimir;
        private ListView lstActividad;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn Metodo;
        private DataGridViewTextBoxColumn Monto;
        private DataGridViewTextBoxColumn Registro;
    }
}