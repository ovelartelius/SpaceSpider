namespace Crawler
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
			this.button1 = new System.Windows.Forms.Button();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.labelUrl = new System.Windows.Forms.Label();
			this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelUserAgent = new System.Windows.Forms.Label();
			this.textBoxUserAgent = new System.Windows.Forms.TextBox();
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.buttonIndexDirectory = new System.Windows.Forms.Button();
			this.textBoxIndexDirectory = new System.Windows.Forms.TextBox();
			this.labelIndexFolder = new System.Windows.Forms.Label();
			this.folderIndexDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.buttonOutputDirectory = new System.Windows.Forms.Button();
			this.buttonLoadCsv = new System.Windows.Forms.Button();
			this.textBoxIgnorePatterns = new System.Windows.Forms.TextBox();
			this.buttonStartWork = new System.Windows.Forms.Button();
			this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
			this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
			this.labelCsvFile = new System.Windows.Forms.Label();
			this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
			this.textBoxCsvFile = new System.Windows.Forms.TextBox();
			this.labelOutputDirectory = new System.Windows.Forms.Label();
			this.labelNewSiteDomain = new System.Windows.Forms.Label();
			this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
			this.tabPageUserAgent = new System.Windows.Forms.TabPage();
			this.tabPageSearch = new System.Windows.Forms.TabPage();
			this.checkBoxIgnoreSearch = new System.Windows.Forms.CheckBox();
			this.labelSearchUrl = new System.Windows.Forms.Label();
			this.textBoxSearchUrl = new System.Windows.Forms.TextBox();
			this.tabPageAdvance = new System.Windows.Forms.TabPage();
			this.checkBoxOverVpn = new System.Windows.Forms.CheckBox();
			this.labelProxy = new System.Windows.Forms.Label();
			this.textBoxProxy = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.tabPageUserAgent.SuspendLayout();
			this.tabPageSearch.SuspendLayout();
			this.tabPageAdvance.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(362, 90);
			this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(66, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Location = new System.Drawing.Point(12, 92);
			this.textBoxUrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(345, 20);
			this.textBoxUrl.TabIndex = 1;
			// 
			// labelUrl
			// 
			this.labelUrl.AutoSize = true;
			this.labelUrl.Location = new System.Drawing.Point(12, 73);
			this.labelUrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelUrl.Name = "labelUrl";
			this.labelUrl.Size = new System.Drawing.Size(23, 13);
			this.labelUrl.TabIndex = 2;
			this.labelUrl.Text = "Url:";
			// 
			// openSettingsDialog
			// 
			this.openSettingsDialog.DefaultExt = "json";
			this.openSettingsDialog.Filter = "json files (*.json)|*.json";
			this.openSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openSettingsDialog_FileOk);
			// 
			// saveSettingsDialog
			// 
			this.saveSettingsDialog.DefaultExt = "json";
			this.saveSettingsDialog.FileName = "*.json";
			this.saveSettingsDialog.Filter = "json files (*.json)|*.json";
			this.saveSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveSettingsDialog_FileOk);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1189, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archiveToolStripMenuItem
			// 
			this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
			this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
			this.archiveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.archiveToolStripMenuItem.Text = "Archive";
			// 
			// loadSettingsToolStripMenuItem
			// 
			this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
			this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.loadSettingsToolStripMenuItem.Text = "Load settings";
			this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
			// 
			// saveSettingsToolStripMenuItem
			// 
			this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
			this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.saveSettingsToolStripMenuItem.Text = "Save settings";
			this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
			// 
			// labelUserAgent
			// 
			this.labelUserAgent.AutoSize = true;
			this.labelUserAgent.Location = new System.Drawing.Point(13, 13);
			this.labelUserAgent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelUserAgent.Name = "labelUserAgent";
			this.labelUserAgent.Size = new System.Drawing.Size(63, 13);
			this.labelUserAgent.TabIndex = 7;
			this.labelUserAgent.Text = "User Agent:";
			// 
			// textBoxUserAgent
			// 
			this.textBoxUserAgent.Location = new System.Drawing.Point(13, 32);
			this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxUserAgent.Name = "textBoxUserAgent";
			this.textBoxUserAgent.Size = new System.Drawing.Size(276, 20);
			this.textBoxUserAgent.TabIndex = 6;
			// 
			// textBoxResult
			// 
			this.textBoxResult.Location = new System.Drawing.Point(756, 34);
			this.textBoxResult.Multiline = true;
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.Size = new System.Drawing.Size(389, 341);
			this.textBoxResult.TabIndex = 8;
			// 
			// buttonIndexDirectory
			// 
			this.buttonIndexDirectory.Location = new System.Drawing.Point(362, 130);
			this.buttonIndexDirectory.Name = "buttonIndexDirectory";
			this.buttonIndexDirectory.Size = new System.Drawing.Size(34, 21);
			this.buttonIndexDirectory.TabIndex = 20;
			this.buttonIndexDirectory.Text = "...";
			this.buttonIndexDirectory.UseVisualStyleBackColor = true;
			this.buttonIndexDirectory.Click += new System.EventHandler(this.buttonIndexDirectory_Click);
			// 
			// textBoxIndexDirectory
			// 
			this.textBoxIndexDirectory.Location = new System.Drawing.Point(11, 131);
			this.textBoxIndexDirectory.Name = "textBoxIndexDirectory";
			this.textBoxIndexDirectory.Size = new System.Drawing.Size(346, 20);
			this.textBoxIndexDirectory.TabIndex = 19;
			// 
			// labelIndexFolder
			// 
			this.labelIndexFolder.AutoSize = true;
			this.labelIndexFolder.Location = new System.Drawing.Point(8, 115);
			this.labelIndexFolder.Name = "labelIndexFolder";
			this.labelIndexFolder.Size = new System.Drawing.Size(79, 13);
			this.labelIndexFolder.TabIndex = 18;
			this.labelIndexFolder.Text = "Index directory:";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageBasic);
			this.tabControl1.Controls.Add(this.tabPageUserAgent);
			this.tabControl1.Controls.Add(this.tabPageSearch);
			this.tabControl1.Controls.Add(this.tabPageAdvance);
			this.tabControl1.Location = new System.Drawing.Point(12, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(470, 562);
			this.tabControl1.TabIndex = 21;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
			this.tabPageBasic.Controls.Add(this.buttonIndexDirectory);
			this.tabPageBasic.Controls.Add(this.buttonLoadCsv);
			this.tabPageBasic.Controls.Add(this.textBoxIndexDirectory);
			this.tabPageBasic.Controls.Add(this.textBoxIgnorePatterns);
			this.tabPageBasic.Controls.Add(this.labelIndexFolder);
			this.tabPageBasic.Controls.Add(this.buttonStartWork);
			this.tabPageBasic.Controls.Add(this.labelIgnoreRegExPatterns);
			this.tabPageBasic.Controls.Add(this.labelUrl);
			this.tabPageBasic.Controls.Add(this.checkBoxCheckDomainBeforeStart);
			this.tabPageBasic.Controls.Add(this.textBoxUrl);
			this.tabPageBasic.Controls.Add(this.button1);
			this.tabPageBasic.Controls.Add(this.labelCsvFile);
			this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
			this.tabPageBasic.Controls.Add(this.textBoxCsvFile);
			this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
			this.tabPageBasic.Controls.Add(this.labelNewSiteDomain);
			this.tabPageBasic.Controls.Add(this.textBoxNewSiteDomain);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(462, 536);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// buttonOutputDirectory
			// 
			this.buttonOutputDirectory.Location = new System.Drawing.Point(422, 459);
			this.buttonOutputDirectory.Name = "buttonOutputDirectory";
			this.buttonOutputDirectory.Size = new System.Drawing.Size(34, 21);
			this.buttonOutputDirectory.TabIndex = 17;
			this.buttonOutputDirectory.Text = "...";
			this.buttonOutputDirectory.UseVisualStyleBackColor = true;
			// 
			// buttonLoadCsv
			// 
			this.buttonLoadCsv.Location = new System.Drawing.Point(422, 24);
			this.buttonLoadCsv.Name = "buttonLoadCsv";
			this.buttonLoadCsv.Size = new System.Drawing.Size(34, 20);
			this.buttonLoadCsv.TabIndex = 16;
			this.buttonLoadCsv.Text = "...";
			this.buttonLoadCsv.UseVisualStyleBackColor = true;
			// 
			// textBoxIgnorePatterns
			// 
			this.textBoxIgnorePatterns.Location = new System.Drawing.Point(9, 239);
			this.textBoxIgnorePatterns.Multiline = true;
			this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
			this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxIgnorePatterns.Size = new System.Drawing.Size(406, 109);
			this.textBoxIgnorePatterns.TabIndex = 3;
			this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
			// 
			// buttonStartWork
			// 
			this.buttonStartWork.Location = new System.Drawing.Point(333, 486);
			this.buttonStartWork.Name = "buttonStartWork";
			this.buttonStartWork.Size = new System.Drawing.Size(123, 44);
			this.buttonStartWork.TabIndex = 2;
			this.buttonStartWork.Text = "Start check URLs";
			this.buttonStartWork.UseVisualStyleBackColor = true;
			// 
			// labelIgnoreRegExPatterns
			// 
			this.labelIgnoreRegExPatterns.AutoSize = true;
			this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(6, 223);
			this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
			this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(116, 13);
			this.labelIgnoreRegExPatterns.TabIndex = 4;
			this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
			// 
			// checkBoxCheckDomainBeforeStart
			// 
			this.checkBoxCheckDomainBeforeStart.AutoSize = true;
			this.checkBoxCheckDomainBeforeStart.Checked = true;
			this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(12, 421);
			this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
			this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(172, 17);
			this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
			this.checkBoxCheckDomainBeforeStart.Text = "Check site domain before start:";
			this.checkBoxCheckDomainBeforeStart.UseVisualStyleBackColor = true;
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
			// textBoxOutputDirectory
			// 
			this.textBoxOutputDirectory.Location = new System.Drawing.Point(9, 460);
			this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
			this.textBoxOutputDirectory.Size = new System.Drawing.Size(406, 20);
			this.textBoxOutputDirectory.TabIndex = 12;
			// 
			// textBoxCsvFile
			// 
			this.textBoxCsvFile.Location = new System.Drawing.Point(9, 24);
			this.textBoxCsvFile.Name = "textBoxCsvFile";
			this.textBoxCsvFile.Size = new System.Drawing.Size(406, 20);
			this.textBoxCsvFile.TabIndex = 0;
			this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
			// 
			// labelOutputDirectory
			// 
			this.labelOutputDirectory.AutoSize = true;
			this.labelOutputDirectory.Location = new System.Drawing.Point(6, 444);
			this.labelOutputDirectory.Name = "labelOutputDirectory";
			this.labelOutputDirectory.Size = new System.Drawing.Size(85, 13);
			this.labelOutputDirectory.TabIndex = 11;
			this.labelOutputDirectory.Text = "Output directory:";
			// 
			// labelNewSiteDomain
			// 
			this.labelNewSiteDomain.AutoSize = true;
			this.labelNewSiteDomain.Location = new System.Drawing.Point(6, 379);
			this.labelNewSiteDomain.Name = "labelNewSiteDomain";
			this.labelNewSiteDomain.Size = new System.Drawing.Size(85, 13);
			this.labelNewSiteDomain.TabIndex = 4;
			this.labelNewSiteDomain.Text = "New site domain";
			// 
			// textBoxNewSiteDomain
			// 
			this.textBoxNewSiteDomain.Location = new System.Drawing.Point(9, 395);
			this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
			this.textBoxNewSiteDomain.Size = new System.Drawing.Size(406, 20);
			this.textBoxNewSiteDomain.TabIndex = 3;
			this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
			// 
			// tabPageUserAgent
			// 
			this.tabPageUserAgent.Controls.Add(this.textBoxUserAgent);
			this.tabPageUserAgent.Controls.Add(this.labelUserAgent);
			this.tabPageUserAgent.Location = new System.Drawing.Point(4, 22);
			this.tabPageUserAgent.Name = "tabPageUserAgent";
			this.tabPageUserAgent.Size = new System.Drawing.Size(462, 536);
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
			// labelSearchUrl
			// 
			this.labelSearchUrl.AutoSize = true;
			this.labelSearchUrl.Location = new System.Drawing.Point(6, 41);
			this.labelSearchUrl.Name = "labelSearchUrl";
			this.labelSearchUrl.Size = new System.Drawing.Size(69, 13);
			this.labelSearchUrl.TabIndex = 9;
			this.labelSearchUrl.Text = "Search URL:";
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1189, 623);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.textBoxResult);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "Form1";
			this.Text = "Crawler";
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
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.OpenFileDialog openSettingsDialog;
        private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.Label labelUserAgent;
        private System.Windows.Forms.TextBox textBoxUserAgent;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonIndexDirectory;
        private System.Windows.Forms.TextBox textBoxIndexDirectory;
        private System.Windows.Forms.Label labelIndexFolder;
        private System.Windows.Forms.FolderBrowserDialog folderIndexDialog;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.Button buttonOutputDirectory;
		private System.Windows.Forms.Button buttonLoadCsv;
		private System.Windows.Forms.TextBox textBoxIgnorePatterns;
		private System.Windows.Forms.Button buttonStartWork;
		private System.Windows.Forms.Label labelIgnoreRegExPatterns;
		private System.Windows.Forms.CheckBox checkBoxCheckDomainBeforeStart;
		private System.Windows.Forms.Label labelCsvFile;
		private System.Windows.Forms.TextBox textBoxOutputDirectory;
		private System.Windows.Forms.TextBox textBoxCsvFile;
		private System.Windows.Forms.Label labelOutputDirectory;
		private System.Windows.Forms.Label labelNewSiteDomain;
		private System.Windows.Forms.TextBox textBoxNewSiteDomain;
		private System.Windows.Forms.TabPage tabPageUserAgent;
		private System.Windows.Forms.TabPage tabPageSearch;
		private System.Windows.Forms.CheckBox checkBoxIgnoreSearch;
		private System.Windows.Forms.Label labelSearchUrl;
		private System.Windows.Forms.TextBox textBoxSearchUrl;
		private System.Windows.Forms.TabPage tabPageAdvance;
		private System.Windows.Forms.CheckBox checkBoxOverVpn;
		private System.Windows.Forms.Label labelProxy;
		private System.Windows.Forms.TextBox textBoxProxy;
	}
}

