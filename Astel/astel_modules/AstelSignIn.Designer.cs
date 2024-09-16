namespace Astel.astel_modules
{
    partial class AstelSignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AstelSignIn));
            this.Panel_BG = new System.Windows.Forms.Panel();
            this.CheckPassword = new System.Windows.Forms.CheckBox();
            this.BtnSignIn = new System.Windows.Forms.Button();
            this.LabelPasswordRepeat = new System.Windows.Forms.Label();
            this.TxtPasswordRepeat = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LabelHeader = new System.Windows.Forms.Label();
            this.Panel_BG.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.BackColor = System.Drawing.Color.Transparent;
            this.Panel_BG.Controls.Add(this.CheckPassword);
            this.Panel_BG.Controls.Add(this.BtnSignIn);
            this.Panel_BG.Controls.Add(this.LabelPasswordRepeat);
            this.Panel_BG.Controls.Add(this.TxtPasswordRepeat);
            this.Panel_BG.Controls.Add(this.LabelPassword);
            this.Panel_BG.Controls.Add(this.TxtPassword);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(3, 3);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(303, 295);
            this.Panel_BG.TabIndex = 0;
            // 
            // CheckPassword
            // 
            this.CheckPassword.AutoSize = true;
            this.CheckPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckPassword.Location = new System.Drawing.Point(10, 191);
            this.CheckPassword.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.CheckPassword.Name = "CheckPassword";
            this.CheckPassword.Size = new System.Drawing.Size(107, 21);
            this.CheckPassword.TabIndex = 5;
            this.CheckPassword.Text = "Şifreyi Göster";
            this.CheckPassword.UseVisualStyleBackColor = true;
            this.CheckPassword.CheckedChanged += new System.EventHandler(this.CheckPassword_CheckedChanged);
            // 
            // BtnSignIn
            // 
            this.BtnSignIn.BackColor = System.Drawing.Color.White;
            this.BtnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSignIn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnSignIn.FlatAppearance.BorderSize = 0;
            this.BtnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSignIn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnSignIn.Location = new System.Drawing.Point(10, 250);
            this.BtnSignIn.Name = "BtnSignIn";
            this.BtnSignIn.Size = new System.Drawing.Size(283, 35);
            this.BtnSignIn.TabIndex = 6;
            this.BtnSignIn.Text = "KAYDET";
            this.BtnSignIn.UseVisualStyleBackColor = false;
            this.BtnSignIn.Click += new System.EventHandler(this.BtnSignIn_Click);
            // 
            // LabelPasswordRepeat
            // 
            this.LabelPasswordRepeat.AutoSize = true;
            this.LabelPasswordRepeat.BackColor = System.Drawing.Color.Transparent;
            this.LabelPasswordRepeat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPasswordRepeat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPasswordRepeat.Location = new System.Drawing.Point(7, 134);
            this.LabelPasswordRepeat.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPasswordRepeat.Name = "LabelPasswordRepeat";
            this.LabelPasswordRepeat.Size = new System.Drawing.Size(84, 19);
            this.LabelPasswordRepeat.TabIndex = 3;
            this.LabelPasswordRepeat.Text = "Şifre Tekrar:";
            // 
            // TxtPasswordRepeat
            // 
            this.TxtPasswordRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPasswordRepeat.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPasswordRepeat.Location = new System.Drawing.Point(10, 156);
            this.TxtPasswordRepeat.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.TxtPasswordRepeat.Name = "TxtPasswordRepeat";
            this.TxtPasswordRepeat.Size = new System.Drawing.Size(283, 25);
            this.TxtPasswordRepeat.TabIndex = 4;
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
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(10, 97);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(283, 25);
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
            this.LabelHeader.Size = new System.Drawing.Size(283, 35);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "ŞİFRE BELİRLE";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AstelSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(309, 301);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(325, 340);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(325, 340);
            this.Name = "AstelSignIn";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelSignIn";
            this.Load += new System.EventHandler(this.AstelSignIn_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private System.Windows.Forms.CheckBox CheckPassword;
        private System.Windows.Forms.Button BtnSignIn;
        internal System.Windows.Forms.Label LabelPasswordRepeat;
        private System.Windows.Forms.TextBox TxtPasswordRepeat;
        internal System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        internal System.Windows.Forms.Label LabelHeader;
    }
}