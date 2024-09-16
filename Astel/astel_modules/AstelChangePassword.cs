using System;
using System.Text;
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
                BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
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
                    }
                }
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                // TEXTS
                Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_title").Trim())), Application.ProductName);
                LabelHeader.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_header").Trim()));
                LabelCurrentPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_current").Trim()));
                LabelNewPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new").Trim()));
                LabelNewPasswordRepeat.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new_repeat").Trim()));
                CheckPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_visible").Trim()));
                BtnChangePassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_btn").Trim()));
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
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(password_new)){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(password_new_repeat)){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_repeat_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (password_new.Length < 6 || password_new.Length > 32){
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_req_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_change_success").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }else{
                        MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_compare_info").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }else{
                    MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_fail_info").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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