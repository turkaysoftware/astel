using System;
using System.Windows.Forms;
using static Astel.TSModules;

namespace Astel.astel_modules{
    public partial class AstelSignIn : Form{
        public AstelSignIn(){ InitializeComponent(); CheckForIllegalCrossThreadCalls = false; }
        // SIGN IN PRELOADER
        // ======================================================================================================
        public void login_system_preloader(){
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
                Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_title")), Application.ProductName);
                LabelHeader.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_header"));
                LabelPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_label_password"));
                LabelPasswordRepeat.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_label_password_repeat"));
                CheckPassword.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_visible"));
                BtnSignIn.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_btn"));
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
            TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
            try{
                string password_1 = TxtPassword.Text.Trim();
                string password_2 = TxtPasswordRepeat.Text.Trim();
                if (string.IsNullOrEmpty(password_1)){
                    MessageBox.Show(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_info")), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(password_2)){
                    MessageBox.Show(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_repeat_info")), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (password_1.Length < 6 || password_1.Length > 32){
                    MessageBox.Show(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_req_info")), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (password_1 == password_2){
                    TSSettingsSave software_setting_save = new TSSettingsSave(ts_sf);
                    software_setting_save.TSWriteSettings(ts_settings_container, "SessionMode", "1");
                    software_setting_save.TSWriteSettings(ts_settings_container, "Password", TSHashPassword(password_1, ts_hash_salting));
                    MessageBox.Show(string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_set_success")), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }else{
                    MessageBox.Show(string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelSignIn", "as_password_set_failed")), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
    }
}