using System;
using System.Windows.Forms;
using static Astel.TSModules;

namespace Astel.astel_modules{
    public partial class AstelChangePassword : Form{
        public AstelChangePassword(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // SIGN IN PRELOADER
        // ======================================================================================================
        public void change_password_system_preloader(){
            try{
                if (Astel.theme == 1){
                    try { if (DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4) != 1) { DwmSetWindowAttribute(Handle, 20, new[] { 0 }, 4); } } catch (Exception) { }
                }else if (Astel.theme == 0){
                    try { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } } catch (Exception) { }
                }
                BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(Astel.theme, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(Astel.theme, "TextboxFEColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(Astel.theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelRightColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelRightColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelRightColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(Astel.theme, "ContentLabelRightColorHover");
                    }
                }
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                // TEXTS
                Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_title")), Application.ProductName);
                LabelHeader.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_header"));
                LabelCurrentPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_current"));
                LabelNewPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new"));
                LabelNewPasswordRepeat.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new_repeat"));
                CheckPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_visible"));
                BtnChangePassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_btn"));
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
            change_password_system_preloader();
        }
        // CHANGE PASSWORD BTN
        // ======================================================================================================
        private void BtnChangePassword_Click(object sender, EventArgs e){
            change_password_function();
        }
        // CHANGE PASSWORD FUNCTION
        // ======================================================================================================
        private void change_password_function(){
            TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
            try{
                string password_current = TxtCurrentPassword.Text.Trim();
                string password_new = TxtNewPassword.Text.Trim();
                string password_new_repeat = TxtNewPasswordRepeat.Text.Trim();
                if (string.IsNullOrEmpty(password_current)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_info")));
                    return;
                }
                if (string.IsNullOrEmpty(password_new)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_info")));
                    return;
                }
                if (string.IsNullOrEmpty(password_new_repeat)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_repeat_info")));
                    return;
                }
                if (password_new.Length < 6 || password_new.Length > 32){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_req_info")));
                    return;
                }
                //
                password_current = TSHashPassword(TxtCurrentPassword.Text.Trim(), ts_hash_salting);
                TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
                string saved_password = software_read_settings.TSReadSettings(ts_settings_container, "Password");
                //
                if (password_current == saved_password){
                    if (password_new == password_new_repeat){
                        TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                        software_setting_save.TSWriteSettings(ts_settings_container, "Password", TSHashPassword(password_new, ts_hash_salting));
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_change_success")), "\n"));
                    }
                    else{
                        TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_compare_info")), "\n"));
                    }
                }else{
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_fail_info")), "\n"));
                }
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