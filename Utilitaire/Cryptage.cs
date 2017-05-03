using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Ecole.Utilitaire
{
    class Cryptage
    {
        public static String clefDuCryptage = "";

        public Cryptage() { 
        }

        // ************************************** DEBUT 1 *****************************

        //public static string Encrypt(string original)
        //{
        //    MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();
        //    byte[] passwordHash = hashMd5.ComputeHash(
        //    UnicodeEncoding.Unicode.GetBytes(clefDuCryptage));

        //    TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        //    des.Key = passwordHash;

        //    des.Mode = CipherMode.ECB;

        //    byte[] buffer = UnicodeEncoding.Unicode.GetBytes(original);

        //    return UnicodeEncoding.Unicode.GetString(
        //    des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));

        //}

        //public static String Decrypt(String StringToDecrypt)
        //{

        //    String StringDecrypted = "";
        //    MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();
        //    byte[] passwordHash = hashMd5.ComputeHash(
        //    UnicodeEncoding.Unicode.GetBytes(clefDuCryptage));

        //    TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        //    des.Key = passwordHash;
        //    des.Mode = CipherMode.ECB;

        //    byte[] buffer = UnicodeEncoding.Unicode.GetBytes(StringToDecrypt);
        //    StringDecrypted = UnicodeEncoding.Unicode.GetString(
        //    des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

        //    return StringDecrypted;
        //}

        // ************************************** FIN 1 *****************************



        // ************************************** DEBUT 2 *****************************
        //public static string Encrypt(string input, string key)
        //{
        //    byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
        //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        //    tripleDES.Mode = CipherMode.ECB;
        //    tripleDES.Padding = PaddingMode.PKCS7;
        //    ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        //    tripleDES.Clear();
        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}

        //public static string Decrypt(string input, string key)
        //{
        //    byte[] inputArray = Convert.FromBase64String(input);
        //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        //    tripleDES.Mode = CipherMode.ECB;
        //    tripleDES.Padding = PaddingMode.PKCS7;
        //    ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        //    tripleDES.Clear();
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}
        // **************************************  FIN 2 *****************************

        /// <summary>
        /// Crypte une chaine en utilisant un chiffreur symétrique
        /// </summary>
        /// <param name="plainText">Chaine à crypter</param>
        /// <param name="pass">Mot de passe utilisé pour dériver la clé</param>
        /// <returns>Chaine cryptée</returns>
        public static string Encrypt(string plainText, string pass)
        {
            string result = string.Empty;

            System.Security.Cryptography.TripleDESCryptoServiceProvider des =
                    new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            des.IV = new byte[8];

            System.Security.Cryptography.PasswordDeriveBytes pdb =
                    new System.Security.Cryptography.PasswordDeriveBytes(pass, new byte[0]);

            des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);

            using (MemoryStream ms = new MemoryStream(plainText.Length * 2))
            {
                using (System.Security.Cryptography.CryptoStream encStream = new
                    System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(),
                    System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    encStream.Write(plainBytes, 0, plainBytes.Length);
                    encStream.FlushFinalBlock();
                    byte[] encryptedBytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(encryptedBytes, 0, (int)ms.Length);
                    encStream.Close();
                    ms.Close();
                    result = Convert.ToBase64String(encryptedBytes);
                }
            }
            return result;
        }


        /// <summary>
        /// Décrypte une chaine cryptée à partir d'un chiffreur symétrique
        /// </summary>
        /// <param name="base64String">chaine cryptée</param>
        /// <param name="pass">Mot de passe utilisé pour dériver la clé</param>
        /// <returns>Chaine décryptée</returns>
        public static string Decrypt(string base64String, string pass)
        {
            string result = string.Empty;

            System.Security.Cryptography.TripleDESCryptoServiceProvider des =
                new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            System.Security.Cryptography.PasswordDeriveBytes pdb =
                new System.Security.Cryptography.PasswordDeriveBytes(pass, new byte[0]);
            des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);
            byte[] encryptedBytes = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream(base64String.Length))
            {
                using (System.Security.Cryptography.CryptoStream decStream =
                    new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(),
                        System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    decStream.FlushFinalBlock();
                    byte[] plainBytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(plainBytes, 0, (int)ms.Length);
                    result = Encoding.UTF8.GetString(plainBytes);
                }
            }
            return result;
        }

    }

    
}
