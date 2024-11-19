using System;
using System.Windows.Forms;
using System.Collections.Generic;
//
using static Astel.TSModules;

namespace Astel.astel_modules{
    public partial class AstelLogin : Form{
        public AstelLogin(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // LOGIN PRELOADER
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
                    if (DwmSetWindowAttribute(Handle, set_attribute, new[]{ 1 }, 4) != Convert.ToInt32(login_global_theme)){
                        DwmSetWindowAttribute(Handle, 20, new[]{ Convert.ToInt32(login_global_theme) == 1 ? 0 : 1 }, 4);
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
                        button.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelRightColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelRightColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelRightColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelRightColorHover");
                    }
                }
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
                Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_title")), Application.ProductName);
                LabelHeader.Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_header")), Environment.UserName);
                LabelPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_label_password"));
                CheckPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_visible"));
                BtnLogin.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_btn"));
            }catch (Exception){ }
        }
        // LOGIN LOAD
        // ======================================================================================================
        private void AstelLogin_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            AcceptButton = BtnLogin;
            //
            login_system_preloader();
        }
        // LOGIN BTN
        // ======================================================================================================
        private void BtnLogin_Click(object sender, EventArgs e){
            login_system();
        }
        // LOGIN FUNCTION
        // ======================================================================================================
        private void login_system(){
            TSGetLangs software_lang = new TSGetLangs(login_global_lang);
            try{
                string get_password = TxtPassword.Text.Trim();
                if (!string.IsNullOrEmpty(get_password)){
                    get_password = TSHashPassword(get_password, ts_hash_salting);
                    TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                    string saved_password = software_read_settings.TSReadSettings(ts_settings_container, "Password");
                    if (get_password == saved_password){
                        Astel astel = new Astel();
                        astel.Show();
                        Hide();
                    }else{
                        TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_password_failed")));
                    }
                }else{
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelLogin", "al_password_info")));
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