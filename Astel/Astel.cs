// ======================================================================================================
// Astel - Password Management Software
// © Copyright 2024-2025, Eray Türkay.
// Project Type: Open Source
// License: MIT License
// Website: https://www.turkaysoftware.com/astel
// GitHub: https://github.com/turkaysoftware/astel
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
// TS MODULES
using Astel.astel_modules;
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel{
    public partial class Astel : Form{
        public Astel(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // GLOBAL VARIABLES
        // ======================================================================================================
        public static string lang, lang_path;
        public static int theme, initial_status, auto_backup_status;
        // LOCAL VARIABLES
        // ======================================================================================================
        string ts_wizard_name = "TS Wizard";
        Task auto_backup;
        private CancellationTokenSource cts;
        // UI COLORS
        // ======================================================================================================
        static List<Color> header_colors = new List<Color>() { Color.Transparent, Color.Transparent, Color.Transparent };
        // HEADER SETTINGS
        // ======================================================================================================
        private class HeaderMenuColors : ToolStripProfessionalRenderer{
            public HeaderMenuColors() : base(new HeaderColors()){ }
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e){ e.ArrowColor = header_colors[1]; base.OnRenderArrow(e); }
            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e){
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                float dpiScale = g.DpiX / 96f;
                // TICK BG
                // using (SolidBrush bgBrush = new SolidBrush(header_colors[0])){ RectangleF bgRect = new RectangleF( e.ImageRectangle.Left, e.ImageRectangle.Top, e.ImageRectangle.Width,e.ImageRectangle.Height); g.FillRectangle(bgBrush, bgRect); }
                using (Pen anti_alias_pen = new Pen(header_colors[2], 3 * dpiScale)){
                    Rectangle rect = e.ImageRectangle;
                    Point p1 = new Point((int)(rect.Left + 3 * dpiScale), (int)(rect.Top + rect.Height / 2));
                    Point p2 = new Point((int)(rect.Left + 7 * dpiScale), (int)(rect.Bottom - 4 * dpiScale));
                    Point p3 = new Point((int)(rect.Right - 2 * dpiScale), (int)(rect.Top + 3 * dpiScale));
                    g.DrawLines(anti_alias_pen, new Point[] { p1, p2, p3 });
                }
            }
        }
        private class HeaderColors : ProfessionalColorTable{
            public override Color MenuItemSelected => header_colors[0];
            public override Color ToolStripDropDownBackground => header_colors[0];
            public override Color ImageMarginGradientBegin => header_colors[0];
            public override Color ImageMarginGradientEnd => header_colors[0];
            public override Color ImageMarginGradientMiddle => header_colors[0];
            public override Color MenuItemSelectedGradientBegin => header_colors[0];
            public override Color MenuItemSelectedGradientEnd => header_colors[0];
            public override Color MenuItemPressedGradientBegin => header_colors[0];
            public override Color MenuItemPressedGradientMiddle => header_colors[0];
            public override Color MenuItemPressedGradientEnd => header_colors[0];
            public override Color MenuItemBorder => header_colors[0];
            public override Color CheckBackground => header_colors[0];
            public override Color ButtonSelectedBorder => header_colors[0];
            public override Color CheckSelectedBackground => header_colors[0];
            public override Color CheckPressedBackground => header_colors[0];
            public override Color MenuBorder => header_colors[0];
            public override Color SeparatorLight => header_colors[1];
            public override Color SeparatorDark => header_colors[1];
        }
        // LOAD SOFTWARE SETTINGS
        // ======================================================================================================
        private void RunSoftwareEngine(){
            // DOUBLE BUFFER TABLE
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, DataMainTable, new object[] { true });
            // TEMPORARY COLUMN
            for (int i = 1; i <= 7; i++){ DataMainTable.Columns.Add("x" + i, "x" + i); }
            //
            foreach (DataGridViewColumn DataTable in DataMainTable.Columns){
                DataTable.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // DPI SET
            BtnCopyUsername.Height = TxtUserName.Height;
            BtnCopyEmail.Height = TxtEmail.Height;
            BtnCopyPassword.Height = TxtPassword.Height;
            // THEME - LANG - VIEW MODE PRELOADER
            // ======================================================================================================
            TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
            //
            int theme_mode = int.TryParse(TS_String_Encoder(software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus")), out int the_status) ? the_status : 1;
            theme_engine(theme_mode);
            darkThemeToolStripMenuItem.Checked = theme_mode == 0;
            lightThemeToolStripMenuItem.Checked = theme_mode == 1;
            //
            string lang_mode = TS_String_Encoder(software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus"));
            var languageFiles = new Dictionary<string, (object langResource, ToolStripMenuItem menuItem, bool fileExists)>{
                { "en", (ts_lang_en, englishToolStripMenuItem, File.Exists(ts_lang_en)) },
                { "tr", (ts_lang_tr, turkishToolStripMenuItem, File.Exists(ts_lang_tr)) },
            };
            foreach (var langLoader in languageFiles) { langLoader.Value.menuItem.Enabled = langLoader.Value.fileExists; }
            var (langResource, selectedMenuItem, _) = languageFiles.ContainsKey(lang_mode) ? languageFiles[lang_mode] : languageFiles["en"];
            lang_engine(Convert.ToString(langResource), lang_mode);
            selectedMenuItem.Checked = true;
            //
            string initial_mode = TS_String_Encoder(software_read_settings.TSReadSettings(ts_settings_container, "InitialStatus"));
            initial_status = int.TryParse(initial_mode, out int ini_status) && (ini_status == 0 || ini_status == 1) ? ini_status : 0;
            WindowState = initial_status == 1 ? FormWindowState.Maximized : FormWindowState.Normal;
            windowedToolStripMenuItem.Checked = initial_status == 0;
            fullScreenToolStripMenuItem.Checked = initial_status == 1;
            //
            string abackup_mode = TS_String_Encoder(software_read_settings.TSReadSettings(ts_settings_container, "AutoBackupStatus"));
            auto_backup_status = int.TryParse(abackup_mode, out int abackup_status) && (abackup_status == 0 || abackup_status == 1) ? abackup_status : 0;
            autoDataBackupOnToolStripMenuItem.Checked = auto_backup_status == 1;
            autoDataBackupOffToolStripMenuItem.Checked = auto_backup_status == 0;
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private void Astel_Load(object sender, EventArgs e){
            Text = TS_VersionEngine.TS_SofwareVersion(0, Program.ts_version_mode);
            HeaderMenu.Cursor = Cursors.Hand;
            // PREFETCH
            ServiceListAdd();
            RunSoftwareEngine();
            // TEMPORARY COLUMN CLEAR
            DataMainTable.Columns.Clear();
            // LOGIN SECURITY
            InitializeLoaderSecurity();
            // LOAD
            AstelLoadXMLData();
            DGVColumnFormatter();
            //
            DataMainTable.Columns[0].Width = 65;
            DataMainTable.Columns[1].Width = 125;
            DataMainTable.Columns[2].Width = 190;
            DataMainTable.Columns[3].Width = 175;
            DataMainTable.Columns[4].Width = 175;
            DataMainTable.Columns[5].Width = 205;
            DataMainTable.Columns[6].Width = 210;
            // RUN TASKS
            Task softwareUpdateCheck = Task.Run(() => software_update_check(0));
            if (auto_backup_status == 1 && (auto_backup == null || auto_backup.IsCompleted)){
                cts = new CancellationTokenSource();
                auto_backup = StartAutoBackup(cts.Token);
            }
        }
        // SERVICE LIST ADD
        // ======================================================================================================
        private void ServiceListAdd(){
            string[] content_services = new string[]{
                "-", "Adobe", "Airbnb", "Amazon", "Asana", "BeReal",
                "Bing", "Bitbucket", "ChatGPT", "ClickUp", "Coursera", "Crunchyroll",
                "Deezer", "Discord", "Disney+", "Dropbox", "Duolingo", "eBay",
                "Epic Games", "Facebook", "GitHub", "Grab", "HBO Max", "Instagram",
                "LinkedIn", "LINE", "Lyft", "Medium", "MEGA", "Microsoft",
                "Miro", "Monday.com", "Netflix", "Notion", "NordVPN", "OneDrive",
                "Paramount+", "PayPal", "Peacock", "Pinterest", "Quora", "Reddit",
                "Revolut", "Roblox", "Salesforce", "Shopify", "Signal", "Slack",
                "Snapchat", "SoundCloud", "Spotify", "Stack Overflow", "Steam", "Stripe",
                "Threads", "TikTok", "Trello", "Twitch", "Twitter (X)", "Uber",
                "Udemy", "Venmo", "Viber", "Waze", "WeChat", "WhatsApp",
                "Wise", "WordPress", "Yahoo", "YouTube", "Zoom", "Google", "Apple"
            };
            Array.Sort(content_services);
            //
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(content_services);
            //
            TxtService.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtService.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtService.AutoCompleteCustomSource = autoComplete;
        }
        private XDocument InitializeAES(){
            var ts_xDoc = XDocument.Load(ts_data_xml_path);
            var root = ts_xDoc.Element("Datas");
            //
            string saltBase64 = root.Attribute("ST")?.Value.Trim();
            string passwordBase64 = root.Attribute("EK")?.Value.Trim();
            //
            byte[] salt_byte;
            string password_string;
            //
            if (string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(passwordBase64)){
                byte[] passwordBytes = new byte[32];
                salt_byte = new byte[16];
                using (var rng = RandomNumberGenerator.Create()){
                    rng.GetBytes(passwordBytes);
                    rng.GetBytes(salt_byte);
                }
                password_string = Convert.ToBase64String(passwordBytes);
                root.SetAttributeValue("EK", password_string);
                root.SetAttributeValue("ST", Convert.ToBase64String(salt_byte));
            }else{
                password_string = passwordBase64;
                salt_byte = Convert.FromBase64String(saltBase64);
            }
            using (var keyDeriver = new Rfc2898DeriveBytes(password_string, salt_byte, 7_500)){
                byte[] aesKey = keyDeriver.GetBytes(32);
                TS_AES_Encryption.SetKey(aesKey);
            }
            ts_xDoc.Save(ts_data_xml_path);
            return ts_xDoc;
        }
        private void InitializeLoaderSecurity(){
            if (!File.Exists(ts_data_xml_path)){ CreateEmptyXmlFile(); }
            //
            TSSettingsSave software_read_settings = new TSSettingsSave(ts_session_file);
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            //
            var ts_xDoc = InitializeAES();
            var root = ts_xDoc.Element("Datas");
            root.SetAttributeValue("SV", TS_VersionEngine.TS_SofwareVersion(1, Program.ts_version_mode));
            //
            string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker").Trim();
            string crossLinker64 = root.Attribute("CL")?.Value.Trim();
            if (!string.IsNullOrEmpty(crossLinker64)){
                if (crossLinker64 != saved_crossLinker64){
                    File.Delete(ts_data_xml_path);
                    CreateEmptyXmlFile();
                    InitializeAES();
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(TS_String_Encoder(software_lang.TSReadLangs("CrossLinker", "cl_message")), "\n\n", "\n\n"));
                    return;
                }
            }else{
                root.SetAttributeValue("CL", saved_crossLinker64);
                ts_xDoc.Save(ts_data_xml_path);
            }
        }
        // CREATES XML FILE IF IT IS EMPTY (ONLY WITH <Datas> ROOT)
        // ======================================================================================================
        private void CreateEmptyXmlFile(){
            string dir = Path.GetDirectoryName(ts_data_xml_path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var ts_xDoc = new XDocument(new XElement("Datas"));
            ts_xDoc.Save(ts_data_xml_path);
        }
        // LOADS THE XML DATA AND PASSES IT TO THE DATATABLE
        // ======================================================================================================
        private void AstelLoadXMLData(){
            if (!File.Exists(ts_data_xml_path)) CreateEmptyXmlFile();
            var ts_xDoc = XDocument.Load(ts_data_xml_path);
            var ts_xDoc_root = ts_xDoc.Element("Datas");
            //
            DataSet ts_dataSet = new DataSet();
            DataTable ts_dataTable = new DataTable("Datas");
            ts_dataTable.Columns.Add("ID", typeof(int));
            ts_dataTable.Columns.Add("Service", typeof(string));
            ts_dataTable.Columns.Add("Username", typeof(string));
            ts_dataTable.Columns.Add("Email", typeof(string));
            ts_dataTable.Columns.Add("Password", typeof(string));
            ts_dataTable.Columns.Add("Note", typeof(string));
            ts_dataTable.Columns.Add("PassChangeDate", typeof(string));
            //
            if (ts_xDoc_root != null){
                foreach (var ts_xml_mode in ts_xDoc_root.Elements("Data")){
                    DataRow ts_xml_row = ts_dataTable.NewRow();
                    ts_xml_row["ID"] = int.Parse(ts_xml_mode.Element("ID")?.Value ?? "0");
                    ts_xml_row["Service"] = ts_xml_mode.Element("Service") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Service").Value) : string.Empty;
                    ts_xml_row["Username"] = ts_xml_mode.Element("Username") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Username").Value) : string.Empty;
                    ts_xml_row["Email"] = ts_xml_mode.Element("Email") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Email").Value) : string.Empty;
                    ts_xml_row["Password"] = ts_xml_mode.Element("Password") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Password").Value) : string.Empty;
                    ts_xml_row["Note"] = ts_xml_mode.Element("Note") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Note").Value) : string.Empty;
                    ts_xml_row["PassChangeDate"] = ts_xml_mode.Element("PassChangeDate") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("PassChangeDate").Value) : string.Empty;
                    ts_dataTable.Rows.Add(ts_xml_row);
                }
            }
            ts_dataSet.Tables.Add(ts_dataTable);
            DataMainTable.DataSource = ts_dataSet.Tables[0];
            DataMainTable.ClearSelection();
        }
        // SECURE ID GENERATOR & REORDER ID
        // ======================================================================================================
        private static readonly object idLock = new object();
        private int TSGenerateNewID(){
            lock (idLock){
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                int xml_max_id = ts_xml_root.Elements("Data").Select(g => (int)g.Element("ID")).DefaultIfEmpty(0).Max();
                return xml_max_id + 1;
            }
        }
        private void TSReorderID(XDocument xDoc){
            var root = xDoc.Element("Datas");
            var allDataElements = root.Elements("Data").ToList();
            int counter = 1;
            foreach (var element in allDataElements){
                element.SetElementValue("ID", counter++);
            }
        }
        // ADD DATA
        // ======================================================================================================
        private void AddBtn_Click(object sender, EventArgs e){
            ProgressData(false);
        }
        // UPDATE DATA
        // ======================================================================================================
        private void UpdateBtn_Click(object sender, EventArgs e){
            ProgressData(true);
        }
        // VALIDATE AND PROGRESS FUNCTIONS
        // ======================================================================================================
        private bool ValidateInputs(out string in_service, out string in_user, out string in_email, out string in_password, out string in_note, out string errorMsg){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            in_service = string.Join(" ", TxtService.Text.Split(' ').Select(k => string.IsNullOrWhiteSpace(k) ? "" : char.ToUpper(k[0]) + k.Substring(1).ToLower()));
            in_user = TxtUserName.Text.Trim();
            in_email = TxtEmail.Text.Trim();
            in_password = TxtPassword.Text.Trim();
            in_note = TxtNote.Text.Trim();
            errorMsg = "";
            //
            if (string.IsNullOrEmpty(in_service)){
                errorMsg = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_service_info")), "\n");
                return false;
            }
            if (string.IsNullOrEmpty(in_user)){
                errorMsg = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_username_info")), "\n");
                return false;
            }
            if (string.IsNullOrEmpty(in_email)){
                errorMsg = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_email_info")), "\n");
                return false;
            }else if (in_email != "-"){
                try{
                    var mail_checker = new MailAddress(in_email);
                    if (mail_checker.Address != in_email)
                        throw new FormatException();
                }catch{
                    errorMsg = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_email_valid"));
                    return false;
                }
            }
            if (string.IsNullOrEmpty(in_password)){
                errorMsg = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_password_info"));
                return false;
            }
            return true;
        }
        private void ProgressData(bool isUpdate){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (isUpdate && DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_select_info")));
                    return;
                }
                if (!ValidateInputs(out var in_service, out var in_user, out var in_email, out var in_password, out var in_note, out var errorMsg)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, errorMsg);
                    return;
                }
                if (isUpdate){
                    var confirm = TS_MessageBoxEngine.TS_MessageBox(this, 4, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_question_info")));
                    if (confirm != DialogResult.Yes)
                        return;
                }
                //
                if (!File.Exists(ts_data_xml_path)) InitializeLoaderSecurity();
                //
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                //
                if (isUpdate){
                    int selectedId = int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString());
                    var elementToUpdate = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == selectedId);
                    //
                    if (elementToUpdate != null){
                        elementToUpdate.SetElementValue("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service));
                        elementToUpdate.SetElementValue("Username", TS_AES_Encryption.TS_AES_Encrypt(in_user));
                        elementToUpdate.SetElementValue("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email));
                        elementToUpdate.SetElementValue("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password));
                        elementToUpdate.SetElementValue("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note));
                        elementToUpdate.SetElementValue("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss")));
                    }
                }else{
                    ts_xml_root.Add(new XElement("Data",
                        new XElement("ID", TSGenerateNewID()),
                        new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service)),
                        new XElement("Username", TS_AES_Encryption.TS_AES_Encrypt(in_user)),
                        new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email)),
                        new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password)),
                        new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note)),
                        new XElement("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss")))
                    ));
                }
                //
                ts_xDoc.Save(ts_data_xml_path);
                AstelLoadXMLData();
                nodeClearInput();
                //
                TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", isUpdate ? "ah_update_success" : "ah_add_success")));
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", isUpdate ? "ah_update_failed" : "ah_add_failed")), "\n"));
            }
        }
        // DELETE DATA
        // ======================================================================================================
        private void DeleteBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_info")));
                    return;
                }
                //
                DialogResult checkDeleteQuery = TS_MessageBoxEngine.TS_MessageBox(this, 4, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_question_info")));
                if (checkDeleteQuery == DialogResult.Yes){
                    var ts_xDoc = XDocument.Load(ts_data_xml_path);
                    var ts_xml_root = ts_xDoc.Element("Datas");
                    //
                    int selectedId = int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString());
                    var elementToDelete = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == selectedId);
                    //
                    if (elementToDelete != null){
                        elementToDelete.Remove();
                    }
                    //
                    TSReorderID(ts_xDoc);
                    ts_xDoc.Save(ts_data_xml_path);
                    AstelLoadXMLData();
                    nodeClearInput();
                    //
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_success")));
                }
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_failed")), "\n"));
            }
        }
        // COPY DATA
        // ======================================================================================================
        private void BtnCopyUsername_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtUserName.Text.Trim())){
                    Clipboard.SetText(TxtUserName.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_user")));
                }
            }catch (Exception){ }
        }
        private void BtnCopyEmail_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtEmail.Text.Trim())){
                    Clipboard.SetText(TxtEmail.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_email")));
                }
            }catch (Exception){ }
        }
        private void BtnCopyPassword_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtPassword.Text.Trim())){
                    Clipboard.SetText(TxtPassword.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_password")));
                }
            }catch (Exception){ }
        }
        // TEXTBOX ROTATE DATA
        // ======================================================================================================
        private void DataMainTable_CellClick(object sender, DataGridViewCellEventArgs e){
            try{
                if (e.RowIndex >= 0){
                    DataGridViewRow xml_select_row = DataMainTable.Rows[e.RowIndex];
                    string serviceName = xml_select_row.Cells[1].Value.ToString();
                    //
                    TxtService.Text = serviceName;
                    TxtUserName.Text = xml_select_row.Cells[2].Value.ToString();
                    TxtEmail.Text = xml_select_row.Cells[3].Value.ToString();
                    TxtPassword.Text = xml_select_row.Cells[4].Value.ToString();
                    TxtNote.Text = xml_select_row.Cells[5].Value.ToString();
                    //
                   
                }
            }catch (Exception){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_data_select_error")), "\n"));
            }
        }
        // CLEAR INPUT
        // ======================================================================================================
        private void nodeClearInput(){
            TxtService.Clear();
            TxtUserName.Clear();
            TxtPassword.Clear();
            TxtEmail.Clear();
            TxtNote.Clear();
            DataMainTable.ClearSelection();
        }
        // ======================================================================================================
        // AUTO BACKUP DATA
        private async Task StartAutoBackup(CancellationToken token){
            while (!token.IsCancellationRequested){
                if (DataMainTable.Rows.Count > 0){
                    var backupFiles = Directory.Exists(ts_data_backup_folder) ? new DirectoryInfo(ts_data_backup_folder).GetFiles() : Array.Empty<FileInfo>();
                    bool shouldBackup = false;
                    if (backupFiles.Length == 0){
                        shouldBackup = true;
                    }else{
                        var lastBackupFile = backupFiles.OrderByDescending(f => f.CreationTime).First();
                        TimeSpan timeSinceLastBackup = DateTime.Now - lastBackupFile.CreationTime;
                        if (timeSinceLastBackup.TotalMinutes >= 60){
                            shouldBackup = true;
                        }
                    }
                    if (shouldBackup){
                        string backupFileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:ddMMyyyy_HHmm}_{GenerateSecureRandomString(7).Substring(3)}{ts_data_backup_extension}";
                        string backupFilePath = Path.Combine(ts_data_backup_folder, backupFileName);
                        try{
                            if (!Directory.Exists(ts_data_backup_folder)){
                                Directory.CreateDirectory(ts_data_backup_folder);
                            }
                            File.Copy(ts_data_xml_path, backupFilePath, overwrite: true);
                        }catch (Exception){ }
                    }
                }
                try{
                    await Task.Delay(60000, token);
                }catch (TaskCanceledException){
                    break;
                }
            }
        }
        // ======================================================================================================
        // THEME SETTINGS
        private void select_theme_active(object target_theme){
            ToolStripMenuItem selected_theme = null;
            select_theme_deactive();
            if (target_theme != null){
                if (selected_theme != (ToolStripMenuItem)target_theme){
                    selected_theme = (ToolStripMenuItem)target_theme;
                    selected_theme.Checked = true;
                }
            }
        }
        private void select_theme_deactive(){
            foreach (ToolStripMenuItem disabled_theme in themeToolStripMenuItem.DropDownItems){
                disabled_theme.Checked = false;
            }
        }
        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e){
            if (theme != 1){ theme_engine(1); select_theme_active(sender); }
        }
        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e){
            if (theme != 0){ theme_engine(0); select_theme_active(sender); }
        }
        private void theme_engine(int ts){
            try{
                theme = ts;
                int set_attribute = theme == 1 ? 20 : 19;
                if (DwmSetWindowAttribute(Handle, set_attribute, new[] { 1 }, 4) != theme){
                    DwmSetWindowAttribute(Handle, 20, new[] { theme == 1 ? 0 : 1 }, 4);
                }
                if (theme == 1){
                    TSImageRenderer(settingsToolStripMenuItem, Properties.Resources.tm_settings_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(themeToolStripMenuItem, Properties.Resources.tm_theme_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(languageToolStripMenuItem, Properties.Resources.tm_language_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(initialViewToolStripMenuItem, Properties.Resources.tm_startup_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(changePasswordToolStripMenuItem, Properties.Resources.tm_change_password_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(checkforUpdatesToolStripMenuItem, Properties.Resources.tm_update_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(dataTransferToolStripMenuItem, Properties.Resources.tm_data_transfer_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(exportDataToolStripMenuItem, Properties.Resources.tm_data_export_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(importDataToolStripMenuItem, Properties.Resources.tm_data_import_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(autoDataBackupToolStripMenuItem, Properties.Resources.tm_auto_backup_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(passwordGeneratorToolStripMenuItem, Properties.Resources.tm_password_generator_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(tSWizardToolStripMenuItem, Properties.Resources.tm_ts_wizard_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(bmacToolStripMenuItem, Properties.Resources.tm_bmac_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(aboutToolStripMenuItem, Properties.Resources.tm_about_light, 0, ContentAlignment.MiddleRight);
                    //
                    TSImageRenderer(AddBtn, Properties.Resources.ct_add_light, 24, ContentAlignment.MiddleLeft);
                    TSImageRenderer(UpdateBtn, Properties.Resources.ct_update_light, 24, ContentAlignment.MiddleLeft);
                    TSImageRenderer(DeleteBtn, Properties.Resources.ct_delete_light, 24, ContentAlignment.MiddleLeft);
                    //
                    TSImageRenderer(BtnCopyUsername, Properties.Resources.ct_copy_light, 12);
                    TSImageRenderer(BtnCopyEmail, Properties.Resources.ct_copy_light, 12);
                    TSImageRenderer(BtnCopyPassword, Properties.Resources.ct_copy_light, 12);
                }else if (theme == 0){
                    TSImageRenderer(settingsToolStripMenuItem, Properties.Resources.tm_settings_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(themeToolStripMenuItem, Properties.Resources.tm_theme_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(languageToolStripMenuItem, Properties.Resources.tm_language_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(initialViewToolStripMenuItem, Properties.Resources.tm_startup_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(changePasswordToolStripMenuItem, Properties.Resources.tm_change_password_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(checkforUpdatesToolStripMenuItem, Properties.Resources.tm_update_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(dataTransferToolStripMenuItem, Properties.Resources.tm_data_transfer_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(exportDataToolStripMenuItem, Properties.Resources.tm_data_export_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(importDataToolStripMenuItem, Properties.Resources.tm_data_import_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(autoDataBackupToolStripMenuItem, Properties.Resources.tm_auto_backup_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(passwordGeneratorToolStripMenuItem, Properties.Resources.tm_password_generator_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(tSWizardToolStripMenuItem, Properties.Resources.tm_ts_wizard_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(bmacToolStripMenuItem, Properties.Resources.tm_bmac_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(aboutToolStripMenuItem, Properties.Resources.tm_about_dark, 0, ContentAlignment.MiddleRight);
                    //
                    TSImageRenderer(AddBtn, Properties.Resources.ct_add_dark, 24, ContentAlignment.MiddleLeft);
                    TSImageRenderer(UpdateBtn, Properties.Resources.ct_update_dark, 24, ContentAlignment.MiddleLeft);
                    TSImageRenderer(DeleteBtn, Properties.Resources.ct_delete_dark, 24, ContentAlignment.MiddleLeft);
                    //
                    TSImageRenderer(BtnCopyUsername, Properties.Resources.ct_copy_dark, 12);
                    TSImageRenderer(BtnCopyEmail, Properties.Resources.ct_copy_dark, 12);
                    TSImageRenderer(BtnCopyPassword, Properties.Resources.ct_copy_dark, 12);
                }
                header_colors[0] = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor");
                header_colors[1] = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor");
                header_colors[2] = TS_ThemeEngine.ColorMode(theme, "AccentMain");
                HeaderMenu.Renderer = new HeaderMenuColors();
                // HEADER MENU
                HeaderMenu.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                HeaderMenu.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // TOOLTIP
                MainToolTip.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                MainToolTip.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // SETTINGS
                settingsToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                settingsToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // THEMES
                themeToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                themeToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                lightThemeToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                lightThemeToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                darkThemeToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                darkThemeToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // LANGS
                languageToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                languageToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                englishToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                englishToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                turkishToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                turkishToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // INITIAL VIEW
                initialViewToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                initialViewToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                windowedToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                windowedToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                fullScreenToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                fullScreenToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // DATA TRANSFER
                dataTransferToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                dataTransferToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                exportDataToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                exportDataToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                importDataToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                importDataToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // DATA BACKUP
                autoDataBackupToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                autoDataBackupToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                autoDataBackupOnToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                autoDataBackupOnToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                autoDataBackupOffToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                autoDataBackupOffToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                autoDataBackupFolderToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                autoDataBackupFolderToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // CHANGE PASSWORD
                changePasswordToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                changePasswordToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // UPDATE ENGINE
                checkforUpdatesToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                checkforUpdatesToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // PASSWORD GEN
                passwordGeneratorToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                passwordGeneratorToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // TS WIZARD
                tSWizardToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                tSWizardToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // BMAC
                bmacToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                bmacToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // ABOUT
                aboutToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                aboutToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // CONTENT BG
                BackColor = TS_ThemeEngine.ColorMode(theme, "PageContainerBGColor");
                // ALL LABEL
                foreach (Control control in Panel_Footer.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelLeftColor");
                    }
                }
                // ALL TEXTBOX
                foreach (Control control in Panel_Footer.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme, "TextboxFEColor");
                    }
                }
                // ALL BUTTON
                var combinedBtnsControls = FLP_Btns.Controls.Cast<Control>().Concat(Panel_Footer.Controls.Cast<Control>());
                foreach (Control control in combinedBtnsControls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme, "AccentMain");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme, "AccentMain");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme, "AccentMain");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme, "AccentMainHover");
                    }
                }
                // DATA TABLE
                DataMainTable.BackgroundColor = TS_ThemeEngine.ColorMode(theme, "DataGridBGColor");
                DataMainTable.GridColor = TS_ThemeEngine.ColorMode(theme, "DataGridGridColor");
                DataMainTable.DefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridBGColor");
                DataMainTable.DefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridFEColor");
                DataMainTable.AlternatingRowsDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridAlternatingColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderFEColor");
                DataMainTable.DefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.DefaultCellStyle.SelectionForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderFEColor");
                //
                software_other_page_preloader();
                //
                try{
                    TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                    software_setting_save.TSWriteSettings(ts_settings_container, "ThemeStatus", Convert.ToString(ts));
                }catch (Exception){ }
            }catch (Exception){ }
        }
        // LANGUAGES SETTINGS
        // ======================================================================================================
        private void select_lang_active(object target_lang){
            ToolStripMenuItem selected_lang = null;
            select_lang_deactive();
            if (target_lang != null){
                if (selected_lang != (ToolStripMenuItem)target_lang){
                    selected_lang = (ToolStripMenuItem)target_lang;
                    selected_lang.Checked = true;
                }
            }
        }
        private void select_lang_deactive(){
            foreach (ToolStripMenuItem disabled_lang in languageToolStripMenuItem.DropDownItems){
                disabled_lang.Checked = false;
            }
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e){
            if (lang != "en"){ lang_preload(ts_lang_en, "en"); select_lang_active(sender); }
        }
        private void turkishToolStripMenuItem_Click(object sender, EventArgs e){
            if (lang != "tr"){ lang_preload(ts_lang_tr, "tr"); select_lang_active(sender); }
        }
        private void lang_preload(string lang_type, string lang_code){
            lang_engine(lang_type, lang_code);
            try{
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "LanguageStatus", lang_code);
            }catch (Exception){ }
            // LANG CHANGE NOTIFICATION
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            DialogResult lang_change_message = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("LangChange", "lang_change_notification")), "\n\n", "\n\n"));
            if (lang_change_message == DialogResult.Yes) { Application.Restart(); }
        }
        private void lang_engine(string lang_type, string lang_code){
            try{
                lang_path = lang_type;
                lang = lang_code;
                // GLOBAL ENGINE
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                // SETTINGS
                settingsToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_settings"));
                // THEMES
                themeToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_theme"));
                lightThemeToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderThemes", "theme_light"));
                darkThemeToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderThemes", "theme_dark"));
                // LANGS
                languageToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_language"));
                englishToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderLangs", "lang_en"));
                turkishToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderLangs", "lang_tr"));
                // INITIAL VIEW
                initialViewToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_start"));
                windowedToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderViewMode", "header_viev_mode_windowed"));
                fullScreenToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderViewMode", "header_viev_mode_full_screen"));
                // DATA TRANSFER
                dataTransferToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_data_transfer"));
                exportDataToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_export"));
                importDataToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import"));
                // AUTO BACKUP
                autoDataBackupToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_auto_backup"));
                autoDataBackupOnToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("AutoBackup", "ab_on"));
                autoDataBackupOffToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("AutoBackup", "ab_off"));
                autoDataBackupFolderToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("AutoBackup", "ab_folder"));
                // CHANGE PASSWORD
                changePasswordToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_change_password"));
                // UPDATE CHECK
                checkforUpdatesToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_update"));
                // PASS GEN
                passwordGeneratorToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_pass_gen"));
                // TS WIZARD
                tSWizardToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_ts_wizard"));
                // BMAC
                bmacToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_bmac"));
                // ABOUT
                aboutToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_about"));
                // HOME
                DGVColumnFormatter();
                //
                LabelService.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_label_service"));
                LabelUsername.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_label_username"));
                LabelMail.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_label_mail"));
                LabelPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_label_password"));
                LabelNote.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_label_note"));
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(BtnCopyUsername, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_hover")));
                MainToolTip.SetToolTip(BtnCopyEmail, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_hover")));
                MainToolTip.SetToolTip(BtnCopyPassword, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_copy_hover")));
                //
                AddBtn.Text = " " + TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_add"));
                UpdateBtn.Text = " " + TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_update"));
                DeleteBtn.Text = " " + TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_delete"));
                //
                software_other_page_preloader();
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        // DGV COLUMN FORMATTER
        // ============================
        private void DGVColumnFormatter(){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            DataMainTable.Columns[1].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_service"));
            DataMainTable.Columns[2].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_username"));
            DataMainTable.Columns[3].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_mail"));
            DataMainTable.Columns[4].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_password"));
            DataMainTable.Columns[5].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_note"));
            DataMainTable.Columns[6].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_update_date"));
        }
        private void software_other_page_preloader(){
            // CHANGE PASSWORD PAGE
            try{
                AstelChangePassword software_change_password = new AstelChangePassword();
                string software_change_password_name = "astel_change_password";
                software_change_password.Name = software_change_password_name;
                if (Application.OpenForms[software_change_password_name] != null){
                    software_change_password = (AstelChangePassword)Application.OpenForms[software_change_password_name];
                    software_change_password.change_password_system_preloader();
                }
            }catch (Exception){ }
            // PASSWORD GENERATOR
            try{
                AstelPasswordGenerator software_pass_generator = new AstelPasswordGenerator();
                string software_pass_generator_name = "astel_password_generator";
                software_pass_generator.Name = software_pass_generator_name;
                if (Application.OpenForms[software_pass_generator_name] != null){
                    software_pass_generator = (AstelPasswordGenerator)Application.OpenForms[software_pass_generator_name];
                    software_pass_generator.password_generator_preloader();
                }
            }catch (Exception) { }
            // SOFTWARE ABOUT
            try{
                AstelAbout software_about = new AstelAbout();
                string software_about_name = "astel_about";
                software_about.Name = software_about_name;
                if (Application.OpenForms[software_about_name] != null){
                    software_about = (AstelAbout)Application.OpenForms[software_about_name];
                    software_about.about_preloader();
                }
            }catch (Exception){ }
        }
        // INITIAL SETINGS
        // ======================================================================================================
        private void select_initial_mode_active(object target_initial_mode){
            ToolStripMenuItem selected_initial_mode = null;
            select_initial_mode_deactive();
            if (target_initial_mode != null){
                if (selected_initial_mode != (ToolStripMenuItem)target_initial_mode){
                    selected_initial_mode = (ToolStripMenuItem)target_initial_mode;
                    selected_initial_mode.Checked = true;
                }
            }
        }
        private void select_initial_mode_deactive(){
            foreach (ToolStripMenuItem disabled_initial in initialViewToolStripMenuItem.DropDownItems){
                disabled_initial.Checked = false;
            }
        }
        private void windowedToolStripMenuItem_Click(object sender, EventArgs e){
            if (initial_status != 0){ initial_status = 0; initial_mode_settings("0"); select_initial_mode_active(sender); }
        }
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e){
            if (initial_status != 1){ initial_status = 1; initial_mode_settings("1"); select_initial_mode_active(sender); }
        }
        private void initial_mode_settings(string get_inital_value){
            try{
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "InitialStatus", get_inital_value);
            }catch (Exception){ }
        }
        // AUTO BACKUP SETTINGS
        // ======================================================================================================
        private void select_abackup_mode_active(object target_abackup_mode){
            ToolStripMenuItem selected_abackup_mode = null;
            select_abackup_mode_deactive();
            if (target_abackup_mode != null){
                if (selected_abackup_mode != (ToolStripMenuItem)target_abackup_mode){
                    selected_abackup_mode = (ToolStripMenuItem)target_abackup_mode;
                    selected_abackup_mode.Checked = true;
                }
            }
        }
        private void select_abackup_mode_deactive(){
            foreach (ToolStripMenuItem disabled_abackup in autoDataBackupToolStripMenuItem.DropDownItems){
                disabled_abackup.Checked = false;
            }
        }
        private void autoDataBackupOnToolStripMenuItem_Click(object sender, EventArgs e){
            if (auto_backup_status != 1){
                auto_backup_status = 1;
                auto_backup_mode_settings("1");
                select_abackup_mode_active(sender);
                try{
                    if (auto_backup == null || auto_backup.IsCompleted){
                        cts = new CancellationTokenSource();
                        auto_backup = StartAutoBackup(cts.Token);
                    }
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    DialogResult open_backup_folder_query = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AutoBackup", "ab_info")), "\n\n", ts_data_backup_folder, "\n\n"));
                    if (open_backup_folder_query == DialogResult.Yes){
                        backup_folder_open();
                    }
                }catch (Exception){ }
            }
        }
        private void autoDataBackupOffToolStripMenuItem_Click(object sender, EventArgs e){
            if (auto_backup_status != 0){ auto_backup_status = 0; auto_backup_mode_settings("0"); select_abackup_mode_active(sender); StopAutoBackup(); }
            }
        private void auto_backup_mode_settings(string get_abackup_value){
            try{
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "AutoBackupStatus", get_abackup_value);
            }catch (Exception){ }
        }
        private void autoDataBackupFolderToolStripMenuItem_Click(object sender, EventArgs e){
            backup_folder_open();
        }
        private void backup_folder_open(){
            try{
                string folderPath = Path.GetFullPath(ts_data_backup_folder.Trim());
                Process.Start(new ProcessStartInfo("explorer.exe", folderPath){ UseShellExecute = true });
            }catch (Exception){ }
        }
        // SOFTWARE OPERATION CONTROLLER MODULE
        // ======================================================================================================
        private static bool software_operation_controller(string __target_software_path){
            var exeFiles = Directory.GetFiles(__target_software_path, "*.exe");
            var runned_process = Process.GetProcesses();
            foreach (var exe_path in exeFiles){
                string exe_name = Path.GetFileNameWithoutExtension(exe_path);
                if (runned_process.Any(p => {
                    try{
                        return string.Equals(p.ProcessName, exe_name, StringComparison.OrdinalIgnoreCase);
                    }catch{
                        return false;
                    }
                })){
                    return true;
                }
            }
            return false;
        }
        // TS WIZARD STARTER MODE
        // ======================================================================================================
        private string[] ts_wizard_starter_mode(){
            string[] ts_wizard_exe_files = { "TSWizard_arm64.exe", "TSWizard_x64.exe", "TSWizard.exe" };
            if (RuntimeInformation.OSArchitecture == Architecture.Arm64){
                return new[] { ts_wizard_exe_files[0], ts_wizard_exe_files[1], ts_wizard_exe_files[2] }; // arm64 > x64 > default
            }else if (Environment.Is64BitOperatingSystem){
                return new[] { ts_wizard_exe_files[1], ts_wizard_exe_files[0], ts_wizard_exe_files[2] }; // x64 > arm64 > default
            }else{
                return new[] { ts_wizard_exe_files[2], ts_wizard_exe_files[1], ts_wizard_exe_files[0] }; // default > x64 > arm64
            }
        }
        // UPDATE CHECK ENGINE
        // ======================================================================================================
        private void checkforUpdatesToolStripMenuItem_Click(object sender, EventArgs e){
            software_update_check(1);
        }
        public void software_update_check(int _check_update_ui){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                if (!IsNetworkCheck()){
                    if (_check_update_ui == 1){
                        TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_not_connection")), "\n\n"), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                    }
                    return;
                }
                using (WebClient webClient = new WebClient()){
                    string client_version = TS_VersionEngine.TS_SofwareVersion(2, Program.ts_version_mode).Trim();
                    int client_num_version = Convert.ToInt32(client_version.Replace(".", string.Empty));
                    //
                    string[] version_content = webClient.DownloadString(TS_LinkSystem.github_link_lv).Split('=');
                    string last_version = version_content[1].Trim();
                    int last_num_version = Convert.ToInt32(last_version.Replace(".", string.Empty));
                    //
                    if (client_num_version < last_num_version){
                        try{
                            string baseDir = Path.Combine(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName).FullName);
                            string ts_wizard_path = ts_wizard_starter_mode().Select(name => Path.Combine(baseDir, name)).FirstOrDefault(File.Exists);
                            //
                            if (ts_wizard_path != null){
                                if (!software_operation_controller(Path.GetDirectoryName(ts_wizard_path))){
                                    // Update available
                                    DialogResult info_update = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_available_ts_wizard")), Application.ProductName, "\n\n", client_version, "\n", last_version, "\n\n", ts_wizard_name), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                                    if (info_update == DialogResult.Yes){
                                        Process.Start(new ProcessStartInfo { FileName = ts_wizard_path, WorkingDirectory = Path.GetDirectoryName(ts_wizard_path) });
                                    }
                                }else{
                                    TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), ts_wizard_name));
                                }
                            }else{
                                // Update available
                                DialogResult info_update = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_available")), Application.ProductName, "\n\n", client_version, "\n", last_version, "\n\n"), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                                if (info_update == DialogResult.Yes){
                                    Process.Start(new ProcessStartInfo(TS_LinkSystem.github_link_lr) { UseShellExecute = true });
                                }
                            }
                        }catch (Exception){ }
                    }else if (_check_update_ui == 1 && client_num_version == last_num_version){
                        // No update available
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_not_available")), Application.ProductName, "\n", client_version), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                    }else if (_check_update_ui == 1 && client_num_version > last_num_version){
                        // Access before public use
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_newer")), "\n\n", string.Format("v{0}", client_version)), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                    }
                }
            }catch (Exception ex){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_error")), "\n\n", ex.Message), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
            }
        }
        // DATA TRANSFER
        // ======================================================================================================
        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            if (!File.Exists(ts_data_xml_path)) return;
            using (var sfd = new SaveFileDialog()){
                sfd.Title = string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_save_location")), Application.ProductName);
                sfd.Filter = string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_save_file_name")), Application.ProductName, string.Format("(*{0})|*{1}", ts_data_backup_extension, ts_data_backup_extension)); ;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.FileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:dd.MM.yyyy_HH_mm}{ts_data_backup_extension}";
                if (sfd.ShowDialog() == DialogResult.OK){
                    try{
                        File.Copy(ts_data_xml_path, sfd.FileName, true);
                        DialogResult open_export_file = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_export_success")), "\n\n", sfd.FileName.Trim(), "\n\n"));
                        if (open_export_file == DialogResult.Yes){
                            string export_file_explorer = $"/select, \"{Path.GetFullPath(sfd.FileName.Trim())}\"";
                            Process.Start(new ProcessStartInfo("explorer.exe", export_file_explorer) { UseShellExecute = true });
                        }
                    }catch (Exception ex){
                        TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_export_failed")), "\n", "\n\n", ex.Message));
                    }
                }
            }
        }
        private void ImportFromFile(string filePath){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            DialogResult import_warning = TS_MessageBoxEngine.TS_MessageBox(this, 6, string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import_warning")), "\n\n", "\n\n"));
            if (import_warning != DialogResult.Yes) return;
            try{
                string target_data = Path.Combine(ts_session_root_path, "AstelData.xml");
                File.Copy(filePath, target_data, true);
                //
                bool fileReady = false;
                int attempts = 0;
                while (!fileReady && attempts < 10){
                    try{
                        using (var stream = File.Open(ts_data_xml_path, FileMode.Open, FileAccess.Read, FileShare.None)){
                            fileReady = true;
                        }
                    }catch (IOException){
                        attempts++;
                        Thread.Sleep(50);
                    }
                }
                //
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_session_file);
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var root = ts_xDoc.Element("Datas");
                string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker");
                root.SetAttributeValue("CL", saved_crossLinker64);
                ts_xDoc.Save(ts_data_xml_path);
                //
                InitializeLoaderSecurity();
                AstelLoadXMLData();
                nodeClearInput();
                //
                TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import_success")));
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import_failed")), "\n", "\n\n", ex.Message));
            }
        }
        private void importDataToolStripMenuItem_Click(object sender, EventArgs e){
            using (var ofd = new OpenFileDialog()){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                ofd.Title = string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import_location")), Application.ProductName, ts_data_backup_extension);
                ofd.Filter = string.Format(TS_String_Encoder(software_lang.TSReadLangs("DataTransfer", "hdt_import_file_name")), Application.ProductName, string.Format("(*{0})|*{1}", ts_data_backup_extension, ts_data_backup_extension));
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK){
                    ImportFromFile(ofd.FileName);
                }
            }
        }
        // DRAG & DROP IMPORT DATA FEATURE
        // ==========================
        private void Astel_DragEnter(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                string[] astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (astel_file.Length == 1 && !Directory.Exists(astel_file[0])){
                    if (Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension){
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }
        private void Astel_DragDrop(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                var astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (astel_file.Length == 1 && Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension){
                    ImportFromFile(astel_file[0]);
                }
            }
        }
        // TS TOOL LAUNCHER MODULE
        // ======================================================================================================
        private void TSToolLauncher<T>(string formName, string langKey) where T : Form, new(){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                T tool = new T { Name = formName };
                if (Application.OpenForms[formName] == null){
                    tool.Show();
                }else{
                    if (Application.OpenForms[formName].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[formName].WindowState = FormWindowState.Normal;
                    }
                    string public_message = string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", langKey)));
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, public_message);
                    Application.OpenForms[formName].Activate();
                }
            }catch (Exception){ }
        }
        // CHANGE PASSWORD
        // ======================================================================================================
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelChangePassword>("astel_change_password", "header_menu_change_password");
        }
        // PASSWORD GENERATOR
        // ======================================================================================================
        private void passwordGeneratorToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelPasswordGenerator>("astel_password_generator", "header_menu_pass_gen");
        }
        // BUY ME A COFFEE LINK
        // ======================================================================================================
        private void bmacToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                Process.Start(new ProcessStartInfo(TS_LinkSystem.ts_bmac) { UseShellExecute = true });
            }catch (Exception) { }
        }
        // TS WIZARD
        // ======================================================================================================
        private void tSWizardToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                string baseDir = Path.Combine(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName).FullName);
                string ts_wizard_path = ts_wizard_starter_mode().Select(name => Path.Combine(baseDir, name)).FirstOrDefault(File.Exists);
                //
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                //
                if (ts_wizard_path != null){
                    if (!software_operation_controller(Path.GetDirectoryName(ts_wizard_path))){
                        Process.Start(new ProcessStartInfo { FileName = ts_wizard_path, WorkingDirectory = Path.GetDirectoryName(ts_wizard_path) });
                    }else{
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), ts_wizard_name));
                    }
                }else{
                    DialogResult ts_wizard_query = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("TSWizard", "tsw_content")), TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_ts_wizard")), Application.CompanyName, "\n\n", Application.ProductName, Application.CompanyName, "\n\n"), string.Format(TS_String_Encoder(software_lang.TSReadLangs("TSWizard", "tsw_title")), Application.ProductName));
                    if (ts_wizard_query == DialogResult.Yes){
                        Process.Start(new ProcessStartInfo(TS_LinkSystem.ts_wizard) { UseShellExecute = true });
                    }
                }
            }catch (Exception){ }
        }
        // ABOUT PAGE
        // ======================================================================================================
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelAbout>("astel_about", "header_menu_about");
        }
        // EXIT
        // ======================================================================================================
        private void StopAutoBackup(){
            try{
                if (cts != null){
                    cts.Cancel();
                    auto_backup?.Wait();
                    auto_backup = null;
                    cts.Dispose();
                    cts = null;
                }
            }catch (Exception){ }
        }
        private void Astel_FormClosing(object sender, FormClosingEventArgs e){ StopAutoBackup(); Application.Exit(); }
    }
}