
namespace CreateJsonFromUrl
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
			this.textBoxSourceUrl = new System.Windows.Forms.TextBox();
			this.textBoxSaveToFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(196, 232);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(298, 88);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBoxSourceUrl
			// 
			this.textBoxSourceUrl.Location = new System.Drawing.Point(213, 61);
			this.textBoxSourceUrl.Name = "textBoxSourceUrl";
			this.textBoxSourceUrl.Size = new System.Drawing.Size(280, 20);
			this.textBoxSourceUrl.TabIndex = 1;
			this.textBoxSourceUrl.Text = "https://seb.se/privat/lana/privatlan-och-krediter/privatlan-enkla-lanet/hur-mycke" +
    "t-kostar-enkla-lanet";
			// 
			// textBoxSaveToFile
			// 
			this.textBoxSaveToFile.Location = new System.Drawing.Point(213, 143);
			this.textBoxSaveToFile.Name = "textBoxSaveToFile";
			this.textBoxSaveToFile.Size = new System.Drawing.Size(280, 20);
			this.textBoxSaveToFile.TabIndex = 2;
			this.textBoxSaveToFile.Text = "E:\\dev\\SpaceSpider\\Spider.Tests\\httpssebseiframepage.json";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(212, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Source URL:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(214, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Save to file:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxSaveToFile);
			this.Controls.Add(this.textBoxSourceUrl);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBoxSourceUrl;
		private System.Windows.Forms.TextBox textBoxSaveToFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

