namespace LinkjuiceCreator
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.textBoxCsvFileSeperator = new System.Windows.Forms.TextBox();
            this.labelCsvFileSeperator = new System.Windows.Forms.Label();
            this.checkBoxFirstRowContainsTitle = new System.Windows.Forms.CheckBox();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.buttonLoadCsv = new System.Windows.Forms.Button();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
            this.labelCsvFile = new System.Windows.Forms.Label();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.textBoxCsvFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelNewSiteDomain = new System.Windows.Forms.Label();
            this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
            this.tabPageUserAgent = new System.Windows.Forms.TabPage();
            this.labelUserAgent = new System.Windows.Forms.Label();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.tabPageAdvance = new System.Windows.Forms.TabPage();
            this.checkBoxOverVpn = new System.Windows.Forms.CheckBox();
            this.labelProxy = new System.Windows.Forms.Label();
            this.textBoxProxy = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCsvDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerLoadCsv = new System.ComponentModel.BackgroundWorker();
            this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderOutputDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.linkLabelResultFolder = new System.Windows.Forms.LinkLabel();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.progressBarWork = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerDoJob = new System.ComponentModel.BackgroundWorker();
            this.textBoxPageVariableHeaderName = new System.Windows.Forms.TextBox();
            this.labelPageVariableHeaderName = new System.Windows.Forms.Label();
            this.checkBoxCleanUpNoneFoundPageIds = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageUserAgent.SuspendLayout();
            this.tabPageAdvance.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBasic);
            this.tabControl1.Controls.Add(this.tabPageUserAgent);
            this.tabControl1.Controls.Add(this.tabPageAdvance);
            this.tabControl1.Location = new System.Drawing.Point(12, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 430);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.checkBoxCleanUpNoneFoundPageIds);
            this.tabPageBasic.Controls.Add(this.textBoxPageVariableHeaderName);
            this.tabPageBasic.Controls.Add(this.labelPageVariableHeaderName);
            this.tabPageBasic.Controls.Add(this.textBoxCsvFileSeperator);
            this.tabPageBasic.Controls.Add(this.labelCsvFileSeperator);
            this.tabPageBasic.Controls.Add(this.checkBoxFirstRowContainsTitle);
            this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
            this.tabPageBasic.Controls.Add(this.buttonLoadCsv);
            this.tabPageBasic.Controls.Add(this.buttonStartWork);
            this.tabPageBasic.Controls.Add(this.checkBoxCheckDomainBeforeStart);
            this.tabPageBasic.Controls.Add(this.labelCsvFile);
            this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.tabPageBasic.Controls.Add(this.textBoxCsvFile);
            this.tabPageBasic.Controls.Add(this.label1);
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
            // textBoxCsvFileSeperator
            // 
            this.textBoxCsvFileSeperator.Location = new System.Drawing.Point(9, 91);
            this.textBoxCsvFileSeperator.Name = "textBoxCsvFileSeperator";
            this.textBoxCsvFileSeperator.Size = new System.Drawing.Size(49, 20);
            this.textBoxCsvFileSeperator.TabIndex = 20;
            // 
            // labelCsvFileSeperator
            // 
            this.labelCsvFileSeperator.AutoSize = true;
            this.labelCsvFileSeperator.Location = new System.Drawing.Point(9, 74);
            this.labelCsvFileSeperator.Name = "labelCsvFileSeperator";
            this.labelCsvFileSeperator.Size = new System.Drawing.Size(131, 13);
            this.labelCsvFileSeperator.TabIndex = 19;
            this.labelCsvFileSeperator.Text = "CSV file column seperator:";
            // 
            // checkBoxFirstRowContainsTitle
            // 
            this.checkBoxFirstRowContainsTitle.AutoSize = true;
            this.checkBoxFirstRowContainsTitle.Checked = true;
            this.checkBoxFirstRowContainsTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFirstRowContainsTitle.Location = new System.Drawing.Point(12, 50);
            this.checkBoxFirstRowContainsTitle.Name = "checkBoxFirstRowContainsTitle";
            this.checkBoxFirstRowContainsTitle.Size = new System.Drawing.Size(132, 17);
            this.checkBoxFirstRowContainsTitle.TabIndex = 18;
            this.checkBoxFirstRowContainsTitle.Text = "First row contains titles";
            this.checkBoxFirstRowContainsTitle.UseVisualStyleBackColor = true;
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
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(9, 325);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(406, 20);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\";
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(9, 24);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(406, 20);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Output directory:";
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
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(9, 233);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(406, 20);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
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
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(13, 12);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(60, 13);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "UserAgent:";
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(16, 28);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(248, 20);
            this.textBoxUserAgent.TabIndex = 8;
            this.textBoxUserAgent.Text = "SpaceSpider";
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.archiveToolStripMenuItem.Text = "&Archive";
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
            // openCsvDialog
            // 
            this.openCsvDialog.Filter = "CSV files (*.csv)|*.csv";
            this.openCsvDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openCsvDialog_FileOk);
            // 
            // backgroundWorkerLoadCsv
            // 
            this.backgroundWorkerLoadCsv.WorkerReportsProgress = true;
            this.backgroundWorkerLoadCsv.WorkerSupportsCancellation = true;
            this.backgroundWorkerLoadCsv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadCsv_DoWork);
            this.backgroundWorkerLoadCsv.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerLoadCsv_ProgressChanged);
            this.backgroundWorkerLoadCsv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoadCsv_RunWorkerCompleted);
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
            // folderOutputDialog
            // 
            this.folderOutputDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.linkLabelResultFolder);
            this.groupBoxResult.Controls.Add(this.textBoxLog);
            this.groupBoxResult.Controls.Add(this.progressBarWork);
            this.groupBoxResult.Location = new System.Drawing.Point(488, 46);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(377, 404);
            this.groupBoxResult.TabIndex = 20;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result:";
            // 
            // linkLabelResultFolder
            // 
            this.linkLabelResultFolder.AutoSize = true;
            this.linkLabelResultFolder.Location = new System.Drawing.Point(7, 352);
            this.linkLabelResultFolder.Name = "linkLabelResultFolder";
            this.linkLabelResultFolder.Size = new System.Drawing.Size(0, 13);
            this.linkLabelResultFolder.TabIndex = 3;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(6, 28);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(365, 317);
            this.textBoxLog.TabIndex = 2;
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(6, 375);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(365, 23);
            this.progressBarWork.TabIndex = 1;
            // 
            // backgroundWorkerDoJob
            // 
            this.backgroundWorkerDoJob.WorkerReportsProgress = true;
            this.backgroundWorkerDoJob.WorkerSupportsCancellation = true;
            this.backgroundWorkerDoJob.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDoJob_DoWork);
            this.backgroundWorkerDoJob.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDoJob_ProgressChanged);
            this.backgroundWorkerDoJob.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDoJob_RunWorkerCompleted);
            // 
            // textBoxPageVariableHeaderName
            // 
            this.textBoxPageVariableHeaderName.Location = new System.Drawing.Point(9, 135);
            this.textBoxPageVariableHeaderName.Name = "textBoxPageVariableHeaderName";
            this.textBoxPageVariableHeaderName.Size = new System.Drawing.Size(406, 20);
            this.textBoxPageVariableHeaderName.TabIndex = 22;
            this.textBoxPageVariableHeaderName.Text = "pageid";
            // 
            // labelPageVariableHeaderName
            // 
            this.labelPageVariableHeaderName.AutoSize = true;
            this.labelPageVariableHeaderName.Location = new System.Drawing.Point(9, 118);
            this.labelPageVariableHeaderName.Name = "labelPageVariableHeaderName";
            this.labelPageVariableHeaderName.Size = new System.Drawing.Size(140, 13);
            this.labelPageVariableHeaderName.TabIndex = 21;
            this.labelPageVariableHeaderName.Text = "Page header varaible name:";
            // 
            // checkBoxCleanUpNoneFoundPageIds
            // 
            this.checkBoxCleanUpNoneFoundPageIds.AutoSize = true;
            this.checkBoxCleanUpNoneFoundPageIds.Checked = true;
            this.checkBoxCleanUpNoneFoundPageIds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCleanUpNoneFoundPageIds.Location = new System.Drawing.Point(12, 172);
            this.checkBoxCleanUpNoneFoundPageIds.Name = "checkBoxCleanUpNoneFoundPageIds";
            this.checkBoxCleanUpNoneFoundPageIds.Size = new System.Drawing.Size(167, 17);
            this.checkBoxCleanUpNoneFoundPageIds.TabIndex = 23;
            this.checkBoxCleanUpNoneFoundPageIds.Text = "Clean up none found PageIds";
            this.checkBoxCleanUpNoneFoundPageIds.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 472);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Linkjuice creator";
            this.tabControl1.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageUserAgent.ResumeLayout(false);
            this.tabPageUserAgent.PerformLayout();
            this.tabPageAdvance.ResumeLayout(false);
            this.tabPageAdvance.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            this.groupBoxResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageBasic;
        private System.Windows.Forms.Button buttonOutputDirectory;
        private System.Windows.Forms.Button buttonLoadCsv;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.CheckBox checkBoxCheckDomainBeforeStart;
        private System.Windows.Forms.Label labelCsvFile;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.TextBox textBoxCsvFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelNewSiteDomain;
        private System.Windows.Forms.TextBox textBoxNewSiteDomain;
        private System.Windows.Forms.TabPage tabPageUserAgent;
        private System.Windows.Forms.Label labelUserAgent;
        private System.Windows.Forms.TextBox textBoxUserAgent;
        private System.Windows.Forms.TabPage tabPageAdvance;
        private System.Windows.Forms.CheckBox checkBoxOverVpn;
        private System.Windows.Forms.Label labelProxy;
        private System.Windows.Forms.TextBox textBoxProxy;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openCsvDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadCsv;
        private System.Windows.Forms.OpenFileDialog openSettingsDialog;
        private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
        private System.Windows.Forms.FolderBrowserDialog folderOutputDialog;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.LinkLabel linkLabelResultFolder;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.ProgressBar progressBarWork;
        private System.Windows.Forms.CheckBox checkBoxFirstRowContainsTitle;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDoJob;
        private System.Windows.Forms.TextBox textBoxCsvFileSeperator;
        private System.Windows.Forms.Label labelCsvFileSeperator;
        private System.Windows.Forms.TextBox textBoxPageVariableHeaderName;
        private System.Windows.Forms.Label labelPageVariableHeaderName;
        private System.Windows.Forms.CheckBox checkBoxCleanUpNoneFoundPageIds;
    }
}

