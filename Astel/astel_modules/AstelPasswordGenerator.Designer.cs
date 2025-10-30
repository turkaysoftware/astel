namespace Astel.astel_modules
{
    partial class AstelPasswordGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AstelPasswordGenerator));
            this.Panel_BG = new System.Windows.Forms.Panel();
            this.PassResultLabel = new System.Windows.Forms.Label();
            this.TLP_Control = new System.Windows.Forms.TableLayoutPanel();
            this.Panel_Feature = new System.Windows.Forms.Panel();
            this.LabelFeature = new System.Windows.Forms.Label();
            this.CheckSpecialChars = new System.Windows.Forms.CheckBox();
            this.CheckNumeric = new System.Windows.Forms.CheckBox();
            this.CheckLowercase = new System.Windows.Forms.CheckBox();
            this.CheckUppercase = new System.Windows.Forms.CheckBox();
            this.Panel_Mode = new System.Windows.Forms.Panel();
            this.LabelMode = new System.Windows.Forms.Label();
            this.RadioMixed = new System.Windows.Forms.RadioButton();
            this.RadioWrite = new System.Windows.Forms.RadioButton();
            this.RadioRead = new System.Windows.Forms.RadioButton();
            this.BtnGenPass = new Astel.TSCustomButton();
            this.PassLenghtLabel = new System.Windows.Forms.Label();
            this.PassGenLenght = new System.Windows.Forms.TrackBar();
            this.LabelHeader = new System.Windows.Forms.Label();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Panel_BG.SuspendLayout();
            this.TLP_Control.SuspendLayout();
            this.Panel_Feature.SuspendLayout();
            this.Panel_Mode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PassGenLenght)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.Controls.Add(this.PassResultLabel);
            this.Panel_BG.Controls.Add(this.TLP_Control);
            this.Panel_BG.Controls.Add(this.BtnGenPass);
            this.Panel_BG.Controls.Add(this.PassLenghtLabel);
            this.Panel_BG.Controls.Add(this.PassGenLenght);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(0, 0);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(584, 501);
            this.Panel_BG.TabIndex = 0;
            // 
            // PassResultLabel
            // 
            this.PassResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassResultLabel.BackColor = System.Drawing.Color.White;
            this.PassResultLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PassResultLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.PassResultLabel.Location = new System.Drawing.Point(10, 372);
            this.PassResultLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassResultLabel.Name = "PassResultLabel";
            this.PassResultLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PassResultLabel.Size = new System.Drawing.Size(564, 53);
            this.PassResultLabel.TabIndex = 4;
            this.PassResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PassResultLabel.DoubleClick += new System.EventHandler(this.PassResultLabel_DoubleClick);
            this.PassResultLabel.MouseEnter += new System.EventHandler(this.PassResultLabel_MouseEnter);
            // 
            // TLP_Control
            // 
            this.TLP_Control.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TLP_Control.BackColor = System.Drawing.Color.Transparent;
            this.TLP_Control.ColumnCount = 2;
            this.TLP_Control.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Control.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Control.Controls.Add(this.Panel_Feature, 0, 0);
            this.TLP_Control.Controls.Add(this.Panel_Mode, 1, 0);
            this.TLP_Control.Location = new System.Drawing.Point(10, 68);
            this.TLP_Control.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.TLP_Control.Name = "TLP_Control";
            this.TLP_Control.RowCount = 1;
            this.TLP_Control.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Control.Size = new System.Drawing.Size(564, 195);
            this.TLP_Control.TabIndex = 1;
            // 
            // Panel_Feature
            // 
            this.Panel_Feature.BackColor = System.Drawing.Color.White;
            this.Panel_Feature.Controls.Add(this.LabelFeature);
            this.Panel_Feature.Controls.Add(this.CheckSpecialChars);
            this.Panel_Feature.Controls.Add(this.CheckNumeric);
            this.Panel_Feature.Controls.Add(this.CheckLowercase);
            this.Panel_Feature.Controls.Add(this.CheckUppercase);
            this.Panel_Feature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Feature.Location = new System.Drawing.Point(0, 0);
            this.Panel_Feature.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Panel_Feature.Name = "Panel_Feature";
            this.Panel_Feature.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Feature.Size = new System.Drawing.Size(279, 195);
            this.Panel_Feature.TabIndex = 0;
            // 
            // LabelFeature
            // 
            this.LabelFeature.BackColor = System.Drawing.Color.Silver;
            this.LabelFeature.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelFeature.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelFeature.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelFeature.Location = new System.Drawing.Point(10, 10);
            this.LabelFeature.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.LabelFeature.Name = "LabelFeature";
            this.LabelFeature.Size = new System.Drawing.Size(259, 35);
            this.LabelFeature.TabIndex = 0;
            this.LabelFeature.Text = "GİRİŞ YAP";
            this.LabelFeature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckSpecialChars
            // 
            this.CheckSpecialChars.AutoSize = true;
            this.CheckSpecialChars.Checked = true;
            this.CheckSpecialChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckSpecialChars.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckSpecialChars.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckSpecialChars.Location = new System.Drawing.Point(10, 160);
            this.CheckSpecialChars.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckSpecialChars.Name = "CheckSpecialChars";
            this.CheckSpecialChars.Size = new System.Drawing.Size(108, 21);
            this.CheckSpecialChars.TabIndex = 4;
            this.CheckSpecialChars.Text = "Özel Karakter";
            this.CheckSpecialChars.UseVisualStyleBackColor = true;
            // 
            // CheckNumeric
            // 
            this.CheckNumeric.AutoSize = true;
            this.CheckNumeric.Checked = true;
            this.CheckNumeric.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckNumeric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckNumeric.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckNumeric.Location = new System.Drawing.Point(10, 126);
            this.CheckNumeric.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckNumeric.Name = "CheckNumeric";
            this.CheckNumeric.Size = new System.Drawing.Size(68, 21);
            this.CheckNumeric.TabIndex = 3;
            this.CheckNumeric.Text = "Rakam";
            this.CheckNumeric.UseVisualStyleBackColor = true;
            // 
            // CheckLowercase
            // 
            this.CheckLowercase.AutoSize = true;
            this.CheckLowercase.Checked = true;
            this.CheckLowercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckLowercase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckLowercase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckLowercase.Location = new System.Drawing.Point(10, 92);
            this.CheckLowercase.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckLowercase.Name = "CheckLowercase";
            this.CheckLowercase.Size = new System.Drawing.Size(94, 21);
            this.CheckLowercase.TabIndex = 2;
            this.CheckLowercase.Text = "Küçük Harf";
            this.CheckLowercase.UseVisualStyleBackColor = true;
            // 
            // CheckUppercase
            // 
            this.CheckUppercase.AutoSize = true;
            this.CheckUppercase.Checked = true;
            this.CheckUppercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckUppercase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckUppercase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckUppercase.Location = new System.Drawing.Point(10, 58);
            this.CheckUppercase.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckUppercase.Name = "CheckUppercase";
            this.CheckUppercase.Size = new System.Drawing.Size(95, 21);
            this.CheckUppercase.TabIndex = 1;
            this.CheckUppercase.Text = "Büyük Harf";
            this.CheckUppercase.UseVisualStyleBackColor = true;
            // 
            // Panel_Mode
            // 
            this.Panel_Mode.BackColor = System.Drawing.Color.White;
            this.Panel_Mode.Controls.Add(this.LabelMode);
            this.Panel_Mode.Controls.Add(this.RadioMixed);
            this.Panel_Mode.Controls.Add(this.RadioWrite);
            this.Panel_Mode.Controls.Add(this.RadioRead);
            this.Panel_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Mode.Location = new System.Drawing.Point(285, 0);
            this.Panel_Mode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Panel_Mode.Name = "Panel_Mode";
            this.Panel_Mode.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Mode.Size = new System.Drawing.Size(279, 195);
            this.Panel_Mode.TabIndex = 1;
            // 
            // LabelMode
            // 
            this.LabelMode.BackColor = System.Drawing.Color.Silver;
            this.LabelMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelMode.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelMode.Location = new System.Drawing.Point(10, 10);
            this.LabelMode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.LabelMode.Name = "LabelMode";
            this.LabelMode.Size = new System.Drawing.Size(259, 35);
            this.LabelMode.TabIndex = 0;
            this.LabelMode.Text = "GİRİŞ YAP";
            this.LabelMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioMixed
            // 
            this.RadioMixed.AutoSize = true;
            this.RadioMixed.Checked = true;
            this.RadioMixed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioMixed.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioMixed.Location = new System.Drawing.Point(10, 126);
            this.RadioMixed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioMixed.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioMixed.Name = "RadioMixed";
            this.RadioMixed.Size = new System.Drawing.Size(65, 21);
            this.RadioMixed.TabIndex = 3;
            this.RadioMixed.TabStop = true;
            this.RadioMixed.Text = "Karışık";
            this.RadioMixed.UseVisualStyleBackColor = true;
            // 
            // RadioWrite
            // 
            this.RadioWrite.AutoSize = true;
            this.RadioWrite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioWrite.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioWrite.Location = new System.Drawing.Point(10, 92);
            this.RadioWrite.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioWrite.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioWrite.Name = "RadioWrite";
            this.RadioWrite.Size = new System.Drawing.Size(111, 21);
            this.RadioWrite.TabIndex = 2;
            this.RadioWrite.Text = "Yazması Kolay";
            this.RadioWrite.UseVisualStyleBackColor = true;
            // 
            // RadioRead
            // 
            this.RadioRead.AutoSize = true;
            this.RadioRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioRead.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioRead.Location = new System.Drawing.Point(10, 58);
            this.RadioRead.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioRead.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioRead.Name = "RadioRead";
            this.RadioRead.Size = new System.Drawing.Size(116, 21);
            this.RadioRead.TabIndex = 1;
            this.RadioRead.Text = "Okuması Kolay";
            this.RadioRead.UseVisualStyleBackColor = true;
            // 
            // BtnGenPass
            // 
            this.BtnGenPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnGenPass.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnGenPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnGenPass.BorderRadius = 10;
            this.BtnGenPass.BorderSize = 0;
            this.BtnGenPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenPass.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnGenPass.FlatAppearance.BorderSize = 0;
            this.BtnGenPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenPass.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnGenPass.ForeColor = System.Drawing.Color.White;
            this.BtnGenPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGenPass.Location = new System.Drawing.Point(10, 456);
            this.BtnGenPass.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.BtnGenPass.Name = "BtnGenPass";
            this.BtnGenPass.Size = new System.Drawing.Size(564, 35);
            this.BtnGenPass.TabIndex = 5;
            this.BtnGenPass.Text = "KAYDET";
            this.BtnGenPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGenPass.TextColor = System.Drawing.Color.White;
            this.BtnGenPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnGenPass.UseVisualStyleBackColor = false;
            this.BtnGenPass.Click += new System.EventHandler(this.BtnGenPass_Click);
            // 
            // PassLenghtLabel
            // 
            this.PassLenghtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassLenghtLabel.BackColor = System.Drawing.Color.White;
            this.PassLenghtLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.PassLenghtLabel.Location = new System.Drawing.Point(10, 271);
            this.PassLenghtLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassLenghtLabel.Name = "PassLenghtLabel";
            this.PassLenghtLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PassLenghtLabel.Size = new System.Drawing.Size(564, 40);
            this.PassLenghtLabel.TabIndex = 2;
            this.PassLenghtLabel.Text = "Şifre Uzunluğu:";
            this.PassLenghtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PassGenLenght
            // 
            this.PassGenLenght.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassGenLenght.BackColor = System.Drawing.Color.White;
            this.PassGenLenght.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PassGenLenght.LargeChange = 1;
            this.PassGenLenght.Location = new System.Drawing.Point(10, 319);
            this.PassGenLenght.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassGenLenght.Maximum = 32;
            this.PassGenLenght.Minimum = 6;
            this.PassGenLenght.Name = "PassGenLenght";
            this.PassGenLenght.Size = new System.Drawing.Size(564, 45);
            this.PassGenLenght.TabIndex = 3;
            this.PassGenLenght.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.PassGenLenght.Value = 12;
            this.PassGenLenght.ValueChanged += new System.EventHandler(this.PassGenLenght_ValueChanged);
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.Color.White;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelHeader.Location = new System.Drawing.Point(10, 10);
            this.LabelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(564, 35);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "GİRİŞ YAP";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainToolTip
            // 
            this.MainToolTip.OwnerDraw = true;
            this.MainToolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.MainToolTip_Draw);
            // 
            // AstelPasswordGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 501);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = Properties.Resources.AstelLogo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AstelPasswordGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelPasswordGenerator";
            this.Load += new System.EventHandler(this.AstelPasswordGenerator_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.TLP_Control.ResumeLayout(false);
            this.Panel_Feature.ResumeLayout(false);
            this.Panel_Feature.PerformLayout();
            this.Panel_Mode.ResumeLayout(false);
            this.Panel_Mode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PassGenLenght)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private System.Windows.Forms.Label PassResultLabel;
        private System.Windows.Forms.TableLayoutPanel TLP_Control;
        private System.Windows.Forms.Panel Panel_Feature;
        internal System.Windows.Forms.Label LabelFeature;
        private System.Windows.Forms.CheckBox CheckSpecialChars;
        private System.Windows.Forms.CheckBox CheckNumeric;
        private System.Windows.Forms.CheckBox CheckLowercase;
        private System.Windows.Forms.CheckBox CheckUppercase;
        private System.Windows.Forms.Panel Panel_Mode;
        internal System.Windows.Forms.Label LabelMode;
        private System.Windows.Forms.RadioButton RadioMixed;
        private System.Windows.Forms.RadioButton RadioWrite;
        private System.Windows.Forms.RadioButton RadioRead;
        private TSCustomButton BtnGenPass;
        private System.Windows.Forms.Label PassLenghtLabel;
        private System.Windows.Forms.TrackBar PassGenLenght;
        internal System.Windows.Forms.Label LabelHeader;
        private System.Windows.Forms.ToolTip MainToolTip;
    }
}