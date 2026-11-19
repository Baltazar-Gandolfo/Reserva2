using Services.Facade;
using System.Windows.Forms;

namespace Reserva2.UI.I18n
{
    public static class TraducirHelper
    {
        public static string T(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave)) return clave;
            try { return IdiomaService.Traducir(clave); }
            catch { return clave; }
        }

        public static void TraducirControles(Control contenedor)
        {
            if (contenedor is Form frm && !string.IsNullOrWhiteSpace(frm.Text))
                frm.Text = T(frm.Text);

            Recorrer(contenedor);
        }

        private static void Recorrer(Control padre)
        {
            foreach (Control c in padre.Controls)
            {
                if (c is TextBox || c is ComboBox || c is NumericUpDown ||
                    c is DateTimePicker || c is DataGridView || c is ListBox ||
                    c is ListView || c is TreeView || c is MonthCalendar ||
                    c is RichTextBox || c is ToolStrip || c is StatusStrip ||
                    c is MenuStrip || c is ProgressBar)
                {
                    continue;
                }

                if (c is Label || c is Button || c is CheckBox || c is RadioButton ||
                    c is GroupBox || c is LinkLabel || c is TabPage)
                {
                    if (!string.IsNullOrWhiteSpace(c.Text) && c.Text.Length < 200)
                        c.Text = T(c.Text);
                }

                if (c.HasChildren)
                    Recorrer(c);
            }
        }

        public static void TraducirColumnas(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!string.IsNullOrWhiteSpace(col.HeaderText))
                    col.HeaderText = T(col.HeaderText);
            }
        }
    }
}
