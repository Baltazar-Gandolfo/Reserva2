using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Services.Logic.Infraestructure
{
    /// <summary>
    /// Lógica de criptografía: hashing MD5 y cifrado/descifrado simétrico AES.
    /// </summary>
    public static class CryptographyLogic
    {
        /// <summary>
        /// Genera un hash MD5 a partir de un texto plano.
        /// </summary>
        /// <param name="textPlain">Texto a hashear.</param>
        /// <returns>Hash MD5 en formato hexadecimal.</returns>
        public static string HashMd5(string textPlain)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(textPlain));
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

        private static string encryptionKey = "RESERVA2_KEY_2026";

        /// <summary>
        /// Cifra un texto plano utilizando AES con clave derivada por PBKDF2.
        /// </summary>
        /// <param name="clearText">Texto plano a cifrar.</param>
        /// <returns>Texto cifrado en Base64.</returns>
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 },
                    1000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Descifra un texto previamente cifrado con AES.
        /// </summary>
        /// <param name="cipherText">Texto cifrado en Base64.</param>
        /// <returns>Texto plano original.</returns>
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 },
                    1000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}