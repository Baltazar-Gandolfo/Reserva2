using Services.Facade;
using System.Windows.Forms;

namespace Reserva2.UI.I18n
{
    /// <summary>
    /// Helper de internacionalización. Traduce textos y controles de formularios al idioma activo.
    /// </summary>
    public static class TraducirHelper
    {
        /// <summary>
        /// Traduce una clave al idioma activo. Si no existe la traducción, devuelve la clave original.
        /// </summary>
        /// <param name="clave">Texto clave a traducir.</param>
        /// <returns>Texto traducido o la clave original si falla.</returns>
        public static string T(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave)) return clave;
            try { return IdiomaService.Traducir(clave); }
            catch { return clave; }
        }

        /// <summary>
        /// Traduce recursivamente los textos de los controles de un formulario.
        /// </summary>
        /// <param name="contenedor">Control raíz (generalmente un Form).</param>
        public static void TraducirControles(Control contenedor)
        {
            if (contenedor is Form frm && !string.IsNullOrWhiteSpace(frm.Text))
                frm.Text = T(frm.Text);

            Recorrer(contenedor);
        }

        /// <summary>Recorre recursivamente los controles hijos traduciendo labels, botones y similares.</summary>
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

        /// <summary>
        /// Traduce los encabezados de columna de un DataGridView.
        /// </summary>
        /// <param name="dgv">Grilla cuyas columnas se traducen.</param>
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
