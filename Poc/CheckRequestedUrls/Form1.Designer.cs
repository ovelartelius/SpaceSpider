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
            this.labelIgnoreRegExPatterns = new System.Windows.Forms.Label();
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
            this.groupBoxInput.Location = new System.Drawing.Point(27, 51);
            this.groupBoxInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInput.Size = new System.Drawing.Size(666, 457);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "groupBox1";
            // 
            // textBoxIgnorePatterns
            // 
            this.textBoxIgnorePatterns.Location = new System.Drawing.Point(28, 159);
            this.textBoxIgnorePatterns.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxIgnorePatterns.Multiline = true;
            this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
            this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnorePatterns.Size = new System.Drawing.Size(547, 166);
            this.textBoxIgnorePatterns.TabIndex = 3;
            this.textBoxIgnorePatterns.Text = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(412, 334);
            this.buttonStartWork.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(184, 89);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Do tha shit";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Location = new System.Drawing.Point(24, 60);
            this.labelCsvFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(118, 20);
            this.labelCsvFile.TabIndex = 1;
            this.labelCsvFile.Text = "CSV file to load";
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(24, 89);
            this.textBoxCsvFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(552, 26);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\test_Toptargetpages.csv";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(855, 794);
            this.progressBarWork.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(597, 35);
            this.progressBarWork.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(249, 794);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(595, 69);
            this.textBoxLog.TabIndex = 2;
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(765, 138);
            this.textBoxNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(374, 26);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://localhost:56321/";
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(765, 111);
            this.labelNewSiteDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(125, 20);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "New site domain";
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(770, 180);
            this.labelProxy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(47, 20);
            this.labelProxy.TabIndex = 5;
            this.labelProxy.Text = "Proxy";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(770, 206);
            this.textBoxProxy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(370, 26);
            this.textBoxProxy.TabIndex = 6;
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(770, 274);
            this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(370, 26);
            this.textBoxUserAgent.TabIndex = 8;
            this.textBoxUserAgent.Text = "SpaceSpider";
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(770, 248);
            this.labelUserAgent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(86, 20);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "UserAgent";
            // 
            // backgroundWorkerUrlCheck
            // 
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
            this.textBoxSearchUrl.Location = new System.Drawing.Point(22, 560);
            this.textBoxSearchUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSearchUrl.Multiline = true;
            this.textBoxSearchUrl.Name = "textBoxSearchUrl";
            this.textBoxSearchUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSearchUrl.Size = new System.Drawing.Size(1627, 189);
            this.textBoxSearchUrl.TabIndex = 10;
            this.textBoxSearchUrl.Text = resources.GetString("textBoxSearchUrl.Text");
            // 
            // labelSearchUrl
            // 
            this.labelSearchUrl.AutoSize = true;
            this.labelSearchUrl.Location = new System.Drawing.Point(18, 535);
            this.labelSearchUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearchUrl.Name = "labelSearchUrl";
            this.labelSearchUrl.Size = new System.Drawing.Size(101, 20);
            this.labelSearchUrl.TabIndex = 9;
            this.labelSearchUrl.Text = "Search URL:";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(774, 415);
            this.textBoxOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(370, 26);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.Text = "C:\\dev\\SpaceSpider\\Poc\\Test\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(774, 389);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Output directory:";
            // 
            // checkBoxOverVpn
            // 
            this.checkBoxOverVpn.AutoSize = true;
            this.checkBoxOverVpn.Location = new System.Drawing.Point(147, 529);
            this.checkBoxOverVpn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxOverVpn.Name = "checkBoxOverVpn";
            this.checkBoxOverVpn.Size = new System.Drawing.Size(165, 24);
            this.checkBoxOverVpn.TabIndex = 13;
            this.checkBoxOverVpn.Text = "Running over VPN";
            this.checkBoxOverVpn.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreSearch
            // 
            this.checkBoxIgnoreSearch.AutoSize = true;
            this.checkBoxIgnoreSearch.Checked = true;
            this.checkBoxIgnoreSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnoreSearch.Location = new System.Drawing.Point(328, 529);
            this.checkBoxIgnoreSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxIgnoreSearch.Name = "checkBoxIgnoreSearch";
            this.checkBoxIgnoreSearch.Size = new System.Drawing.Size(133, 24);
            this.checkBoxIgnoreSearch.TabIndex = 14;
            this.checkBoxIgnoreSearch.Text = "Ignore search";
            this.checkBoxIgnoreSearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDomainBeforeStart
            // 
            this.checkBoxCheckDomainBeforeStart.AutoSize = true;
            this.checkBoxCheckDomainBeforeStart.Checked = true;
            this.checkBoxCheckDomainBeforeStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckDomainBeforeStart.Location = new System.Drawing.Point(470, 529);
            this.checkBoxCheckDomainBeforeStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxCheckDomainBeforeStart.Name = "checkBoxCheckDomainBeforeStart";
            this.checkBoxCheckDomainBeforeStart.Size = new System.Drawing.Size(255, 24);
            this.checkBoxCheckDomainBeforeStart.TabIndex = 15;
            this.checkBoxCheckDomainBeforeStart.Text = "Check site domain before start:";
            this.checkBoxCheckDomainBeforeStart.UseVisualStyleBackColor = true;
            // 
            // labelIgnoreRegExPatterns
            // 
            this.labelIgnoreRegExPatterns.AutoSize = true;
            this.labelIgnoreRegExPatterns.Location = new System.Drawing.Point(27, 135);
            this.labelIgnoreRegExPatterns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIgnoreRegExPatterns.Name = "labelIgnoreRegExPatterns";
            this.labelIgnoreRegExPatterns.Size = new System.Drawing.Size(174, 20);
            this.labelIgnoreRegExPatterns.TabIndex = 4;
            this.labelIgnoreRegExPatterns.Text = "Ignore RegEx patterns:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1914, 883);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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

