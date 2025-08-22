﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
// TS MODULES
using static Astel.TSModules;
using System.Drawing;

namespace Astel.astel_modules{
    public partial class AstelPasswordGenerator : Form{
        // ASTEL PASS GENERATOR CLASS
        // ======================================================================================================
        public class TS_AstelPasswordGenerator{
            private readonly Random random_gen = new Random();
            private readonly Dictionary<string, (string upper, string lower, string digit, string special)> modes;
            public TS_AstelPasswordGenerator(){
                modes = new Dictionary<string, (string, string, string, string)> {
                    { "readable", ("ABCDEFGHJKLMNPQRSTUVWXYZ", "abcdefghjkmnpqrstuvwxyz", "23456789", "") },
                    { "writable", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789", "-_") },
                    { "random", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789", "!@#$%^&*()-_=+[]{}|;:,.<>?") }
                };
            }
            public string AstelGeneratePassword(bool includeUppercase, bool includeLowercase, bool includeNumeric, bool includeSpecialChars, string mode, int passwordLength){
                var (upper, lower, digit, special) = modes[mode];
                var charSet = new StringBuilder();
                //
                foreach (var (condition, chars) in new[]{
                    (includeUppercase, upper),
                    (includeLowercase, lower),
                    (includeNumeric, digit),
                    (includeSpecialChars, special)
                }){
                    if (condition) charSet.Append(chars);
                }
                //
                if (charSet.Length == 0){
                    TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
                    throw new ArgumentException(lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_info"));
                }
                //
                var password = new StringBuilder();
                for (int i = 0; i < passwordLength; i++){
                    password.Append(charSet[random_gen.Next(charSet.Length)]);
                }
                return password.ToString();
            }
        }
        // AUXILIARY METHODS
        // ======================================================================================================
        private void SetControlColors<T>(Control container, Action<T> setColors) where T : Control{
            foreach (Control control in container.Controls){
                if (control is T typedControl)
                    setColors(typedControl);
            }
        }
        // FORM EVENTS & INIT
        // ======================================================================================================
        private readonly TS_AstelPasswordGenerator generator;
        private readonly Dictionary<string, (bool upperEnabled, bool lowerEnabled, bool numericEnabled, bool specialEnabled, bool upperChecked, bool lowerChecked, bool numericChecked, bool specialChecked)> modes;
        public AstelPasswordGenerator(){
            InitializeComponent();
            generator = new TS_AstelPasswordGenerator();
            modes = new Dictionary<string, (bool, bool, bool, bool, bool, bool, bool, bool)>(){
                { "readable", (false, true, true, false, false, true, true, false) },
                { "writable", (true, true, true, true, true, true, true, true) },
                { "random", (true, true, true, true, true, true, true, true) }
            };
            //
            RadioRead.CheckedChanged += RadioButton_CheckedChanged;
            RadioWrite.CheckedChanged += RadioButton_CheckedChanged;
            RadioMixed.CheckedChanged += RadioButton_CheckedChanged;
        }
        public void Password_generator_preloader(){
            try{
                //
                TSSetWindowTheme(Handle, AstelMain.theme);
                //
                MainToolTip.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "HeaderFEColor2");
                MainToolTip.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "HeaderBGColor2");
                //
                BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "HeaderBGColor2");
                //
                SetControlColors<Label>(Panel_BG, lbl => lbl.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor"));
                SetControlColors<TextBox>(Panel_BG, tb =>{
                    tb.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TextboxBGColor");
                    tb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TextboxFEColor");
                });
                SetControlColors<Button>(Panel_BG, btn =>{
                    btn.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                    var color = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                    btn.BackColor = color;
                    btn.FlatAppearance.BorderColor = color;
                    btn.FlatAppearance.MouseDownBackColor = color;
                    btn.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMainHover");
                });
                //
                LabelHeader.BackColor = Panel_Feature.BackColor = Panel_Mode.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "PageContainerUIBGColor");
                LabelHeader.ForeColor = LabelFeature.ForeColor = LabelMode.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor");
                LabelFeature.BackColor = LabelMode.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "HeaderBGColor2");
                //
                SetControlColors<CheckBox>(Panel_Feature, cb => cb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor"));
                SetControlColors<RadioButton>(Panel_Mode, rb => rb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor"));
                //
                PassLenghtLabel.BackColor = PassGenLenght.BackColor = PassResultLabel.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "PageContainerUIBGColor");
                PassLenghtLabel.ForeColor = PassGenLenght.ForeColor = PassResultLabel.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "ContentLabelLeftColor");
                //
                TSImageRenderer(BtnGenPass, AstelMain.theme == 1 ? Properties.Resources.ct_generate_light : Properties.Resources.ct_generate_dark, 18, ContentAlignment.MiddleLeft);
                //
                RadioRead.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                RadioWrite.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                RadioMixed.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentMain");
                //
                var lang = new TSGetLangs(AstelMain.lang_path);
                Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_title"), Application.ProductName);
                LabelHeader.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_header");
                LabelFeature.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_title");
                LabelMode.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_title");
                CheckUppercase.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_uppercase");
                CheckLowercase.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_lowercase");
                CheckNumeric.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_numeric");
                CheckSpecialChars.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_special_chars");
                RadioRead.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_read");
                RadioWrite.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_write");
                RadioMixed.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_mixed");
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(PassResultLabel, lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy"));
                BtnGenPass.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_gen_pass_btn") + " ";
            }catch (Exception){ }
        }
        // TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e) { e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private void AstelPasswordGenerator_Load(object sender, EventArgs e){
            AcceptButton = BtnGenPass;
            TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
            PassLenghtLabel.Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length"), PassGenLenght.Value);
            Password_generator_preloader();
        }
        // UI FUNCTIONS
        // ======================================================================================================
        private void RadioButton_CheckedChanged(object sender, EventArgs e){
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            UpdateCheckboxesByMode(mode);
        }
        private void UpdateCheckboxesByMode(string mode){
            var (_, _, _, _, upperChecked, lowerChecked, numericChecked, specialChecked) = modes[mode];
            CheckUppercase.Checked = upperChecked;
            CheckLowercase.Checked = lowerChecked;
            CheckNumeric.Checked = numericChecked;
            CheckSpecialChars.Checked = specialChecked;
        }
        private void PassGenLenght_ValueChanged(object sender, EventArgs e){
            TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
            PassLenghtLabel.Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length"), PassGenLenght.Value);
        }
        private void Astel_pass_gen_engine(){
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            try{
                string password = generator.AstelGeneratePassword(
                    CheckUppercase.Checked,
                    CheckLowercase.Checked,
                    CheckNumeric.Checked,
                    CheckSpecialChars.Checked,
                    mode,
                    PassGenLenght.Value
                );
                PassResultLabel.Text = password;
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, ex.Message);
            }
        }
        // LAUNCHER
        // ======================================================================================================
        private void BtnGenPass_Click(object sender, EventArgs e) => Astel_pass_gen_engine();
        // COPY PASSWORD
        // ======================================================================================================
        private void PassResultLabel_DoubleClick(object sender, EventArgs e){
            if (!string.IsNullOrWhiteSpace(PassResultLabel.Text)){
                Clipboard.SetText(PassResultLabel.Text.Trim());
                TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 1, lang.TSReadLangs("AstelPasswordGenerator", "apg_copy_password"));
            }
        }
        // PASSWORD HOVER TEXT
        // ======================================================================================================
        private void PassResultLabel_MouseEnter(object sender, EventArgs e){
            MainToolTip.RemoveAll();
            if (!string.IsNullOrEmpty(PassResultLabel.Text)){
                TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
                MainToolTip.SetToolTip(PassResultLabel, lang.TSReadLangs("AstelPasswordGenerator", "apg_pass_copy"));
            }
        }
    }
}