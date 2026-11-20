using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para el servicio de internacionalización (i18n).
    /// </summary>
    public static class IdiomaService
    {
        /// <summary>
        /// Traduce un texto al idioma actualmente configurado.
        /// </summary>
        /// <param name="texto">Texto en idioma base a traducir.</param>
        /// <returns>Texto traducido o el original si no existe traducción.</returns>
        public static string Traducir(string texto)
        {
            return Logic.IdiomaLogic.Traducir(texto);
        }

        /// <summary>
        /// Obtiene la lista de idiomas disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de culturas soportadas.</returns>
        public static List<CultureInfo> ObtenerIdiomas()
        {
            return Logic.IdiomaLogic.ObtenerIdiomas();
        }
    }
}
