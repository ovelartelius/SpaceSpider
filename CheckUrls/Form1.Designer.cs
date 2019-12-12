namespace CheckRequestedUrls
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
            this.textBoxIgnorePatterns = new System.Windows.Forms.TextBox();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.labelCsvFile = new System.Windows.Forms.Label();
            this.textBoxCsvFile = new System.Windows.Forms.TextBox();
            this.openCsvDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBarWork = new System.Windows.Forms.ProgressBar();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
            this.labelNewSiteDomain = new System.Windows.Forms.Label();
            this.labelProxy = new System.Windows.Forms.Label();
            this.textBoxProxy = new System.Windows.Forms.TextBox();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.labelUserAgent = new System.Windows.Forms.Label();
            this.backgroundWorkerUrlCheck = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerLoadCsv = new System.ComponentModel.BackgroundWorker();
            this.textBoxSearchUrl = new System.Windows.Forms.TextBox();
            this.labelSearchUrl = new System.Windows.Forms.Label();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.checkBoxOverVpn = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreSearch = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
            this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.buttonLoadCsv = new System.Windows.Forms.Button();
            this.tabPageUserAgent = new System.Windows.Forms.TabPage();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.tabPageAdvance = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelResultFolder = new System.Windows.Forms.LinkLabel();
            this.folderOutputDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageUserAgent.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageAdvance.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIgnoreRegExPatterns
            // 
            this.labelIgnoreRegExPatterns.AutoSize = true;
            this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(6, 61);
            this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
            this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(116, 13);
            this.labelIgnoreRegExPatterns.TabIndex = 4;
            this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
            // 
            // textBoxIgnorePatterns
            // 
            this.textBoxIgnorePatterns.Location = new System.Drawing.Point(9, 77);
            this.textBoxIgnorePatterns.Multiline = true;
            this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
            this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnorePatterns.Size = new System.Drawing.Size(406, 109);
            this.textBoxIgnorePatterns.TabIndex = 3;
            this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(333, 354);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(123, 44);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Start check URLs";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Location = new System.Drawing.Point(6, 8);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(79, 13);
            this.labelCsvFile.TabIndex = 1;
            this.labelCsvFile.Text = "CSV file to load";
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(9, 24);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(406, 20);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
            // 
            // openCsvDialog
            // 
            this.openCsvDialog.Filter = "CSV files (*.csv)|*.csv";
            this.openCsvDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openCsvDialog_FileOk);
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(6, 375);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(365, 23);
            this.progressBarWork.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(6, 28);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(365, 317);
            this.textBoxLog.TabIndex = 2;
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(9, 233);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(406, 20);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(6, 217);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(85, 13);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "New site domain";
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(9, 41);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(33, 13);
            this.labelProxy.TabIndex = 5;
            this.labelProxy.Text = "Proxy";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(12, 57);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(248, 20);
            this.textBoxProxy.TabIndex = 6;
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(16, 28);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(248, 20);
            this.textBoxUserAgent.TabIndex = 8;
            this.textBoxUserAgent.Text = "SpaceSpider";
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(13, 12);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(57, 13);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "UserAgent";
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
            //this.backgroundWorkerLoadCsv.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerLoadCsv_ProgressChanged);
            this.backgroundWorkerLoadCsv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoadCsv_RunWorkerCompleted);
            // 
            // textBoxSearchUrl
            // 
            this.textBoxSearchUrl.Location = new System.Drawing.Point(6, 57);
            this.textBoxSearchUrl.Multiline = true;
            this.textBoxSearchUrl.Name = "textBoxSearchUrl";
            this.textBoxSearchUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSearchUrl.Size = new System.Drawing.Size(450, 341);
            this.textBoxSearchUrl.TabIndex = 10;
            this.textBoxSearchUrl.Text = resources.GetString("textBoxSearchUrl.Text");
            // 
            // labelSearchUrl
            // 
            this.labelSearchUrl.AutoSize = true;
            this.labelSearchUrl.Location = new System.Drawing.Point(6, 41);
            this.labelSearchUrl.Name = "labelSearchUrl";
            this.labelSearchUrl.Size = new System.Drawing.Size(69, 13);
            this.labelSearchUrl.TabIndex = 9;
            this.labelSearchUrl.Text = "Search URL:";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(9, 325);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(406, 20);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.TextChanged += new System.EventHandler(this.textBoxOutputDirectory_TextChanged);
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(6, 309);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(85, 13);
            this.labelOutputDirectory.TabIndex = 11;
            this.labelOutputDirectory.Text = "Output directory:";
            // 
            // checkBoxOverVpn
            // 
            this.checkBoxOverVpn.AutoSize = true;
            this.checkBoxOverVpn.Location = new System.Drawing.Point(12, 19);
            this.checkBoxOverVpn.Name = "checkBoxOverVpn";
            this.checkBoxOverVpn.Size = new System.Drawing.Size(115, 17);
            this.checkBoxOverVpn.TabIndex = 13;
            this.checkBoxOverVpn.Text = "Running over VPN";
            this.checkBoxOverVpn.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreSearch
            // 
            this.checkBoxIgnoreSearch.AutoSize = true;
            this.checkBoxIgnoreSearch.Checked = true;
            this.checkBoxIgnoreSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnoreSearch.Location = new System.Drawing.Point(6, 19);
            this.checkBoxIgnoreSearch.Name = "checkBoxIgnoreSearch";
            this.checkBoxIgnoreSearch.Size = new System.Drawing.Size(91, 17);
            this.checkBoxIgnoreSearch.TabIndex = 14;
            this.checkBoxIgnoreSearch.Text = "Ignore search";
            this.checkBoxIgnoreSearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDomainBeforeStart
            // 
            this.checkBoxCheckDomainBeforeStart.AutoSize = true;
            this.checkBoxCheckDomainBeforeStart.Checked = true;
            this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(12, 259);
            this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
            this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(172, 17);
            this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
            this.checkBoxCheckDomainBeforeStart.Text = "Check site domain before start:";
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
            this.archiveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(878, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(59, 22);
            this.archiveToolStripMenuItem.Text = "&Archive";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.loadSettingsToolStripMenuItem.Text = "&Load settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.saveSettingsToolStripMenuItem.Text = "&Save settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBasic);
            this.tabControl1.Controls.Add(this.tabPageUserAgent);
            this.tabControl1.Controls.Add(this.tabPageSearch);
            this.tabControl1.Controls.Add(this.tabPageAdvance);
            this.tabControl1.Location = new System.Drawing.Point(15, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 430);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
            this.tabPageBasic.Controls.Add(this.buttonLoadCsv);
            this.tabPageBasic.Controls.Add(this.textBoxIgnorePatterns);
            this.tabPageBasic.Controls.Add(this.buttonStartWork);
            this.tabPageBasic.Controls.Add(this.labelIgnoreRegExPatterns);
            this.tabPageBasic.Controls.Add(this.checkBoxCheckDomainBeforeStart);
            this.tabPageBasic.Controls.Add(this.labelCsvFile);
            this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.tabPageBasic.Controls.Add(this.textBoxCsvFile);
            this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
            this.tabPageBasic.Controls.Add(this.labelNewSiteDomain);
            this.tabPageBasic.Controls.Add(this.textBoxNewSiteDomain);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasic.Size = new System.Drawing.Size(462, 404);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = "Basic";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Location = new System.Drawing.Point(422, 324);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(34, 21);
            this.buttonOutputDirectory.TabIndex = 17;
            this.buttonOutputDirectory.Text = "...";
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonOutputDirectory.Click += new System.EventHandler(this.buttonOutputDirectory_Click);
            // 
            // buttonLoadCsv
            // 
            this.buttonLoadCsv.Location = new System.Drawing.Point(422, 24);
            this.buttonLoadCsv.Name = "buttonLoadCsv";
            this.buttonLoadCsv.Size = new System.Drawing.Size(34, 20);
            this.buttonLoadCsv.TabIndex = 16;
            this.buttonLoadCsv.Text = "...";
            this.buttonLoadCsv.UseVisualStyleBackColor = true;
            this.buttonLoadCsv.Click += new System.EventHandler(this.buttonLoadCsv_Click);
            // 
            // tabPageUserAgent
            // 
            this.tabPageUserAgent.Controls.Add(this.labelUserAgent);
            this.tabPageUserAgent.Controls.Add(this.textBoxUserAgent);
            this.tabPageUserAgent.Location = new System.Drawing.Point(4, 22);
            this.tabPageUserAgent.Name = "tabPageUserAgent";
            this.tabPageUserAgent.Size = new System.Drawing.Size(462, 404);
            this.tabPageUserAgent.TabIndex = 3;
            this.tabPageUserAgent.Text = "UserAgent";
            this.tabPageUserAgent.UseVisualStyleBackColor = true;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.checkBoxIgnoreSearch);
            this.tabPageSearch.Controls.Add(this.labelSearchUrl);
            this.tabPageSearch.Controls.Add(this.textBoxSearchUrl);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(462, 404);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvance
            // 
            this.tabPageAdvance.Controls.Add(this.checkBoxOverVpn);
            this.tabPageAdvance.Controls.Add(this.labelProxy);
            this.tabPageAdvance.Controls.Add(this.textBoxProxy);
            this.tabPageAdvance.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvance.Name = "tabPageAdvance";
            this.tabPageAdvance.Size = new System.Drawing.Size(462, 404);
            this.tabPageAdvance.TabIndex = 2;
            this.tabPageAdvance.Text = "Advance";
            this.tabPageAdvance.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabelResultFolder);
            this.groupBox1.Controls.Add(this.textBoxLog);
            this.groupBox1.Controls.Add(this.progressBarWork);
            this.groupBox1.Location = new System.Drawing.Point(491, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 404);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // linkLabelResultFolder
            // 
            this.linkLabelResultFolder.AutoSize = true;
            this.linkLabelResultFolder.Location = new System.Drawing.Point(7, 352);
            this.linkLabelResultFolder.Name = "linkLabelResultFolder";
            this.linkLabelResultFolder.Size = new System.Drawing.Size(0, 13);
            this.linkLabelResultFolder.TabIndex = 3;
            // 
            // folderOutputDialog
            // 
            this.folderOutputDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 463);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Check URLs";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageUserAgent.ResumeLayout(false);
            this.tabPageUserAgent.PerformLayout();
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            this.tabPageAdvance.ResumeLayout(false);
            this.tabPageAdvance.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxCsvFile;
        private System.Windows.Forms.OpenFileDialog openCsvDialog;
        private System.Windows.Forms.ProgressBar progressBarWork;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.Label labelCsvFile;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TextBox textBoxNewSiteDomain;
        private System.Windows.Forms.Label labelNewSiteDomain;
        private System.Windows.Forms.Label labelProxy;
        private System.Windows.Forms.TextBox textBoxProxy;
        private System.Windows.Forms.TextBox textBoxUserAgent;
        private System.Windows.Forms.Label labelUserAgent;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUrlCheck;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadCsv;
        private System.Windows.Forms.TextBox textBoxIgnorePatterns;
        private System.Windows.Forms.TextBox textBoxSearchUrl;
        private System.Windows.Forms.Label labelSearchUrl;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.Label labelOutputDirectory;
        private System.Windows.Forms.CheckBox checkBoxOverVpn;
        private System.Windows.Forms.CheckBox checkBoxIgnoreSearch;
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
		private System.Windows.Forms.TabPage tabPageUserAgent;
		private System.Windows.Forms.TabPage tabPageSearch;
		private System.Windows.Forms.TabPage tabPageAdvance;
		private System.Windows.Forms.Button buttonLoadCsv;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonOutputDirectory;
		private System.Windows.Forms.FolderBrowserDialog folderOutputDialog;
		private System.Windows.Forms.LinkLabel linkLabelResultFolder;
	}
}

