namespace Reserva2.UI.Disponibilidad
{
    partial class frmDisponibilidad
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
            cmbElemento = new ComboBox();
            numDesde = new NumericUpDown();
            numHasta = new NumericUpDown();
            btnCopiarLunes = new Button();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            btnMarcarTodo = new Button();
            btnCancelar = new Button();
            btnGuardar = new Button();
            Hora = new DataGridViewTextBoxColumn();
            Lun = new DataGridViewCheckBoxColumn();
            Mar = new DataGridViewCheckBoxColumn();
            Mié = new DataGridViewCheckBoxColumn();
            Jue = new DataGridViewCheckBoxColumn();
            Vie = new DataGridViewCheckBoxColumn();
            Sáb = new DataGridViewCheckBoxColumn();
            Dom = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)numDesde).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHasta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(22, 11);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 0;
            label1.Text = "Elemento";
            // 
            // cmbElemento
            // 
            cmbElemento.FormattingEnabled = true;
            cmbElemento.Location = new Point(22, 29);
            cmbElemento.Name = "cmbElemento";
            cmbElemento.Size = new Size(208, 23);
            cmbElemento.TabIndex = 1;
            // 
            // numDesde
            // 
            numDesde.Location = new Point(257, 28);
            numDesde.Name = "numDesde";
            numDesde.Size = new Size(83, 23);
            numDesde.TabIndex = 2;
            // 
            // numHasta
            // 
            numHasta.Location = new Point(355, 28);
            numHasta.Name = "numHasta";
            numHasta.Size = new Size(83, 23);
            numHasta.TabIndex = 3;
            // 
            // btnCopiarLunes
            // 
            btnCopiarLunes.Location = new Point(620, 28);
            btnCopiarLunes.Name = "btnCopiarLunes";
            btnCopiarLunes.Size = new Size(131, 25);
            btnCopiarLunes.TabIndex = 4;
            btnCopiarLunes.Text = "Copiar lunes al resto";
            btnCopiarLunes.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Hora, Lun, Mar, Mié, Jue, Vie, Sáb, Dom });
            dataGridView1.Location = new Point(22, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(843, 150);
            dataGridView1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(257, 10);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 8;
            label2.Text = "Desde";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(364, 11);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 9;
            label3.Text = "Hasta";
            // 
            // btnMarcarTodo
            // 
            btnMarcarTodo.Location = new Point(767, 29);
            btnMarcarTodo.Name = "btnMarcarTodo";
            btnMarcarTodo.Size = new Size(98, 25);
            btnMarcarTodo.TabIndex = 10;
            btnMarcarTodo.Text = "Marcar Todo";
            btnMarcarTodo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(789, 226);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(76, 27);
            btnCancelar.TabIndex = 18;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(707, 226);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(76, 27);
            btnGuardar.TabIndex = 17;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // Hora
            // 
            Hora.HeaderText = "Hora";
            Hora.Name = "Hora";
            // 
            // Lun
            // 
            Lun.HeaderText = "Lun";
            Lun.Name = "Lun";
            // 
            // Mar
            // 
            Mar.HeaderText = "Mar";
            Mar.Name = "Mar";
            // 
            // Mié
            // 
            Mié.HeaderText = "Mié";
            Mié.Name = "Mié";
            Mié.Resizable = DataGridViewTriState.True;
            Mié.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Jue
            // 
            Jue.HeaderText = "Jue";
            Jue.Name = "Jue";
            // 
            // Vie
            // 
            Vie.HeaderText = "Vie";
            Vie.Name = "Vie";
            // 
            // Sáb
            // 
            Sáb.HeaderText = "Sáb";
            Sáb.Name = "Sáb";
            // 
            // Dom
            // 
            Dom.HeaderText = "Dom";
            Dom.Name = "Dom";
            // 
            // frmDisponibilidad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(886, 265);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(btnMarcarTodo);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(btnCopiarLunes);
            Controls.Add(numHasta);
            Controls.Add(numDesde);
            Controls.Add(cmbElemento);
            Controls.Add(label1);
            Name = "frmDisponibilidad";
            Text = "frmDisponibilidad";
            ((System.ComponentModel.ISupportInitialize)numDesde).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHasta).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbElemento;
        private NumericUpDown numDesde;
        private NumericUpDown numHasta;
        private Button btnCopiarLunes;
        private DataGridView dataGridView1;
        private Label label2;
        private Label label3;
        private Button btnMarcarTodo;
        private DataGridViewTextBoxColumn Hora;
        private DataGridViewCheckBoxColumn Lun;
        private DataGridViewCheckBoxColumn Mar;
        private DataGridViewCheckBoxColumn Mié;
        private DataGridViewCheckBoxColumn Jue;
        private DataGridViewCheckBoxColumn Vie;
        private DataGridViewCheckBoxColumn Sáb;
        private DataGridViewCheckBoxColumn Dom;
        private Button btnCancelar;
        private Button btnGuardar;
    }
}