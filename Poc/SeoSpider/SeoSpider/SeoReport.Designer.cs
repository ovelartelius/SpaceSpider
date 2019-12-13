namespace SeoSpider
{
	partial class SeoReport
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
			this.label1 = new System.Windows.Forms.Label();
			this.labNumberOfPages = new System.Windows.Forms.Label();
			this.txtReportText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of pages:";
			// 
			// labNumberOfPages
			// 
			this.labNumberOfPages.AutoSize = true;
			this.labNumberOfPages.Location = new System.Drawing.Point(111, 13);
			this.labNumberOfPages.Name = "labNumberOfPages";
			this.labNumberOfPages.Size = new System.Drawing.Size(13, 13);
			this.labNumberOfPages.TabIndex = 1;
			this.labNumberOfPages.Text = "0";
			// 
			// txtReportText
			// 
			this.txtReportText.Location = new System.Drawing.Point(16, 30);
			this.txtReportText.Multiline = true;
			this.txtReportText.Name = "txtReportText";
			this.txtReportText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtReportText.Size = new System.Drawing.Size(620, 435);
			this.txtReportText.TabIndex = 2;
			// 
			// SeoReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 477);
			this.Controls.Add(this.txtReportText);
			this.Controls.Add(this.labNumberOfPages);
			this.Controls.Add(this.label1);
			this.Name = "SeoReport";
			this.Text = "SeoReport";
			this.Load += new System.EventHandler(this.SeoReport_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labNumberOfPages;
		private System.Windows.Forms.TextBox txtReportText;
	}
}