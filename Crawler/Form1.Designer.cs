namespace Crawler
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
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.labelUrl = new System.Windows.Forms.Label();
            this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelUserAgent = new System.Windows.Forms.Label();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonIndexDirectory = new System.Windows.Forms.Button();
            this.textBoxIndexDirectory = new System.Windows.Forms.TextBox();
            this.labelIndexFolder = new System.Windows.Forms.Label();
            this.folderIndexDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1414, 527);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(1041, 530);
            this.textBoxUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(367, 22);
            this.textBoxUrl.TabIndex = 1;
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(1041, 507);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(30, 17);
            this.labelUrl.TabIndex = 2;
            this.labelUrl.Text = "Url:";
            // 
            // openSettingsDialog
            // 
            this.openSettingsDialog.DefaultExt = "json";
            this.openSettingsDialog.Filter = "json files (*.json)|*.json";
            this.openSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openSettingsDialog_FileOk);
            // 
            // saveSettingsDialog
            // 
            this.saveSettingsDialog.DefaultExt = "json";
            this.saveSettingsDialog.FileName = "*.json";
            this.saveSettingsDialog.Filter = "json files (*.json)|*.json";
            this.saveSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveSettingsDialog_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1585, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.loadSettingsToolStripMenuItem.Text = "Load settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.saveSettingsToolStripMenuItem.Text = "Save settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(1041, 562);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(83, 17);
            this.labelUserAgent.TabIndex = 7;
            this.labelUserAgent.Text = "User Agent:";
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(1041, 586);
            this.textBoxUserAgent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(367, 22);
            this.textBoxUserAgent.TabIndex = 6;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(1008, 42);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(517, 419);
            this.textBoxResult.TabIndex = 8;
            // 
            // buttonIndexDirectory
            // 
            this.buttonIndexDirectory.Location = new System.Drawing.Point(1510, 636);
            this.buttonIndexDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonIndexDirectory.Name = "buttonIndexDirectory";
            this.buttonIndexDirectory.Size = new System.Drawing.Size(45, 26);
            this.buttonIndexDirectory.TabIndex = 20;
            this.buttonIndexDirectory.Text = "...";
            this.buttonIndexDirectory.UseVisualStyleBackColor = true;
            this.buttonIndexDirectory.Click += new System.EventHandler(this.buttonIndexDirectory_Click);
            // 
            // textBoxIndexDirectory
            // 
            this.textBoxIndexDirectory.Location = new System.Drawing.Point(1041, 637);
            this.textBoxIndexDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIndexDirectory.Name = "textBoxIndexDirectory";
            this.textBoxIndexDirectory.Size = new System.Drawing.Size(460, 22);
            this.textBoxIndexDirectory.TabIndex = 19;
            // 
            // labelIndexFolder
            // 
            this.labelIndexFolder.AutoSize = true;
            this.labelIndexFolder.Location = new System.Drawing.Point(1037, 618);
            this.labelIndexFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIndexFolder.Name = "labelIndexFolder";
            this.labelIndexFolder.Size = new System.Drawing.Size(104, 17);
            this.labelIndexFolder.TabIndex = 18;
            this.labelIndexFolder.Text = "Index directory:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1585, 767);
            this.Controls.Add(this.buttonIndexDirectory);
            this.Controls.Add(this.textBoxIndexDirectory);
            this.Controls.Add(this.labelIndexFolder);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.labelUserAgent);
            this.Controls.Add(this.textBoxUserAgent);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Crawler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.OpenFileDialog openSettingsDialog;
        private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.Label labelUserAgent;
        private System.Windows.Forms.TextBox textBoxUserAgent;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonIndexDirectory;
        private System.Windows.Forms.TextBox textBoxIndexDirectory;
        private System.Windows.Forms.Label labelIndexFolder;
        private System.Windows.Forms.FolderBrowserDialog folderIndexDialog;
    }
}

