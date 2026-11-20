using Services.DataAccess;
using Services.Facade;
using Services.Logic.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de negocio para la traducción de textos (internacionalización i18n).
    /// </summary>
    internal static class IdiomaLogic
    {
        /// <summary>
        /// Traduce un texto al idioma actual del hilo. Si no se encuentra, agrega la clave vacía.
        /// </summary>
        /// <param name="texto">Texto clave a traducir.</param>
        /// <returns>Texto traducido o el original si no se encontró.</returns>
        public static string Traducir(string texto)
        {
            try
            {
                string traduccion = IdiomaDal.Traducir(texto);
                return string.IsNullOrWhiteSpace(traduccion) ? texto : traduccion;
            }
            catch (PalabraNoEncontradaException ex)
            {
                // Que podría hacer?
                // 1) Puedo agregar la clave en el idioma que se solicita actualmente, con valor vacío
                // 2) Puedo agregar la clave en el idioma padre y/o todos aquellos otros idiomas que no tengan la palabra
                // 3) Puedo no hacer nada
                // 4) Puedo llamar a un modelito LLM, buscar la traducción y agregarla como value en l paso 1 o 2
                
                // En nuestro DEMO tomamos la determinación de ir por la opción 1
                IdiomaDal.AgregarPalabra(texto);

                Console.WriteLine($"La palabra que no pudo traducirse fue: {ex.Palabra}. ");

                return texto; //En nuestro una palabra que no pudo traducirse, retorna la misma palabra
            }
            catch (Exception ex)
            {
                //Si estoy acá, es una exception más genérica. Para ver después
                //Bitácora y relanzamos la excepción

                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de idiomas disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de culturas disponibles.</returns>
        public static List<CultureInfo> ObtenerIdiomas()
        {
            return IdiomaDal.ObtenerIdiomas();
        }
    }
}
