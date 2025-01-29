using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RML_SECURITY_ENCRYPTION
{
    public class Cls_Encryption
    {
        public static string gs_aes = null;
        public static string gs_aesIV = null;
        public static void Main()
        {
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{System.Windows.Forms.Application.StartupPath}\\_encriptyon.xml");
            }
            catch (Exception)
            {
                new Exception($"File '{System.Windows.Forms.Application.StartupPath}\\_encriptyon.xml' could not be found");
            }

            foreach (var sboElement in xmlDoc.Descendants("SBO"))
            {
                string aes = null;
                string aesIV = null;

                foreach (var addElement in sboElement.Elements("add"))
                {
                    string key = (string)addElement.Attribute("key");
                    string value = (string)addElement.Attribute("value");

                    // Asignar valores al objeto SBO
                    if (key == "aesSecret")
                    {
                        gs_aes = value;
                    }
                    else if (key == "aesIV")
                    {
                        gs_aesIV = value;
                    }
                }
            }

            if (string.IsNullOrEmpty(gs_aes) || string.IsNullOrEmpty(gs_aesIV)) { throw new Exception("Values ​​for 'aesSecret' or 'aesIV' were not placed inside the file '_encriptyon.xml'"); };
        }
        public Cls_Encryption()
        {
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{System.Windows.Forms.Application.StartupPath}\\_encriptyon.xml");
            }
            catch (Exception)
            {
                new Exception($"File '{System.Windows.Forms.Application.StartupPath}\\_encriptyon.xml' could not be found");
            }

            foreach (var sboElement in xmlDoc.Descendants("SBO"))
            {
                string aes = null;
                string aesIV = null;

                foreach (var addElement in sboElement.Elements("add"))
                {
                    string key = (string)addElement.Attribute("key");
                    string value = (string)addElement.Attribute("value");

                    // Asignar valores al objeto SBO
                    if (key == "aesSecret")
                    {
                        gs_aes = value;
                    }
                    else if (key == "aesIV")
                    {
                        gs_aesIV = value;
                    }
                }
            }

            if (string.IsNullOrEmpty(gs_aes) || string.IsNullOrEmpty(gs_aesIV)) { throw new Exception("Values ​​for 'aesSecret' or 'aesIV' were not placed inside the file '_encriptyon.xml'"); };
        }
        public string Fn_GetEncryption(string textToEncrypt)
        {
            // Contraseña que deseas encriptar (en texto claro)
            string plainTextPassword = textToEncrypt;

            // Convertir la clave secreta y el IV de cadenas a bytes
            List<Byte[]> lista = SetKey(gs_aes, gs_aesIV);

            byte[] secretKeyBytes = lista[0];
            byte[] ivBytes = lista[1];

            string encryptedPassword;
            // Crear un objeto Aes para cifrar
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secretKeyBytes;
                aesAlg.IV = ivBytes;

                // Crear un cifrador
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Convertir la contraseña en texto claro a bytes
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainTextPassword);

                // Cifrar la contraseña
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                // Convertir la contraseña cifrada a Base64 (para almacenamiento)
                encryptedPassword = Convert.ToBase64String(encryptedBytes);
            }
            return encryptedPassword;

        }
        public string Fn_GetDesEncryption(string textToDescrypt)
        {
            // Convertir la clave secreta y el IV de cadenas a bytes
            List<Byte[]> lista = SetKey(gs_aes, gs_aesIV);

            byte[] secretKeyBytes = lista[0];
            byte[] ivBytes = lista[1];

            string decryptedPassword;

            // Crear un objeto Aes para descifrar
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secretKeyBytes;
                aesAlg.IV = ivBytes;

                // Crear un descifrador
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Convertir el token cifrado (Base64) a bytes
                byte[] encryptedBytes = Convert.FromBase64String(textToDescrypt);

                // Descifrar el token
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                // Convertir los bytes descifrados a texto claro
                decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
            }
            return decryptedPassword;

        }
        public static List<Byte[]> SetKey(string securityAESSecret, string securityAESIV)
        {

            byte[] secretKey;
            byte[] iv;
            // Convierte la cadena securityAESSecret en bytes UTF-8
            byte[] keyBytes = Encoding.UTF8.GetBytes(securityAESSecret);

            // Calcula un resumen hash SHA-1 de la clave secreta
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashedKeyBytes = sha1.ComputeHash(keyBytes);

                // Ajusta la longitud de la clave a 16 bytes
                Array.Resize(ref hashedKeyBytes, 16);

                // Asigna la clave secreta
                secretKey = hashedKeyBytes;
            }

            // Convierte la cadena securityAESIV en bytes UTF-8
            byte[] ivBytes = Encoding.UTF8.GetBytes(securityAESIV);

            // Ajusta la longitud del IV a 16 bytes
            Array.Resize(ref ivBytes, 16);

            // Asigna el IV
            iv = ivBytes;

            return new List<Byte[]> { secretKey, iv };
        }
    }
}
