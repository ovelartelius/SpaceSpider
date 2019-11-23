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
            this.groupBoxInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.textBoxIgnorePatterns);
            this.groupBoxInput.Controls.Add(this.buttonStartWork);
            this.groupBoxInput.Controls.Add(this.labelCsvFile);
            this.groupBoxInput.Controls.Add(this.textBoxCsvFile);
            this.groupBoxInput.Location = new System.Drawing.Point(18, 33);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(444, 297);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "groupBox1";
            // 
            // textBoxIgnorePatterns
            // 
            this.textBoxIgnorePatterns.Location = new System.Drawing.Point(19, 85);
            this.textBoxIgnorePatterns.Multiline = true;
            this.textBoxIgnorePatterns.Name = "textBoxIgnorePatterns";
            this.textBoxIgnorePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIgnorePatterns.Size = new System.Drawing.Size(366, 109);
            this.textBoxIgnorePatterns.TabIndex = 3;
            this.textBoxIgnorePatterns.Text = resources.GetString("textBoxIgnorePatterns.Text");
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Location = new System.Drawing.Point(275, 217);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(123, 58);
            this.buttonStartWork.TabIndex = 2;
            this.buttonStartWork.Text = "Do tha shit";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Location = new System.Drawing.Point(16, 39);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(79, 13);
            this.labelCsvFile.TabIndex = 1;
            this.labelCsvFile.Text = "CSV file to load";
            // 
            // textBoxCsvFile
            // 
            this.textBoxCsvFile.Location = new System.Drawing.Point(16, 58);
            this.textBoxCsvFile.Name = "textBoxCsvFile";
            this.textBoxCsvFile.Size = new System.Drawing.Size(369, 20);
            this.textBoxCsvFile.TabIndex = 0;
            this.textBoxCsvFile.Text = "C:\\ws\\Other\\CheckRequestedUrls\\somefile.csv";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBarWork
            // 
            this.progressBarWork.Location = new System.Drawing.Point(570, 516);
            this.progressBarWork.Name = "progressBarWork";
            this.progressBarWork.Size = new System.Drawing.Size(398, 23);
            this.progressBarWork.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(166, 516);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(398, 46);
            this.textBoxLog.TabIndex = 2;
            // 
            // textBoxNewSiteDomain
            // 
            this.textBoxNewSiteDomain.Location = new System.Drawing.Point(510, 90);
            this.textBoxNewSiteDomain.Name = "textBoxNewSiteDomain";
            this.textBoxNewSiteDomain.Size = new System.Drawing.Size(251, 20);
            this.textBoxNewSiteDomain.TabIndex = 3;
            this.textBoxNewSiteDomain.Text = "http://admin3-seb-se.sebank.se/";
            // 
            // labelNewSiteDomain
            // 
            this.labelNewSiteDomain.AutoSize = true;
            this.labelNewSiteDomain.Location = new System.Drawing.Point(510, 72);
            this.labelNewSiteDomain.Name = "labelNewSiteDomain";
            this.labelNewSiteDomain.Size = new System.Drawing.Size(85, 13);
            this.labelNewSiteDomain.TabIndex = 4;
            this.labelNewSiteDomain.Text = "New site domain";
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(513, 117);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(33, 13);
            this.labelProxy.TabIndex = 5;
            this.labelProxy.Text = "Proxy";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(513, 134);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(248, 20);
            this.textBoxProxy.TabIndex = 6;
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(513, 178);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(248, 20);
            this.textBoxUserAgent.TabIndex = 8;
            this.textBoxUserAgent.Text = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
    "Chrome/60.0.3112.113 Safari/537.36";
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(513, 161);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(57, 13);
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
            this.textBoxSearchUrl.Location = new System.Drawing.Point(15, 364);
            this.textBoxSearchUrl.Multiline = true;
            this.textBoxSearchUrl.Name = "textBoxSearchUrl";
            this.textBoxSearchUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSearchUrl.Size = new System.Drawing.Size(1086, 124);
            this.textBoxSearchUrl.TabIndex = 10;
            this.textBoxSearchUrl.Text = resources.GetString("textBoxSearchUrl.Text");
            // 
            // labelSearchUrl
            // 
            this.labelSearchUrl.AutoSize = true;
            this.labelSearchUrl.Location = new System.Drawing.Point(12, 348);
            this.labelSearchUrl.Name = "labelSearchUrl";
            this.labelSearchUrl.Size = new System.Drawing.Size(69, 13);
            this.labelSearchUrl.TabIndex = 9;
            this.labelSearchUrl.Text = "Search URL:";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(516, 270);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(248, 20);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.Text = "C:\\ws\\Other\\CheckRequestedUrls\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Output directory:";
            // 
            // checkBoxOverVpn
            // 
            this.checkBoxOverVpn.AutoSize = true;
            this.checkBoxOverVpn.Location = new System.Drawing.Point(98, 344);
            this.checkBoxOverVpn.Name = "checkBoxOverVpn";
            this.checkBoxOverVpn.Size = new System.Drawing.Size(115, 17);
            this.checkBoxOverVpn.TabIndex = 13;
            this.checkBoxOverVpn.Text = "Running over VPN";
            this.checkBoxOverVpn.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreSearch
            // 
            this.checkBoxIgnoreSearch.AutoSize = true;
            this.checkBoxIgnoreSearch.Location = new System.Drawing.Point(219, 344);
            this.checkBoxIgnoreSearch.Name = "checkBoxIgnoreSearch";
            this.checkBoxIgnoreSearch.Size = new System.Drawing.Size(91, 17);
            this.checkBoxIgnoreSearch.TabIndex = 14;
            this.checkBoxIgnoreSearch.Text = "Ignore search";
            this.checkBoxIgnoreSearch.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 574);
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
    }
}

