namespace SeoSpider.AgiltyTest
{
	partial class AgilityForm
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
			this.textBoxSiteUrl = new System.Windows.Forms.TextBox();
			this.labelStartUrl = new System.Windows.Forms.Label();
			this.buttonStart = new System.Windows.Forms.Button();
			this.labelPagesFound = new System.Windows.Forms.Label();
			this.labelPagesHandled = new System.Windows.Forms.Label();
			this.labelTimer = new System.Windows.Forms.Label();
			this.labelTimeLeft = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxSiteUrl
			// 
			this.textBoxSiteUrl.Location = new System.Drawing.Point(32, 27);
			this.textBoxSiteUrl.Name = "textBoxSiteUrl";
			this.textBoxSiteUrl.Size = new System.Drawing.Size(265, 20);
			this.textBoxSiteUrl.TabIndex = 0;
			this.textBoxSiteUrl.Text = "http://comprend.com";
			// 
			// labelStartUrl
			// 
			this.labelStartUrl.AutoSize = true;
			this.labelStartUrl.Location = new System.Drawing.Point(32, 13);
			this.labelStartUrl.Name = "labelStartUrl";
			this.labelStartUrl.Size = new System.Drawing.Size(53, 13);
			this.labelStartUrl.TabIndex = 1;
			this.labelStartUrl.Text = "Site URL:";
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(304, 23);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(75, 23);
			this.buttonStart.TabIndex = 2;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// labelPagesFound
			// 
			this.labelPagesFound.AutoSize = true;
			this.labelPagesFound.Location = new System.Drawing.Point(32, 54);
			this.labelPagesFound.Name = "labelPagesFound";
			this.labelPagesFound.Size = new System.Drawing.Size(79, 13);
			this.labelPagesFound.TabIndex = 3;
			this.labelPagesFound.Text = "Pages found: 0";
			// 
			// labelPagesHandled
			// 
			this.labelPagesHandled.AutoSize = true;
			this.labelPagesHandled.Location = new System.Drawing.Point(32, 78);
			this.labelPagesHandled.Name = "labelPagesHandled";
			this.labelPagesHandled.Size = new System.Drawing.Size(90, 13);
			this.labelPagesHandled.TabIndex = 4;
			this.labelPagesHandled.Text = "Pages handled: 0";
			// 
			// labelTimer
			// 
			this.labelTimer.AutoSize = true;
			this.labelTimer.Location = new System.Drawing.Point(171, 54);
			this.labelTimer.Name = "labelTimer";
			this.labelTimer.Size = new System.Drawing.Size(42, 13);
			this.labelTimer.TabIndex = 5;
			this.labelTimer.Text = "Time: 0";
			// 
			// labelTimeLeft
			// 
			this.labelTimeLeft.AutoSize = true;
			this.labelTimeLeft.Location = new System.Drawing.Point(174, 77);
			this.labelTimeLeft.Name = "labelTimeLeft";
			this.labelTimeLeft.Size = new System.Drawing.Size(59, 13);
			this.labelTimeLeft.TabIndex = 6;
			this.labelTimeLeft.Text = "Time left: 0";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(32, 104);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(347, 23);
			this.progressBar.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(551, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(257, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "https://www.w3.org/TR/microdata/#content-models";
			// 
			// AgilityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1135, 362);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.labelTimeLeft);
			this.Controls.Add(this.labelTimer);
			this.Controls.Add(this.labelPagesHandled);
			this.Controls.Add(this.labelPagesFound);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.labelStartUrl);
			this.Controls.Add(this.textBoxSiteUrl);
			this.Name = "AgilityForm";
			this.Text = "AgilityForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxSiteUrl;
		private System.Windows.Forms.Label labelStartUrl;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Label labelPagesFound;
		private System.Windows.Forms.Label labelPagesHandled;
		private System.Windows.Forms.Label labelTimer;
		private System.Windows.Forms.Label labelTimeLeft;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label1;
	}
}