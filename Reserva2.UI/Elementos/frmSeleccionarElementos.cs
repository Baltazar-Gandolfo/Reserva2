using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reserva2.UI
{
    public class frmSeleccionarComercio : Form
    {
        private ListBox lstComercios;
        private Button btnAceptar;
        private Button btnCancelar;

        private void InitializeComponent()
        {

        }

        public int IdComercioSeleccionado { get; private set; }

        public frmSeleccionarComercio(List<Comercio> comercios)
        {
            this.Text = "Seleccionar Comercio";
            this.Size = new System.Drawing.Size(350, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lstComercios = new ListBox();
            lstComercios.Location = new System.Drawing.Point(15, 15);
            lstComercios.Size = new System.Drawing.Size(300, 140);
            lstComercios.DisplayMember = "Nombre";
            lstComercios.ValueMember = "IdComercio";
            lstComercios.DataSource = comercios;

            btnAceptar = new Button();
            btnAceptar.Text = "Aceptar";
            btnAceptar.Location = new System.Drawing.Point(150, 170);
            btnAceptar.Click += (s, e) =>
            {
                if (lstComercios.SelectedItem is Comercio c)
                {
                    IdComercioSeleccionado = c.IdComercio;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new System.Drawing.Point(235, 170);
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lstComercios, btnAceptar, btnCancelar });
        }
    }
}