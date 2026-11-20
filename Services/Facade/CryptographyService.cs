using Services.Logic.Infraestructure;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para los servicios de criptografía (hashing y cifrado simétrico).
    /// </summary>
    public static class CryptographyService
    {
        /// <summary>
        /// Genera un hash MD5 a partir de un texto plano.
        /// </summary>
        /// <param name="textPlain">Texto a hashear.</param>
        /// <returns>Hash MD5 en formato hexadecimal.</returns>
        public static string HashMd5(string textPlain)
        {
            return CryptographyLogic.HashMd5(textPlain);
        }

        /// <summary>
        /// Cifra un texto plano utilizando cifrado simétrico.
        /// </summary>
        /// <param name="clearText">Texto plano a cifrar.</param>
        /// <returns>Texto cifrado en Base64.</returns>
        public static string Encrypt(string clearText)
        {
            return CryptographyLogic.Encrypt(clearText);
        }

        /// <summary>
        /// Descifra un texto previamente cifrado.
        /// </summary>
        /// <param name="encryptedText">Texto cifrado en Base64.</param>
        /// <returns>Texto plano original.</returns>
        public static string Decrypt(string encryptedText)
        {
            return CryptographyLogic.Decrypt(encryptedText);
        }
    }
}