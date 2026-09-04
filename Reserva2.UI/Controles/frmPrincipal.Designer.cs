namespace Reserva2.UI.Controles
{
    partial class frmPrincipal
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
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            reservasToolStripMenuItem = new ToolStripMenuItem();
            comercioToolStripMenuItem = new ToolStripMenuItem();
            pagosToolStripMenuItem = new ToolStripMenuItem();
            plataformaToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripLabel3 = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel4 = new ToolStripLabel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.MediumPurple;
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, reservasToolStripMenuItem, comercioToolStripMenuItem, pagosToolStripMenuItem, plataformaToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // reservasToolStripMenuItem
            // 
            reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            reservasToolStripMenuItem.Size = new Size(64, 20);
            reservasToolStripMenuItem.Text = "Reservas";
            // 
            // comercioToolStripMenuItem
            // 
            comercioToolStripMenuItem.Name = "comercioToolStripMenuItem";
            comercioToolStripMenuItem.Size = new Size(71, 20);
            comercioToolStripMenuItem.Text = "Comercio";
            // 
            // pagosToolStripMenuItem
            // 
            pagosToolStripMenuItem.Name = "pagosToolStripMenuItem";
            pagosToolStripMenuItem.Size = new Size(51, 20);
            pagosToolStripMenuItem.Text = "Pagos";
            // 
            // plataformaToolStripMenuItem
            // 
            plataformaToolStripMenuItem.Name = "plataformaToolStripMenuItem";
            plataformaToolStripMenuItem.Size = new Size(77, 20);
            plataformaToolStripMenuItem.Text = "Plataforma";
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(53, 20);
            ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.SlateBlue;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripSeparator1, toolStripLabel2, toolStripSeparator2, toolStripLabel3, toolStripSeparator3, toolStripLabel4 });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.ForeColor = Color.White;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(92, 22);
            toolStripLabel1.Text = "+ Nueva reserva";
            toolStripLabel1.Click += toolStripLabel1_Click_1;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.ForeColor = Color.White;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(52, 22);
            toolStripLabel2.Text = "Reservas";
            toolStripLabel2.Click += toolStripLabel2_Click;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.ForeColor = Color.White;
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(39, 22);
            toolStripLabel3.Text = "Pagos";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.ForeColor = Color.White;
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(59, 22);
            toolStripLabel4.Text = "Actualizar";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.SlateBlue;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4, toolStripStatusLabel5 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(146, 17);
            toolStripStatusLabel1.Text = "Usuario: Baltazar Gandolfo";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.ForeColor = Color.White;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(16, 17);
            toolStripStatusLabel2.Text = " | ";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.ForeColor = Color.White;
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(65, 17);
            toolStripStatusLabel3.Text = "Rol: Owner";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.ForeColor = Color.White;
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(16, 17);
            toolStripStatusLabel4.Text = " | ";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.ForeColor = Color.White;
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new Size(171, 17);
            toolStripStatusLabel5.Text = "Comercio: Complejo deportivo";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmPrincipal";
            Text = "frmPrincipal";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem reservasToolStripMenuItem;
        private ToolStripMenuItem comercioToolStripMenuItem;
        private ToolStripMenuItem pagosToolStripMenuItem;
        private ToolStripMenuItem plataformaToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripSeparator toolStripSeparator3;
    }
}