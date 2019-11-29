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
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
            this.textBoxIgnorePatterns = new System.Windows.Forms.TextBox();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.labelCsvFile = new System.Windows.Forms.Label();
            this.textBoxCsvFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxOverVpn = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreSearch = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDomainBeforeStart = new System.Windows.Forms.CheckBox();
            this.groupBoxInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.labelIgnoreRegExPatterns);
            this.groupBoxInput.Controls.Add(this.textBoxIgnorePatterns);
            this.groupBoxInput.Controls.Add(this.buttonStartWork);
            this.groupBoxInput.Controls.Add(this.labelCsvFile);
            this.groupBoxInput.Controls.Add(this.textBoxCsvFile);
            this.groupBoxInput.Location = new System.Drawing.Point(24, 41);
            this.groupBoxInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxInput.Size = new System.Drawing.Size(592, 366);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "groupBox1";
            // 
            // labelIgnoreRegExPatterns
            // 
            this.labelIgnoreRegExPatterns.AutoSize = true;
            this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(24, 108);
            this.labelIgnoreRegExPatterns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
            this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(153, 17);
            this.labelIgnoreRegExPatterns.TabIndex = 4;
            this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
            // 
            // textBoxIgnorePatterns
            // 
            this.textBoxIgnorePatterns.Location = new System.Drawing.Point(25, 127);
            this.textBoxIgnorePatterns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIgnorePatterns.Multiline = true;
            this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
            this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnorePatterns.Size = new System.Drawing.Size(487, 134);
            this.textBoxIgnorePatterns.TabIndex = 3;
            this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(366, 267);
            this.buttonStartWork.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(164, 71);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Do tha shit";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Location = new System.Drawing.Point(21, 48);
            this.labelCsvFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(104, 17);
            this.labelCsvFile.TabIndex = 1;
            this.labelCsvFile.Text = "CSV file to load";
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(21, 71);
            this.textBoxCsvFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(491, 22);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(760, 635);
            this.progressBarWork.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(531, 28);
            this.progressBarWork.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(221, 635);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(529, 56);
            this.textBoxLog.TabIndex = 2;
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(680, 110);
            this.textBoxNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(333, 22);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(680, 89);
            this.labelNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(111, 17);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "New site domain";
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(684, 144);
            this.labelProxy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(43, 17);
            this.labelProxy.TabIndex = 5;
            this.labelProxy.Text = "Proxy";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(684, 165);
            this.textBoxProxy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(329, 22);
            this.textBoxProxy.TabIndex = 6;
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(684, 219);
            this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(329, 22);
            this.textBoxUserAgent.TabIndex = 8;
            this.textBoxUserAgent.Text = "SpaceSpider";
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(684, 198);
            this.labelUserAgent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(75, 17);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "UserAgent";
            // 
            // backgroundWorkerUrlCheck
            // 
            this.backgroundWorkerUrlCheck.WorkerReportsProgress = true;
            this.backgroundWorkerUrlCheck.WorkerSupportsCancellation = true;
            this.backgroundWorkerUrlCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUrlCheck_DoWork);
            this.backgroundWorkerUrlCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerUrlCheck_RunWorkerCompleted);
            // 
            // backgroundWorkerLoadCsv
            // 
            this.backgroundWorkerLoadCsv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadCsv_DoWork);
            this.backgroundWorkerLoadCsv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoadCsv_RunWorkerCompleted);
            // 
            // textBoxSearchUrl
            // 
            this.textBoxSearchUrl.Location = new System.Drawing.Point(20, 448);
            this.textBoxSearchUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSearchUrl.Multiline = true;
            this.textBoxSearchUrl.Name = "textBoxSearchUrl";
            this.textBoxSearchUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSearchUrl.Size = new System.Drawing.Size(1447, 152);
            this.textBoxSearchUrl.TabIndex = 10;
            this.textBoxSearchUrl.Text = resources.GetString("textBoxSearchUrl.Text");
            // 
            // labelSearchUrl
            // 
            this.labelSearchUrl.AutoSize = true;
            this.labelSearchUrl.Location = new System.Drawing.Point(16, 428);
            this.labelSearchUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearchUrl.Name = "labelSearchUrl";
            this.labelSearchUrl.Size = new System.Drawing.Size(89, 17);
            this.labelSearchUrl.TabIndex = 9;
            this.labelSearchUrl.Text = "Search URL:";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(688, 332);
            this.textBoxOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(329, 22);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(688, 311);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Output directory:";
            // 
            // checkBoxOverVpn
            // 
            this.checkBoxOverVpn.AutoSize = true;
            this.checkBoxOverVpn.Location = new System.Drawing.Point(131, 423);
            this.checkBoxOverVpn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxOverVpn.Name = "checkBoxOverVpn";
            this.checkBoxOverVpn.Size = new System.Drawing.Size(147, 21);
            this.checkBoxOverVpn.TabIndex = 13;
            this.checkBoxOverVpn.Text = "Running over VPN";
            this.checkBoxOverVpn.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreSearch
            // 
            this.checkBoxIgnoreSearch.AutoSize = true;
            this.checkBoxIgnoreSearch.Checked = true;
            this.checkBoxIgnoreSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnoreSearch.Location = new System.Drawing.Point(292, 423);
            this.checkBoxIgnoreSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxIgnoreSearch.Name = "checkBoxIgnoreSearch";
            this.checkBoxIgnoreSearch.Size = new System.Drawing.Size(117, 21);
            this.checkBoxIgnoreSearch.TabIndex = 14;
            this.checkBoxIgnoreSearch.Text = "Ignore search";
            this.checkBoxIgnoreSearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDomainBeforeStart
            // 
            this.checkBoxCheckDomainBeforeStart.AutoSize = true;
            this.checkBoxCheckDomainBeforeStart.Checked = true;
            this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(418, 423);
            this.checkBoxCheckDomainBeforeStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
            this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(226, 21);
            this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
            this.checkBoxCheckDomainBeforeStart.Text = "Check site domain before start:";
            this.checkBoxCheckDomainBeforeStart.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1701, 706);
            this.Controls.Add(this.checkBoxCheckDomainBeforeStart);
            this.Controls.Add(this.checkBoxIgnoreSearch);
            this.Controls.Add(this.checkBoxOverVpn);
            this.Controls.Add(this.textBoxOutputDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearchUrl);
            this.Controls.Add(this.labelSearchUrl);
            this.Controls.Add(this.textBoxUserAgent);
            this.Controls.Add(this.labelUserAgent);
            this.Controls.Add(this.textBoxProxy);
            this.Controls.Add(this.labelProxy);
            this.Controls.Add(this.labelNewSiteDomain);
            this.Controls.Add(this.textBoxNewSiteDomain);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.progressBarWork);
            this.Controls.Add(this.groupBoxInput);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.TextBox textBoxCsvFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxOverVpn;
        private System.Windows.Forms.CheckBox checkBoxIgnoreSearch;
        private System.Windows.Forms.CheckBox checkBoxCheckDomainBeforeStart;
        private System.Windows.Forms.Label labelIgnoreRegExPatterns;
    }
}

