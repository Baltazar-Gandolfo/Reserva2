namespace Reserva2.UI.Usuarios
{
    partial class frmPermisos
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
            treeView1 = new TreeView();
            label1 = new Label();
            label2 = new Label();
            txtFamiliaNombre = new TextBox();
            txtFamiliaDescripcion = new TextBox();
            btnCrearFamilia = new Button();
            btnEditarFamilia = new Button();
            btnEliminarFamilia = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnEliminarPatente = new Button();
            btnEditarPatente = new Button();
            btnCrearPatente = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            groupBox3 = new GroupBox();
            label5 = new Label();
            cboUsuarios = new ComboBox();
            btnAsignarFamilia = new Button();
            btnQuitarFamilia = new Button();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.SlateBlue;
            treeView1.Location = new Point(12, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(321, 316);
            treeView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(366, 39);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 1;
            label1.Text = "Nombre de Familia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(366, 82);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 2;
            label2.Text = "Descripción";
            // 
            // txtFamiliaNombre
            // 
            txtFamiliaNombre.Location = new Point(510, 31);
            txtFamiliaNombre.Name = "txtFamiliaNombre";
            txtFamiliaNombre.Size = new Size(192, 23);
            txtFamiliaNombre.TabIndex = 3;
            // 
            // txtFamiliaDescripcion
            // 
            txtFamiliaDescripcion.Location = new Point(510, 77);
            txtFamiliaDescripcion.Name = "txtFamiliaDescripcion";
            txtFamiliaDescripcion.Size = new Size(192, 23);
            txtFamiliaDescripcion.TabIndex = 4;
            // 
            // btnCrearFamilia
            // 
            btnCrearFamilia.Location = new Point(363, 121);
            btnCrearFamilia.Name = "btnCrearFamilia";
            btnCrearFamilia.Size = new Size(71, 27);
            btnCrearFamilia.TabIndex = 5;
            btnCrearFamilia.Text = "Crear";
            btnCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // btnEditarFamilia
            // 
            btnEditarFamilia.Location = new Point(452, 121);
            btnEditarFamilia.Name = "btnEditarFamilia";
            btnEditarFamilia.Size = new Size(71, 27);
            btnEditarFamilia.TabIndex = 6;
            btnEditarFamilia.Text = "Editar";
            btnEditarFamilia.UseVisualStyleBackColor = true;
            // 
            // btnEliminarFamilia
            // 
            btnEliminarFamilia.Location = new Point(543, 121);
            btnEliminarFamilia.Name = "btnEliminarFamilia";
            btnEliminarFamilia.Size = new Size(71, 27);
            btnEliminarFamilia.TabIndex = 7;
            btnEliminarFamilia.Text = "Eliminar";
            btnEliminarFamilia.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(353, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(364, 159);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Familia";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnEliminarPatente);
            groupBox2.Controls.Add(btnEditarPatente);
            groupBox2.Controls.Add(btnCrearPatente);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(353, 177);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(364, 151);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Patente";
            // 
            // btnEliminarPatente
            // 
            btnEliminarPatente.ForeColor = Color.Black;
            btnEliminarPatente.Location = new Point(193, 107);
            btnEliminarPatente.Name = "btnEliminarPatente";
            btnEliminarPatente.Size = new Size(71, 27);
            btnEliminarPatente.TabIndex = 15;
            btnEliminarPatente.Text = "Eliminar";
            btnEliminarPatente.UseVisualStyleBackColor = true;
            // 
            // btnEditarPatente
            // 
            btnEditarPatente.ForeColor = Color.Black;
            btnEditarPatente.Location = new Point(102, 107);
            btnEditarPatente.Name = "btnEditarPatente";
            btnEditarPatente.Size = new Size(71, 27);
            btnEditarPatente.TabIndex = 14;
            btnEditarPatente.Text = "Editar";
            btnEditarPatente.UseVisualStyleBackColor = true;
            // 
            // btnCrearPatente
            // 
            btnCrearPatente.ForeColor = Color.Black;
            btnCrearPatente.Location = new Point(13, 107);
            btnCrearPatente.Name = "btnCrearPatente";
            btnCrearPatente.Size = new Size(71, 27);
            btnCrearPatente.TabIndex = 13;
            btnCrearPatente.Text = "Crear";
            btnCrearPatente.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(160, 63);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(192, 23);
            textBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(160, 17);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(192, 23);
            textBox2.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 68);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 10;
            label3.Text = "Descripción";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 25);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 9;
            label4.Text = "Nombre de Familia";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnQuitarFamilia);
            groupBox3.Controls.Add(btnAsignarFamilia);
            groupBox3.Controls.Add(cboUsuarios);
            groupBox3.Controls.Add(label5);
            groupBox3.ForeColor = Color.White;
            groupBox3.Location = new Point(12, 338);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(705, 82);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Asignar a usuario";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 28);
            label5.Name = "label5";
            label5.Size = new Size(108, 15);
            label5.TabIndex = 10;
            label5.Text = "Nombre de Familia";
            // 
            // cboUsuarios
            // 
            cboUsuarios.FormattingEnabled = true;
            cboUsuarios.Location = new Point(6, 46);
            cboUsuarios.Name = "cboUsuarios";
            cboUsuarios.Size = new Size(239, 23);
            cboUsuarios.TabIndex = 11;
            // 
            // btnAsignarFamilia
            // 
            btnAsignarFamilia.ForeColor = Color.Black;
            btnAsignarFamilia.Location = new Point(264, 43);
            btnAsignarFamilia.Name = "btnAsignarFamilia";
            btnAsignarFamilia.Size = new Size(228, 27);
            btnAsignarFamilia.TabIndex = 12;
            btnAsignarFamilia.Text = "Asignar familia seleccionada al usuario";
            btnAsignarFamilia.UseVisualStyleBackColor = true;
            // 
            // btnQuitarFamilia
            // 
            btnQuitarFamilia.ForeColor = Color.Black;
            btnQuitarFamilia.Location = new Point(510, 43);
            btnQuitarFamilia.Name = "btnQuitarFamilia";
            btnQuitarFamilia.Size = new Size(158, 27);
            btnQuitarFamilia.TabIndex = 13;
            btnQuitarFamilia.Text = "Quitar familia al usuario";
            btnQuitarFamilia.UseVisualStyleBackColor = true;
            // 
            // frmPermisos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(731, 434);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(btnEliminarFamilia);
            Controls.Add(btnEditarFamilia);
            Controls.Add(btnCrearFamilia);
            Controls.Add(txtFamiliaDescripcion);
            Controls.Add(txtFamiliaNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(treeView1);
            Controls.Add(groupBox1);
            Name = "frmPermisos";
            Text = "frmPermisos";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private Label label1;
        private Label label2;
        private TextBox txtFamiliaNombre;
        private TextBox txtFamiliaDescripcion;
        private Button btnCrearFamilia;
        private Button btnEditarFamilia;
        private Button btnEliminarFamilia;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnEliminarPatente;
        private Button btnEditarPatente;
        private Button btnCrearPatente;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private GroupBox groupBox3;
        private Button btnQuitarFamilia;
        private Button btnAsignarFamilia;
        private ComboBox cboUsuarios;
        private Label label5;
    }
}