namespace Astel
{
    partial class Astel
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Astel));
            this.HeaderMenu = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turkishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initialViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkforUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.Panel_Footer = new System.Windows.Forms.Panel();
            this.BtnCopyPassword = new System.Windows.Forms.Button();
            this.BtnCopyEmail = new System.Windows.Forms.Button();
            this.BtnCopyUsername = new System.Windows.Forms.Button();
            this.LabelNote = new System.Windows.Forms.Label();
            this.TxtNote = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LabelMail = new System.Windows.Forms.Label();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.LabelUsername = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.AddBtn = new System.Windows.Forms.Button();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.DataMainTable = new System.Windows.Forms.DataGridView();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HeaderMenu.SuspendLayout();
            this.Panel_Main.SuspendLayout();
            this.Panel_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderMenu
            // 
            this.HeaderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.passwordGeneratorToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.HeaderMenu.Location = new System.Drawing.Point(0, 0);
            this.HeaderMenu.Name = "HeaderMenu";
            this.HeaderMenu.Size = new System.Drawing.Size(1008, 24);
            this.HeaderMenu.TabIndex = 0;
            this.HeaderMenu.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.initialViewToolStripMenuItem,
            this.loginSettingsToolStripMenuItem,
            this.checkforUpdatesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightThemeToolStripMenuItem,
            this.darkThemeToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // lightThemeToolStripMenuItem
            // 
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.lightThemeToolStripMenuItem.Text = "Light Theme";
            this.lightThemeToolStripMenuItem.Click += new System.EventHandler(this.lightThemeToolStripMenuItem_Click);
            // 
            // darkThemeToolStripMenuItem
            // 
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.darkThemeToolStripMenuItem.Text = "Dark Theme";
            this.darkThemeToolStripMenuItem.Click += new System.EventHandler(this.darkThemeToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.turkishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // turkishToolStripMenuItem
            // 
            this.turkishToolStripMenuItem.Name = "turkishToolStripMenuItem";
            this.turkishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.turkishToolStripMenuItem.Text = "Turkish";
            this.turkishToolStripMenuItem.Click += new System.EventHandler(this.turkishToolStripMenuItem_Click);
            // 
            // initialViewToolStripMenuItem
            // 
            this.initialViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowedToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
            this.initialViewToolStripMenuItem.Name = "initialViewToolStripMenuItem";
            this.initialViewToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.initialViewToolStripMenuItem.Text = "Initial Mode";
            // 
            // windowedToolStripMenuItem
            // 
            this.windowedToolStripMenuItem.Name = "windowedToolStripMenuItem";
            this.windowedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.windowedToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.windowedToolStripMenuItem.Text = "Windowed";
            this.windowedToolStripMenuItem.Click += new System.EventHandler(this.windowedToolStripMenuItem_Click);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
            // 
            // loginSettingsToolStripMenuItem
            // 
            this.loginSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPasswordToolStripMenuItem,
            this.changePasswordToolStripMenuItem});
            this.loginSettingsToolStripMenuItem.Name = "loginSettingsToolStripMenuItem";
            this.loginSettingsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loginSettingsToolStripMenuItem.Text = "Login Settings";
            // 
            // setPasswordToolStripMenuItem
            // 
            this.setPasswordToolStripMenuItem.Name = "setPasswordToolStripMenuItem";
            this.setPasswordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.setPasswordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.setPasswordToolStripMenuItem.Text = "Set Password";
            this.setPasswordToolStripMenuItem.Click += new System.EventHandler(this.setPasswordToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // checkforUpdatesToolStripMenuItem
            // 
            this.checkforUpdatesToolStripMenuItem.Name = "checkforUpdatesToolStripMenuItem";
            this.checkforUpdatesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.checkforUpdatesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.checkforUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkforUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkforUpdatesToolStripMenuItem_Click);
            // 
            // passwordGeneratorToolStripMenuItem
            // 
            this.passwordGeneratorToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.passwordGeneratorToolStripMenuItem.Name = "passwordGeneratorToolStripMenuItem";
            this.passwordGeneratorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.passwordGeneratorToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.passwordGeneratorToolStripMenuItem.Text = "Password Generator";
            this.passwordGeneratorToolStripMenuItem.Click += new System.EventHandler(this.passwordGeneratorToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Panel_Main
            // 
            this.Panel_Main.Controls.Add(this.Panel_Footer);
            this.Panel_Main.Controls.Add(this.DataMainTable);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 24);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Main.Size = new System.Drawing.Size(1008, 577);
            this.Panel_Main.TabIndex = 1;
            // 
            // Panel_Footer
            // 
            this.Panel_Footer.Controls.Add(this.BtnCopyPassword);
            this.Panel_Footer.Controls.Add(this.BtnCopyEmail);
            this.Panel_Footer.Controls.Add(this.BtnCopyUsername);
            this.Panel_Footer.Controls.Add(this.LabelNote);
            this.Panel_Footer.Controls.Add(this.TxtNote);
            this.Panel_Footer.Controls.Add(this.LabelPassword);
            this.Panel_Footer.Controls.Add(this.TxtPassword);
            this.Panel_Footer.Controls.Add(this.LabelMail);
            this.Panel_Footer.Controls.Add(this.TxtEmail);
            this.Panel_Footer.Controls.Add(this.LabelUsername);
            this.Panel_Footer.Controls.Add(this.TxtUserName);
            this.Panel_Footer.Controls.Add(this.AddBtn);
            this.Panel_Footer.Controls.Add(this.UpdateBtn);
            this.Panel_Footer.Controls.Add(this.DeleteBtn);
            this.Panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Footer.Location = new System.Drawing.Point(10, 387);
            this.Panel_Footer.Name = "Panel_Footer";
            this.Panel_Footer.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Footer.Size = new System.Drawing.Size(988, 180);
            this.Panel_Footer.TabIndex = 1;
            // 
            // BtnCopyPassword
            // 
            this.BtnCopyPassword.BackColor = System.Drawing.Color.DimGray;
            this.BtnCopyPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyPassword.FlatAppearance.BorderSize = 0;
            this.BtnCopyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyPassword.Location = new System.Drawing.Point(275, 150);
            this.BtnCopyPassword.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyPassword.Name = "BtnCopyPassword";
            this.BtnCopyPassword.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyPassword.TabIndex = 8;
            this.BtnCopyPassword.UseVisualStyleBackColor = false;
            this.BtnCopyPassword.Click += new System.EventHandler(this.BtnCopyPassword_Click);
            // 
            // BtnCopyEmail
            // 
            this.BtnCopyEmail.BackColor = System.Drawing.Color.DimGray;
            this.BtnCopyEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyEmail.FlatAppearance.BorderSize = 0;
            this.BtnCopyEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyEmail.Location = new System.Drawing.Point(275, 91);
            this.BtnCopyEmail.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyEmail.Name = "BtnCopyEmail";
            this.BtnCopyEmail.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyEmail.TabIndex = 5;
            this.BtnCopyEmail.UseVisualStyleBackColor = false;
            this.BtnCopyEmail.Click += new System.EventHandler(this.BtnCopyEmail_Click);
            // 
            // BtnCopyUsername
            // 
            this.BtnCopyUsername.BackColor = System.Drawing.Color.DimGray;
            this.BtnCopyUsername.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyUsername.FlatAppearance.BorderSize = 0;
            this.BtnCopyUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyUsername.Location = new System.Drawing.Point(275, 32);
            this.BtnCopyUsername.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyUsername.Name = "BtnCopyUsername";
            this.BtnCopyUsername.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyUsername.TabIndex = 2;
            this.BtnCopyUsername.UseVisualStyleBackColor = false;
            this.BtnCopyUsername.Click += new System.EventHandler(this.BtnCopyUsername_Click);
            // 
            // LabelNote
            // 
            this.LabelNote.AutoSize = true;
            this.LabelNote.BackColor = System.Drawing.Color.Transparent;
            this.LabelNote.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelNote.Location = new System.Drawing.Point(313, 10);
            this.LabelNote.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelNote.Name = "LabelNote";
            this.LabelNote.Size = new System.Drawing.Size(81, 19);
            this.LabelNote.TabIndex = 9;
            this.LabelNote.Text = "Sürücü Ara:";
            // 
            // TxtNote
            // 
            this.TxtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNote.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtNote.Location = new System.Drawing.Point(315, 32);
            this.TxtNote.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtNote.Multiline = true;
            this.TxtNote.Name = "TxtNote";
            this.TxtNote.Size = new System.Drawing.Size(275, 143);
            this.TxtNote.TabIndex = 10;
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPassword.Location = new System.Drawing.Point(-1, 128);
            this.LabelPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(81, 19);
            this.LabelPassword.TabIndex = 6;
            this.LabelPassword.Text = "Sürücü Ara:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(1, 150);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(275, 25);
            this.TxtPassword.TabIndex = 7;
            // 
            // LabelMail
            // 
            this.LabelMail.AutoSize = true;
            this.LabelMail.BackColor = System.Drawing.Color.Transparent;
            this.LabelMail.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelMail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelMail.Location = new System.Drawing.Point(-1, 69);
            this.LabelMail.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelMail.Name = "LabelMail";
            this.LabelMail.Size = new System.Drawing.Size(81, 19);
            this.LabelMail.TabIndex = 3;
            this.LabelMail.Text = "Sürücü Ara:";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtEmail.Location = new System.Drawing.Point(1, 91);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(275, 25);
            this.TxtEmail.TabIndex = 4;
            // 
            // LabelUsername
            // 
            this.LabelUsername.AutoSize = true;
            this.LabelUsername.BackColor = System.Drawing.Color.Transparent;
            this.LabelUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelUsername.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelUsername.Location = new System.Drawing.Point(-1, 10);
            this.LabelUsername.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelUsername.Name = "LabelUsername";
            this.LabelUsername.Size = new System.Drawing.Size(81, 19);
            this.LabelUsername.TabIndex = 0;
            this.LabelUsername.Text = "Sürücü Ara:";
            // 
            // TxtUserName
            // 
            this.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtUserName.Location = new System.Drawing.Point(1, 32);
            this.TxtUserName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(275, 25);
            this.TxtUserName.TabIndex = 1;
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.BackColor = System.Drawing.Color.DarkGray;
            this.AddBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddBtn.FlatAppearance.BorderSize = 0;
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.AddBtn.Location = new System.Drawing.Point(791, 58);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(197, 35);
            this.AddBtn.TabIndex = 11;
            this.AddBtn.Text = "EKLE";
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBtn.BackColor = System.Drawing.Color.DarkGray;
            this.UpdateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.UpdateBtn.Location = new System.Drawing.Point(791, 99);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(197, 35);
            this.UpdateBtn.TabIndex = 12;
            this.UpdateBtn.Text = "GÜNCELLE";
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBtn.BackColor = System.Drawing.Color.DarkGray;
            this.DeleteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.DeleteBtn.Location = new System.Drawing.Point(791, 140);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(197, 35);
            this.DeleteBtn.TabIndex = 13;
            this.DeleteBtn.Text = "SİL";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // DataMainTable
            // 
            this.DataMainTable.AllowUserToAddRows = false;
            this.DataMainTable.AllowUserToDeleteRows = false;
            this.DataMainTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DataMainTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataMainTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataMainTable.BackgroundColor = System.Drawing.Color.White;
            this.DataMainTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataMainTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(87)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(87)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataMainTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataMainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataMainTable.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(87)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataMainTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataMainTable.EnableHeadersVisualStyles = false;
            this.DataMainTable.GridColor = System.Drawing.Color.Gray;
            this.DataMainTable.Location = new System.Drawing.Point(10, 10);
            this.DataMainTable.MultiSelect = false;
            this.DataMainTable.Name = "DataMainTable";
            this.DataMainTable.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(87)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataMainTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataMainTable.RowHeadersVisible = false;
            this.DataMainTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataMainTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataMainTable.Size = new System.Drawing.Size(988, 557);
            this.DataMainTable.TabIndex = 0;
            this.DataMainTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataMainTable_CellDoubleClick);
            // 
            // MainToolTip
            // 
            this.MainToolTip.OwnerDraw = true;
            this.MainToolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.MainToolTip_Draw);
            // 
            // Astel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.Panel_Main);
            this.Controls.Add(this.HeaderMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 630);
            this.Name = "Astel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Astel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Astel_FormClosing);
            this.Load += new System.EventHandler(this.Astel_Load);
            this.HeaderMenu.ResumeLayout(false);
            this.HeaderMenu.PerformLayout();
            this.Panel_Main.ResumeLayout(false);
            this.Panel_Footer.ResumeLayout(false);
            this.Panel_Footer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip HeaderMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turkishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initialViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkforUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Footer;
        private System.Windows.Forms.Button BtnCopyPassword;
        private System.Windows.Forms.Button BtnCopyEmail;
        private System.Windows.Forms.Button BtnCopyUsername;
        internal System.Windows.Forms.Label LabelNote;
        private System.Windows.Forms.TextBox TxtNote;
        internal System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        internal System.Windows.Forms.Label LabelMail;
        private System.Windows.Forms.TextBox TxtEmail;
        internal System.Windows.Forms.Label LabelUsername;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.DataGridView DataMainTable;
        private System.Windows.Forms.ToolTip MainToolTip;
    }
}

