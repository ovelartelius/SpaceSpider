namespace SeoSpider
{
	partial class Form2
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
			this.Url = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.ResultDataGridView = new System.Windows.Forms.DataGridView();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.cbRobotsTxt = new System.Windows.Forms.CheckBox();
			this.cbSitemapXml = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.numMaxFolderDepth = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numMaxNumPages = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMatchPattern = new System.Windows.Forms.TextBox();
			this.btnShowReport = new System.Windows.Forms.Button();
			this.cbExcludeExtHosts = new System.Windows.Forms.CheckBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.cbOldReports = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGridViewExtResources = new System.Windows.Forms.DataGridView();
			this.label4 = new System.Windows.Forms.Label();
			this.dataGridViewIframes = new System.Windows.Forms.DataGridView();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxFolderDepth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxNumPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtResources)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIframes)).BeginInit();
			this.SuspendLayout();
			// 
			// Url
			// 
			this.Url.Location = new System.Drawing.Point(8, 7);
			this.Url.Name = "Url";
			this.Url.Size = new System.Drawing.Size(195, 20);
			this.Url.TabIndex = 0;
			this.Url.Text = "https://www.rule.se/";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(209, 5);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// ResultDataGridView
			// 
			this.ResultDataGridView.AllowUserToAddRows = false;
			this.ResultDataGridView.AllowUserToDeleteRows = false;
			this.ResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ResultDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ResultDataGridView.Location = new System.Drawing.Point(0, 217);
			this.ResultDataGridView.Name = "ResultDataGridView";
			this.ResultDataGridView.Size = new System.Drawing.Size(1031, 450);
			this.ResultDataGridView.TabIndex = 2;
			this.ResultDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellClick);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(12, 34);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(290, 5);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(209, 29);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(156, 23);
			this.progressBar1.TabIndex = 5;
			// 
			// cbRobotsTxt
			// 
			this.cbRobotsTxt.AutoSize = true;
			this.cbRobotsTxt.Location = new System.Drawing.Point(398, 5);
			this.cbRobotsTxt.Name = "cbRobotsTxt";
			this.cbRobotsTxt.Size = new System.Drawing.Size(74, 17);
			this.cbRobotsTxt.TabIndex = 6;
			this.cbRobotsTxt.Text = "Robots.txt";
			this.cbRobotsTxt.UseVisualStyleBackColor = true;
			// 
			// cbSitemapXml
			// 
			this.cbSitemapXml.AutoSize = true;
			this.cbSitemapXml.Location = new System.Drawing.Point(478, 5);
			this.cbSitemapXml.Name = "cbSitemapXml";
			this.cbSitemapXml.Size = new System.Drawing.Size(111, 17);
			this.cbSitemapXml.TabIndex = 7;
			this.cbSitemapXml.Text = "Use Sitemap XML";
			this.cbSitemapXml.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(949, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Test button";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// numMaxFolderDepth
			// 
			this.numMaxFolderDepth.Location = new System.Drawing.Point(490, 29);
			this.numMaxFolderDepth.Name = "numMaxFolderDepth";
			this.numMaxFolderDepth.Size = new System.Drawing.Size(62, 20);
			this.numMaxFolderDepth.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(398, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Max folder depth:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(586, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Max num pages:";
			// 
			// numMaxNumPages
			// 
			this.numMaxNumPages.Location = new System.Drawing.Point(670, 29);
			this.numMaxNumPages.Name = "numMaxNumPages";
			this.numMaxNumPages.Size = new System.Drawing.Size(47, 20);
			this.numMaxNumPages.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(746, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Match pattern:";
			// 
			// txtMatchPattern
			// 
			this.txtMatchPattern.Location = new System.Drawing.Point(828, 5);
			this.txtMatchPattern.Name = "txtMatchPattern";
			this.txtMatchPattern.Size = new System.Drawing.Size(100, 20);
			this.txtMatchPattern.TabIndex = 14;
			// 
			// btnShowReport
			// 
			this.btnShowReport.Location = new System.Drawing.Point(814, 26);
			this.btnShowReport.Name = "btnShowReport";
			this.btnShowReport.Size = new System.Drawing.Size(75, 23);
			this.btnShowReport.TabIndex = 15;
			this.btnShowReport.Text = "Show report";
			this.btnShowReport.UseVisualStyleBackColor = true;
			this.btnShowReport.Visible = false;
			this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
			// 
			// cbExcludeExtHosts
			// 
			this.cbExcludeExtHosts.AutoSize = true;
			this.cbExcludeExtHosts.Location = new System.Drawing.Point(589, 5);
			this.cbExcludeExtHosts.Name = "cbExcludeExtHosts";
			this.cbExcludeExtHosts.Size = new System.Drawing.Size(122, 17);
			this.cbExcludeExtHosts.TabIndex = 16;
			this.cbExcludeExtHosts.Text = "Exclude all ext hosts";
			this.cbExcludeExtHosts.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1031, 24);
			this.menuStrip1.TabIndex = 17;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// cbOldReports
			// 
			this.cbOldReports.FormattingEnabled = true;
			this.cbOldReports.Location = new System.Drawing.Point(490, 54);
			this.cbOldReports.Name = "cbOldReports";
			this.cbOldReports.Size = new System.Drawing.Size(255, 21);
			this.cbOldReports.TabIndex = 18;
			this.cbOldReports.SelectedIndexChanged += new System.EventHandler(this.btnShowReport_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(15, 52);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 19;
			this.button2.Text = "NewDbSpider";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dataGridViewExtResources
			// 
			this.dataGridViewExtResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewExtResources.Location = new System.Drawing.Point(228, 105);
			this.dataGridViewExtResources.Name = "dataGridViewExtResources";
			this.dataGridViewExtResources.Size = new System.Drawing.Size(387, 106);
			this.dataGridViewExtResources.TabIndex = 20;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(228, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 13);
			this.label4.TabIndex = 21;
			this.label4.Text = "External links:";
			// 
			// dataGridViewIframes
			// 
			this.dataGridViewIframes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewIframes.Location = new System.Drawing.Point(632, 105);
			this.dataGridViewIframes.Name = "dataGridViewIframes";
			this.dataGridViewIframes.Size = new System.Drawing.Size(387, 106);
			this.dataGridViewIframes.TabIndex = 22;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(632, 86);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "Iframes";
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1031, 667);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dataGridViewIframes);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dataGridViewExtResources);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.cbOldReports);
			this.Controls.Add(this.cbExcludeExtHosts);
			this.Controls.Add(this.btnShowReport);
			this.Controls.Add(this.txtMatchPattern);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numMaxNumPages);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numMaxFolderDepth);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cbSitemapXml);
			this.Controls.Add(this.cbRobotsTxt);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.ResultDataGridView);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.Url);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.SizeChanged += new System.EventHandler(this.formResize);
			((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxFolderDepth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxNumPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtResources)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIframes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Url;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.DataGridView ResultDataGridView;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.CheckBox cbRobotsTxt;
		private System.Windows.Forms.CheckBox cbSitemapXml;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown numMaxFolderDepth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numMaxNumPages;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtMatchPattern;
		private System.Windows.Forms.Button btnShowReport;
		private System.Windows.Forms.CheckBox cbExcludeExtHosts;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ComboBox cbOldReports;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridView dataGridViewExtResources;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dataGridViewIframes;
		private System.Windows.Forms.Label label5;
	}
}