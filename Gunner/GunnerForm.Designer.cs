namespace Gunner
{
	partial class GunnerForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkLabelResultFolder = new System.Windows.Forms.LinkLabel();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.progressBarWork = new System.Windows.Forms.ProgressBar();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.buttonStart = new System.Windows.Forms.Button();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.tabPageUserAgent = new System.Windows.Forms.TabPage();
			this.labelUserAgent = new System.Windows.Forms.Label();
			this.textBoxUserAgent = new System.Windows.Forms.TextBox();
			this.tabPageAdvance = new System.Windows.Forms.TabPage();
			this.labelProxy = new System.Windows.Forms.Label();
			this.textBoxProxy = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelUrl = new System.Windows.Forms.Label();
			this.labelInterval = new System.Windows.Forms.Label();
			this.textBoxInterval = new System.Windows.Forms.TextBox();
			this.numericInterval = new System.Windows.Forms.NumericUpDown();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.numericRepeats = new System.Windows.Forms.NumericUpDown();
			this.labelRepeats = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			this.tabPageUserAgent.SuspendLayout();
			this.tabPageAdvance.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericInterval)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericRepeats)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkLabelResultFolder);
			this.groupBox1.Controls.Add(this.textBoxLog);
			this.groupBox1.Controls.Add(this.progressBarWork);
			this.groupBox1.Location = new System.Drawing.Point(499, 70);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(377, 404);
			this.groupBox1.TabIndex = 20;
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
			// progressBarWork
			// 
			this.progressBarWork.Location = new System.Drawing.Point(6, 375);
			this.progressBarWork.Name = "progressBarWork";
			this.progressBarWork.Size = new System.Drawing.Size(365, 23);
			this.progressBarWork.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageBasic);
			this.tabControl1.Controls.Add(this.tabPageUserAgent);
			this.tabControl1.Controls.Add(this.tabPageAdvance);
			this.tabControl1.Location = new System.Drawing.Point(23, 48);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(470, 534);
			this.tabControl1.TabIndex = 19;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.numericRepeats);
			this.tabPageBasic.Controls.Add(this.labelRepeats);
			this.tabPageBasic.Controls.Add(this.numericInterval);
			this.tabPageBasic.Controls.Add(this.labelInterval);
			this.tabPageBasic.Controls.Add(this.textBoxInterval);
			this.tabPageBasic.Controls.Add(this.labelUrl);
			this.tabPageBasic.Controls.Add(this.buttonCancel);
			this.tabPageBasic.Controls.Add(this.buttonStart);
			this.tabPageBasic.Controls.Add(this.textBoxUrl);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(462, 508);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(176, 458);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(123, 44);
			this.buttonStart.TabIndex = 2;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Location = new System.Drawing.Point(9, 264);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(406, 20);
			this.textBoxUrl.TabIndex = 3;
			this.textBoxUrl.Text = "https://seb.se/";
			// 
			// tabPageUserAgent
			// 
			this.tabPageUserAgent.Controls.Add(this.labelUserAgent);
			this.tabPageUserAgent.Controls.Add(this.textBoxUserAgent);
			this.tabPageUserAgent.Location = new System.Drawing.Point(4, 22);
			this.tabPageUserAgent.Name = "tabPageUserAgent";
			this.tabPageUserAgent.Size = new System.Drawing.Size(462, 508);
			this.tabPageUserAgent.TabIndex = 3;
			this.tabPageUserAgent.Text = "UserAgent";
			this.tabPageUserAgent.UseVisualStyleBackColor = true;
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
			this.tabPageAdvance.Controls.Add(this.labelProxy);
			this.tabPageAdvance.Controls.Add(this.textBoxProxy);
			this.tabPageAdvance.Location = new System.Drawing.Point(4, 22);
			this.tabPageAdvance.Name = "tabPageAdvance";
			this.tabPageAdvance.Size = new System.Drawing.Size(462, 508);
			this.tabPageAdvance.TabIndex = 2;
			this.tabPageAdvance.Text = "Advance";
			this.tabPageAdvance.UseVisualStyleBackColor = true;
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
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(305, 458);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(123, 44);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelUrl
			// 
			this.labelUrl.AutoSize = true;
			this.labelUrl.Location = new System.Drawing.Point(9, 245);
			this.labelUrl.Name = "labelUrl";
			this.labelUrl.Size = new System.Drawing.Size(23, 13);
			this.labelUrl.TabIndex = 5;
			this.labelUrl.Text = "Url:";
			// 
			// labelInterval
			// 
			this.labelInterval.AutoSize = true;
			this.labelInterval.Location = new System.Drawing.Point(9, 291);
			this.labelInterval.Name = "labelInterval";
			this.labelInterval.Size = new System.Drawing.Size(45, 13);
			this.labelInterval.TabIndex = 7;
			this.labelInterval.Text = "Interval:";
			// 
			// textBoxInterval
			// 
			this.textBoxInterval.Location = new System.Drawing.Point(9, 310);
			this.textBoxInterval.Name = "textBoxInterval";
			this.textBoxInterval.Size = new System.Drawing.Size(406, 20);
			this.textBoxInterval.TabIndex = 6;
			this.textBoxInterval.Text = "10";
			// 
			// numericInterval
			// 
			this.numericInterval.Location = new System.Drawing.Point(9, 336);
			this.numericInterval.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			this.numericInterval.Name = "numericInterval";
			this.numericInterval.Size = new System.Drawing.Size(120, 20);
			this.numericInterval.TabIndex = 8;
			this.numericInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1100, 24);
			this.menuStrip1.TabIndex = 21;
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
			this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadSettingsToolStripMenuItem.Text = "Load settings";
			// 
			// saveSettingsToolStripMenuItem
			// 
			this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
			this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.saveSettingsToolStripMenuItem.Text = "Save settings";
			// 
			// numericRepeats
			// 
			this.numericRepeats.Location = new System.Drawing.Point(9, 388);
			this.numericRepeats.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			this.numericRepeats.Name = "numericRepeats";
			this.numericRepeats.Size = new System.Drawing.Size(120, 20);
			this.numericRepeats.TabIndex = 10;
			this.numericRepeats.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// labelRepeats
			// 
			this.labelRepeats.AutoSize = true;
			this.labelRepeats.Location = new System.Drawing.Point(9, 372);
			this.labelRepeats.Name = "labelRepeats";
			this.labelRepeats.Size = new System.Drawing.Size(50, 13);
			this.labelRepeats.TabIndex = 9;
			this.labelRepeats.Text = "Repeats:";
			// 
			// GunnerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1100, 705);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "GunnerForm";
			this.Text = "Gunner";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			this.tabPageUserAgent.ResumeLayout(false);
			this.tabPageUserAgent.PerformLayout();
			this.tabPageAdvance.ResumeLayout(false);
			this.tabPageAdvance.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericInterval)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericRepeats)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel linkLabelResultFolder;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.ProgressBar progressBarWork;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageBasic;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.TextBox textBoxUrl;
		private System.Windows.Forms.TabPage tabPageUserAgent;
		private System.Windows.Forms.Label labelUserAgent;
		private System.Windows.Forms.TextBox textBoxUserAgent;
		private System.Windows.Forms.TabPage tabPageAdvance;
		private System.Windows.Forms.Label labelProxy;
		private System.Windows.Forms.TextBox textBoxProxy;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelUrl;
		private System.Windows.Forms.NumericUpDown numericInterval;
		private System.Windows.Forms.Label labelInterval;
		private System.Windows.Forms.TextBox textBoxInterval;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
		private System.Windows.Forms.NumericUpDown numericRepeats;
		private System.Windows.Forms.Label labelRepeats;
	}
}

