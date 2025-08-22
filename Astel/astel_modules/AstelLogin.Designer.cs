namespace Astel.astel_modules
{
    partial class AstelLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AstelLogin));
            this.Panel_BG = new System.Windows.Forms.Panel();
            this.CheckPassword = new System.Windows.Forms.CheckBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LabelHeader = new System.Windows.Forms.Label();
            this.BtnLogin = new Astel.TSCustomButton();
            this.Panel_BG.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.BackColor = System.Drawing.Color.Transparent;
            this.Panel_BG.Controls.Add(this.CheckPassword);
            this.Panel_BG.Controls.Add(this.LabelPassword);
            this.Panel_BG.Controls.Add(this.TxtPassword);
            this.Panel_BG.Controls.Add(this.BtnLogin);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(3, 3);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(353, 235);
            this.Panel_BG.TabIndex = 0;
            // 
            // CheckPassword
            // 
            this.CheckPassword.AutoSize = true;
            this.CheckPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckPassword.Location = new System.Drawing.Point(10, 132);
            this.CheckPassword.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.CheckPassword.Name = "CheckPassword";
            this.CheckPassword.Size = new System.Drawing.Size(107, 21);
            this.CheckPassword.TabIndex = 3;
            this.CheckPassword.Text = "Şifreyi Göster";
            this.CheckPassword.UseVisualStyleBackColor = true;
            this.CheckPassword.CheckedChanged += new System.EventHandler(this.CheckPassword_CheckedChanged);
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPassword.Location = new System.Drawing.Point(7, 75);
            this.LabelPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(41, 19);
            this.LabelPassword.TabIndex = 1;
            this.LabelPassword.Text = "Şifre:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(10, 97);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.TxtPassword.MaxLength = 32;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(333, 25);
            this.TxtPassword.TabIndex = 2;
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.Color.White;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelHeader.Location = new System.Drawing.Point(10, 10);
            this.LabelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(333, 35);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "GİRİŞ YAP";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnLogin
            // 
            this.BtnLogin.BackColor = System.Drawing.Color.Green;
            this.BtnLogin.BackgroundColor = System.Drawing.Color.Green;
            this.BtnLogin.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnLogin.BorderRadius = 10;
            this.BtnLogin.BorderSize = 0;
            this.BtnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnLogin.FlatAppearance.BorderSize = 0;
            this.BtnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnLogin.ForeColor = System.Drawing.Color.White;
            this.BtnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLogin.Location = new System.Drawing.Point(10, 190);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(333, 35);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "KAYDET";
            this.BtnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnLogin.TextColor = System.Drawing.Color.White;
            this.BtnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.BtnLogin.UseVisualStyleBackColor = false;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // AstelLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(359, 241);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(375, 280);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(375, 280);
            this.Name = "AstelLogin";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelLogin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AstelLogin_FormClosing);
            this.Load += new System.EventHandler(this.AstelLogin_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private System.Windows.Forms.CheckBox CheckPassword;
        internal System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        private TSCustomButton BtnLogin;
        internal System.Windows.Forms.Label LabelHeader;
    }
}