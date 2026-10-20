namespace Reserva2.UI.Controles
{
    // Ítem genérico para combos: muestra un texto y conserva el objeto asociado.
    public class ItemCombo<T>
    {
        public T Valor { get; }
        public string Texto { get; }

        public ItemCombo(T valor, string texto)
        {
            Valor = valor;
            Texto = texto;
        }

        public override string ToString() => Texto;
    }
}
