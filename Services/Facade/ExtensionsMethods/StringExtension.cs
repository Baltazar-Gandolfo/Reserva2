using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Extentions
{
    /// <summary>
    /// Métodos de extensión para cadenas de texto.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>Traduce un texto utilizando el sistema de internacionalización.</summary>
        /// <param name="texto">Texto a traducir.</param>
        /// <returns>Texto traducido al idioma activo.</returns>
        public static string Traducir(this string texto)
        {
            return Logic.IdiomaLogic.Traducir(texto);
        }
    }
}
