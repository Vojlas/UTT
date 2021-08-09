using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace UniversalTimerTool.CryptoController
{
    class CryptoController
    {
    #region AES_Crypto_Symetry
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 127, 44, 141, 129, 18, 50, 132, 178, 108, 75, 144, 40, 174, 175, 231, 150, 134, 216, 124, 35, 155, 106, 255, 175, 200, 158, 128, 97, 39, 166, 27, 132, 226, 54, 142, 223, 114, 179, 37, 71, 124, 179, 135, 230, 29, 180, 60, 172, 121, 152, 163, 182, 235, 241, 96, 213, 107, 185, 228, 118, 12, 96, 131, 132 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        try { cs.Close(); }
                        catch (Exception) { Environment.Exit(1); }
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 127, 44, 141, 129, 18, 50, 132, 178, 108, 75, 144, 40, 174, 175, 231, 150, 134, 216, 124, 35, 155, 106, 255, 175, 200, 158, 128, 97, 39, 166, 27, 132, 226, 54, 142, 223, 114, 179, 37, 71, 124, 179, 135, 230, 29, 180, 60, 172, 121, 152, 163, 182, 235, 241, 96, 213, 107, 185, 228, 118, 12, 96, 131, 132 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        /*try { */
                        cs.Close();/* }
                        catch (Exception e) { makeFailtureLog(e); Environment.Exit(1); }*/
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
        public void AES_EncryptFile(string path, string pass, string outputPath = "")
        {
            byte[] bytesToBeEncrypted = null;
            try { bytesToBeEncrypted = File.ReadAllBytes(path); }
            catch (Exception) { Environment.Exit(1); } //File not found

            byte[] passwordBytes = Encoding.UTF8.GetBytes(pass);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            if (outputPath == "") { File.WriteAllBytes(path, bytesEncrypted); }
            else { File.WriteAllBytes(outputPath, bytesEncrypted); }
        }
        public void AES_DecryptFile(string path, string pass, string outputPath = "")
        {
            byte[] bytesToBeDecrypted = null;
            try { bytesToBeDecrypted = File.ReadAllBytes(path); }
            catch (Exception) { Environment.Exit(1); }
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pass);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            try
            {
                if (outputPath == "") { File.WriteAllBytes(path, bytesDecrypted); }
                else { File.WriteAllBytes(outputPath, bytesDecrypted); }
            }
            catch (Exception e) {Environment.Exit(1); } //Path can't be reached
        }
        public string AES_EncryptText(string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public string AES_DecryptText(string input, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
        #endregion
        #region RSA_Crypto_Asymetry
        #region ____Key_Manipulation 
        /// <summary>
        /// 
        /// </summary>
        /// <returns>obj[0] return priate Key, obj[1] return public key</returns>
        public object[] RSA_GenerateKeyPair() {
            var csp = new RSACryptoServiceProvider(2048);
            var privKey = csp.ExportParameters(true);
            var publicKey = csp.ExportParameters(false);
            object[] obj = new object[2];
            obj[0] = privKey;
            obj[1] = publicKey;
            return obj;
        }
        public string RSA_ConvertPubKeyToString(RSAParameters publicKey)
        {
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, publicKey);
            return sw.ToString();
        }
        public RSAParameters RSA_ConvertPubKeyToKey(string publicKey)
        {
            var sr = new System.IO.StringReader(publicKey);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            return (RSAParameters)xs.Deserialize(sr);
        }
        #endregion
        public string RSA_Encryption(RSAParameters pubKey, string dataToEncrypt)
        {
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);
            var dataBytes = System.Text.Encoding.Unicode.GetBytes(dataToEncrypt);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(dataBytes, false);
            return Convert.ToBase64String(bytesCypherText);
        }
        public string RSA_Decryption(RSAParameters privKey, string dataToDecrypt)
        {
            var bytesCypherText = Convert.FromBase64String(dataToDecrypt);
            var csp = new RSACryptoServiceProvider();
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            var bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            return System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
        }
        #endregion
        #region CaesarCipher
        private char CaesarCrypt(char ch, int key)
        {
            if (!char.IsLetter(ch)){return ch;}
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }
        public string CaesarEncrypt(string input, int key)
        {
            string output = string.Empty;
            foreach (char ch in input)
                output += CaesarCrypt(ch, key);
            return output;
        }
        public string CaesarDecrypt(string input, int key){return CaesarEncrypt(input, 26 - key);}
        #endregion
        #region Hash
        public string ComputeSha512Hash(string rawData)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    #endregion
    }
}
