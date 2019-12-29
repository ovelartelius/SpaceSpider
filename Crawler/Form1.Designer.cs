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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.buttonLoadCsv = new System.Windows.Forms.Button();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
            this.labelCsvFile = new System.Windows.Forms.Label();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.textBoxCsvFile = new System.Windows.Forms.TextBox();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.labelNewSiteDomain = new System.Windows.Forms.Label();
            this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
            this.tabPageUserAgent = new System.Windows.Forms.TabPage();
            this.tabPageIgnore = new System.Windows.Forms.TabPage();
            this.textBoxIgnoreExternalHostsRegExPatterns = new System.Windows.Forms.TextBox();
            this.labelIgnoreExternalHostsRegExPatterns = new System.Windows.Forms.Label();
            this.textBoxIgnoreLinksPatterns = new System.Windows.Forms.TextBox();
            this.labelIgnoreLinksRegExPatterns = new System.Windows.Forms.Label();
            this.tabPageAdvance = new System.Windows.Forms.TabPage();
            this.checkBoxOverVpn = new System.Windows.Forms.CheckBox();
            this.labelProxy = new System.Windows.Forms.Label();
            this.textBoxProxy = new System.Windows.Forms.TextBox();
            this.backgroundWorkerMaster = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerGetUrl = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerAnchorParsePage = new System.ComponentModel.BackgroundWorker();
            this.progressBarGetUrls = new System.Windows.Forms.ProgressBar();
            this.progressBarParsePages = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageUserAgent.SuspendLayout();
            this.tabPageIgnore.SuspendLayout();
            this.tabPageAdvance.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(14, 118);
            this.textBoxUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(459, 22);
            this.textBoxUrl.TabIndex = 1;
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(13, 99);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(30, 17);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1585, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.loadSettingsToolStripMenuItem.Text = "Load settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.saveSettingsToolStripMenuItem.Text = "Save settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(17, 16);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(83, 17);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "User Agent:";
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(17, 39);
            this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(367, 22);
            this.textBoxUserAgent.TabIndex = 6;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(1008, 42);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(517, 419);
            this.textBoxResult.TabIndex = 8;
            // 
            // buttonIndexDirectory
            // 
            this.buttonIndexDirectory.Location = new System.Drawing.Point(483, 160);
            this.buttonIndexDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.buttonIndexDirectory.Name = "buttonIndexDirectory";
            this.buttonIndexDirectory.Size = new System.Drawing.Size(45, 26);
            this.buttonIndexDirectory.TabIndex = 20;
            this.buttonIndexDirectory.Text = "...";
            this.buttonIndexDirectory.UseVisualStyleBackColor = true;
            this.buttonIndexDirectory.Click += new System.EventHandler(this.buttonIndexDirectory_Click);
            // 
            // textBoxIndexDirectory
            // 
            this.textBoxIndexDirectory.Location = new System.Drawing.Point(15, 161);
            this.textBoxIndexDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIndexDirectory.Name = "textBoxIndexDirectory";
            this.textBoxIndexDirectory.Size = new System.Drawing.Size(460, 22);
            this.textBoxIndexDirectory.TabIndex = 19;
            // 
            // labelIndexFolder
            // 
            this.labelIndexFolder.AutoSize = true;
            this.labelIndexFolder.Location = new System.Drawing.Point(11, 142);
            this.labelIndexFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIndexFolder.Name = "labelIndexFolder";
            this.labelIndexFolder.Size = new System.Drawing.Size(104, 17);
            this.labelIndexFolder.TabIndex = 18;
            this.labelIndexFolder.Text = "Index directory:";
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageBasic);
            this.tabControlSettings.Controls.Add(this.tabPageUserAgent);
            this.tabControlSettings.Controls.Add(this.tabPageIgnore);
            this.tabControlSettings.Controls.Add(this.tabPageAdvance);
            this.tabControlSettings.Location = new System.Drawing.Point(16, 33);
            this.tabControlSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(627, 692);
            this.tabControlSettings.TabIndex = 21;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
            this.tabPageBasic.Controls.Add(this.buttonIndexDirectory);
            this.tabPageBasic.Controls.Add(this.buttonLoadCsv);
            this.tabPageBasic.Controls.Add(this.textBoxIndexDirectory);
            this.tabPageBasic.Controls.Add(this.labelIndexFolder);
            this.tabPageBasic.Controls.Add(this.buttonStartWork);
            this.tabPageBasic.Controls.Add(this.labelUrl);
            this.tabPageBasic.Controls.Add(this.checkBoxCheckDomainBeforeStart);
            this.tabPageBasic.Controls.Add(this.textBoxUrl);
            this.tabPageBasic.Controls.Add(this.labelCsvFile);
            this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.tabPageBasic.Controls.Add(this.textBoxCsvFile);
            this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
            this.tabPageBasic.Controls.Add(this.labelNewSiteDomain);
            this.tabPageBasic.Controls.Add(this.textBoxNewSiteDomain);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 25);
            this.tabPageBasic.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageBasic.Size = new System.Drawing.Size(619, 663);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = "Basic";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Location = new System.Drawing.Point(563, 565);
            this.buttonOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(45, 26);
            this.buttonOutputDirectory.TabIndex = 17;
            this.buttonOutputDirectory.Text = "...";
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            // 
            // buttonLoadCsv
            // 
            this.buttonLoadCsv.Location = new System.Drawing.Point(563, 30);
            this.buttonLoadCsv.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadCsv.Name = "buttonLoadCsv";
            this.buttonLoadCsv.Size = new System.Drawing.Size(45, 25);
            this.buttonLoadCsv.TabIndex = 16;
            this.buttonLoadCsv.Text = "...";
            this.buttonLoadCsv.UseVisualStyleBackColor = true;
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(444, 598);
            this.buttonStartWork.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(164, 54);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Start check URLs";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // checkBoxCheckDomainBeforeStart
            // 
            this.checkBoxCheckDomainBeforeStart.AutoSize = true;
            this.checkBoxCheckDomainBeforeStart.Checked = true;
            this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(16, 518);
            this.checkBoxCheckDomainBeforeStart.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
            this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(226, 21);
            this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
            this.checkBoxCheckDomainBeforeStart.Text = "Check site domain before start:";
            this.checkBoxCheckDomainBeforeStart.UseVisualStyleBackColor = true;
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Location = new System.Drawing.Point(8, 10);
            this.labelCsvFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(104, 17);
            this.labelCsvFile.TabIndex = 1;
            this.labelCsvFile.Text = "CSV file to load";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(12, 566);
            this.textBoxOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(540, 22);
            this.textBoxOutputDirectory.TabIndex = 12;
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(12, 30);
            this.textBoxCsvFile.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(540, 22);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(8, 546);
            this.labelOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(114, 17);
            this.labelOutputDirectory.TabIndex = 11;
            this.labelOutputDirectory.Text = "Output directory:";
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(8, 466);
            this.labelNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(111, 17);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "New site domain";
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(12, 486);
            this.textBoxNewSiteDomain.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(540, 22);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
            // 
            // tabPageUserAgent
            // 
            this.tabPageUserAgent.Controls.Add(this.textBoxUserAgent);
            this.tabPageUserAgent.Controls.Add(this.labelUserAgent);
            this.tabPageUserAgent.Location = new System.Drawing.Point(4, 25);
            this.tabPageUserAgent.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageUserAgent.Name = "tabPageUserAgent";
            this.tabPageUserAgent.Size = new System.Drawing.Size(619, 663);
            this.tabPageUserAgent.TabIndex = 3;
            this.tabPageUserAgent.Text = "UserAgent";
            this.tabPageUserAgent.UseVisualStyleBackColor = true;
            // 
            // tabPageIgnore
            // 
            this.tabPageIgnore.Controls.Add(this.textBoxIgnoreExternalHostsRegExPatterns);
            this.tabPageIgnore.Controls.Add(this.labelIgnoreExternalHostsRegExPatterns);
            this.tabPageIgnore.Controls.Add(this.textBoxIgnoreLinksPatterns);
            this.tabPageIgnore.Controls.Add(this.labelIgnoreLinksRegExPatterns);
            this.tabPageIgnore.Location = new System.Drawing.Point(4, 25);
            this.tabPageIgnore.Name = "tabPageIgnore";
            this.tabPageIgnore.Size = new System.Drawing.Size(619, 663);
            this.tabPageIgnore.TabIndex = 4;
            this.tabPageIgnore.Text = "Ignore";
            this.tabPageIgnore.UseVisualStyleBackColor = true;
            // 
            // textBoxIgnoreExternalHostsRegExPatterns
            // 
            this.textBoxIgnoreExternalHostsRegExPatterns.Location = new System.Drawing.Point(14, 219);
            this.textBoxIgnoreExternalHostsRegExPatterns.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIgnoreExternalHostsRegExPatterns.Multiline = true;
            this.textBoxIgnoreExternalHostsRegExPatterns.Name = "textBoxIgnoreExternalHostsRegExPatterns";
            this.textBoxIgnoreExternalHostsRegExPatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnoreExternalHostsRegExPatterns.Size = new System.Drawing.Size(540, 133);
            this.textBoxIgnoreExternalHostsRegExPatterns.TabIndex = 6;
            this.textBoxIgnoreExternalHostsRegExPatterns.Text = "^https:\\\\/\\\\/accounts.google.com\\\\/\r\n^http:\\\\/\\\\/www.facebook.com\\\\/\r\n";
            // 
            // labelIgnoreExternalHostsRegExPatterns
            // 
            this.labelIgnoreExternalHostsRegExPatterns.AutoSize = true;
            this.labelIgnoreExternalHostsRegExPatterns.Location = new System.Drawing.Point(11, 197);
            this.labelIgnoreExternalHostsRegExPatterns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIgnoreExternalHostsRegExPatterns.Name = "labelIgnoreExternalHostsRegExPatterns";
            this.labelIgnoreExternalHostsRegExPatterns.Size = new System.Drawing.Size(248, 17);
            this.labelIgnoreExternalHostsRegExPatterns.TabIndex = 5;
            this.labelIgnoreExternalHostsRegExPatterns.Text = "Ignore External Hosts RegEx patterns:";
            // 
            // textBoxIgnoreLinksPatterns
            // 
            this.textBoxIgnoreLinksPatterns.Location = new System.Drawing.Point(14, 35);
            this.textBoxIgnoreLinksPatterns.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIgnoreLinksPatterns.Multiline = true;
            this.textBoxIgnoreLinksPatterns.Name = "textBoxIgnoreLinksPatterns";
            this.textBoxIgnoreLinksPatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnoreLinksPatterns.Size = new System.Drawing.Size(540, 133);
            this.textBoxIgnoreLinksPatterns.TabIndex = 3;
            this.textBoxIgnoreLinksPatterns.Text = "^#\r\n^{{\r\n^javascript:\r\n^mailto:\r\n^tel:\r\n^sms:\r\n^data:image\r\n^@Model\r\n^\\\\?";
            // 
            // labelIgnoreLinksRegExPatterns
            // 
            this.labelIgnoreLinksRegExPatterns.AutoSize = true;
            this.labelIgnoreLinksRegExPatterns.Location = new System.Drawing.Point(11, 14);
            this.labelIgnoreLinksRegExPatterns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIgnoreLinksRegExPatterns.Name = "labelIgnoreLinksRegExPatterns";
            this.labelIgnoreLinksRegExPatterns.Size = new System.Drawing.Size(190, 17);
            this.labelIgnoreLinksRegExPatterns.TabIndex = 4;
            this.labelIgnoreLinksRegExPatterns.Text = "Ignore Links RegEx patterns:";
            // 
            // tabPageAdvance
            // 
            this.tabPageAdvance.Controls.Add(this.checkBoxOverVpn);
            this.tabPageAdvance.Controls.Add(this.labelProxy);
            this.tabPageAdvance.Controls.Add(this.textBoxProxy);
            this.tabPageAdvance.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdvance.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAdvance.Name = "tabPageAdvance";
            this.tabPageAdvance.Size = new System.Drawing.Size(619, 663);
            this.tabPageAdvance.TabIndex = 2;
            this.tabPageAdvance.Text = "Advance";
            this.tabPageAdvance.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverVpn
            // 
            this.checkBoxOverVpn.AutoSize = true;
            this.checkBoxOverVpn.Location = new System.Drawing.Point(16, 23);
            this.checkBoxOverVpn.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxOverVpn.Name = "checkBoxOverVpn";
            this.checkBoxOverVpn.Size = new System.Drawing.Size(147, 21);
            this.checkBoxOverVpn.TabIndex = 13;
            this.checkBoxOverVpn.Text = "Running over VPN";
            this.checkBoxOverVpn.UseVisualStyleBackColor = true;
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
            // backgroundWorkerMaster
            // 
            this.backgroundWorkerMaster.WorkerReportsProgress = true;
            this.backgroundWorkerMaster.WorkerSupportsCancellation = true;
            this.backgroundWorkerMaster.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMaster_DoWork);
            this.backgroundWorkerMaster.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMaster_ProgressChanged);
            this.backgroundWorkerMaster.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMaster_RunWorkerCompleted);
            // 
            // backgroundWorkerGetUrl
            // 
            this.backgroundWorkerGetUrl.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGetUrl_DoWork);
            // 
            // backgroundWorkerAnchorParsePage
            // 
            this.backgroundWorkerAnchorParsePage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAnchorParsePage_DoWork);
            // 
            // progressBarGetUrls
            // 
            this.progressBarGetUrls.Location = new System.Drawing.Point(902, 517);
            this.progressBarGetUrls.Name = "progressBarGetUrls";
            this.progressBarGetUrls.Size = new System.Drawing.Size(342, 23);
            this.progressBarGetUrls.TabIndex = 22;
            // 
            // progressBarParsePages
            // 
            this.progressBarParsePages.Location = new System.Drawing.Point(902, 555);
            this.progressBarParsePages.Name = "progressBarParsePages";
            this.progressBarParsePages.Size = new System.Drawing.Size(342, 23);
            this.progressBarParsePages.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1585, 767);
            this.Controls.Add(this.progressBarParsePages);
            this.Controls.Add(this.progressBarGetUrls);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Crawler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageUserAgent.ResumeLayout(false);
            this.tabPageUserAgent.PerformLayout();
            this.tabPageIgnore.ResumeLayout(false);
            this.tabPageIgnore.PerformLayout();
            this.tabPageAdvance.ResumeLayout(false);
            this.tabPageAdvance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
		private System.Windows.Forms.TabControl tabControlSettings;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.Button buttonOutputDirectory;
		private System.Windows.Forms.Button buttonLoadCsv;
		private System.Windows.Forms.TextBox textBoxIgnoreLinksPatterns;
		private System.Windows.Forms.Button buttonStartWork;
		private System.Windows.Forms.Label labelIgnoreLinksRegExPatterns;
		private System.Windows.Forms.CheckBox checkBoxCheckDomainBeforeStart;
		private System.Windows.Forms.Label labelCsvFile;
		private System.Windows.Forms.TextBox textBoxOutputDirectory;
		private System.Windows.Forms.TextBox textBoxCsvFile;
		private System.Windows.Forms.Label labelOutputDirectory;
		private System.Windows.Forms.Label labelNewSiteDomain;
		private System.Windows.Forms.TextBox textBoxNewSiteDomain;
		private System.Windows.Forms.TabPage tabPageUserAgent;
		private System.Windows.Forms.TabPage tabPageAdvance;
		private System.Windows.Forms.CheckBox checkBoxOverVpn;
		private System.Windows.Forms.Label labelProxy;
		private System.Windows.Forms.TextBox textBoxProxy;
        private System.Windows.Forms.TabPage tabPageIgnore;
        private System.Windows.Forms.TextBox textBoxIgnoreExternalHostsRegExPatterns;
        private System.Windows.Forms.Label labelIgnoreExternalHostsRegExPatterns;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMaster;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGetUrl;
        private System.ComponentModel.BackgroundWorker backgroundWorkerAnchorParsePage;
        private System.Windows.Forms.ProgressBar progressBarGetUrls;
        private System.Windows.Forms.ProgressBar progressBarParsePages;
    }
}

