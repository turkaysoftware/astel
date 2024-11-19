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
                    { "random", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789", "!@#$%^&*()-_=+[]{}|;:,.<>?") } // Mixed
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
                    TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                    throw new ArgumentException(TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_info")));
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
                //
                LabelFeature.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                LabelFeature.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                LabelMode.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "HeaderBGColor2");
                LabelMode.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                Panel_Feature.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                Panel_Mode.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
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
                PassLenghtLabel.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                PassLenghtLabel.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                PassGenLenght.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                PassGenLenght.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                //
                PassResultLabel.BackColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "PageContainerUIBGColor");
                PassResultLabel.ForeColor = TS_ThemeEngine.ColorMode(Convert.ToInt32(Astel.theme), "ContentLabelLeftColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(Astel.lang_path);
                // TEXTS
                Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_title")), Application.ProductName);
                LabelHeader.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_header"));
                //
                LabelFeature.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_title"));
                LabelMode.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_title"));
                //
                CheckUppercase.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_uppercase"));
                CheckLowercase.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_lowercase"));
                CheckNumeric.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_numeric"));
                CheckSpecialChars.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_special_chars"));
                //
                RadioRead.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_read"));
                RadioWrite.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_write"));
                RadioMixed.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_mixed"));
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(PassResultLabel, TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy")));
                //
                BtnGenPass.Text = TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_gen_pass_btn"));
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
            PassLenghtLabel.Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length")), PassGenLenght.Value.ToString());
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
            PassLenghtLabel.Text = string.Format(TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length")), PassGenLenght.Value.ToString());
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
                TS_MessageBoxEngine.TS_MessageBox(this, 2, ex.Message);
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
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_copy_password")));
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
                MainToolTip.SetToolTip(PassResultLabel, TS_String_Encoder(software_lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy")));
            }
        }
    }
}