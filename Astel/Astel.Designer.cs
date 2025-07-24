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
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.DataMainTable = new System.Windows.Forms.DataGridView();
            this.Panel_Footer = new System.Windows.Forms.Panel();
            this.FLP_Btns = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.LabelService = new System.Windows.Forms.Label();
            this.TxtService = new System.Windows.Forms.TextBox();
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
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.dataTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkforUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tSWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bmacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).BeginInit();
            this.Panel_Footer.SuspendLayout();
            this.FLP_Btns.SuspendLayout();
            this.HeaderMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.Controls.Add(this.DataMainTable);
            this.Panel_Main.Controls.Add(this.Panel_Footer);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 24);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Main.Size = new System.Drawing.Size(1008, 577);
            this.Panel_Main.TabIndex = 1;
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
            this.DataMainTable.Size = new System.Drawing.Size(988, 377);
            this.DataMainTable.TabIndex = 0;
            this.DataMainTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataMainTable_CellClick);
            // 
            // Panel_Footer
            // 
            this.Panel_Footer.Controls.Add(this.FLP_Btns);
            this.Panel_Footer.Controls.Add(this.LabelService);
            this.Panel_Footer.Controls.Add(this.TxtService);
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
            this.Panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Footer.Location = new System.Drawing.Point(10, 387);
            this.Panel_Footer.Name = "Panel_Footer";
            this.Panel_Footer.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.Panel_Footer.Size = new System.Drawing.Size(988, 180);
            this.Panel_Footer.TabIndex = 1;
            // 
            // FLP_Btns
            // 
            this.FLP_Btns.AutoSize = true;
            this.FLP_Btns.Controls.Add(this.DeleteBtn);
            this.FLP_Btns.Controls.Add(this.UpdateBtn);
            this.FLP_Btns.Controls.Add(this.AddBtn);
            this.FLP_Btns.Dock = System.Windows.Forms.DockStyle.Right;
            this.FLP_Btns.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.FLP_Btns.Location = new System.Drawing.Point(803, 10);
            this.FLP_Btns.Name = "FLP_Btns";
            this.FLP_Btns.Size = new System.Drawing.Size(185, 165);
            this.FLP_Btns.TabIndex = 14;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBtn.BackColor = System.Drawing.Color.DimGray;
            this.DeleteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.DeleteBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteBtn.Location = new System.Drawing.Point(0, 125);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DeleteBtn.Size = new System.Drawing.Size(185, 40);
            this.DeleteBtn.TabIndex = 2;
            this.DeleteBtn.Text = "SİL";
            this.DeleteBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBtn.BackColor = System.Drawing.Color.DimGray;
            this.UpdateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Location = new System.Drawing.Point(0, 75);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.UpdateBtn.Size = new System.Drawing.Size(185, 40);
            this.UpdateBtn.TabIndex = 1;
            this.UpdateBtn.Text = "GÜNCELLE";
            this.UpdateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.BackColor = System.Drawing.Color.DimGray;
            this.AddBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddBtn.FlatAppearance.BorderSize = 0;
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.AddBtn.ForeColor = System.Drawing.Color.White;
            this.AddBtn.Location = new System.Drawing.Point(0, 25);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.AddBtn.Size = new System.Drawing.Size(185, 40);
            this.AddBtn.TabIndex = 0;
            this.AddBtn.Text = "EKLE";
            this.AddBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // LabelService
            // 
            this.LabelService.AutoSize = true;
            this.LabelService.BackColor = System.Drawing.Color.Transparent;
            this.LabelService.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelService.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelService.Location = new System.Drawing.Point(-2, 10);
            this.LabelService.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelService.Name = "LabelService";
            this.LabelService.Size = new System.Drawing.Size(54, 19);
            this.LabelService.TabIndex = 0;
            this.LabelService.Text = "Hizmet";
            // 
            // TxtService
            // 
            this.TxtService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtService.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtService.Location = new System.Drawing.Point(0, 32);
            this.TxtService.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtService.MaxLength = 64;
            this.TxtService.Name = "TxtService";
            this.TxtService.Size = new System.Drawing.Size(299, 25);
            this.TxtService.TabIndex = 1;
            // 
            // BtnCopyPassword
            // 
            this.BtnCopyPassword.BackColor = System.Drawing.Color.DimGray;
            this.BtnCopyPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyPassword.FlatAppearance.BorderSize = 0;
            this.BtnCopyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyPassword.Location = new System.Drawing.Point(588, 32);
            this.BtnCopyPassword.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyPassword.Name = "BtnCopyPassword";
            this.BtnCopyPassword.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyPassword.TabIndex = 13;
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
            this.BtnCopyEmail.Location = new System.Drawing.Point(274, 150);
            this.BtnCopyEmail.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyEmail.Name = "BtnCopyEmail";
            this.BtnCopyEmail.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyEmail.TabIndex = 12;
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
            this.BtnCopyUsername.Location = new System.Drawing.Point(274, 91);
            this.BtnCopyUsername.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyUsername.Name = "BtnCopyUsername";
            this.BtnCopyUsername.Size = new System.Drawing.Size(25, 25);
            this.BtnCopyUsername.TabIndex = 11;
            this.BtnCopyUsername.UseVisualStyleBackColor = false;
            this.BtnCopyUsername.Click += new System.EventHandler(this.BtnCopyUsername_Click);
            // 
            // LabelNote
            // 
            this.LabelNote.AutoSize = true;
            this.LabelNote.BackColor = System.Drawing.Color.Transparent;
            this.LabelNote.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelNote.Location = new System.Drawing.Point(312, 69);
            this.LabelNote.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelNote.Name = "LabelNote";
            this.LabelNote.Size = new System.Drawing.Size(33, 19);
            this.LabelNote.TabIndex = 9;
            this.LabelNote.Text = "Not";
            // 
            // TxtNote
            // 
            this.TxtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNote.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtNote.Location = new System.Drawing.Point(314, 91);
            this.TxtNote.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtNote.MaxLength = 96;
            this.TxtNote.Multiline = true;
            this.TxtNote.Name = "TxtNote";
            this.TxtNote.Size = new System.Drawing.Size(299, 84);
            this.TxtNote.TabIndex = 10;
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPassword.Location = new System.Drawing.Point(312, 10);
            this.LabelPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(38, 19);
            this.LabelPassword.TabIndex = 7;
            this.LabelPassword.Text = "Şifre";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(314, 32);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtPassword.MaxLength = 64;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(275, 25);
            this.TxtPassword.TabIndex = 8;
            // 
            // LabelMail
            // 
            this.LabelMail.AutoSize = true;
            this.LabelMail.BackColor = System.Drawing.Color.Transparent;
            this.LabelMail.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelMail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelMail.Location = new System.Drawing.Point(-2, 128);
            this.LabelMail.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelMail.Name = "LabelMail";
            this.LabelMail.Size = new System.Drawing.Size(56, 19);
            this.LabelMail.TabIndex = 5;
            this.LabelMail.Text = "E-Posta";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtEmail.Location = new System.Drawing.Point(0, 150);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtEmail.MaxLength = 64;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(275, 25);
            this.TxtEmail.TabIndex = 6;
            // 
            // LabelUsername
            // 
            this.LabelUsername.AutoSize = true;
            this.LabelUsername.BackColor = System.Drawing.Color.Transparent;
            this.LabelUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelUsername.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelUsername.Location = new System.Drawing.Point(-2, 69);
            this.LabelUsername.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelUsername.Name = "LabelUsername";
            this.LabelUsername.Size = new System.Drawing.Size(89, 19);
            this.LabelUsername.TabIndex = 3;
            this.LabelUsername.Text = "Kullanıcı Adı";
            // 
            // TxtUserName
            // 
            this.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtUserName.Location = new System.Drawing.Point(0, 91);
            this.TxtUserName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtUserName.MaxLength = 64;
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(275, 25);
            this.TxtUserName.TabIndex = 4;
            // 
            // MainToolTip
            // 
            this.MainToolTip.OwnerDraw = true;
            this.MainToolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.MainToolTip_Draw);
            // 
            // HeaderMenu
            // 
            this.HeaderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.passwordGeneratorToolStripMenuItem,
            this.tSWizardToolStripMenuItem,
            this.bmacToolStripMenuItem,
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
            this.dataTransferToolStripMenuItem,
            this.autoDataBackupToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.checkforUpdatesToolStripMenuItem});
            this.settingsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // lightThemeToolStripMenuItem
            // 
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.lightThemeToolStripMenuItem.Text = "Light Theme";
            this.lightThemeToolStripMenuItem.Click += new System.EventHandler(this.lightThemeToolStripMenuItem_Click);
            // 
            // darkThemeToolStripMenuItem
            // 
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.darkThemeToolStripMenuItem.Text = "Dark Theme";
            this.darkThemeToolStripMenuItem.Click += new System.EventHandler(this.darkThemeToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.turkishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // turkishToolStripMenuItem
            // 
            this.turkishToolStripMenuItem.Name = "turkishToolStripMenuItem";
            this.turkishToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.turkishToolStripMenuItem.Text = "Turkish";
            this.turkishToolStripMenuItem.Click += new System.EventHandler(this.turkishToolStripMenuItem_Click);
            // 
            // initialViewToolStripMenuItem
            // 
            this.initialViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowedToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
            this.initialViewToolStripMenuItem.Name = "initialViewToolStripMenuItem";
            this.initialViewToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.initialViewToolStripMenuItem.Text = "Initial";
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
            // dataTransferToolStripMenuItem
            // 
            this.dataTransferToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDataToolStripMenuItem,
            this.importDataToolStripMenuItem});
            this.dataTransferToolStripMenuItem.Name = "dataTransferToolStripMenuItem";
            this.dataTransferToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.dataTransferToolStripMenuItem.Text = "Data Transfer";
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            this.exportDataToolStripMenuItem.Click += new System.EventHandler(this.exportDataToolStripMenuItem_Click);
            // 
            // importDataToolStripMenuItem
            // 
            this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
            this.importDataToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.importDataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.importDataToolStripMenuItem.Text = "Import Data";
            this.importDataToolStripMenuItem.Click += new System.EventHandler(this.importDataToolStripMenuItem_Click);
            // 
            // autoDataBackupToolStripMenuItem
            // 
            this.autoDataBackupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoDataBackupOnToolStripMenuItem,
            this.autoDataBackupOffToolStripMenuItem,
            this.autoDataBackupFolderToolStripMenuItem});
            this.autoDataBackupToolStripMenuItem.Name = "autoDataBackupToolStripMenuItem";
            this.autoDataBackupToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.autoDataBackupToolStripMenuItem.Text = "Data Backup";
            // 
            // autoDataBackupOnToolStripMenuItem
            // 
            this.autoDataBackupOnToolStripMenuItem.Name = "autoDataBackupOnToolStripMenuItem";
            this.autoDataBackupOnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.autoDataBackupOnToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.autoDataBackupOnToolStripMenuItem.Text = "Data Backup On";
            this.autoDataBackupOnToolStripMenuItem.Click += new System.EventHandler(this.autoDataBackupOnToolStripMenuItem_Click);
            // 
            // autoDataBackupOffToolStripMenuItem
            // 
            this.autoDataBackupOffToolStripMenuItem.Name = "autoDataBackupOffToolStripMenuItem";
            this.autoDataBackupOffToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.autoDataBackupOffToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.autoDataBackupOffToolStripMenuItem.Text = "Data Backup Off";
            this.autoDataBackupOffToolStripMenuItem.Click += new System.EventHandler(this.autoDataBackupOffToolStripMenuItem_Click);
            // 
            // autoDataBackupFolderToolStripMenuItem
            // 
            this.autoDataBackupFolderToolStripMenuItem.Name = "autoDataBackupFolderToolStripMenuItem";
            this.autoDataBackupFolderToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.autoDataBackupFolderToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.autoDataBackupFolderToolStripMenuItem.Text = "Data Backup Folder";
            this.autoDataBackupFolderToolStripMenuItem.Click += new System.EventHandler(this.autoDataBackupFolderToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // checkforUpdatesToolStripMenuItem
            // 
            this.checkforUpdatesToolStripMenuItem.Name = "checkforUpdatesToolStripMenuItem";
            this.checkforUpdatesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.checkforUpdatesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.checkforUpdatesToolStripMenuItem.Text = "Check Update";
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
            // tSWizardToolStripMenuItem
            // 
            this.tSWizardToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tSWizardToolStripMenuItem.Name = "tSWizardToolStripMenuItem";
            this.tSWizardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tSWizardToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.tSWizardToolStripMenuItem.Text = "TSWizard";
            this.tSWizardToolStripMenuItem.Click += new System.EventHandler(this.tSWizardToolStripMenuItem_Click);
            // 
            // bmacToolStripMenuItem
            // 
            this.bmacToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bmacToolStripMenuItem.Name = "bmacToolStripMenuItem";
            this.bmacToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.bmacToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.bmacToolStripMenuItem.Text = "Bmac";
            this.bmacToolStripMenuItem.Click += new System.EventHandler(this.bmacToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Astel
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.Panel_Main);
            this.Controls.Add(this.HeaderMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.HeaderMenu;
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "Astel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Astel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Astel_FormClosing);
            this.Load += new System.EventHandler(this.Astel_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Astel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Astel_DragEnter);
            this.Panel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).EndInit();
            this.Panel_Footer.ResumeLayout(false);
            this.Panel_Footer.PerformLayout();
            this.FLP_Btns.ResumeLayout(false);
            this.HeaderMenu.ResumeLayout(false);
            this.HeaderMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.MenuStrip HeaderMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turkishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initialViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkforUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tSWizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bmacToolStripMenuItem;
        internal System.Windows.Forms.Label LabelService;
        private System.Windows.Forms.TextBox TxtService;
        private System.Windows.Forms.FlowLayoutPanel FLP_Btns;
        private System.Windows.Forms.ToolStripMenuItem dataTransferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupFolderToolStripMenuItem;
    }
}

