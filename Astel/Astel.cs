using System;
using System.IO;
using System.Net;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Xml.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
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
        public static int theme, initial_status;
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
            string session_mode = TS_String_Encoder(software_read_settings.TSReadSettings(ts_settings_container, "SessionMode"));
            var sessionSettings = new Dictionary<string, (bool setEnabled, bool changeEnabled)> {
                { "0", (true, false) },
                { "1", (false, true) }
            };
            var (setEnabled, changeEnabled) = sessionSettings.ContainsKey(session_mode) ? sessionSettings[session_mode] : sessionSettings["0"];
            setPasswordToolStripMenuItem.Enabled = setEnabled;
            changePasswordToolStripMenuItem.Enabled = changeEnabled;
        }
        // ASYNC SECURE LOGIN QUERY
        // ======================================================================================================
        private void async_secure_login_query(){
            try{
                Thread.Sleep(7000);
                // DYNAMIC STARTUP
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                string session_mode = software_read_settings.TSReadSettings(ts_settings_container, "SessionMode");
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                switch (session_mode){
                    case "0":
                        try{
                            AstelSignIn astel_set_password = new AstelSignIn();
                            string astel_set_password_name = "astel_set_password";
                            astel_set_password.Name = astel_set_password_name;
                            if (Application.OpenForms[astel_set_password_name] == null){
                                DialogResult query_secure_login = MessageBox.Show(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_secure_login_info")), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (query_secure_login == DialogResult.Yes){
                                    astel_set_password.ShowDialog();
                                    Application.OpenForms[astel_set_password_name].Activate();
                                }
                            }
                        }catch (Exception){ }
                        break;
                }
            }catch (Exception){ }
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private void Astel_Load(object sender, EventArgs e){
            Text = TS_VersionEngine.TS_SofwareVersion(0, Program.ts_version_mode);
            HeaderMenu.Cursor = Cursors.Hand;
            AstelLoadXMLData();
            RunSoftwareEngine();
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 4, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_username_info")), "\n"));
                    return;
                }
                if (string.IsNullOrEmpty(in_email)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 4, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_email_info")), "\n"));
                    return;
                }
                if (string.IsNullOrEmpty(in_password)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 4, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_password_info")));
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
                TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_success")));
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_add_failed")), "\n"));
            }
        }
        // UPDATE DATA
        // ======================================================================================================
        private void UpdateBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_select_info")));
                }
                else{
                    DialogResult checkUpdateQuery = TS_MessageBoxEngine.TS_MessageBox(this, 4, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_question_info")));
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
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_success")));
                    }
                }
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_update_failed")), "\n"));
            }
        }
        // DELETE DATA
        // ======================================================================================================
        private void DeleteBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_info")));
                }else{
                    DialogResult checkUpdateQuery = TS_MessageBoxEngine.TS_MessageBox(this, 4, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_question_info")));
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
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_success")));
                    }
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
                    TxtUserName.Text = xml_select_row.Cells[1].Value.ToString();
                    TxtEmail.Text = xml_select_row.Cells[2].Value.ToString();
                    TxtPassword.Text = xml_select_row.Cells[3].Value.ToString();
                    TxtNote.Text = xml_select_row.Cells[4].Value.ToString();
                }
            }catch (Exception){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_delete_failed")), "\n"));
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
                int set_attribute = theme == 1 ? 20 : 19;
                if (DwmSetWindowAttribute(Handle, set_attribute, new[] { 1 }, 4) != theme){
                    DwmSetWindowAttribute(Handle, 20, new[] { theme == 1 ? 0 : 1 }, 4);
                }
                if (theme == 1){
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
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelRightColorHover");
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
                // LOGIN
                loginSettingsToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_login_settings"));
                setPasswordToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderLogin", "hl_set_password"));
                changePasswordToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderLogin", "hl_change_password"));
                // UPDATE CHECK
                checkforUpdatesToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_update"));
                // PASS GEN
                passwordGeneratorToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_pass_gen"));
                // ABOUT
                aboutToolStripMenuItem.Text = TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_about"));
                // HOME
                DataMainTable.Columns[1].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_username"));
                DataMainTable.Columns[2].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_mail"));
                DataMainTable.Columns[3].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_password"));
                DataMainTable.Columns[4].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_note"));
                DataMainTable.Columns[5].HeaderText = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_table_update_date"));
                //
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
                AddBtn.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_add"));
                UpdateBtn.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_update"));
                DeleteBtn.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelHome", "ah_button_delete"));
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
                if (Application.OpenForms[software_set_passowrd_name] != null){
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
            Ping check_ping = new Ping();
            try{
                PingReply check_ping_reply = check_ping.Send("www.google.com");
                if (check_ping_reply.Status == IPStatus.Success){
                    return true;
                }
            }catch (PingException){ }
            return false;
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
                    string[] version_content = webClient.DownloadString(TS_LinkSystem.github_link_lt).Split('=');
                    string last_version = version_content[1].Trim();
                    int last_num_version = Convert.ToInt32(last_version.Replace(".", string.Empty));
                    //
                    if (client_num_version < last_num_version){
                        // Update available
                        DialogResult info_update = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_available")), Application.ProductName, "\n\n", client_version, "\n", last_version, "\n\n"), string.Format(TS_String_Encoder(software_lang.TSReadLangs("SoftwareUpdate", "su_title")), Application.ProductName));
                        if (info_update == DialogResult.Yes){
                            Process.Start(new ProcessStartInfo(TS_LinkSystem.github_link_lr){ UseShellExecute = true });
                        }
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
        // SET PASSWORD
        // ======================================================================================================
        private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e){
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), TS_String_Encoder(software_lang.TSReadLangs("HeaderLogin", "hl_set_password"))));
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), TS_String_Encoder(software_lang.TSReadLangs("HeaderLogin", "hl_change_password"))));
                    Application.OpenForms[astel_change_password_name].Activate();
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_pass_gen"))));
                    Application.OpenForms[astel_password_generator_name].Activate();
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification")), TS_String_Encoder(software_lang.TSReadLangs("HeaderMenu", "header_menu_about"))));
                    Application.OpenForms[astel_about_name].Activate();
                }
            }catch (Exception){ }
        }
        // EXIT
        // ======================================================================================================
        private void software_exit(){ Application.Exit(); }
        private void Astel_FormClosing(object sender, FormClosingEventArgs e){ software_exit(); }
    }
}