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
        public void EncryptFile(string path, string pass, string outputPath = "")
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
        public void DecryptFile(string path, string pass, string outputPath = "")
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
        public string EncryptText(string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public string DecryptText(string input, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
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
    }
}
