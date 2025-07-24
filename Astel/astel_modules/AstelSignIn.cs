using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelSignIn : Form{
        public AstelSignIn(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // SIGN IN PRELOADER
        // ======================================================================================================
        string login_global_lang;
        string login_global_theme;
        public void login_system_preloader(){
            try{
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                Dictionary<string, string> themeModes = new Dictionary<string, string>{
                    { "0", "0" },
                    { "1", "1" }
                };
                string theme_mode = software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus");
                login_global_theme = themeModes.ContainsKey(theme_mode) ? themeModes[theme_mode] : "1";
                //
                try{
                    int set_attribute = Convert.ToInt32(login_global_theme) == 1 ? 20 : 19;
                    if (DwmSetWindowAttribute(Handle, set_attribute, new[] { 1 }, 4) != Convert.ToInt32(login_global_theme)){
                        DwmSetWindowAttribute(Handle, 20, new[] { Convert.ToInt32(login_global_theme) == 1 ? 0 : 1 }, 4);
                    }
                }catch (Exception){ }
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
                TSImageRenderer(BtnSignIn, Convert.ToInt32(login_global_theme) == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleLeft);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                // ======================================================================================================
                string lang_mode = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                TSGetLangs software_lang;
                Dictionary<string, (TSGetLangs LangObject, string LangCode)> languageModes = new Dictionary<string, (TSGetLangs, string)>{
                    { "en", (new TSGetLangs(ts_lang_en), ts_lang_en) },
                    { "tr", (new TSGetLangs(ts_lang_tr), ts_lang_tr) }
                };
                var selectedLanguage = languageModes.ContainsKey(lang_mode) ? languageModes[lang_mode] : languageModes["en"];
                software_lang = selectedLanguage.LangObject;
                login_global_lang = selectedLanguage.LangCode;
                // TEXTS
                Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_title")), Application.ProductName);
                LabelHeader.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_header"));
                LabelPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_label_password"));
                LabelPasswordRepeat.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_label_password_repeat"));
                CheckPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_visible"));
                BtnSignIn.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_btn")) + " ";
            }catch (Exception){ }
        }
        // SIGN IN LOAD
        // ======================================================================================================
        private void AstelSignIn_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            TxtPasswordRepeat.UseSystemPasswordChar = true;
            AcceptButton = BtnSignIn;
            //
            login_system_preloader();
        }
        // SIGN IN BTN
        // ======================================================================================================
        private void BtnSignIn_Click(object sender, EventArgs e){
            sign_in_function();
        }
        // SIGN IN FUNCTION
        // ======================================================================================================
        private void sign_in_function(){
            TSGetLangs software_lang = new TSGetLangs(login_global_lang);
            try{
                string password_1 = TxtPassword.Text.Trim();
                string password_2 = TxtPasswordRepeat.Text.Trim();
                if (string.IsNullOrEmpty(password_1)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_info")));
                    return;
                }
                if (string.IsNullOrEmpty(password_2)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_repeat_info")));
                    return;
                }
                if (password_1.Length < 6 || password_1.Length > 32){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_req_info")));
                    return;
                }
                if (password_1 != password_2){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_set_failed")), "\n"));
                    return;
                }
                string salt = GenerateSalt();
                string hashed_password = TSHashPassword(password_1, salt);
                //
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_session_file);
                software_setting_save.TSWriteSettings(ts_session_container, "SessionMode", "1");
                software_setting_save.TSWriteSettings(ts_session_container, "PasswordHash", hashed_password);
                software_setting_save.TSWriteSettings(ts_session_container, "PasswordSalt", salt);
                software_setting_save.TSWriteSettings(ts_session_container, "CrossLinker", GenerateSecureRandomString(32));
                //
                TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_set_success")));
                //
                Astel astel_ui = new Astel();
                astel_ui.Show();
                Hide();
            }catch (Exception){ }
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtPassword.UseSystemPasswordChar = false;
                TxtPasswordRepeat.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtPassword.UseSystemPasswordChar = true;
                TxtPasswordRepeat.UseSystemPasswordChar = true;
            }
        }
        // EXIT
        // ======================================================================================================
        private void AstelSignIn_FormClosing(object sender, FormClosingEventArgs e){
            Application.Exit();
        }
    }
}