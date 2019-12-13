namespace SeoSpider
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
			this.button1 = new System.Windows.Forms.Button();
			this.SiteUrl = new System.Windows.Forms.TextBox();
			this.PagesWith404Result = new System.Windows.Forms.TextBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(196, 36);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(212, 26);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start spidah";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// SiteUrl
			// 
			this.SiteUrl.Location = new System.Drawing.Point(198, 10);
			this.SiteUrl.Name = "SiteUrl";
			this.SiteUrl.Size = new System.Drawing.Size(210, 20);
			this.SiteUrl.TabIndex = 1;
			// 
			// PagesWith404Result
			// 
			this.PagesWith404Result.Location = new System.Drawing.Point(12, 541);
			this.PagesWith404Result.Multiline = true;
			this.PagesWith404Result.Name = "PagesWith404Result";
			this.PagesWith404Result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.PagesWith404Result.Size = new System.Drawing.Size(950, 181);
			this.PagesWith404Result.TabIndex = 6;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(15, 237);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(1220, 298);
			this.dataGridView1.TabIndex = 7;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1247, 787);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.PagesWith404Result);
			this.Controls.Add(this.SiteUrl);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox SiteUrl;
		private System.Windows.Forms.TextBox PagesWith404Result;
		private System.Windows.Forms.DataGridView dataGridView1;
	}
}

