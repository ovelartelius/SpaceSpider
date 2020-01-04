namespace CheckUrls
{
    partial class SitemapForm
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
            this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
            this.textBoxIgnorePatterns = new System.Windows.Forms.TextBox();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.progressBarWork = new System.Windows.Forms.ProgressBar();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
            this.labelNewSiteDomain = new System.Windows.Forms.Label();
            this.labelProxy = new System.Windows.Forms.Label();
            this.textBoxProxy = new System.Windows.Forms.TextBox();
            this.backgroundWorkerUrlCheck = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerLoadCsv = new System.ComponentModel.BackgroundWorker();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
            this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sitemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.textBoxSitemapUrl = new System.Windows.Forms.TextBox();
            this.labelSitemapUrl = new System.Windows.Forms.Label();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.tabPageAdvance = new System.Windows.Forms.TabPage();
            this.labelUserAgent = new System.Windows.Forms.Label();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelResultFolder = new System.Windows.Forms.LinkLabel();
            this.folderOutputDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageAdvance.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIgnoreRegExPatterns
            // 
            this.labelIgnoreRegExPatterns.AutoSize = true;
            this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(8, 105);
            this.labelIgnoreRegExPatterns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
            this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(153, 17);
            this.labelIgnoreRegExPatterns.TabIndex = 4;
            this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
            // 
            // textBoxIgnorePatterns
            // 
            this.textBoxIgnorePatterns.Location = new System.Drawing.Point(12, 126);
            this.textBoxIgnorePatterns.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIgnorePatterns.Multiline = true;
            this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
            this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnorePatterns.Size = new System.Drawing.Size(540, 133);
            this.textBoxIgnorePatterns.TabIndex = 3;
            this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(444, 514);
            this.buttonStartWork.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(164, 54);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Start check URLs";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(8, 462);
            this.progressBarWork.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(487, 28);
            this.progressBarWork.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(8, 34);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(485, 389);
            this.textBoxLog.TabIndex = 2;
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(12, 325);
            this.textBoxNewSiteDomain.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(540, 22);
            this.textBoxNewSiteDomain.TabIndex = 3;
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(9, 304);
            this.labelNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(161, 17);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "Test on new site domain";
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(12, 50);
            this.labelProxy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(43, 17);
            this.labelProxy.TabIndex = 5;
            this.labelProxy.Text = "Proxy";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(16, 70);
            this.textBoxProxy.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(329, 22);
            this.textBoxProxy.TabIndex = 6;
            // 
            // backgroundWorkerUrlCheck
            // 
            this.backgroundWorkerUrlCheck.WorkerReportsProgress = true;
            this.backgroundWorkerUrlCheck.WorkerSupportsCancellation = true;
            this.backgroundWorkerUrlCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUrlCheck_DoWork);
            this.backgroundWorkerUrlCheck.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerUrlCheck_ProgressChanged);
            this.backgroundWorkerUrlCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerUrlCheck_RunWorkerCompleted);
            // 
            // backgroundWorkerLoadCsv
            // 
            this.backgroundWorkerLoadCsv.WorkerReportsProgress = true;
            this.backgroundWorkerLoadCsv.WorkerSupportsCancellation = true;
            this.backgroundWorkerLoadCsv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadCsv_DoWork);
            this.backgroundWorkerLoadCsv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoadCsv_RunWorkerCompleted);
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(12, 438);
            this.textBoxOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(540, 22);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.TextChanged += new System.EventHandler(this.textBoxOutputDirectory_TextChanged);
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(8, 418);
            this.labelOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(114, 17);
            this.labelOutputDirectory.TabIndex = 11;
            this.labelOutputDirectory.Text = "Output directory:";
            // 
            // checkBoxCheckDomainBeforeStart
            // 
            this.checkBoxCheckDomainBeforeStart.AutoSize = true;
            this.checkBoxCheckDomainBeforeStart.Checked = true;
            this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(12, 355);
            this.checkBoxCheckDomainBeforeStart.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
            this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(255, 21);
            this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
            this.checkBoxCheckDomainBeforeStart.Text = "Check new site domain before start:";
            this.checkBoxCheckDomainBeforeStart.UseVisualStyleBackColor = true;
            // 
            // openSettingsDialog
            // 
            this.openSettingsDialog.Filter = "json files (*.json)|*.json";
            this.openSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openSettingsDialog_FileOk);
            // 
            // saveSettingsDialog
            // 
            this.saveSettingsDialog.FileName = "*.json";
            this.saveSettingsDialog.Filter = "json files (*.json)|*.json";
            this.saveSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveSettingsDialog_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem,
            this.checkTypesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1171, 26);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.archiveToolStripMenuItem.Text = "&Archive";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadSettingsToolStripMenuItem.Text = "&Load settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveSettingsToolStripMenuItem.Text = "&Save settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // checkTypesToolStripMenuItem
            // 
            this.checkTypesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVFileToolStripMenuItem,
            this.sitemapToolStripMenuItem});
            this.checkTypesToolStripMenuItem.Name = "checkTypesToolStripMenuItem";
            this.checkTypesToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.checkTypesToolStripMenuItem.Text = "Check types";
            // 
            // cSVFileToolStripMenuItem
            // 
            this.cSVFileToolStripMenuItem.Name = "cSVFileToolStripMenuItem";
            this.cSVFileToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.cSVFileToolStripMenuItem.Text = "CSV file";
            this.cSVFileToolStripMenuItem.Click += new System.EventHandler(this.cSVFileToolStripMenuItem_Click);
            // 
            // sitemapToolStripMenuItem
            // 
            this.sitemapToolStripMenuItem.Name = "sitemapToolStripMenuItem";
            this.sitemapToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.sitemapToolStripMenuItem.Text = "Sitemap";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBasic);
            this.tabControl1.Controls.Add(this.tabPageAdvance);
            this.tabControl1.Location = new System.Drawing.Point(20, 33);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 657);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.textBoxSitemapUrl);
            this.tabPageBasic.Controls.Add(this.labelSitemapUrl);
            this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
            this.tabPageBasic.Controls.Add(this.textBoxIgnorePatterns);
            this.tabPageBasic.Controls.Add(this.buttonStartWork);
            this.tabPageBasic.Controls.Add(this.labelIgnoreRegExPatterns);
            this.tabPageBasic.Controls.Add(this.checkBoxCheckDomainBeforeStart);
            this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
            this.tabPageBasic.Controls.Add(this.labelNewSiteDomain);
            this.tabPageBasic.Controls.Add(this.textBoxNewSiteDomain);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 25);
            this.tabPageBasic.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageBasic.Size = new System.Drawing.Size(619, 628);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = "Basic";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // textBoxSitemapUrl
            // 
            this.textBoxSitemapUrl.Location = new System.Drawing.Point(11, 36);
            this.textBoxSitemapUrl.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSitemapUrl.Name = "textBoxSitemapUrl";
            this.textBoxSitemapUrl.Size = new System.Drawing.Size(540, 22);
            this.textBoxSitemapUrl.TabIndex = 19;
            // 
            // labelSitemapUrl
            // 
            this.labelSitemapUrl.AutoSize = true;
            this.labelSitemapUrl.Location = new System.Drawing.Point(9, 19);
            this.labelSitemapUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSitemapUrl.Name = "labelSitemapUrl";
            this.labelSitemapUrl.Size = new System.Drawing.Size(95, 17);
            this.labelSitemapUrl.TabIndex = 18;
            this.labelSitemapUrl.Text = "Sitemap URL:";
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Location = new System.Drawing.Point(563, 437);
            this.buttonOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(45, 26);
            this.buttonOutputDirectory.TabIndex = 17;
            this.buttonOutputDirectory.Text = "...";
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonOutputDirectory.Click += new System.EventHandler(this.buttonOutputDirectory_Click);
            // 
            // tabPageAdvance
            // 
            this.tabPageAdvance.Controls.Add(this.labelUserAgent);
            this.tabPageAdvance.Controls.Add(this.textBoxUserAgent);
            this.tabPageAdvance.Controls.Add(this.labelProxy);
            this.tabPageAdvance.Controls.Add(this.textBoxProxy);
            this.tabPageAdvance.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdvance.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAdvance.Name = "tabPageAdvance";
            this.tabPageAdvance.Size = new System.Drawing.Size(619, 628);
            this.tabPageAdvance.TabIndex = 2;
            this.tabPageAdvance.Text = "Advance";
            this.tabPageAdvance.UseVisualStyleBackColor = true;
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(12, 117);
            this.labelUserAgent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(75, 17);
            this.labelUserAgent.TabIndex = 14;
            this.labelUserAgent.Text = "UserAgent";
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(16, 136);
            this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(329, 22);
            this.textBoxUserAgent.TabIndex = 15;
            this.textBoxUserAgent.Text = "SpaceSpider";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabelResultFolder);
            this.groupBox1.Controls.Add(this.textBoxLog);
            this.groupBox1.Controls.Add(this.progressBarWork);
            this.groupBox1.Location = new System.Drawing.Point(655, 60);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(503, 497);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // linkLabelResultFolder
            // 
            this.linkLabelResultFolder.AutoSize = true;
            this.linkLabelResultFolder.Location = new System.Drawing.Point(9, 433);
            this.linkLabelResultFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelResultFolder.Name = "linkLabelResultFolder";
            this.linkLabelResultFolder.Size = new System.Drawing.Size(0, 17);
            this.linkLabelResultFolder.TabIndex = 3;
            // 
            // folderOutputDialog
            // 
            this.folderOutputDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SitemapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1171, 738);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SitemapForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Check Sitemap URLs";
            this.Load += new System.EventHandler(this.SitemapForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageAdvance.ResumeLayout(false);
            this.tabPageAdvance.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarWork;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TextBox textBoxNewSiteDomain;
        private System.Windows.Forms.Label labelNewSiteDomain;
        private System.Windows.Forms.Label labelProxy;
        private System.Windows.Forms.TextBox textBoxProxy;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUrlCheck;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadCsv;
        private System.Windows.Forms.TextBox textBoxIgnorePatterns;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.Label labelOutputDirectory;
        private System.Windows.Forms.CheckBox checkBoxCheckDomainBeforeStart;
        private System.Windows.Forms.Label labelIgnoreRegExPatterns;
        private System.Windows.Forms.OpenFileDialog openSettingsDialog;
        private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.TabPage tabPageAdvance;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonOutputDirectory;
		private System.Windows.Forms.FolderBrowserDialog folderOutputDialog;
		private System.Windows.Forms.LinkLabel linkLabelResultFolder;
        private System.Windows.Forms.ToolStripMenuItem checkTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sitemapToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxSitemapUrl;
        private System.Windows.Forms.Label labelSitemapUrl;
        private System.Windows.Forms.Label labelUserAgent;
        private System.Windows.Forms.TextBox textBoxUserAgent;
    }
}

