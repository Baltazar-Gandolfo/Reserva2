namespace Reserva2.UI.Disponibilidad
{
    partial class frmCalendarioSlots
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
            components = new System.ComponentModel.Container();
            mcSemana = new MonthCalendar();
            cboElemento = new ComboBox();
            dvgSlots = new DataGridView();
            label1 = new Label();
            Hora = new DataGridViewTextBoxColumn();
            Lun = new DataGridViewTextBoxColumn();
            Mar = new DataGridViewTextBoxColumn();
            Mié = new DataGridViewTextBoxColumn();
            Jue = new DataGridViewTextBoxColumn();
            Vie = new DataGridViewTextBoxColumn();
            Sáb = new DataGridViewTextBoxColumn();
            Dom = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            reservarEsteTurnoToolStripMenuItem = new ToolStripMenuItem();
            verReservaToolStripMenuItem = new ToolStripMenuItem();
            bloquearTurnoToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dvgSlots).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // mcSemana
            // 
            mcSemana.Location = new Point(9, 9);
            mcSemana.Name = "mcSemana";
            mcSemana.TabIndex = 0;
            // 
            // cboElemento
            // 
            cboElemento.FormattingEnabled = true;
            cboElemento.Location = new Point(9, 204);
            cboElemento.Name = "cboElemento";
            cboElemento.Size = new Size(227, 23);
            cboElemento.TabIndex = 1;
            // 
            // dvgSlots
            // 
            dvgSlots.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgSlots.Columns.AddRange(new DataGridViewColumn[] { Hora, Lun, Mar, Mié, Jue, Vie, Sáb, Dom });
            dvgSlots.Location = new Point(9, 233);
            dvgSlots.Name = "dvgSlots";
            dvgSlots.Size = new Size(843, 205);
            dvgSlots.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(9, 186);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "Elemento";
            // 
            // Hora
            // 
            Hora.HeaderText = "Hora";
            Hora.Name = "Hora";
            // 
            // Lun
            // 
            Lun.HeaderText = "Lun X";
            Lun.Name = "Lun";
            // 
            // Mar
            // 
            Mar.HeaderText = "Mar X";
            Mar.Name = "Mar";
            // 
            // Mié
            // 
            Mié.HeaderText = "Mié X";
            Mié.Name = "Mié";
            // 
            // Jue
            // 
            Jue.HeaderText = "Jue X";
            Jue.Name = "Jue";
            // 
            // Vie
            // 
            Vie.HeaderText = "Vie X";
            Vie.Name = "Vie";
            // 
            // Sáb
            // 
            Sáb.HeaderText = "Sáb X";
            Sáb.Name = "Sáb";
            // 
            // Dom
            // 
            Dom.HeaderText = "Dom X";
            Dom.Name = "Dom";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { reservarEsteTurnoToolStripMenuItem, verReservaToolStripMenuItem, bloquearTurnoToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(175, 70);
            // 
            // reservarEsteTurnoToolStripMenuItem
            // 
            reservarEsteTurnoToolStripMenuItem.Name = "reservarEsteTurnoToolStripMenuItem";
            reservarEsteTurnoToolStripMenuItem.Size = new Size(174, 22);
            reservarEsteTurnoToolStripMenuItem.Text = "Reservar este turno";
            // 
            // verReservaToolStripMenuItem
            // 
            verReservaToolStripMenuItem.Name = "verReservaToolStripMenuItem";
            verReservaToolStripMenuItem.Size = new Size(174, 22);
            verReservaToolStripMenuItem.Text = "Ver reserva";
            // 
            // bloquearTurnoToolStripMenuItem
            // 
            bloquearTurnoToolStripMenuItem.Name = "bloquearTurnoToolStripMenuItem";
            bloquearTurnoToolStripMenuItem.Size = new Size(174, 22);
            bloquearTurnoToolStripMenuItem.Text = "Bloquear turno";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(633, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 131);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Referencias";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 19);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 0;
            label2.Text = "Libre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 46);
            label3.Name = "label3";
            label3.Size = new Size(103, 15);
            label3.TabIndex = 1;
            label3.Text = "Pendiente de seña";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 76);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 2;
            label4.Text = "Confirmada";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 104);
            label5.Name = "label5";
            label5.Size = new Size(132, 15);
            label5.TabIndex = 3;
            label5.Text = "Cerrado / no disponible";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(9, 446);
            label6.Name = "label6";
            label6.Size = new Size(272, 15);
            label6.TabIndex = 12;
            label6.Text = "Semana 03/08 — 09/08 · 21 turnos ocupados de 35";
            // 
            // frmCalendarioSlots
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(864, 470);
            Controls.Add(label6);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(dvgSlots);
            Controls.Add(cboElemento);
            Controls.Add(mcSemana);
            Name = "frmCalendarioSlots";
            Text = "frmCalendarioSlots";
            ((System.ComponentModel.ISupportInitialize)dvgSlots).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar mcSemana;
        private ComboBox cboElemento;
        private DataGridView dvgSlots;
        private DataGridViewTextBoxColumn Hora;
        private DataGridViewTextBoxColumn Lun;
        private DataGridViewTextBoxColumn Mar;
        private DataGridViewTextBoxColumn Mié;
        private DataGridViewTextBoxColumn Jue;
        private DataGridViewTextBoxColumn Vie;
        private DataGridViewTextBoxColumn Sáb;
        private DataGridViewTextBoxColumn Dom;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem reservarEsteTurnoToolStripMenuItem;
        private ToolStripMenuItem verReservaToolStripMenuItem;
        private ToolStripMenuItem bloquearTurnoToolStripMenuItem;
        private GroupBox groupBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label6;
    }
}