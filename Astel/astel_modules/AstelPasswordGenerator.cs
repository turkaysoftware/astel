using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using static Astel.TSModules;

namespace Astel.astel_modules{
    public partial class AstelPasswordGenerator : Form{
        // Astel PASS GENERATOR CLASS
        // ======================================================================================================
        public class TS_AstelPasswordGenerator{
            private Random random_gen = new Random();
            // Password modes and character set definitions for each mode
            private readonly Dictionary<string, (string upperCase, string lowerCase, string numeric, string specialChars)> modes;
            public TS_AstelPasswordGenerator(){
                // Define character sets for modes
                modes = new Dictionary<string, (string, string, string, string)>(){
                    { "readable", ("ABCDEFGHJKLMNPQRSTUVWXYZ", "abcdefghjkmnpqrstuvwxyz", "23456789", "") }, // Easy to Read
                    { "writable", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789", "-_") }, // Easy to Write
                    { "random", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789", "!@#$%^&*()-_=+[]{}|;:,.<>?") } // Random
                };
            }
            // Password generation function
            public string AstelGeneratePassword(bool includeUppercase, bool includeLowercase, bool includeNumeric, bool includeSpecialChars, string mode, int passwordLength){
                // Character set generation
                StringBuilder charSet = new StringBuilder();
                // Get the character set of the selected mode from the modes dictionary
                var selectedMode = modes[mode];
                //
                if (includeUppercase) charSet.Append(selectedMode.upperCase);
                if (includeLowercase) charSet.Append(selectedMode.lowerCase);
                if (includeNumeric) charSet.Append(selectedMode.numeric);
                if (includeSpecialChars) charSet.Append(selectedMode.specialChars);
                // If no character set is selected, give an error
                if (charSet.Length == 0){
                    throw new ArgumentException("Lütfen en az bir karakter tipi seçiniz.");
                }
                // Randomize password
                StringBuilder password = new StringBuilder();
                for (int i = 0; i < passwordLength; i++){
                    int index = random_gen.Next(charSet.Length);
                    password.Append(charSet[index]);
                }
                return password.ToString();
            }
        }
        // ======================================================================================================
        // ======================================================================================================
        private TS_AstelPasswordGenerator generator;
        // Dictionary holding checkbox settings according to radio button modes
        private Dictionary<string, (bool upperEnabled, bool lowerEnabled, bool numericEnabled, bool specialEnabled, bool upperChecked, bool lowerChecked, bool numericChecked, bool specialChecked)> modes;
        public AstelPasswordGenerator(){
            InitializeComponent();
            generator = new TS_AstelPasswordGenerator();
            // Dictionary that holds the status of checkboxes and whether they are selected or not according to radio button modes
            modes = new Dictionary<string, (bool, bool, bool, bool, bool, bool, bool, bool)>{
                { "readable", (false, true, true, false, false, true, true, false) }, // Easy to read mode
                { "writable", (true, true, true, true, true, true, true, true) },    // Easy to write mode
                { "random", (true, true, true, true, true, true, true, true) }       // Mixed mode
            };
            // Binding CheckedChanged events for radio buttons
            RadioRead.CheckedChanged += RadioButton_CheckedChanged;
            RadioWrite.CheckedChanged += RadioButton_CheckedChanged;
            RadioMixed.CheckedChanged += RadioButton_CheckedChanged;
        }
        // PASSWORD GENERATOR PRELOADER
        // ======================================================================================================
        public void password_generator_preloader(){
            try{
                if (Astel.theme == 1){
                    try { if (DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4) != 1) { DwmSetWindowAttribute(Handle, 20, new[] { 0 }, 4); } } catch (Exception) { }
                }else if (Astel.theme == 0){
                    try { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } } catch (Exception) { }
                }
                // TOOLTIP
                MainToolTip.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderFEColor2");
                MainToolTip.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                //
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
                //
                LabelFeature.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                LabelFeature.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                LabelMode.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                LabelMode.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                Panel_Feature.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                Panel_Mode.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                foreach (Control control in Panel_Feature.Controls){
                    if (control is CheckBox button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_Mode.Controls){
                    if (control is RadioButton button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                    }
                }
                //
                PassLenghtLabel.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                PassLenghtLabel.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                PassGenLenght.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                PassGenLenght.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                PassResultLabel.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerBGColor");
                PassResultLabel.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                // TEXTS
                Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_title").Trim())), Application.ProductName);
                LabelHeader.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_header").Trim()));
                //
                LabelFeature.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_title").Trim()));
                LabelMode.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_title").Trim()));
                //
                CheckUppercase.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_uppercase").Trim()));
                CheckLowercase.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_lowercase").Trim()));
                CheckNumeric.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_numeric").Trim()));
                CheckSpecialChars.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_special_chars").Trim()));
                //
                RadioRead.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_read").Trim()));
                RadioWrite.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_write").Trim()));
                RadioMixed.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_mixed").Trim()));
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(PassResultLabel, Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy").Trim())));
                //
                BtnGenPass.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_gen_pass_btn").Trim()));
            }catch (Exception){ }
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText();  }
        // Astel PASSWORD GENERATOR LOAD
        // ======================================================================================================
        private void AstelPasswordGenerator_Load(object sender, EventArgs e){
            AcceptButton = BtnGenPass;
            //
            TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
            PassLenghtLabel.Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length").Trim())), PassGenLenght.Value.ToString());
            //
            password_generator_preloader();
        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e){
            // Specify which mode is selected
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            // Update status of checkboxes according to mode
            UpdateCheckboxesByMode(mode);
        }
        // Function that updates the activation and selection status of checkboxes according to the mode
        private void UpdateCheckboxesByMode(string mode){
            var settings = modes[mode];
            //// Checkbox'ların aktiflik durumlarını ayarla
            //CheckUppercase.Enabled = settings.upperEnabled;
            //CheckLowercase.Enabled = settings.lowerEnabled;
            //CheckNumeric.Enabled = settings.numericEnabled;
            //CheckSpecialChars.Enabled = settings.specialEnabled;
            //
            // Checkbox'ların seçili olup olmadığını ayarla
            CheckUppercase.Checked = settings.upperChecked;
            CheckLowercase.Checked = settings.lowerChecked;
            CheckNumeric.Checked = settings.numericChecked;
            CheckSpecialChars.Checked = settings.specialChecked;
        }
        // PASS LENGHT CHANGE
        // ======================================================================================================
        private void PassGenLenght_ValueChanged(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
            PassLenghtLabel.Text = string.Format(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length").Trim())), PassGenLenght.Value.ToString());
        }
        // PASSWORD GENERATOR ENGINE
        // ======================================================================================================
        private void astel_pass_gen_engine(){
            // Specify which mode is selected
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            // Generate password
            try{
                string password = generator.AstelGeneratePassword(
                    CheckUppercase.Checked,
                    CheckLowercase.Checked,
                    CheckNumeric.Checked,
                    CheckSpecialChars.Checked,
                    mode,
                    PassGenLenght.Value
                );
                // Show generated password
                PassResultLabel.Text = password;
            }catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        // PASSWORD GENERATOR ENGINE START
        // ======================================================================================================
        private void BtnGenPass_Click(object sender, EventArgs e){
            astel_pass_gen_engine();
        }
        // PASSWORD GENERATOR COPY
        // ======================================================================================================
        private void PassResultLabel_DoubleClick(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(PassResultLabel.Text.Trim())){
                    Clipboard.SetText(PassResultLabel.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                    MessageBox.Show(Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_copy_password").Trim())), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception){ }
        }
        // PASSWORD GENERATOR HOVER TXT
        // ======================================================================================================
        private void PassResultLabel_MouseEnter(object sender, EventArgs e){
            if (string.IsNullOrEmpty(PassResultLabel.Text)){
                MainToolTip.RemoveAll();
            }else{
                TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(PassResultLabel, Encoding.UTF8.GetString(Encoding.Default.GetBytes(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy").Trim())));
            }
        }
    }
}