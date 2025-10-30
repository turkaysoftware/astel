using System;
using System.Drawing;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelChangePassword : Form{
        public AstelChangePassword(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // SIGN IN PRELOADER
        // ======================================================================================================
        public void Change_password_system_preloader(){
            try{
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(AstelMain.theme), "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(AstelMain.theme), "HeaderBGColor2");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TextboxFEColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMainHover");
                    }
                }
                //
                TSImageRenderer(BtnChangePassword, AstelMain.theme == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleRight);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(AstelMain.theme), "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(AstelMain.theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(AstelMain.theme), "ContentLabelLeftColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(AstelMain.lang_path);
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_header");
                LabelCurrentPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_current");
                LabelNewPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new");
                LabelNewPasswordRepeat.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new_repeat");
                CheckPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_visible");
                BtnChangePassword.Text = " " + software_lang.TSReadLangs("AstelChangePassword", "asp_btn");
            }catch (Exception){ }
        }
        // CHANGE PASSWORD LOAD
        // ======================================================================================================
        private void AstelChangePassword_Load(object sender, EventArgs e){
            TxtCurrentPassword.UseSystemPasswordChar = true;
            TxtNewPassword.UseSystemPasswordChar = true;
            TxtNewPasswordRepeat.UseSystemPasswordChar = true;
            AcceptButton = BtnChangePassword;
            //
            Change_password_system_preloader();
        }
        // CHANGE PASSWORD BTN
        // ======================================================================================================
        private void BtnChangePassword_Click(object sender, EventArgs e){
            Change_password_function();
        }
        // CHANGE PASSWORD FUNCTION
        // ======================================================================================================
        private void Change_password_function(){
            TSGetLangs software_lang = new TSGetLangs(AstelMain.lang_path);
            try{
                string password_current = TxtCurrentPassword.Text.Trim();
                string password_new = TxtNewPassword.Text.Trim();
                string password_new_repeat = TxtNewPasswordRepeat.Text.Trim();
                //
                if (string.IsNullOrEmpty(password_current)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_info"));
                    return;
                }
                if (string.IsNullOrEmpty(password_new)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_info"));
                    return;
                }
                if (string.IsNullOrEmpty(password_new_repeat)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_repeat_info"));
                    return;
                }
                if (password_new.Length < 6 || password_new.Length > 32){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_pass_req_info"));
                    return;
                }
                if (password_new != password_new_repeat){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_compare_info"), "\n"));
                    return;
                }
                //
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_session_file);
                string saved_salt = software_read_settings.TSReadSettings(ts_session_container, "PasswordSalt");
                string saved_password = software_read_settings.TSReadSettings(ts_session_container, "PasswordHash");
                //
                if (string.IsNullOrEmpty(saved_salt) || string.IsNullOrEmpty(saved_password)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_fail_info"), "\n"));
                    return;
                }
                //
                string hashed_current = TSHashPassword(password_current, saved_salt);
                //
                if (hashed_current != saved_password){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_fail_info"), "\n"));
                    return;
                }
                //
                string new_salt = GenerateSalt();
                string new_hashed_password = TSHashPassword(password_new, new_salt);
                //
                TSSettingsSave software_setting_save = new TSSettingsSave(ts_session_file);
                software_setting_save.TSWriteSettings(ts_session_container, "PasswordSalt", new_salt);
                software_setting_save.TSWriteSettings(ts_session_container, "PasswordHash", new_hashed_password);
                //
                TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_change_success"), "\n"));
                Hide();
            }catch (Exception){ }
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtCurrentPassword.UseSystemPasswordChar = false;
                TxtNewPassword.UseSystemPasswordChar = false;
                TxtNewPasswordRepeat.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtCurrentPassword.UseSystemPasswordChar = true;
                TxtNewPassword.UseSystemPasswordChar = true;
                TxtNewPasswordRepeat.UseSystemPasswordChar = true;
            }
        }
    }
}