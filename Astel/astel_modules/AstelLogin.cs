using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelLogin : Form{
        public AstelLogin(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // LOGIN PRELOADER
        // ======================================================================================================
        string login_global_lang;
        public void Login_system_preloader(){
            try{
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                Dictionary<string, string> themeModes = new Dictionary<string, string>{
                    { "0", "0" },
                    { "1", "1" }
                };
                string theme_mode = software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus");
                string login_global_theme = themeModes.ContainsKey(theme_mode) ? themeModes[theme_mode] : "1";
                //
                TSSetWindowTheme(Handle, Convert.ToInt32(login_global_theme));
                //
                BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "HeaderBGColor2");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "TextboxFEColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "AccentMain");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "AccentMain");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "AccentMain");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "AccentMainHover");
                    }
                }
                //
                TSImageRenderer(BtnLogin, Convert.ToInt32(login_global_theme) == 1 ? Properties.Resources.ct_login_light : Properties.Resources.ct_login_dark, 18, ContentAlignment.MiddleLeft);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                // ======================================================================================================
                // Mevcut dil ayarını oku
                string lang_code = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                string selectedLangCode = TSPreloaderSetDefaultLanguage(lang_code);
                string lang_file_path = AllLanguageFiles[selectedLangCode];
                TSGetLangs software_lang = new TSGetLangs(lang_file_path);
                login_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName);
                LabelHeader.Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_header"), Environment.UserName);
                LabelPassword.Text = software_lang.TSReadLangs("AstelLogin", "al_label_password");
                CheckPassword.Text = software_lang.TSReadLangs("AstelLogin", "al_visible");
                BtnLogin.Text = software_lang.TSReadLangs("AstelLogin", "al_btn") + " ";
            }catch (Exception){ }
        }
        // LOGIN LOAD
        // ======================================================================================================
        private void AstelLogin_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            AcceptButton = BtnLogin;
            //
            Login_system_preloader();
        }
        // LOGIN BTN
        // ======================================================================================================
        private void BtnLogin_Click(object sender, EventArgs e){
            Login_system();
        }
        // LOGIN FUNCTION
        // ======================================================================================================
        private void Login_system(){
            TSGetLangs software_lang = new TSGetLangs(login_global_lang);
            try{
                string get_password = TxtPassword.Text.Trim();
                if (string.IsNullOrEmpty(get_password)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelLogin", "al_password_info"));
                    return;
                }
                //
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_session_file);
                string saved_salt = software_read_settings.TSReadSettings(ts_session_container, "PasswordSalt");
                string saved_password = software_read_settings.TSReadSettings(ts_session_container, "PasswordHash");
                //
                string hashed_input = TSHashPassword(get_password, saved_salt);
                if (hashed_input == saved_password){
                    AstelMain astel = new AstelMain();
                    astel.Show();
                    Hide();
                }else{
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelLogin", "al_password_failed"), "\n"));
                }
            }catch (Exception){ }
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtPassword.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtPassword.UseSystemPasswordChar = true;
            }
        }
        // EXIT
        // ======================================================================================================
        private void AstelLogin_FormClosing(object sender, FormClosingEventArgs e){ Application.Exit(); }
    }
}