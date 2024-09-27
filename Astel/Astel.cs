using System;
using System.IO;
using System.Net;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
//
using static Astel.TSModules;
using Astel.astel_modules;

namespace Astel{
    public partial class Astel : Form{
        public Astel(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // GLOBAL VARIABLES
        // ======================================================================================================
        public static string lang, lang_path;
        public static int theme, ts_version_mode = 0, initial_status;
        // SOFTWARE VERSION - MEDIA LINK SYSTEM
        // ======================================================================================================
        static TS_VersionEngine TS_SoftwareVersion = new TS_VersionEngine();
        static TS_LinkSystem TS_LinkSystem = new TS_LinkSystem();
        // UI COLORS
        // ======================================================================================================
        static List<Color> header_colors = new List<Color>() { Color.Transparent, Color.Transparent };
        // HEADER SETTINGS
        // ======================================================================================================
        private class HeaderMenuColors : ToolStripProfessionalRenderer{
            public HeaderMenuColors() : base(new HeaderColors()){ }
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e){ e.ArrowColor = header_colors[1]; base.OnRenderArrow(e); }
        }
        private class HeaderColors : ProfessionalColorTable{
            public override Color MenuItemSelected { get { return header_colors[0]; } }
            public override Color ToolStripDropDownBackground { get { return header_colors[0]; } }
            public override Color ImageMarginGradientBegin { get { return header_colors[0]; } }
            public override Color ImageMarginGradientEnd { get { return header_colors[0]; } }
            public override Color ImageMarginGradientMiddle { get { return header_colors[0]; } }
            public override Color MenuItemSelectedGradientBegin { get { return header_colors[0]; } }
            public override Color MenuItemSelectedGradientEnd { get { return header_colors[0]; } }
            public override Color MenuItemPressedGradientBegin { get { return header_colors[0]; } }
            public override Color MenuItemPressedGradientMiddle { get { return header_colors[0]; } }
            public override Color MenuItemPressedGradientEnd { get { return header_colors[0]; } }
            public override Color MenuItemBorder { get { return header_colors[0]; } }
            public override Color CheckBackground { get { return header_colors[0]; } }
            public override Color ButtonSelectedBorder { get { return header_colors[0]; } }
            public override Color CheckSelectedBackground { get { return header_colors[0]; } }
            public override Color CheckPressedBackground { get { return header_colors[0]; } }
            public override Color MenuBorder { get { return header_colors[0]; } }
            public override Color SeparatorLight { get { return header_colors[1]; } }
            public override Color SeparatorDark { get { return header_colors[1]; } }
        }
        // ======================================================================================================
        // SOFTWARE PRELOADER
        /*
            -- THEME --      |  -- LANGUAGE --    |   -- INITIAL MODE --
            0 = Dark Theme   |  Moved to          |   0 = Normal Windowed
            1 = Light Theme  |  TSModules.cs   |   1 = FullScreen Mode
        */
        private void software_preloader(){
            try{
                //
                bool alt_lang_available = false;
                bool en_lang_available = false;
                //
                // CHECK OS NAME
                string ui_lang = CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.Trim();
                // CHECK SOFTWARE LANG FOLDER
                if (Directory.Exists(astel_lf)){
                    // CHECK LANG FILES
                    int get_langs_file = Directory.GetFiles(astel_lf, "*.ini").Length;
                    if (get_langs_file > 0){
                        // CHECK SETTINGS
                        try{
                            // CHECK LANG FILES
                            if (!File.Exists(astel_lang_en)){ englishToolStripMenuItem.Enabled = false; }else{ en_lang_available = true; }
                            if (!File.Exists(astel_lang_tr)){ turkishToolStripMenuItem.Enabled = false; }else{ alt_lang_available = true; }
                            //
                            if (en_lang_available == true){
                                // CHECK TR LANG
                                if (File.Exists(ts_sf)){
                                    GetSoftwareSetting(alt_lang_available);
                                }else{
                                    // DETECT SYSTEM THEME
                                    TSSettingsSave software_settings_save = new TSSettingsSave(ts_sf);
                                    string get_system_theme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "").ToString().Trim();
                                    software_settings_save.TSWriteSettings(ts_settings_container, "ThemeStatus", get_system_theme);
                                    // DETECT SYSTEM LANG / BYPASS LANGUAGE
                                    if (alt_lang_available){
                                        if (ui_lang != "" && ui_lang != string.Empty){
                                            software_settings_save.TSWriteSettings(ts_settings_container, "LanguageStatus", ui_lang);
                                        }else{
                                            software_settings_save.TSWriteSettings(ts_settings_container, "LanguageStatus", "en");
                                        }
                                        GetSoftwareSetting(true);
                                    }else{
                                        software_settings_save.TSWriteSettings(ts_settings_container, "LanguageStatus", "en");
                                        GetSoftwareSetting(false);
                                    }
                                    // SET INITIAL MODE
                                    software_settings_save.TSWriteSettings(ts_settings_container, "InitialStatus", "0");
                                    // SET SESSION MODE
                                    software_settings_save.TSWriteSettings(ts_settings_container, "SessionMode", "0");
                                }
                            }else{
                                software_prelaoder_alert(0);
                            }
                        }catch (Exception){ }
                    }else{
                        software_prelaoder_alert(1);
                    }
                }else{
                    software_prelaoder_alert(2);
                }
            }catch (Exception){ }
        }
        // ======================================================================================================
        // PRELOAD ALERT
        private void software_prelaoder_alert(int pre_mode){
            DialogResult open_last_release = DialogResult.OK;
            if (pre_mode == 0){
                open_last_release = MessageBox.Show($"English language (English.ini) is a compulsory language. The English.ini file seems to be missing.\n\nWould you like to view and download the latest version of {Application.ProductName} again?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }else if (pre_mode == 1){
                open_last_release = MessageBox.Show($"No language file found.\nThere seems to be a problem with the language files.\n\nWould you like to view and download the latest version of {Application.ProductName} again?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }else if (pre_mode == 2){
                open_last_release = MessageBox.Show($"a_langs folder not found.\nThe folder seems to be missing.\n\nWould you like to view and download the latest version of {Application.ProductName} again?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            if (open_last_release == DialogResult.Yes){
                Process.Start(TS_LinkSystem.github_link_lr);
            }
            software_exit();
        }
        // ======================================================================================================
        // SOFTWARE LOAD LANGS SETTINGS
        private void GetSoftwareSetting(bool _lang_wrapper){
            // THEME - LANG - VIEW MODE PRELOADER
            TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
            string theme_mode = software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus");
            switch (theme_mode){
                case "0":
                    theme_engine(0);
                    darkThemeToolStripMenuItem.Checked = true;
                    break;
                default:
                    theme_engine(1);
                    lightThemeToolStripMenuItem.Checked = true;
                    break;
            }
            string lang_mode = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
            if (_lang_wrapper){
                switch (lang_mode){
                    case "en":
                        lang_engine(astel_lang_en, "en");
                        englishToolStripMenuItem.Checked = true;
                        break;
                    case "tr":
                        lang_engine(astel_lang_tr, "tr");
                        turkishToolStripMenuItem.Checked = true;
                        break;
                    default:
                        lang_engine(astel_lang_en, "en");
                        englishToolStripMenuItem.Checked = true;
                        break;
                }
            }else{
                lang_engine(astel_lang_en, "en");
                englishToolStripMenuItem.Checked = true;
            }
            string initial_mode = software_read_settings.TSReadSettings(ts_settings_container, "InitialStatus");
            switch (initial_mode){
                case "0":
                    initial_status = 0;
                    windowedToolStripMenuItem.Checked = true;
                    //WindowState = FormWindowState.Normal;
                    break;
                case "1":
                    initial_status = 1;
                    fullScreenToolStripMenuItem.Checked = true;
                    WindowState = FormWindowState.Maximized;
                    break;
                default:
                    initial_status = 0;
                    windowedToolStripMenuItem.Checked = true;
                    //WindowState = FormWindowState.Normal;
                    break;
            }
            string session_mode = software_read_settings.TSReadSettings(ts_settings_container, "SessionMode");
            switch (session_mode){
                case "0":
                    setPasswordToolStripMenuItem.Enabled = true;
                    changePasswordToolStripMenuItem.Enabled = false;
                    break;
                case "1":
                    setPasswordToolStripMenuItem.Enabled = false;
                    changePasswordToolStripMenuItem.Enabled = true;
                    break;
                default:
                    setPasswordToolStripMenuItem.Enabled = true;
                    changePasswordToolStripMenuItem.Enabled = false;
                    break;
            }
            // DOUBLE BUFFER TABLE
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, DataMainTable, new object[] { true });
            //
            DataMainTable.Columns[0].Width = 65;
            DataMainTable.Columns[1].Width = 175;
            DataMainTable.Columns[2].Width = 225;
            DataMainTable.Columns[3].Width = 225;
            DataMainTable.Columns[4].Width = 250;
            DataMainTable.Columns[5].Width = 175;
            //
            foreach (DataGridViewColumn DataTable in DataMainTable.Columns){
                DataTable.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // DPI SET
            BtnCopyUsername.Height = TxtUserName.Height;
            BtnCopyEmail.Height = TxtEmail.Height;
            BtnCopyPassword.Height = TxtPassword.Height;
        }
        // ASYNC SECURE LOGIN QUERY
        // ======================================================================================================
        private void async_secure_login_query(){
            try{
                Thread.Sleep(7000);
                // DYNAMIC STARTUP
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                string session_mode = software_read_settings.TSReadSettings(ts_settings_container, "SessionMode");
                switch (session_mode){
                    case "0":
                        DialogResult query_secure_login = MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_secure_login_info").Trim())), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (query_secure_login == DialogResult.Yes){
                            set_password_page();
                        }
                        break;
                }
            }
            catch (Exception) { }
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private void Astel_Load(object sender, EventArgs e){
            Text = TS_SoftwareVersion.TS_SofwareVersion(0, ts_version_mode);
            HeaderMenu.Cursor = Cursors.Hand;
            AstelLoadXMLData();
            software_preloader();
            //
            Task secureLoginQuery = Task.Run(() => async_secure_login_query());
            Task softwareUpdateCheck = Task.Run(() => software_update_check(0));
        }
        // CREATE XML FILE
        // ======================================================================================================
        private string xmlFilePath = string.Format("{0}Data.xml", Application.ProductName);
        private void CreateEmptyXmlFile(){
            var ts_xDoc = new XDocument(new XElement("Datas"));
            ts_xDoc.Save(xmlFilePath);
        }
        private void AstelLoadXMLData(){
            if (!File.Exists(xmlFilePath)){ CreateEmptyXmlFile(); }
            //
            DataSet ts_dataSet = new DataSet();
            DataTable ts_dataTable = new DataTable("Datas");
            ts_dataTable.Columns.Add("ID", typeof(int));
            ts_dataTable.Columns.Add("Username", typeof(string));
            ts_dataTable.Columns.Add("Email", typeof(string));
            ts_dataTable.Columns.Add("Password", typeof(string));
            ts_dataTable.Columns.Add("Note", typeof(string));
            ts_dataTable.Columns.Add("PassChangeDate", typeof(string));
            //
            var ts_xDoc = XDocument.Load(xmlFilePath);
            var ts_xDoc_root = ts_xDoc.Element("Datas");
            //
            if (ts_xDoc_root != null){
                foreach (var ts_xml_mode in ts_xDoc_root.Elements("Data")){
                    DataRow ts_xml_row = ts_dataTable.NewRow();
                    ts_xml_row["ID"] = int.Parse(ts_xml_mode.Element("ID")?.Value ?? "0");
                    ts_xml_row["Username"] = ts_xml_mode.Element("Username") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Username").Value) : string.Empty;
                    ts_xml_row["Email"] = ts_xml_mode.Element("Email") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Email").Value) : string.Empty;
                    ts_xml_row["Password"] = ts_xml_mode.Element("Password") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Password").Value) : string.Empty;
                    ts_xml_row["Note"] = ts_xml_mode.Element("Note") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Note").Value) : string.Empty;
                    ts_xml_row["PassChangeDate"] = ts_xml_mode.Element("PassChangeDate")?.Value ?? string.Empty;
                    ts_dataTable.Rows.Add(ts_xml_row);
                }
            }
            ts_dataSet.Tables.Add(ts_dataTable);
            DataMainTable.DataSource = ts_dataSet.Tables[0];
            DataMainTable.ClearSelection();
        }
        // GENERATE NEW ID
        // ======================================================================================================
        private int TSGenerateNewID(){
            var ts_xDoc = XDocument.Load(xmlFilePath);
            var ts_xml_root = ts_xDoc.Element("Datas");
            int xml_max_id = ts_xml_root.Elements("Data").Select(g => (int)g.Element("ID")).DefaultIfEmpty(0).Max();
            return xml_max_id + 1;
        }
        // ADD DATA
        // ======================================================================================================
        private void AddBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                string in_user = TxtUserName.Text.Trim(), in_email = TxtEmail.Text.Trim(), in_password = TxtPassword.Text.Trim(), in_note = TxtNote.Text.Trim();
                if (string.IsNullOrEmpty(in_user)){
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_add_username_info").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(in_email)){
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_add_email_info").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(in_password)){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_add_password_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //
                var ts_xDoc = XDocument.Load(xmlFilePath);
                var ts_xml_root = ts_xDoc.Element("Datas");
                //
                ts_xml_root.Add(new XElement("Data",
                    new XElement("ID", TSGenerateNewID()),
                    new XElement("Username", TS_AES_Encryption.TS_AES_Encrypt(TxtUserName.Text.Trim())),
                    new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(TxtEmail.Text.Trim())),
                    new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(TxtPassword.Text.Trim())),
                    new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(TxtNote.Text.Trim())),
                    new XElement("PassChangeDate", DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss"))));
                //
                ts_xDoc.Save(xmlFilePath);
                AstelLoadXMLData();
                nodeClearInput();
                //
                MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_add_success").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch (Exception){
                MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_add_failed").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // UPDATE DATA
        // ======================================================================================================
        private void UpdateBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_update_select_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else{
                    DialogResult checkUpdateQuery = MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_update_question_info").Trim())), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (checkUpdateQuery == DialogResult.Yes){
                        var ts_xDoc = XDocument.Load(xmlFilePath);
                        var ts_xml_root = ts_xDoc.Element("Datas");
                        var ts_xml_elementToUpdate = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString()));
                        //
                        if (ts_xml_elementToUpdate != null){
                            ts_xml_elementToUpdate.SetElementValue("Username", TS_AES_Encryption.TS_AES_Encrypt(TxtUserName.Text.Trim()));
                            ts_xml_elementToUpdate.SetElementValue("Email", TS_AES_Encryption.TS_AES_Encrypt(TxtEmail.Text.Trim()));
                            ts_xml_elementToUpdate.SetElementValue("Password", TS_AES_Encryption.TS_AES_Encrypt(TxtPassword.Text.Trim()));
                            ts_xml_elementToUpdate.SetElementValue("Note", TS_AES_Encryption.TS_AES_Encrypt(TxtNote.Text.Trim()));
                            ts_xml_elementToUpdate.SetElementValue("PassChangeDate", DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss"));
                        }
                        //
                        ts_xDoc.Save(xmlFilePath);
                        AstelLoadXMLData();
                        nodeClearInput();
                        //
                        MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_update_success").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }catch (Exception){
                MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_update_failed").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // DELETE DATA
        // ======================================================================================================
        private void DeleteBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_delete_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else{
                    DialogResult checkUpdateQuery = MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_delete_question_info").Trim())), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (checkUpdateQuery == DialogResult.Yes){
                        var ts_xDoc = XDocument.Load(xmlFilePath);
                        var ts_xml_root = ts_xDoc.Element("Datas");
                        var ts_xml_elementToDelete = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString()));
                        //
                        if (ts_xml_elementToDelete != null){
                            ts_xml_elementToDelete.Remove();
                        }
                        //
                        ts_xDoc.Save(xmlFilePath);
                        AstelLoadXMLData();
                        nodeClearInput();
                        //
                        MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_delete_success").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }catch (Exception){
                MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_delete_failed").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // COPY DATA
        // ======================================================================================================
        private void BtnCopyUsername_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtUserName.Text.Trim())){
                    Clipboard.SetText(TxtUserName.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_user").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        private void BtnCopyEmail_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtEmail.Text.Trim())){
                    Clipboard.SetText(TxtEmail.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_email").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        private void BtnCopyPassword_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtPassword.Text.Trim())){
                    Clipboard.SetText(TxtPassword.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_password").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // TEXTBOX ROTATE DATA
        // ======================================================================================================
        private void DataMainTable_CellClick(object sender, DataGridViewCellEventArgs e){
            try{
                if (e.RowIndex >= 0){
                    DataGridViewRow xml_select_row = DataMainTable.Rows[e.RowIndex];
                    TxtUserName.Text = xml_select_row.Cells[1].Value.ToString();
                    TxtEmail.Text = xml_select_row.Cells[2].Value.ToString();
                    TxtPassword.Text = xml_select_row.Cells[3].Value.ToString();
                    TxtNote.Text = xml_select_row.Cells[4].Value.ToString();
                }
            }catch (Exception){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_delete_failed").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // CLEAR INPUT
        // ======================================================================================================
        private void nodeClearInput(){
            TxtUserName.Clear();
            TxtPassword.Clear();
            TxtEmail.Clear();
            TxtNote.Clear();
            DataMainTable.ClearSelection();
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
                if (theme == 1){
                    try { if (DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4) != 1) { DwmSetWindowAttribute(Handle, 20, new[] { 0 }, 4); } } catch (Exception) { }
                    //
                    settingsToolStripMenuItem.Image = Properties.Resources.header_settings_light;
                    themeToolStripMenuItem.Image = Properties.Resources.header_theme_light;
                    languageToolStripMenuItem.Image = Properties.Resources.header_language_light;
                    initialViewToolStripMenuItem.Image = Properties.Resources.header_initial_light;
                    loginSettingsToolStripMenuItem.Image = Properties.Resources.header_login_light;
                    setPasswordToolStripMenuItem.Image = Properties.Resources.header_set_password_light;
                    changePasswordToolStripMenuItem.Image = Properties.Resources.header_change_password_light;
                    checkforUpdatesToolStripMenuItem.Image = Properties.Resources.header_update_light;
                    passwordGeneratorToolStripMenuItem.Image = Properties.Resources.header_pass_gen_light;
                    aboutToolStripMenuItem.Image = Properties.Resources.header_about_light;
                }else if (theme == 0){
                    try { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } } catch (Exception) { }
                    //
                    settingsToolStripMenuItem.Image = Properties.Resources.header_settings_dark;
                    themeToolStripMenuItem.Image = Properties.Resources.header_theme_dark;
                    languageToolStripMenuItem.Image = Properties.Resources.header_language_dark;
                    initialViewToolStripMenuItem.Image = Properties.Resources.header_initial_dark;
                    loginSettingsToolStripMenuItem.Image = Properties.Resources.header_login_dark;
                    setPasswordToolStripMenuItem.Image = Properties.Resources.header_set_password_dark;
                    changePasswordToolStripMenuItem.Image = Properties.Resources.header_change_password_dark;
                    checkforUpdatesToolStripMenuItem.Image = Properties.Resources.header_update_dark;
                    passwordGeneratorToolStripMenuItem.Image = Properties.Resources.header_pass_gen_dark;
                    aboutToolStripMenuItem.Image = Properties.Resources.header_about_dark;
                }
                //
                BtnCopyUsername.Image = Properties.Resources.mid_copy_all_theme;
                BtnCopyEmail.Image = Properties.Resources.mid_copy_all_theme;
                BtnCopyPassword.Image = Properties.Resources.mid_copy_all_theme;
                //
                header_colors[0] = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor");
                header_colors[1] = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor");
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
                // LOGIN
                loginSettingsToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                loginSettingsToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                setPasswordToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                setPasswordToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                changePasswordToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                changePasswordToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // UPDATE ENGINE
                checkforUpdatesToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                checkforUpdatesToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                // PASSWORD GEN
                passwordGeneratorToolStripMenuItem.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                passwordGeneratorToolStripMenuItem.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
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
                foreach (Control control in Panel_Footer.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelRightColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelRightColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelRightColor");
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
            if (lang != "en"){ lang_preload(astel_lang_en, "en"); select_lang_active(sender); }
        }
        private void turkishToolStripMenuItem_Click(object sender, EventArgs e){
            if (lang != "tr"){ lang_preload(astel_lang_en, "tr"); select_lang_active(sender); }
        }
        private void lang_preload(string lang_type, string lang_code){
            lang_engine(lang_type, lang_code);
            try{
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "LanguageStatus", lang_code);
            }catch (Exception){ }
            // LANG CHANGE NOTIFICATION
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            DialogResult lang_change_message = MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("LangChange", "lang_change_notification").Trim())), "\n\n", "\n\n"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (lang_change_message == DialogResult.Yes) { Application.Restart(); }
        }
        private void lang_engine(string lang_type, string lang_code){
            try{
                lang_path = lang_type;
                lang = lang_code;
                // GLOBAL ENGINE
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                // SETTINGS
                settingsToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_settings").Trim()));
                // THEMES
                themeToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_theme").Trim()));
                lightThemeToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderThemes", "theme_light").Trim()));
                darkThemeToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderThemes", "theme_dark").Trim()));
                // LANGS
                languageToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_language").Trim()));
                englishToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLangs", "lang_en").Trim()));
                turkishToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLangs", "lang_tr").Trim()));
                // INITIAL VIEW
                initialViewToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_start").Trim()));
                windowedToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderViewMode", "header_viev_mode_windowed").Trim()));
                fullScreenToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderViewMode", "header_viev_mode_full_screen").Trim()));
                // LOGIN
                loginSettingsToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_login_settings").Trim()));
                setPasswordToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLogin", "hl_set_password").Trim()));
                changePasswordToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLogin", "hl_change_password").Trim()));
                // UPDATE CHECK
                checkforUpdatesToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_update").Trim()));
                // PASS GEN
                passwordGeneratorToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_pass_gen").Trim()));
                // ABOUT
                aboutToolStripMenuItem.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_about").Trim()));
                // HOME
                DataMainTable.Columns[1].HeaderText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_table_username").Trim()));
                DataMainTable.Columns[2].HeaderText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_table_mail").Trim()));
                DataMainTable.Columns[3].HeaderText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_table_password").Trim()));
                DataMainTable.Columns[4].HeaderText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_table_note").Trim()));
                DataMainTable.Columns[5].HeaderText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_table_update_date").Trim()));
                //
                LabelUsername.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_label_username").Trim()));
                LabelMail.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_label_mail").Trim()));
                LabelPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_label_password").Trim()));
                LabelNote.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_label_note").Trim()));
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(BtnCopyUsername, Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_hover").Trim())));
                MainToolTip.SetToolTip(BtnCopyEmail, Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_hover").Trim())));
                MainToolTip.SetToolTip(BtnCopyPassword, Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_copy_hover").Trim())));
                //
                AddBtn.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_button_add").Trim()));
                UpdateBtn.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_button_update").Trim()));
                DeleteBtn.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelHome", "ah_button_delete").Trim()));
                //
                software_other_page_preloader();
            }catch (Exception) { }
        }
        private void software_other_page_preloader(){
            // SET PASSWORD PAGE
            try{
                AstelSignIn software_set_password = new AstelSignIn();
                string software_set_passowrd_name = "astel_set_password";
                software_set_password.Name = software_set_passowrd_name;
                if (Application.OpenForms[software_set_passowrd_name] != null)
                {
                    software_set_password = (AstelSignIn)Application.OpenForms[software_set_passowrd_name];
                    software_set_password.login_system_preloader();
                }
            }catch (Exception){ }
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
        // UPDATE CHECK ENGINE
        // ======================================================================================================
        private void checkforUpdatesToolStripMenuItem_Click(object sender, EventArgs e){
            software_update_check(1);
        }
        public bool IsNetworkCheck(){
            Ping ping = new Ping();
            try{
                PingReply reply = ping.Send("www.google.com");
                if (reply.Status == IPStatus.Success){
                    return true;
                }
            }catch (PingException){ }
            return false;
        }
        public void software_update_check(int _check_update_ui){
            string client_version = TS_SoftwareVersion.TS_SofwareVersion(2, ts_version_mode).Trim();
            int client_num_version = Convert.ToInt32(client_version.Replace(".", string.Empty));
            if (IsNetworkCheck() == true){
                string software_version_url = TS_LinkSystem.github_link_lt;
                WebClient webClient = new WebClient();
                try{
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    string[] version_content = webClient.DownloadString(software_version_url).Split('=');
                    string last_version = version_content[1].Trim();
                    int last_num_version = Convert.ToInt32(version_content[1].Trim().Replace(".", string.Empty));
                    if (client_num_version < last_num_version){
                        DialogResult info_update = MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("SoftwareUpdate", "su_message").Trim())), Application.ProductName, "\n\n", client_version, "\n", last_version, "\n\n"), string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("SoftwareUpdate", "su_title").Trim())), Application.ProductName), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (info_update == DialogResult.Yes){
                            Process.Start(TS_LinkSystem.github_link_lr);
                        }
                    }else{
                        if (_check_update_ui == 1){
                            if (client_num_version == last_num_version){
                                MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("SoftwareUpdate", "su_no_update").Trim())), Application.ProductName, "\n", client_version), string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("SoftwareUpdate", "su_title").Trim())), Application.ProductName), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }catch (WebException){ }
            }else{
                checkforUpdatesToolStripMenuItem.Enabled = false;
            }
        }
        // SET PASSWORD
        // ======================================================================================================
        private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e){
            set_password_page();
        }
        private void set_password_page(){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                AstelSignIn astel_set_password = new AstelSignIn();
                string astel_set_password_name = "astel_set_password";
                astel_set_password.Name = astel_set_password_name;
                if (Application.OpenForms[astel_set_password_name] == null){
                    astel_set_password.ShowDialog();
                }else{
                    if (Application.OpenForms[astel_set_password_name].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[astel_set_password_name].WindowState = FormWindowState.Normal;
                    }
                    Application.OpenForms[astel_set_password_name].Activate();
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification").Trim())), Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLogin", "hl_set_password").Trim()))), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // CHANGE PASSWORD
        // ======================================================================================================
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                AstelChangePassword astel_change_password = new AstelChangePassword();
                string astel_change_password_name = "astel_change_password";
                astel_change_password.Name = astel_change_password_name;
                if (Application.OpenForms[astel_change_password_name] == null){
                    astel_change_password.ShowDialog();
                }else{
                    if (Application.OpenForms[astel_change_password_name].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[astel_change_password_name].WindowState = FormWindowState.Normal;
                    }
                    Application.OpenForms[astel_change_password_name].Activate();
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLogin", "hl_change_password").Trim())), Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_about").Trim()))), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // PASSWORD GENERATOR
        // ======================================================================================================
        private void passwordGeneratorToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                AstelPasswordGenerator astel_password_generator = new AstelPasswordGenerator();
                string astel_password_generator_name = "astel_password_generator";
                astel_password_generator.Name = astel_password_generator_name;
                if (Application.OpenForms[astel_password_generator_name] == null){
                    astel_password_generator.Show();
                }else{
                    if (Application.OpenForms[astel_password_generator_name].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[astel_password_generator_name].WindowState = FormWindowState.Normal;
                    }
                    Application.OpenForms[astel_password_generator_name].Activate();
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderLogin", "hl_change_password").Trim())), Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_about").Trim()))), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // ABOUT PAGE
        // ======================================================================================================
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                AstelAbout astel_about = new AstelAbout();
                string astel_about_name = "astel_about";
                astel_about.Name = astel_about_name;
                if (Application.OpenForms[astel_about_name] == null){
                    astel_about.Show();
                }else{
                    if (Application.OpenForms[astel_about_name].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[astel_about_name].WindowState = FormWindowState.Normal;
                    }
                    Application.OpenForms[astel_about_name].Activate();
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification").Trim())), Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("HeaderMenu", "header_menu_about").Trim()))), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // EXIT
        // ======================================================================================================
        private void software_exit(){ Application.Exit(); }
        private void Astel_FormClosing(object sender, FormClosingEventArgs e){ software_exit(); }
    }
}