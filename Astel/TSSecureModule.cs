using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Astel{
    internal class TSSecureModule{
        // GLOBAL VARIABLES
        // ======================================================================================================
        public static string ts_session_root_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.CompanyName.Replace(" ", "").Replace("ü", "u"), Application.ProductName);
        public static string ts_data_xml_path = Path.Combine(ts_session_root_path, $"{Application.ProductName}Data.xml");
        public static string ts_session_file = Path.Combine(ts_session_root_path, $"{Application.ProductName}Session.ini");
        public static string ts_session_container = Path.GetFileNameWithoutExtension(ts_session_file);
        public static string ts_data_backup_folder = Path.Combine(ts_session_root_path, $"backups");
        public static string ts_data_backup_extension = ".astel";
        // AES ENCRYPTION & DECRYPTION
        // ======================================================================================================
        public class TS_AES_Encryption{
            private static byte[] AESKey;
            public static void SetKey(byte[] key) { AESKey = key; }
            public static string TS_AES_Encrypt(string get_plainText){
                if (AESKey == null) throw new Exception("AES Key is not set.");
                //
                using (Aes gen_aes = Aes.Create()){
                    gen_aes.Key = AESKey;
                    gen_aes.Mode = CipherMode.CBC;
                    gen_aes.Padding = PaddingMode.PKCS7;
                    //
                    gen_aes.GenerateIV();
                    //
                    ICryptoTransform transform_encryptor = gen_aes.CreateEncryptor(gen_aes.Key, gen_aes.IV);
                    using (MemoryStream transform_ms = new MemoryStream()){
                        transform_ms.Write(gen_aes.IV, 0, gen_aes.IV.Length);
                        //
                        using (CryptoStream transform_cs = new CryptoStream(transform_ms, transform_encryptor, CryptoStreamMode.Write))
                        using (StreamWriter transform_sw = new StreamWriter(transform_cs)){
                            transform_sw.Write(get_plainText);
                        }
                        //
                        return Convert.ToBase64String(transform_ms.ToArray());
                    }
                }
            }
            public static string TS_AES_Decrypt(string cipherText){
                if (AESKey == null) throw new Exception("AES Key is not set.");
                //
                byte[] fullCipher = Convert.FromBase64String(cipherText);
                //
                using (Aes gen_aes = Aes.Create()){
                    gen_aes.Key = AESKey;
                    gen_aes.Mode = CipherMode.CBC;
                    gen_aes.Padding = PaddingMode.PKCS7;
                    //
                    byte[] iv = new byte[16];
                    Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                    gen_aes.IV = iv;
                    //
                    ICryptoTransform transform_decryptor = gen_aes.CreateDecryptor(gen_aes.Key, gen_aes.IV);
                    //
                    using (MemoryStream transform_ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                    using (CryptoStream transform_cs = new CryptoStream(transform_ms, transform_decryptor, CryptoStreamMode.Read))
                    using (StreamReader transform_sr = new StreamReader(transform_cs)){
                        return transform_sr.ReadToEnd();
                    }
                }
            }
        }
        // ADVANCED SHA512 ENCRYPTION
        // ======================================================================================================
        public static string TSHashPassword(string password, string saltBase64, int iterations = 7_500){
            byte[] saltBytes = Convert.FromBase64String(saltBase64);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations)){
                byte[] hash = pbkdf2.GetBytes(64);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
        // RANDOM SALT GENERATOR
        // ======================================================================================================
        public static string GenerateSalt(int size = 16){
            using (var rng = new RNGCryptoServiceProvider()){
                byte[] saltBytes = new byte[size];
                rng.GetBytes(saltBytes);
                return BitConverter.ToString(saltBytes).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
        // GENERATE SECURE RANDOM STRING
        // ======================================================================================================
        public static string GenerateSecureRandomString(int str_length){
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = RandomNumberGenerator.Create()){
                var data = new byte[str_length];
                rng.GetBytes(data);
                var result = new char[str_length];
                for (int i = 0; i < str_length; i++){
                    result[i] = chars[data[i] % chars.Length];
                }
                return "ts_" + new string(result);
            }
        }
    }
}