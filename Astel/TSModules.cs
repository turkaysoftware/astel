using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Astel{
    internal class TSModules{
        // LINK SYSTEM
        // ======================================================================================================
        public class TS_LinkSystem{
            public const string
            // Main Control Links
            github_link_lv      = "https://raw.githubusercontent.com/turkaysoftware/astel/main/Astel/SoftwareVersion.txt",
            github_link_lr      = "https://github.com/turkaysoftware/astel/releases/latest",
            // Social Links
            website_link        = "https://www.turkaysoftware.com",
            github_link         = "https://github.com/turkaysoftware",
            // Other Links
            ts_wizard           = "https://www.turkaysoftware.com/ts-wizard",
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
                { 1, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Information) },           // Ok ve Bilgi
                { 2, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Warning) },               // Ok ve Uyarı
                { 3, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.OK, MessageBoxIcon.Error) },                 // Ok ve Hata
                { 4, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Question) },           // Yes/No ve Soru
                { 5, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Information) },        // Yes/No ve Bilgi
                { 6, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Warning) },            // Yes/No ve Uyarı
                { 7, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNo, MessageBoxIcon.Error) },              // Yes/No ve Hata
                { 8, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) },        // Retry/Cancel ve Hata
                { 9, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) },     // Yes/No/Cancel ve Soru
                { 10, new KeyValuePair<MessageBoxButtons, MessageBoxIcon>(MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) }  // Yes/No/Cancel ve Bilgi
            };
            public static DialogResult TS_MessageBox(Form m_form, int m_mode, string m_message, string m_title = ""){
                if (m_form.InvokeRequired){
                    m_form.Invoke((Action)(() => BringFormToFront(m_form)));
                }else{
                    BringFormToFront(m_form);
                }
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
            private static void BringFormToFront(Form m_form){
                if (m_form.WindowState == FormWindowState.Minimized)
                    m_form.WindowState = FormWindowState.Normal;
                m_form.BringToFront();
                m_form.Activate();
            }
        }
        // TS SOFTWARE COPYRIGHT DATE
        // ======================================================================================================
        public class TS_SoftwareCopyrightDate{
            public static string ts_scd_preloader = string.Format("\u00a9 {0}{1}, {2}.", DateTime.Now.Year == 2025 ? "2024-" : "", DateTime.Now.Year, Application.CompanyName);
        }
        // SETTINGS SAVE PATHS & SESSION SAVE PATHS
        // ======================================================================================================
        public static string ts_df = Application.StartupPath;
        public static string ts_sf = ts_df + @"\" + Application.ProductName + "Settings.ini";
        public static string ts_settings_container = Path.GetFileNameWithoutExtension(ts_sf);
        // SETTINGS SAVE CLASS
        // ======================================================================================================
        public class TSSettingsSave{
            private readonly string _iniFilePath;
            private readonly object _fileLock = new object();
            public TSSettingsSave(string filePath) { _iniFilePath = filePath; }
            public string TSReadSettings(string sectionName, string keyName){
                lock (_fileLock){
                    if (!File.Exists(_iniFilePath)) { return string.Empty; }
                    string[] lines = File.ReadAllLines(_iniFilePath, Encoding.UTF8);
                    bool isInSection = string.IsNullOrEmpty(sectionName);
                    foreach (string rawLine in lines){
                        string line = rawLine.Trim();
                        if (line.Length == 0 || line.StartsWith(";")) { continue; }
                        if (line.StartsWith("[") && line.EndsWith("]")){
                            isInSection = line.Equals("[" + sectionName + "]", StringComparison.OrdinalIgnoreCase);
                            continue;
                        }
                        if (isInSection){
                            int equalsIndex = line.IndexOf('=');
                            if (equalsIndex > 0){
                                string currentKey = line.Substring(0, equalsIndex).Trim();
                                if (currentKey.Equals(keyName, StringComparison.OrdinalIgnoreCase)){
                                    return line.Substring(equalsIndex + 1).Trim();
                                }
                            }
                        }
                    }
                    return string.Empty;
                }
            }
            public void TSWriteSettings(string sectionName, string keyName, string value){
                lock (_fileLock){
                    List<string> lines = File.Exists(_iniFilePath) ? File.ReadAllLines(_iniFilePath, Encoding.UTF8).ToList() : new List<string>();
                    bool sectionFound = string.IsNullOrEmpty(sectionName);
                    bool keyUpdated = false;
                    int insertIndex = lines.Count;
                    for (int i = 0; i < lines.Count; i++){
                        string trimmedLine = lines[i].Trim();
                        if (trimmedLine.Length == 0 || trimmedLine.StartsWith(";")) { continue; }
                        if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]")){
                            if (sectionFound && !keyUpdated){
                                insertIndex = i;
                                break;
                            }
                            sectionFound = trimmedLine.Equals("[" + sectionName + "]", StringComparison.OrdinalIgnoreCase);
                            continue;
                        }
                        if (sectionFound){
                            int equalsIndex = trimmedLine.IndexOf('=');
                            if (equalsIndex > 0){
                                string currentKey = trimmedLine.Substring(0, equalsIndex).Trim();
                                if (currentKey.Equals(keyName, StringComparison.OrdinalIgnoreCase)){
                                    lines[i] = keyName + "=" + value;
                                    keyUpdated = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!sectionFound){
                        if (lines.Count > 0) { lines.Add(""); }
                        lines.Add("[" + sectionName + "]");
                        lines.Add(keyName + "=" + value);
                    }else if (!keyUpdated){
                        insertIndex = (insertIndex == lines.Count) ? lines.Count : insertIndex;
                        lines.Insert(insertIndex, keyName + "=" + value);
                    }
                    try{
                        File.WriteAllLines(_iniFilePath, lines, Encoding.UTF8);
                    }catch (IOException){
                        // Hata loglanabilir, bu örnekte yazdırıyoruz
                        //Console.Error.WriteLine("INI yazma hatası: " + ex.Message);
                    }
                }
            }
        }
        // READ LANG PATHS
        // ======================================================================================================
        public static string ts_lf = @"a_langs";                            // Main Path
        public static string ts_lang_en = ts_lf + @"\English.ini";          // English      | en
        public static string ts_lang_it = ts_lf + @"\Italian.ini";          // Italian      | it
        public static string ts_lang_tr = ts_lf + @"\Turkish.ini";          // Turkish      | tr
        // LANGUAGE MANAGE FUNCTIONS
        // ======================================================================================================
        public static Dictionary<string, string> AllLanguageFiles = new Dictionary<string, string> {
            { "en", ts_lang_en },
            { "it", ts_lang_it },
            { "tr", ts_lang_tr },
        };
        public static string TSPreloaderSetDefaultLanguage(string ui_lang){
            bool anyLanguageFileExists = AllLanguageFiles.Values.Any(File.Exists);
            bool isUiLangValid = !string.IsNullOrEmpty(ui_lang) && AllLanguageFiles.ContainsKey(ui_lang) && File.Exists(AllLanguageFiles[ui_lang]);
            return anyLanguageFileExists && isUiLangValid ? ui_lang : "en";
        }
        public static List<string> AvailableLanguages = AllLanguageFiles.Values.Where(filePath => File.Exists(filePath)).ToList();
        // READ LANG CLASS
        // ======================================================================================================
        public class TSGetLangs{
            private readonly string _iniFilePath;
            private readonly object _cacheLock = new object();
            private string[] _cachedLines = null;
            private DateTime _lastFileWriteTime = DateTime.MinValue;
            public TSGetLangs(string iniFilePath) { _iniFilePath = iniFilePath; }
            public string TSReadLangs(string sectionName, string keyName){
                string[] iniLines = GetIniLinesCached();
                bool isInSection = string.IsNullOrEmpty(sectionName);
                foreach (string rawLine in iniLines){
                    string line = rawLine.Trim();
                    if (line.Length == 0 || line.StartsWith(";")) { continue; }
                    if (line.StartsWith("[") && line.EndsWith("]")){
                        isInSection = line.Equals("[" + sectionName + "]", StringComparison.OrdinalIgnoreCase);
                        continue;
                    }
                    if (isInSection){
                        int eqIndex = line.IndexOf('=');
                        if (eqIndex > 0){
                            string currentKey = line.Substring(0, eqIndex).Trim();
                            if (currentKey.Equals(keyName, StringComparison.OrdinalIgnoreCase)){
                                return line.Substring(eqIndex + 1).Trim();
                            }
                        }
                    }
                }
                return string.Empty;
            }
            private string[] GetIniLinesCached(){
                lock (_cacheLock){
                    try{
                        if (!File.Exists(_iniFilePath)) { return new string[0]; }
                        DateTime currentWriteTime = File.GetLastWriteTimeUtc(_iniFilePath);
                        if (_cachedLines == null || currentWriteTime != _lastFileWriteTime){
                            _cachedLines = File.ReadAllLines(_iniFilePath, Encoding.UTF8);
                            _lastFileWriteTime = currentWriteTime;
                        }
                        return _cachedLines;
                    }catch (IOException){
                        // Console.Error.WriteLine("INI okuma hatası: " + ex.Message);
                        return new string[0];
                    }
                }
            }
        }
        // TURKISH LETTER CONVERTER
        // ======================================================================================================
        public static string ConvertTurkishCharacters(string input){
            if (string.IsNullOrWhiteSpace(input)) { return input; }
            var characterMap = new Dictionary<char, char>{
                { 'Ç', 'C' }, { 'ç', 'c' },
                { 'Ğ', 'G' }, { 'ğ', 'g' },
                { 'İ', 'I' }, { 'ı', 'i' },
                { 'Ö', 'O' }, { 'ö', 'o' },
                { 'Ş', 'S' }, { 'ş', 's' },
                { 'Ü', 'U' }, { 'ü', 'u' }
            };
            var result = new StringBuilder(input.Length);
            foreach (var c in input){
                result.Append(characterMap.TryGetValue(c, out var replacement) ? replacement : c);
            }
            return result.ToString().Trim();
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
                { "TSBT_AccentColor", Color.FromArgb(28, 122, 25) },
                { "TSBT_LabelColor1", Color.FromArgb(51, 51, 51) },
                { "TSBT_LabelColor2", Color.FromArgb(100, 100, 100) },
                { "TSBT_CloseBG", Color.FromArgb(25, 255, 255, 255) },
                { "TSBT_CloseBGHover", Color.FromArgb(50, 255, 255, 255) },
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.White },
                { "HeaderFEColor", Color.FromArgb(51, 51, 51) },
                { "HeaderFEColor2", Color.FromArgb(51, 51, 51) },
                { "HeaderBGColor2", Color.FromArgb(236, 242, 248) },
                // UI COLOR
                //
                { "SelectBoxBGColor", Color.White },
                { "SelectBoxBGColor2", Color.FromArgb(236, 242, 248) },
                { "SelectBoxFEColor", Color.FromArgb(51, 51, 51) },
                { "SelectBoxBorderColor", Color.FromArgb(223, 233, 243) },
                //
                { "PageContainerBGColor", Color.FromArgb(236, 242, 248) },
                { "PageContainerUIBGColor", Color.White },
                { "ContentLabelLeftColor", Color.FromArgb(51, 51, 51) },
                { "AccentMain", Color.FromArgb(28, 122, 25) },
                { "AccentMainHover", Color.FromArgb(33, 136, 29) },
                { "TextboxBGColor", Color.White },
                { "TextboxFEColor", Color.FromArgb(51, 51, 51) },
                { "DataGridBGColor", Color.White },
                { "DataGridFEColor", Color.FromArgb(51, 51, 51) },
                { "DataGridGridColor", Color.FromArgb(226, 226, 226) },
                { "DataGridAlternatingColor", Color.FromArgb(236, 242, 248) },
                { "DataGridHeaderBGColor", Color.FromArgb(28, 122, 25) },
                { "DataGridHeaderFEColor", Color.WhiteSmoke },
                { "DynamicThemeActiveBtnBGColor", Color.WhiteSmoke }
            };
            // DARK THEME COLORS
            // ====================================
            public static readonly Dictionary<string, Color> DarkTheme = new Dictionary<string, Color>{
                // TS PRELOADER
                { "TSBT_BGColor", Color.FromArgb(21, 23, 32) },
                { "TSBT_BGColor2", Color.FromArgb(25, 31, 42) },
                { "TSBT_AccentColor", Color.FromArgb(38, 187, 33) },
                { "TSBT_LabelColor1", Color.WhiteSmoke },
                { "TSBT_LabelColor2", Color.FromArgb(176, 184, 196) },
                { "TSBT_CloseBG", Color.FromArgb(75, 25, 31, 42) },
                { "TSBT_CloseBGHover", Color.FromArgb(100, 25, 31, 42) },
                // HEADER MENU COLOR MODE
                { "HeaderBGColor", Color.FromArgb(25, 31, 42) },
                { "HeaderFEColor", Color.WhiteSmoke },
                { "HeaderFEColor2", Color.WhiteSmoke },
                { "HeaderBGColor2", Color.FromArgb(21, 23, 32) },
                // UI COLOR
                //
                { "SelectBoxBGColor", Color.FromArgb(25, 31, 42) },
                { "SelectBoxBGColor2", Color.FromArgb(21, 23, 32) },
                { "SelectBoxFEColor", Color.WhiteSmoke },
                { "SelectBoxBorderColor", Color.FromArgb(30, 38, 53) },
                //
                { "PageContainerBGColor", Color.FromArgb(21, 23, 32) },
                { "PageContainerUIBGColor", Color.FromArgb(25, 31, 42) },
                { "ContentLabelLeftColor", Color.WhiteSmoke },
                { "AccentMain", Color.FromArgb(38, 187, 33) },
                { "AccentMainHover", Color.FromArgb(43, 206, 38) },
                { "TextboxBGColor", Color.FromArgb(25, 31, 42) },
                { "TextboxFEColor", Color.WhiteSmoke },
                { "DataGridBGColor", Color.FromArgb(25, 31, 42) },
                { "DataGridFEColor", Color.WhiteSmoke },
                { "DataGridGridColor", Color.FromArgb(36, 45, 61) },
                { "DataGridAlternatingColor", Color.FromArgb(21, 23, 32) },
                { "DataGridHeaderBGColor", Color.FromArgb(38, 187, 33) },
                { "DataGridHeaderFEColor", Color.FromArgb(21, 23, 32) },
                { "DynamicThemeActiveBtnBGColor", Color.FromArgb(21, 23, 32) }
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
        // DPI SENSITIVE DYNAMIC IMAGE RENDERER
        // ======================================================================================================
        public static void TSImageRenderer(object baseTarget, Image sourceImage, int basePadding, ContentAlignment imageAlign = ContentAlignment.MiddleCenter){
            if (sourceImage == null || baseTarget == null) return;
            const int minImageSize = 16;
            try{
                int calculatedSize;
                Image previousImage = null;
                Image ResizeImage(Image targetImg, int targetSize){
                    Bitmap resizedEngine = new Bitmap(targetSize, targetSize, PixelFormat.Format32bppArgb);
                    using (Graphics renderGraphics = Graphics.FromImage(resizedEngine)){
                        renderGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        renderGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                        renderGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        renderGraphics.CompositingQuality = CompositingQuality.HighQuality;
                        renderGraphics.DrawImage(targetImg, 0, 0, targetSize, targetSize);
                    }
                    return resizedEngine;
                }
                if (baseTarget is Control targetControl){
                    float dpi = targetControl.DeviceDpi > 0 ? targetControl.DeviceDpi : 96f;
                    float dpiScaleFactor = dpi / 96f;
                    int paddingWithScale = (int)Math.Round(basePadding * dpiScaleFactor);
                    //
                    calculatedSize = targetControl.Height - paddingWithScale;
                    if (calculatedSize <= 0) { calculatedSize = minImageSize; }
                    Image resizedImage = ResizeImage(sourceImage, calculatedSize);
                    if (targetControl is Button buttonMode){
                        previousImage = buttonMode.Image;
                        buttonMode.Image = resizedImage;
                        buttonMode.ImageAlign = imageAlign;
                    }else if (targetControl is PictureBox pictureBoxMode){
                        previousImage = pictureBoxMode.Image;
                        pictureBoxMode.Image = resizedImage;
                        pictureBoxMode.SizeMode = PictureBoxSizeMode.Zoom;
                    }else{
                        resizedImage.Dispose();
                    }
                }else if (baseTarget is ToolStripItem toolStripItemMode){
                    calculatedSize = toolStripItemMode.Height - basePadding;
                    if (calculatedSize <= 0){ calculatedSize = minImageSize; }
                    Image resizedImage = ResizeImage(sourceImage, calculatedSize);
                    previousImage = toolStripItemMode.Image;
                    toolStripItemMode.Image = resizedImage;
                }else{
                    return;
                }
                if (previousImage != null && previousImage != sourceImage){ previousImage.Dispose(); }
            }catch (Exception){ }
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
        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        public static void TSSetWindowTheme(IntPtr Handle, int theme){
            if (DwmSetWindowAttribute(Handle, theme == 1 ? 20 : 19, new[] { 1 }, 4) != theme){
                DwmSetWindowAttribute(Handle, 20, new[] { theme == 1 ? 0 : 1 }, 4);
            }
        }
        // DPI AWARE V2
        // ======================================================================================================
        [DllImport("user32.dll", PreserveSig = true)]
        public static extern bool SetProcessDpiAwarenessContext(IntPtr dpiFlag);
    }
}