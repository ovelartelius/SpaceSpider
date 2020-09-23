namespace CsvMerger
{
	partial class MergeForm
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.textBoxCsvFileSeperatorCsvFile2 = new System.Windows.Forms.TextBox();
			this.labelCsvFileSeperatorCsvFile2 = new System.Windows.Forms.Label();
			this.checkBoxFirstRowContainsTitleCsvFile2 = new System.Windows.Forms.CheckBox();
			this.buttonLoadCsvFile2 = new System.Windows.Forms.Button();
			this.labelCsvFile2 = new System.Windows.Forms.Label();
			this.textBoxCsvFile2 = new System.Windows.Forms.TextBox();
			this.textBoxCsvFileSeperatorCsvFile1 = new System.Windows.Forms.TextBox();
			this.labelCsvFileSeperatorCsvFile1 = new System.Windows.Forms.Label();
			this.checkBoxFirstRowContainsTitleCsvFile1 = new System.Windows.Forms.CheckBox();
			this.buttonOutputDirectory = new System.Windows.Forms.Button();
			this.buttonLoadCsvFile1 = new System.Windows.Forms.Button();
			this.textBoxIgnorePatterns = new System.Windows.Forms.TextBox();
			this.buttonStartWork = new System.Windows.Forms.Button();
			this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
			this.labelCsvFile1 = new System.Windows.Forms.Label();
			this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
			this.textBoxCsvFile1 = new System.Windows.Forms.TextBox();
			this.labelOutputDirectory = new System.Windows.Forms.Label();
			this.labelNewSiteDomain = new System.Windows.Forms.Label();
			this.textBoxNewSiteDomain = new System.Windows.Forms.TextBox();
			this.tabPageUserAgent = new System.Windows.Forms.TabPage();
			this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
			this.openCsvDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
			this.openCsvDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.folderOutputDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkLabelResultFolder = new System.Windows.Forms.LinkLabel();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.backgroundWorkerLoadCsv = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorkerMerger = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
			this.menuStrip1.Size = new System.Drawing.Size(875, 24);
			this.menuStrip1.TabIndex = 17;
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
			this.tabControl1.Location = new System.Drawing.Point(12, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(470, 534);
			this.tabControl1.TabIndex = 18;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.textBoxCsvFileSeperatorCsvFile2);
			this.tabPageBasic.Controls.Add(this.labelCsvFileSeperatorCsvFile2);
			this.tabPageBasic.Controls.Add(this.checkBoxFirstRowContainsTitleCsvFile2);
			this.tabPageBasic.Controls.Add(this.buttonLoadCsvFile2);
			this.tabPageBasic.Controls.Add(this.labelCsvFile2);
			this.tabPageBasic.Controls.Add(this.textBoxCsvFile2);
			this.tabPageBasic.Controls.Add(this.textBoxCsvFileSeperatorCsvFile1);
			this.tabPageBasic.Controls.Add(this.labelCsvFileSeperatorCsvFile1);
			this.tabPageBasic.Controls.Add(this.checkBoxFirstRowContainsTitleCsvFile1);
			this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
			this.tabPageBasic.Controls.Add(this.buttonLoadCsvFile1);
			this.tabPageBasic.Controls.Add(this.textBoxIgnorePatterns);
			this.tabPageBasic.Controls.Add(this.buttonStartWork);
			this.tabPageBasic.Controls.Add(this.labelIgnoreRegExPatterns);
			this.tabPageBasic.Controls.Add(this.labelCsvFile1);
			this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
			this.tabPageBasic.Controls.Add(this.textBoxCsvFile1);
			this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
			this.tabPageBasic.Controls.Add(this.labelNewSiteDomain);
			this.tabPageBasic.Controls.Add(this.textBoxNewSiteDomain);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(462, 508);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// textBoxCsvFileSeperatorCsvFile2
			// 
			this.textBoxCsvFileSeperatorCsvFile2.Location = new System.Drawing.Point(7, 171);
			this.textBoxCsvFileSeperatorCsvFile2.Name = "textBoxCsvFileSeperatorCsvFile2";
			this.textBoxCsvFileSeperatorCsvFile2.Size = new System.Drawing.Size(49, 20);
			this.textBoxCsvFileSeperatorCsvFile2.TabIndex = 29;
			this.textBoxCsvFileSeperatorCsvFile2.Text = ",";
			// 
			// labelCsvFileSeperatorCsvFile2
			// 
			this.labelCsvFileSeperatorCsvFile2.AutoSize = true;
			this.labelCsvFileSeperatorCsvFile2.Location = new System.Drawing.Point(7, 154);
			this.labelCsvFileSeperatorCsvFile2.Name = "labelCsvFileSeperatorCsvFile2";
			this.labelCsvFileSeperatorCsvFile2.Size = new System.Drawing.Size(131, 13);
			this.labelCsvFileSeperatorCsvFile2.TabIndex = 28;
			this.labelCsvFileSeperatorCsvFile2.Text = "CSV file column seperator:";
			// 
			// checkBoxFirstRowContainsTitleCsvFile2
			// 
			this.checkBoxFirstRowContainsTitleCsvFile2.AutoSize = true;
			this.checkBoxFirstRowContainsTitleCsvFile2.Checked = true;
			this.checkBoxFirstRowContainsTitleCsvFile2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFirstRowContainsTitleCsvFile2.Location = new System.Drawing.Point(238, 169);
			this.checkBoxFirstRowContainsTitleCsvFile2.Name = "checkBoxFirstRowContainsTitleCsvFile2";
			this.checkBoxFirstRowContainsTitleCsvFile2.Size = new System.Drawing.Size(132, 17);
			this.checkBoxFirstRowContainsTitleCsvFile2.TabIndex = 27;
			this.checkBoxFirstRowContainsTitleCsvFile2.Text = "First row contains titles";
			this.checkBoxFirstRowContainsTitleCsvFile2.UseVisualStyleBackColor = true;
			// 
			// buttonLoadCsvFile2
			// 
			this.buttonLoadCsvFile2.Location = new System.Drawing.Point(422, 128);
			this.buttonLoadCsvFile2.Name = "buttonLoadCsvFile2";
			this.buttonLoadCsvFile2.Size = new System.Drawing.Size(34, 20);
			this.buttonLoadCsvFile2.TabIndex = 26;
			this.buttonLoadCsvFile2.Text = "...";
			this.buttonLoadCsvFile2.UseVisualStyleBackColor = true;
			this.buttonLoadCsvFile2.Click += new System.EventHandler(this.buttonLoadCsvFile2_Click);
			// 
			// labelCsvFile2
			// 
			this.labelCsvFile2.AutoSize = true;
			this.labelCsvFile2.Location = new System.Drawing.Point(6, 112);
			this.labelCsvFile2.Name = "labelCsvFile2";
			this.labelCsvFile2.Size = new System.Drawing.Size(88, 13);
			this.labelCsvFile2.TabIndex = 25;
			this.labelCsvFile2.Text = "CSV file 2 to load";
			// 
			// textBoxCsvFile2
			// 
			this.textBoxCsvFile2.Location = new System.Drawing.Point(9, 128);
			this.textBoxCsvFile2.Name = "textBoxCsvFile2";
			this.textBoxCsvFile2.Size = new System.Drawing.Size(406, 20);
			this.textBoxCsvFile2.TabIndex = 24;
			this.textBoxCsvFile2.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
			// 
			// textBoxCsvFileSeperatorCsvFile1
			// 
			this.textBoxCsvFileSeperatorCsvFile1.Location = new System.Drawing.Point(7, 67);
			this.textBoxCsvFileSeperatorCsvFile1.Name = "textBoxCsvFileSeperatorCsvFile1";
			this.textBoxCsvFileSeperatorCsvFile1.Size = new System.Drawing.Size(49, 20);
			this.textBoxCsvFileSeperatorCsvFile1.TabIndex = 23;
			this.textBoxCsvFileSeperatorCsvFile1.Text = ",";
			// 
			// labelCsvFileSeperatorCsvFile1
			// 
			this.labelCsvFileSeperatorCsvFile1.AutoSize = true;
			this.labelCsvFileSeperatorCsvFile1.Location = new System.Drawing.Point(7, 50);
			this.labelCsvFileSeperatorCsvFile1.Name = "labelCsvFileSeperatorCsvFile1";
			this.labelCsvFileSeperatorCsvFile1.Size = new System.Drawing.Size(131, 13);
			this.labelCsvFileSeperatorCsvFile1.TabIndex = 22;
			this.labelCsvFileSeperatorCsvFile1.Text = "CSV file column seperator:";
			// 
			// checkBoxFirstRowContainsTitleCsvFile1
			// 
			this.checkBoxFirstRowContainsTitleCsvFile1.AutoSize = true;
			this.checkBoxFirstRowContainsTitleCsvFile1.Checked = true;
			this.checkBoxFirstRowContainsTitleCsvFile1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFirstRowContainsTitleCsvFile1.Location = new System.Drawing.Point(238, 65);
			this.checkBoxFirstRowContainsTitleCsvFile1.Name = "checkBoxFirstRowContainsTitleCsvFile1";
			this.checkBoxFirstRowContainsTitleCsvFile1.Size = new System.Drawing.Size(132, 17);
			this.checkBoxFirstRowContainsTitleCsvFile1.TabIndex = 21;
			this.checkBoxFirstRowContainsTitleCsvFile1.Text = "First row contains titles";
			this.checkBoxFirstRowContainsTitleCsvFile1.UseVisualStyleBackColor = true;
			// 
			// buttonOutputDirectory
			// 
			this.buttonOutputDirectory.Location = new System.Drawing.Point(422, 408);
			this.buttonOutputDirectory.Name = "buttonOutputDirectory";
			this.buttonOutputDirectory.Size = new System.Drawing.Size(34, 21);
			this.buttonOutputDirectory.TabIndex = 17;
			this.buttonOutputDirectory.Text = "...";
			this.buttonOutputDirectory.UseVisualStyleBackColor = true;
			this.buttonOutputDirectory.Click += new System.EventHandler(this.buttonOutputDirectory_Click);
			// 
			// buttonLoadCsvFile1
			// 
			this.buttonLoadCsvFile1.Location = new System.Drawing.Point(422, 24);
			this.buttonLoadCsvFile1.Name = "buttonLoadCsvFile1";
			this.buttonLoadCsvFile1.Size = new System.Drawing.Size(34, 20);
			this.buttonLoadCsvFile1.TabIndex = 16;
			this.buttonLoadCsvFile1.Text = "...";
			this.buttonLoadCsvFile1.UseVisualStyleBackColor = true;
			this.buttonLoadCsvFile1.Click += new System.EventHandler(this.buttonLoadCsvFile1_Click);
			// 
			// textBoxIgnorePatterns
			// 
			this.textBoxIgnorePatterns.Location = new System.Drawing.Point(9, 234);
			this.textBoxIgnorePatterns.Multiline = true;
			this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
			this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxIgnorePatterns.Size = new System.Drawing.Size(406, 109);
			this.textBoxIgnorePatterns.TabIndex = 3;
			this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
			// 
			// buttonStartWork
			// 
			this.buttonStartWork.Location = new System.Drawing.Point(333, 458);
			this.buttonStartWork.Name = "buttonStartWork";
			this.buttonStartWork.Size = new System.Drawing.Size(123, 44);
			this.buttonStartWork.TabIndex = 2;
			this.buttonStartWork.Text = "Start merge CSV files";
			this.buttonStartWork.UseVisualStyleBackColor = true;
			this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
			// 
			// labelIgnoreRegExPatterns
			// 
			this.labelIgnoreRegExPatterns.AutoSize = true;
			this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(6, 217);
			this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
			this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(116, 13);
			this.labelIgnoreRegExPatterns.TabIndex = 4;
			this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
			// 
			// labelCsvFile1
			// 
			this.labelCsvFile1.AutoSize = true;
			this.labelCsvFile1.Location = new System.Drawing.Point(6, 8);
			this.labelCsvFile1.Name = "labelCsvFile1";
			this.labelCsvFile1.Size = new System.Drawing.Size(88, 13);
			this.labelCsvFile1.TabIndex = 1;
			this.labelCsvFile1.Text = "CSV file 1 to load";
			// 
			// textBoxOutputDirectory
			// 
			this.textBoxOutputDirectory.Location = new System.Drawing.Point(9, 409);
			this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
			this.textBoxOutputDirectory.Size = new System.Drawing.Size(406, 20);
			this.textBoxOutputDirectory.TabIndex = 12;
			// 
			// textBoxCsvFile1
			// 
			this.textBoxCsvFile1.Location = new System.Drawing.Point(9, 24);
			this.textBoxCsvFile1.Name = "textBoxCsvFile1";
			this.textBoxCsvFile1.Size = new System.Drawing.Size(406, 20);
			this.textBoxCsvFile1.TabIndex = 0;
			this.textBoxCsvFile1.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
			// 
			// labelOutputDirectory
			// 
			this.labelOutputDirectory.AutoSize = true;
			this.labelOutputDirectory.Location = new System.Drawing.Point(6, 393);
			this.labelOutputDirectory.Name = "labelOutputDirectory";
			this.labelOutputDirectory.Size = new System.Drawing.Size(85, 13);
			this.labelOutputDirectory.TabIndex = 11;
			this.labelOutputDirectory.Text = "Output directory:";
			// 
			// labelNewSiteDomain
			// 
			this.labelNewSiteDomain.AutoSize = true;
			this.labelNewSiteDomain.Location = new System.Drawing.Point(7, 349);
			this.labelNewSiteDomain.Name = "labelNewSiteDomain";
			this.labelNewSiteDomain.Size = new System.Drawing.Size(85, 13);
			this.labelNewSiteDomain.TabIndex = 4;
			this.labelNewSiteDomain.Text = "New site domain";
			// 
			// textBoxNewSiteDomain
			// 
			this.textBoxNewSiteDomain.Location = new System.Drawing.Point(9, 366);
			this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
			this.textBoxNewSiteDomain.Size = new System.Drawing.Size(406, 20);
			this.textBoxNewSiteDomain.TabIndex = 3;
			this.textBoxNewSiteDomain.Text = "http://domain.com/";
			// 
			// tabPageUserAgent
			// 
			this.tabPageUserAgent.Location = new System.Drawing.Point(4, 22);
			this.tabPageUserAgent.Name = "tabPageUserAgent";
			this.tabPageUserAgent.Size = new System.Drawing.Size(462, 508);
			this.tabPageUserAgent.TabIndex = 3;
			this.tabPageUserAgent.Text = "UserAgent";
			this.tabPageUserAgent.UseVisualStyleBackColor = true;
			// 
			// openSettingsDialog
			// 
			this.openSettingsDialog.Filter = "json files (*.json)|*.json";
			this.openSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openSettingsDialog_FileOk);
			// 
			// openCsvDialog
			// 
			this.openCsvDialog.Filter = "CSV files (*.csv)|*.csv";
			this.openCsvDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openCsvDialog_FileOk);
			// 
			// saveSettingsDialog
			// 
			this.saveSettingsDialog.FileName = "*.json";
			this.saveSettingsDialog.Filter = "json files (*.json)|*.json";
			this.saveSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveSettingsDialog_FileOk);
			// 
			// openCsvDialog2
			// 
			this.openCsvDialog2.Filter = "CSV files (*.csv)|*.csv";
			this.openCsvDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openCsvDialog2_FileOk);
			// 
			// folderOutputDialog
			// 
			this.folderOutputDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkLabelResultFolder);
			this.groupBox1.Controls.Add(this.textBoxLog);
			this.groupBox1.Location = new System.Drawing.Point(488, 49);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(377, 404);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Result:";
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
			// backgroundWorkerLoadCsv
			// 
			this.backgroundWorkerLoadCsv.WorkerReportsProgress = true;
			this.backgroundWorkerLoadCsv.WorkerSupportsCancellation = true;
			this.backgroundWorkerLoadCsv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadCsv_DoWork);
			this.backgroundWorkerLoadCsv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoadCsv_RunWorkerCompleted);
			// 
			// backgroundWorkerMerger
			// 
			this.backgroundWorkerMerger.WorkerReportsProgress = true;
			this.backgroundWorkerMerger.WorkerSupportsCancellation = true;
			// 
			// MergeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(875, 571);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "MergeForm";
			this.Text = "Merge CSV files";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.TextBox textBoxCsvFileSeperatorCsvFile1;
		private System.Windows.Forms.Label labelCsvFileSeperatorCsvFile1;
		private System.Windows.Forms.CheckBox checkBoxFirstRowContainsTitleCsvFile1;
		private System.Windows.Forms.Button buttonOutputDirectory;
		private System.Windows.Forms.Button buttonLoadCsvFile1;
		private System.Windows.Forms.TextBox textBoxIgnorePatterns;
		private System.Windows.Forms.Button buttonStartWork;
		private System.Windows.Forms.Label labelIgnoreRegExPatterns;
		private System.Windows.Forms.Label labelCsvFile1;
		private System.Windows.Forms.TextBox textBoxOutputDirectory;
		private System.Windows.Forms.TextBox textBoxCsvFile1;
		private System.Windows.Forms.Label labelOutputDirectory;
		private System.Windows.Forms.Label labelNewSiteDomain;
		private System.Windows.Forms.TextBox textBoxNewSiteDomain;
		private System.Windows.Forms.TextBox textBoxCsvFileSeperatorCsvFile2;
		private System.Windows.Forms.Label labelCsvFileSeperatorCsvFile2;
		private System.Windows.Forms.CheckBox checkBoxFirstRowContainsTitleCsvFile2;
		private System.Windows.Forms.Button buttonLoadCsvFile2;
		private System.Windows.Forms.Label labelCsvFile2;
		private System.Windows.Forms.TextBox textBoxCsvFile2;
		private System.Windows.Forms.TabPage tabPageUserAgent;
		private System.Windows.Forms.OpenFileDialog openSettingsDialog;
		private System.Windows.Forms.OpenFileDialog openCsvDialog;
		private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
		private System.Windows.Forms.OpenFileDialog openCsvDialog2;
		private System.Windows.Forms.FolderBrowserDialog folderOutputDialog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel linkLabelResultFolder;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.ComponentModel.BackgroundWorker backgroundWorkerLoadCsv;
		private System.ComponentModel.BackgroundWorker backgroundWorkerMerger;
	}
}

