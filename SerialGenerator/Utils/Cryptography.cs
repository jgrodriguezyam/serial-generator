using System;
using System.Security.Cryptography;
using System.Text;

namespace SerialGenerator.Utils
{
    public static class Cryptography
    {
        public static string Encrypt(string code, string company, string applicationKey)
        {
            var codeAndCompany = string.Format("{0} {1}", code, company);
            var encryptArray = UTF8Encoding.UTF8.GetBytes(codeAndCompany);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(applicationKey));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider
                       {
                           Key = keyArray,
                           Mode = CipherMode.ECB,
                           Padding = PaddingMode.PKCS7
                        };
            var cTransform = tdes.CreateEncryptor();
            var arrayResultado = cTransform.TransformFinalBlock(encryptArray, 0, encryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(arrayResultado, 0, arrayResultado.Length);
        }

        public static string Decrypt(string codeEncrypt, string applicationKey)
        {
            var arrayToDecrypt = Convert.FromBase64String(codeEncrypt);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(applicationKey));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider
                       {
                           Key = keyArray,
                           Mode = CipherMode.ECB,
                           Padding = PaddingMode.PKCS7
                        };
            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(arrayToDecrypt, 0, arrayToDecrypt.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        } 
    }
}