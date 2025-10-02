﻿using System;
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
        string signin_global_lang;
        public void Login_system_preloader(){
            try{
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                int theme_mode = int.TryParse(software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus"), out int the_status) && (the_status == 0 || the_status == 1 || the_status == 2) ? the_status : 1;
                theme_mode = GetSystemTheme(theme_mode);
                //
                TSThemeModeHelper.SetThemeMode(theme_mode == 0);
                //
                BackColor = TS_ThemeEngine.ColorMode(theme_mode, "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "HeaderBGColor2");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TextboxFEColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentMain");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentMain");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentMain");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentMainHover");
                    }
                }
                //
                TSImageRenderer(BtnSignIn, theme_mode == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleRight);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                // ======================================================================================================
                string lang_code = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                string selectedLangCode = TSPreloaderSetDefaultLanguage(lang_code);
                string lang_file_path = AllLanguageFiles[selectedLangCode];
                TSGetLangs software_lang = new TSGetLangs(lang_file_path);
                signin_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelSignIn", "as_header");
                LabelPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password");
                LabelPasswordRepeat.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password_repeat");
                CheckPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_visible");
                BtnSignIn.Text = " " + software_lang.TSReadLangs("AstelSignIn", "as_btn");
            }catch (Exception){ }
        }
        // SIGN IN LOAD
        // ======================================================================================================
        private void AstelSignIn_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            TxtPasswordRepeat.UseSystemPasswordChar = true;
            AcceptButton = BtnSignIn;
            //
            Login_system_preloader();
        }
        // SIGN IN BTN
        // ======================================================================================================
        private void BtnSignIn_Click(object sender, EventArgs e){
            Sign_in_function();
        }
        // SIGN IN FUNCTION
        // ======================================================================================================
        private void Sign_in_function(){
            TSGetLangs software_lang = new TSGetLangs(signin_global_lang);
            try{
                string password_1 = TxtPassword.Text.Trim();
                string password_2 = TxtPasswordRepeat.Text.Trim();
                if (string.IsNullOrEmpty(password_1)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_info"));
                    return;
                }
                if (string.IsNullOrEmpty(password_2)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_repeat_info"));
                    return;
                }
                if (password_1.Length < 6 || password_1.Length > 32){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_req_info"));
                    return;
                }
                if (password_1 != password_2){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelSignIn", "as_password_set_failed"), "\n"));
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
                TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelSignIn", "as_password_set_success"));
                //
                AstelMain astel_ui = new AstelMain();
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