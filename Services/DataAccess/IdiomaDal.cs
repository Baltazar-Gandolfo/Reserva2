using Services.Logic.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    /// <summary>
    /// Acceso a datos para la capa de internacionalización (i18n) basada en archivos de texto.
    /// </summary>
    internal static class IdiomaDal
    {
        /// <summary>Carpeta donde se almacenan los archivos de idioma.</summary>
        private static string folder = ResolverCarpeta(ConfigurationManager.AppSettings["I18NFolder"]);

        /// <summary>Nombre base del archivo de idioma (sin extensión de cultura).</summary>
        private static string fileName = ConfigurationManager.AppSettings["I18NFileName"];

        /// <summary>Resuelve la carpeta de idiomas respecto de la carpeta del ejecutable y no de la carpeta de trabajo.</summary>
        /// <param name="ruta">Ruta absoluta o relativa configurada.</param>
        /// <returns>Ruta absoluta de la carpeta de idiomas.</returns>
        private static string ResolverCarpeta(string ruta)
        {
            return Path.IsPathRooted(ruta) ? ruta : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ruta);
        }

        /// <summary>
        /// Busca la traducción de un texto en el archivo del idioma actual del hilo.
        /// </summary>
        /// <param name="texto">Texto clave a traducir.</param>
        /// <returns>Traducción encontrada.</returns>
        /// <exception cref="PalabraNoEncontradaException">Si no se encuentra la clave.</exception>
        public static string Traducir(string texto)
        {
            string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

            using (StreamReader sr = new StreamReader(fileNameIdioma))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');

                    if (parts.Length == 2 && parts[0].Trim().ToLower() == texto.ToLower())
                    {
                        //Si estoy acá significa que encontré la clave buscada.
                        return parts[1].Trim();
                    }
                }
            }
            throw new PalabraNoEncontradaException(texto);
        }



        /// <summary>
        /// Obtiene la lista de idiomas disponibles a partir de los archivos en la carpeta de i18n.
        /// </summary>
        /// <returns>Lista de culturas soportadas.</returns>
        public static List<CultureInfo> ObtenerIdiomas()
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folder);

                List<CultureInfo> idiomas = new List<CultureInfo>();

                foreach (FileInfo fo in directoryInfo.GetFiles())
                {
                    idiomas.Add(new CultureInfo(fo.Extension.Replace(".", "")));
                }

                return idiomas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agrega una palabra al archivo del idioma actual con traducción vacía por defecto.
        /// </summary>
        /// <param name="palabra">Clave a agregar.</param>
        /// <param name="traduccion">Traducción (vacía por defecto).</param>
        public static void AgregarPalabra(string palabra, string traduccion = "")
        {
            string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

            try
            {
                using (StreamWriter sw = new StreamWriter(fileNameIdioma, true)) //Append: Sirve para agregar data al final del archivo
                {
                    sw.WriteLine($"{palabra}:{traduccion}");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // La carpeta de instalación puede ser de solo lectura: se muestra la clave sin traducir.
            }
            catch (IOException)
            {
            }
        }
    }
}
