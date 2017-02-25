using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.CrossCutting.Security
{
    public class Cryptography
    {
        #region Fields

        private const string keyTripleDES = "5971354930644123";

        private static byte[] sampleKeyExternalRijndael = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        private const string sampleKeyInternalRijndael = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";

        #endregion Fields

        /// <summary>
        /// Encrypts a string using the SHA1 encryption model. You can not decrypt the text.
        /// Validations must be done by encrypting a value and compared with the already encrypted value.
        /// </summary>        
        public static string EncryptSHA1(string value)
        {
            SHA1CryptoServiceProvider criptSHA1 = new SHA1CryptoServiceProvider();
            string result = "";

            byte[] bytesToHash = Encoding.ASCII.GetBytes(value);

            bytesToHash = criptSHA1.ComputeHash(bytesToHash);

            foreach (byte item in bytesToHash)
                result += item.ToString("x2");

            return result;
        }

        /// <summary>
        /// Encrypt a string using the encryption model Triple Des.
        /// </summary>        
        public static string EncryptTripleDES(string value, string key = null)
        {
            TripleDESCryptoServiceProvider criptTripleDES = new TripleDESCryptoServiceProvider();

            byte[] bytesValue = Encoding.UTF8.GetBytes(value);
            byte[] bytesKey = Encoding.UTF8.GetBytes(key ?? keyTripleDES);

            criptTripleDES.Key = bytesKey;
            criptTripleDES.Mode = CipherMode.ECB;
            criptTripleDES.Padding = PaddingMode.PKCS7;

            ICryptoTransform criptTransform = criptTripleDES.CreateEncryptor();
            byte[] result = criptTransform.TransformFinalBlock(bytesValue, 0, bytesValue.Length);

            criptTripleDES.Clear();

            return Convert.ToBase64String(result, 0, result.Length);
        }

        /// <summary>
        /// Decrypt a string using the encryption model Triple DES.
        /// </summary>        
        public static string DecryptTripleDES(string value, string key = null)
        {
            byte[] bytesKey = Encoding.UTF8.GetBytes(key ?? keyTripleDES);
            byte[] bytesValue = Convert.FromBase64String(value.Replace(" ", "+"));

            TripleDESCryptoServiceProvider criptTripleDES = new TripleDESCryptoServiceProvider();
            criptTripleDES.Key = bytesKey;
            criptTripleDES.Mode = CipherMode.ECB;
            criptTripleDES.Padding = PaddingMode.PKCS7;

            ICryptoTransform criptTransform = criptTripleDES.CreateDecryptor();
            byte[] bytesCript = criptTransform.TransformFinalBlock(bytesValue, 0, bytesValue.Length);

            criptTripleDES.Clear();

            return Encoding.UTF8.GetString(bytesCript);
        }

        /// <summary>
        /// Encrypt MD5.
        /// </summary>        
        public static string EncryptMD5(string value)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] cryptValue = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < cryptValue.Length; i++)
                strBuilder.Append(cryptValue[i].ToString("x2"));

            return strBuilder.ToString();
        }


        public static string EncryptRijndael(string value, byte[] foreignKey, string privateKey)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {

                    byte[] bKey = Convert.FromBase64String(privateKey);
                    byte[] bText = new UTF8Encoding().GetBytes(value);

                    Rijndael rijndael = new RijndaelManaged();

                    rijndael.KeySize = 256;

                    MemoryStream mStream = new MemoryStream();

                    CryptoStream encryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateEncryptor(bKey, foreignKey),
                        CryptoStreamMode.Write);

                    encryptor.Write(bText, 0, bText.Length);

                    encryptor.FlushFinalBlock();

                    return Convert.ToBase64String(mStream.ToArray());
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptRijndael(string value, byte[] foreignKey, string privateKey)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] bKey = Convert.FromBase64String(privateKey);
                    byte[] bText = Convert.FromBase64String(value);

                    Rijndael rijndael = new RijndaelManaged();

                    rijndael.KeySize = 256;

                    MemoryStream mStream = new MemoryStream();

                    CryptoStream decryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateDecryptor(bKey, foreignKey),
                        CryptoStreamMode.Write);

                    decryptor.Write(bText, 0, bText.Length);

                    decryptor.FlushFinalBlock();

                    UTF8Encoding utf8 = new UTF8Encoding();

                    return utf8.GetString(mStream.ToArray());
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetHashText(string value, HashAlgorithm algorithm)
        {
            byte[] byteText;
            StringBuilder hexHash;

            if ((algorithm != null))
            {

                algorithm.Initialize();
                                 
                byteText = value != null ? UTF8Encoding.UTF8.GetBytes(value) : new byte[0];
                
                algorithm.ComputeHash(byteText);

                hexHash = new StringBuilder();
                foreach (byte b in algorithm.Hash)
                    hexHash.Append(b.ToString("x2"));

                algorithm.Clear();

                return hexHash.ToString();
            }
            else
                
                return null;

        }
    }
}

