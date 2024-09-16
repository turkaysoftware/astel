using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Astel{
    internal class TSModules{
        // LINK SYSTEM
        // ======================================================================================================
        public class TS_LinkSystem{
            public string
            website_link = "https://www.turkaysoftware.com",
            github_link = "https://github.com/turkaysoftware",
            twitter_link = "https://x.com/turkaysoftware",
            //
            github_link_lt = "https://raw.githubusercontent.com/turkaysoftware/astel/main/Astel/SoftwareVersion.txt",
            github_link_lr = "https://github.com/turkaysoftware/astel/releases/latest";
        }
        // VERSIONS
        // ======================================================================================================
        public class TS_VersionEngine{
            public string TS_SofwareVersion(int v_type, int v_mode){
                string version_mode = "";
                string versionSubstring = v_mode == 0 ? Application.ProductVersion.Substring(0, 5) : Application.ProductVersion.Substring(0, 7);
                switch (v_type){
                    case 0:
                        version_mode = v_mode == 0 ? $"{Application.ProductName} - v{versionSubstring}" : $"{Application.ProductName} - v{Application.ProductVersion.Substring(0, 7)}";
                        break;
                    case 1:
                        version_mode = $"v{versionSubstring}";
                        break;
                    case 2:
                        version_mode = versionSubstring;
                        break;
                    default:
                        break;
                }
                return version_mode;
            }
        }
        // LANG PATHS
        // ======================================================================================================
        public static string astel_lf = @"a_langs";                                 // Main Path
        public static string astel_lang_en = astel_lf + @"\English.ini";            // English      | en
        public static string astel_lang_tr = astel_lf + @"\Turkish.ini";            // Turkish      | tr
        public class TSGetLangs{
            [DllImport("kernel32.dll")]
            private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
            private readonly string _saveFilePath;
            public TSGetLangs(string filePath){ _saveFilePath = filePath; }
            public string TSReadLangs(string episode, string settingName){
                StringBuilder stringBuilder = new StringBuilder(512);
                GetPrivateProfileString(episode, settingName, string.Empty, stringBuilder, 511, _saveFilePath);
                return stringBuilder.ToString();
            }
        }
        // SAVE PATHS
        // ======================================================================================================
        public static string ts_df = Application.StartupPath;
        public static string ts_sf = ts_df + @"\AstelSettings.ini";
        public static string ts_settings_container = Path.GetFileNameWithoutExtension(ts_sf);
        // GLOW SETTINGS SAVE CLASS
        // ======================================================================================================
        public class TSSettingsSave{
            [DllImport("kernel32.dll")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32.dll")]
            private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
            private readonly string _saveFilePath;
            public TSSettingsSave(string filePath){ _saveFilePath = filePath; }
            public string TSReadSettings(string episode, string settingName){
                StringBuilder stringBuilder = new StringBuilder(512);
                GetPrivateProfileString(episode, settingName, string.Empty, stringBuilder, 511, _saveFilePath);
                return stringBuilder.ToString();
            }
            public long TSWriteSettings(string episode, string settingName, string value){
                return WritePrivateProfileString(episode, settingName, value, _saveFilePath);
            }
        }
        // TS AES256 ALGORITHM
        // ======================================================================================================
        public class TS_AES_Encryption{
            // 32 byte (256-bit) Key
            private static readonly string AESKey = "4f5a2e8d8c9f0b1a7d3e7b5a9f1c3d4e5b6a7f9c0b1a2c3d4e5f6a7b8c9d0e1f";
            // 16 byte (128-bit) IV
            private static readonly string AESIV = "1a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d";
            private static byte[] TSHexStringToByteArray(string get_hex){
                int hex_length = get_hex.Length;
                byte[] con_bytes = new byte[hex_length / 2];
                for (int b = 0; b < hex_length; b += 2)
                {
                    con_bytes[b / 2] = Convert.ToByte(get_hex.Substring(b, 2), 16);
                }
                return con_bytes;
            }
            public static string TS_AES_Encrypt(string get_plainText){
                using (Aes gen_aes = Aes.Create())
                {
                    gen_aes.Key = TSHexStringToByteArray(AESKey);
                    gen_aes.IV = TSHexStringToByteArray(AESIV);
                    gen_aes.Mode = CipherMode.CBC;
                    ICryptoTransform transform_encryptor = gen_aes.CreateEncryptor(gen_aes.Key, gen_aes.IV);
                    using (MemoryStream transform_ms = new MemoryStream()){
                        using (CryptoStream transform_cs = new CryptoStream(transform_ms, transform_encryptor, CryptoStreamMode.Write)){
                            using (StreamWriter transform_sw = new StreamWriter(transform_cs)){
                                transform_sw.Write(get_plainText);
                            }
                        }
                        return Convert.ToBase64String(transform_ms.ToArray());
                    }
                }
            }
            public static string TS_AES_Decrypt(string cipherText){
                using (Aes gen_aes = Aes.Create()){
                    gen_aes.Key = TSHexStringToByteArray(AESKey);
                    gen_aes.IV = TSHexStringToByteArray(AESIV);
                    gen_aes.Mode = CipherMode.CBC;
                    ICryptoTransform transform_decryptor = gen_aes.CreateDecryptor(gen_aes.Key, gen_aes.IV);
                    using (MemoryStream transform_ms = new MemoryStream(Convert.FromBase64String(cipherText))){
                        using (CryptoStream transform_cs = new CryptoStream(transform_ms, transform_decryptor, CryptoStreamMode.Read)){
                            using (StreamReader transform_sr = new StreamReader(transform_cs)){
                                return transform_sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
        // SHA512 AND SALT HASHING
        // ======================================================================================================
        public static string ts_hash_salting = "turkaysoftware";
        public static string TSHashPassword(string password, string salt){
            using (SHA512 sha512 = SHA512.Create()){
                byte[] inputBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                return TSHashStringRotate(hashBytes);
            }
        }
        private static string TSHashStringRotate(byte[] hash_bytes){
            StringBuilder hash = new StringBuilder(256);
            foreach (byte be in hash_bytes){
                hash.Append(be.ToString("X2").ToLower());
            }
            return hash.ToString();
        }
        // TS THEME ENGINE
        // ======================================================================================================
        public class TS_ThemeEngine{
            // Light Theme Colors
            public static readonly Dictionary<string, Color> LightTheme = new Dictionary<string, Color>{
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.FromArgb(222, 222, 222) },
                { "HeaderFEColor", Color.FromArgb(31, 31, 31) },

                // HEADER AND TOOLTIP COLOR MODE
                { "HeaderFEColor2", Color.FromArgb(32, 32, 32) },
                { "HeaderBGColor2", Color.FromArgb(235, 235, 235) },

                // CONTENT BG COLOR MODE
                { "PageContainerBGColor", Color.WhiteSmoke },

                // UI COLOR MODES
                { "ContentLabelLeftColor", Color.FromArgb(32, 32, 32) },
                { "ContentLabelRightColor", Color.FromArgb(14, 76, 56) },
                { "TextboxBGColor", Color.White },
                { "TextboxFEColor", Color.FromArgb(32, 32, 32) },
                { "DataGridBGColor", Color.White },
                { "DataGridFEColor", Color.FromArgb(32, 32, 32) },
                { "DataGridGridColor", Color.FromArgb(217, 217, 217) },
                { "DataGridAlternatingColor", Color.FromArgb(235, 235, 235) },
                { "DataGridHeaderBGColor", Color.FromArgb(14, 76, 56) },
                { "DataGridHeaderFEColor", Color.WhiteSmoke },
                { "DynamicThemeActiveBtnBGColor", Color.WhiteSmoke }
            };
            // Dark Theme Colors
            public static readonly Dictionary<string, Color> DarkTheme = new Dictionary<string, Color>{
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.FromArgb(31, 31, 31) },
                { "HeaderFEColor", Color.FromArgb(222, 222, 222) },

                // HEADER AND TOOLTIP COLOR MODE
                { "HeaderFEColor2", Color.WhiteSmoke },
                { "HeaderBGColor2", Color.FromArgb(24, 24, 24) },

                // CONTENT BG COLOR MODE
                { "PageContainerBGColor", Color.FromArgb(31, 31, 31) },

                // UI COLOR MODES
                { "ContentLabelLeftColor", Color.WhiteSmoke },
                { "ContentLabelRightColor", Color.FromArgb(19, 97, 72) },
                { "TextboxBGColor", Color.FromArgb(24, 24, 24) },
                { "TextboxFEColor", Color.WhiteSmoke },
                { "DataGridBGColor", Color.FromArgb(24, 24, 24) },
                { "DataGridFEColor", Color.WhiteSmoke },
                { "DataGridGridColor", Color.FromArgb(50, 50, 50) },
                { "DataGridAlternatingColor", Color.FromArgb(31, 31, 31) },
                { "DataGridHeaderBGColor", Color.FromArgb(19, 97, 72) },
                { "DataGridHeaderFEColor", Color.WhiteSmoke },
                { "DynamicThemeActiveBtnBGColor", Color.WhiteSmoke }
            };
            // Method to get color for the current theme
            public static Color ColorMode(int theme, string key){
                if (theme == 0){
                    return DarkTheme.ContainsKey(key) ? DarkTheme[key] : Color.Transparent;
                }else if (theme == 1){
                    return LightTheme.ContainsKey(key) ? LightTheme[key] : Color.Transparent;
                }
                return Color.Transparent;
            }
        }
        // TITLE BAR SETTINGS DWM API
        // ======================================================================================================
        [DllImport("DwmApi")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        // DPI AWARE
        // ======================================================================================================
        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
    }
}