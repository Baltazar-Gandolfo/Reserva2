using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logic.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando una palabra clave no se encuentra en los archivos de traducción i18n.
    /// </summary>
    /// <summary>
    /// Excepción lanzada cuando una palabra no se encuentra en el diccionario de traducciones.
    /// </summary>
    internal class PalabraNoEncontradaException : Exception
    {
        /// <summary>Palabra que no pudo ser traducida.</summary>
        public string Palabra { get; private set; }

        /// <summary>
        /// Crea una nueva excepción indicando la palabra no encontrada.
        /// </summary>
        /// <param name="palabra">Palabra que no se encontró.</param>
        public PalabraNoEncontradaException(string palabra) : base($"La palabra { palabra } no ha sido encontrada")
        {
            this.Palabra = palabra;
            HelpLink = "https://www.google.com/search?q=palabra+no+encontrada+exception";
        }
    }
}
    