using System;
using System.Text;
using System.Windows.Forms;
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
                string theme_mode = software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus");
                switch (theme_mode){
                    case "0":
                        login_global_theme = theme_mode;
                        break;
                    case "1":
                        login_global_theme = theme_mode;
                        break;
                    default:
                        login_global_theme = "1";
                        break;
                }
                //
                if (Convert.ToInt32(login_global_theme) == 1){
                    try { if (DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4) != 1) { DwmSetWindowAttribute(Handle, 20, new[] { 0 }, 4); } } catch (Exception) { }
                }else if (Convert.ToInt32(login_global_theme) == 0){
                    try { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } } catch (Exception) { }
                }
                BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "PageContainerBGColor");
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
                    }
                }
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "PageContainerBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(login_global_theme), "ContentLabelLeftColor");
                // ======================================================================================================
                string lang_mode = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                TSGetLangs software_lang;
                switch (lang_mode){
                    case "en":
                        software_lang = new TSGetLangs(astel_lang_en);
                        login_global_lang = astel_lang_en;
                        break;
                    case "tr":
                        software_lang = new TSGetLangs(astel_lang_tr);
                        login_global_lang = astel_lang_tr;
                        break;
                    default:
                        software_lang = new TSGetLangs(astel_lang_en);
                        login_global_lang = astel_lang_en;
                        break;
                }
                // TEXTS
                Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_title").Trim())), Application.ProductName);
                LabelHeader.Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_header").Trim())), Environment.UserName);
                LabelPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_label_password").Trim()));
                CheckPassword.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_visible").Trim()));
                BtnLogin.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_btn").Trim()));
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
                        MessageBox.Show(string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_password_failed").Trim())), "\n"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }else{
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelLogin", "al_password_info").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}