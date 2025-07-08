using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Astel{
    internal class TSModules{
        // LINK SYSTEM
        // ======================================================================================================
        public class TS_LinkSystem{
            public static string
            website_link        = "https://www.turkaysoftware.com",
            twitter_x_link      = "https://x.com/turkaysoftware",
            instagram_link      = "https://www.instagram.com/erayturkayy/",
            github_link         = "https://github.com/turkaysoftware",
            //
            github_link_lt      = "https://raw.githubusercontent.com/turkaysoftware/astel/main/Astel/SoftwareVersion.txt",
            github_link_lr      = "https://github.com/turkaysoftware/astel/releases/latest",
            //
            ts_wizard           = "https://www.turkaysoftware.com/ts-wizard",
            //
            ts_bmac             = "https://buymeacoffee.com/turkaysoftware";
        }
        // VERSIONS
        // ======================================================================================================
        public class TS_VersionEngine{
            public static string TS_SofwareVersion(int v_type, int v_mode){
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
        // TS MESSAGEBOX ENGINE
        // ======================================================================================================
        public static class TS_MessageBoxEngine{
            private static readonly Dictionary<int, KeyValuePair<MessageBoxButtons, MessageBoxIcon>> TSMessageBoxConfig = new Dictionary<int, KeyValuePair<MessageBoxButtons, MessageBoxIcon>>(){
                { 1, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Information) },       // Ok ve Bilgi
                { 2, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Warning) },           // Ok ve Uyarı
                { 3, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Error) },             // Ok ve Hata
                { 4, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Question) },       // Yes/No ve Soru
                { 5, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Information) },    // Yes/No ve Bilgi
                { 6, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Warning) },        // Yes/No ve Uyarı
                { 7, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Error) },          // Yes/No ve Hata
                { 8, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) },    // Retry/Cancel ve Hata
                { 9, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) }  // Yes/No/Cancel ve Soru
            };
            public static DialogResult TS_MessageBox(Form m_form, int m_mode, string m_message, string m_title = ""){
                m_form.BringToFront();
                //
                string m_box_title = string.IsNullOrEmpty(m_title) ? Application.ProductName : m_title;
                //
                MessageBoxButtons m_button = MessageBoxButtons.OK;
                MessageBoxIcon m_icon = MessageBoxIcon.Information;
                //
                if (TSMessageBoxConfig.ContainsKey(m_mode)){
                    var m_serialize = TSMessageBoxConfig[m_mode];
                    m_button = m_serialize.Key;
                    m_icon = m_serialize.Value;
                }
                //
                return MessageBox.Show(m_form, m_message, m_box_title, m_button, m_icon);
            }
        }
        // TS SOFTWARE COPYRIGHT DATE
        // ======================================================================================================
        public class TS_SoftwareCopyrightDate{
            public static string ts_scd_preloader = string.Format("\u00a9 {0}{1}, {2}.", DateTime.Now.Year == 2025 ? "2024-" : "", DateTime.Now.Year, Application.CompanyName);
        }
        // SETTINGS SAVE PATHS
        // ======================================================================================================
        public static string ts_df = Application.StartupPath;
        public static string ts_sf = ts_df + @"\" + Application.ProductName + "Settings.ini";
        public static string ts_settings_container = Path.GetFileNameWithoutExtension(ts_sf);
        // SETTINGS SAVE CLASS
        // ======================================================================================================
        public class TSSettingsSave{
            [DllImport("kernel32.dll")]
            private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32.dll")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
            private readonly string _settingFilePath;
            public TSSettingsSave(string filePath){ _settingFilePath = filePath; }
            public string TSReadSettings(string episode, string settingName){
                StringBuilder stringBuilder = new StringBuilder(4096);
                GetPrivateProfileString(episode, settingName, string.Empty, stringBuilder, 4096, _settingFilePath);
                return stringBuilder.ToString();
            }
            public int TSWriteSettings(string episode, string settingName, string value){
                return WritePrivateProfileString(episode, settingName, value, _settingFilePath);
            }
        }
        // READ LANG PATHS
        // ======================================================================================================
        public static string ts_lf = @"a_langs";                            // Main Path
        public static string ts_lang_en = ts_lf + @"\English.ini";          // English      | en
        public static string ts_lang_tr = ts_lf + @"\Turkish.ini";          // Turkish      | tr
        // READ LANG CLASS
        // ======================================================================================================
        public class TSGetLangs{
            [DllImport("kernel32.dll")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
            private readonly string _readFilePath;
            public TSGetLangs(string filePath){ _readFilePath = filePath; }
            public string TSReadLangs(string episode, string settingName){
                StringBuilder stringBuilder = new StringBuilder(4096);
                GetPrivateProfileString(episode, settingName, string.Empty, stringBuilder, 4096, _readFilePath);
                return stringBuilder.ToString();
            }
        }
        // TS STRING ENCODER
        // ======================================================================================================
        public static string TS_String_Encoder(string get_text){
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(get_text)).Trim();
        }
        // TURKISH LETTER CONVERTER
        // ======================================================================================================
        public static string TS_TR_LetterConverter(string called_text){
            if (string.IsNullOrEmpty(called_text)) { return called_text; }
            StringBuilder str_con = new StringBuilder(called_text);
            str_con.Replace('Ç', 'C').Replace('ç', 'c');
            str_con.Replace('Ğ', 'G').Replace('ğ', 'g');
            str_con.Replace('İ', 'I').Replace('ı', 'i');
            str_con.Replace('Ö', 'O').Replace('ö', 'o');
            str_con.Replace('Ş', 'S').Replace('ş', 's');
            str_con.Replace('Ü', 'U').Replace('ü', 'u');
            return str_con.ToString().Trim();
        }
        // TS THEME ENGINE
        // ======================================================================================================
        public class TS_ThemeEngine{
            // LIGHT THEME COLORS
            // ====================================
            public static readonly Dictionary<string, Color> LightTheme = new Dictionary<string, Color>{
                // TS PRELOADER
                { "TSBT_BGColor", Color.FromArgb(236, 242, 248) },
                { "TSBT_BGColor2", Color.White },
                { "TSBT_AccentColor", Color.FromArgb(14, 76, 56) },
                { "TSBT_LabelColor1", Color.FromArgb(51, 51, 51) },
                { "TSBT_LabelColor2", Color.FromArgb(100, 100, 100) },
                { "TSBT_CloseBG", Color.FromArgb(200, 255, 255, 255) },
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.White },
                { "HeaderFEColor", Color.FromArgb(51, 51, 51) },
                { "HeaderFEColor2", Color.FromArgb(51, 51, 51) },
                { "HeaderBGColor2", Color.FromArgb(236, 242, 248) },
                // UI COLOR
                { "PageContainerBGColor", Color.FromArgb(236, 242, 248) },
                { "PageContainerUIBGColor", Color.White },
                { "ContentLabelLeftColor", Color.FromArgb(51, 51, 51) },
                { "ContentLabelRightColor", Color.FromArgb(14, 76, 56) },
                { "ContentLabelRightColorHover", Color.FromArgb(18, 94, 70) },
                { "TextboxBGColor", Color.White },
                { "TextboxFEColor", Color.FromArgb(51, 51, 51) },
                { "DataGridBGColor", Color.White },
                { "DataGridFEColor", Color.FromArgb(51, 51, 51) },
                { "DataGridGridColor", Color.FromArgb(226, 226, 226) },
                { "DataGridAlternatingColor", Color.FromArgb(236, 242, 248) },
                { "DataGridHeaderBGColor", Color.FromArgb(14, 76, 56) },
                { "DataGridHeaderFEColor", Color.WhiteSmoke },
                { "DynamicThemeActiveBtnBGColor", Color.WhiteSmoke }
            };
            // DARK THEME COLORS
            // ====================================
            public static readonly Dictionary<string, Color> DarkTheme = new Dictionary<string, Color>{
                // TS PRELOADER
                { "TSBT_BGColor", Color.FromArgb(21, 23, 32) },
                { "TSBT_BGColor2", Color.FromArgb(25, 31, 42) },
                { "TSBT_AccentColor", Color.FromArgb(24, 133, 98) },
                { "TSBT_LabelColor1", Color.WhiteSmoke },
                { "TSBT_LabelColor2", Color.FromArgb(176, 184, 196) },
                { "TSBT_CloseBG", Color.FromArgb(210, 25, 31, 42) },
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.FromArgb(25, 31, 42) },
                { "HeaderFEColor", Color.WhiteSmoke },
                { "HeaderFEColor2", Color.WhiteSmoke },
                { "HeaderBGColor2", Color.FromArgb(21, 23, 32) },
                // UI COLOR
                { "PageContainerBGColor", Color.FromArgb(21, 23, 32) },
                { "PageContainerUIBGColor", Color.FromArgb(25, 31, 42) },
                { "ContentLabelLeftColor", Color.WhiteSmoke },
                { "ContentLabelRightColor", Color.FromArgb(19, 97, 72) },
                { "ContentLabelRightColorHover", Color.FromArgb(24, 116, 86) },
                { "TextboxBGColor", Color.FromArgb(25, 31, 42) },
                { "TextboxFEColor", Color.WhiteSmoke },
                { "DataGridBGColor", Color.FromArgb(25, 31, 42) },
                { "DataGridFEColor", Color.WhiteSmoke },
                { "DataGridGridColor", Color.FromArgb(36, 45, 61) },
                { "DataGridAlternatingColor", Color.FromArgb(21, 23, 32) },
                { "DataGridHeaderBGColor", Color.FromArgb(19, 97, 72) },
                { "DataGridHeaderFEColor", Color.WhiteSmoke },
                { "DynamicThemeActiveBtnBGColor", Color.WhiteSmoke }
            };
            // THEME SWITCHER
            // ====================================
            public static Color ColorMode(int theme, string key){
                if (theme == 0){
                    return DarkTheme.ContainsKey(key) ? DarkTheme[key] : Color.Transparent;
                }else if (theme == 1){
                    return LightTheme.ContainsKey(key) ? LightTheme[key] : Color.Transparent;
                }
                return Color.Transparent;
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
                for (int b = 0; b < hex_length; b += 2){
                    con_bytes[b / 2] = Convert.ToByte(get_hex.Substring(b, 2), 16);
                }
                return con_bytes;
            }
            public static string TS_AES_Encrypt(string get_plainText){
                using (Aes gen_aes = Aes.Create()){
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
        // INTERNET CONNECTION STATUS
        // ======================================================================================================
        public static bool IsNetworkCheck(){
            try{
                HttpWebRequest server_request = (HttpWebRequest)WebRequest.Create("http://clients3.google.com/generate_204");
                server_request.KeepAlive = false;
                server_request.Timeout = 2500;
                using (var server_response = (HttpWebResponse)server_request.GetResponse()){
                    return server_response.StatusCode == HttpStatusCode.NoContent;
                }
            }catch{
                return false;
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