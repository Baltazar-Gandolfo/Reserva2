namespace Reserva2.UI.Controles
{
    /// <summary>
    /// Ítem genérico para controles ComboBox. Muestra un texto y conserva el objeto asociado.
    /// </summary>
    /// <typeparam name="T">Tipo del valor subyacente.</typeparam>
    public class ItemCombo<T>
    {
        /// <summary>Valor subyacente del ítem.</summary>
        public T Valor { get; }

        /// <summary>Texto visible en el combo.</summary>
        public string Texto { get; }

        /// <summary>
        /// Crea un nuevo ítem de combo.
        /// </summary>
        /// <param name="valor">Valor asociado.</param>
        /// <param name="texto">Texto a mostrar.</param>
        public ItemCombo(T valor, string texto)
        {
            Valor = valor;
            Texto = texto;
        }

        /// <summary>Devuelve el texto visible del ítem.</summary>
        /// <returns>El texto del ítem.</returns>
        public override string ToString() => Texto;
    }
}
