using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Crawler.Models;
using Spider;
using Spider.Extensions;
using Spider.Models;

namespace Crawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Spider.Settings.LoadRegistrySetting("Crawler.SettingsFile")))
            {
                var settingsFile = Spider.Settings.LoadRegistrySetting("Crawler.SettingsFile");
                var settings = Spider.Settings.LoadSettings<CrawlerSettings>(settingsFile);
                Console.WriteLine($"Autoload latest settings {settingsFile}");
                PopulateFormWithSettingsValues(settings);
            }

        }

        private void openSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            Console.WriteLine(openSettingsDialog.FileName);

            var settings = Spider.Settings.LoadSettings<CrawlerSettings>(openSettingsDialog.FileName);
			PopulateFormWithSettingsValues(settings);

			var folder = new FileInfo(openSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("Crawler.SettingsFolder", folder);
            Spider.Settings.SaveRegistrySetting("Crawler.SettingsFile", openSettingsDialog.FileName);
        }

        private void saveSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            var settings = new CrawlerSettings();
			PopulateSettingsWithFormValues(settings);

			Spider.Settings.SaveSettings(saveSettingsDialog.FileName, settings);

			var folder = new FileInfo(saveSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("Crawler.SettingsFolder", folder);

		}

		private void PopulateFormWithSettingsValues(CrawlerSettings settings)
		{
            textBoxUrl.Text = settings.Url;
            textBoxUserAgent.Text = settings.UserAgent;
            textBoxIndexDirectory.Text = settings.IndexFolder;
        }

		private void PopulateSettingsWithFormValues(CrawlerSettings settings)
		{
			settings.Url = textBoxUrl.Text;
            settings.UserAgent = textBoxUserAgent.Text;
            settings.IndexFolder = textBoxIndexDirectory.Text;
        }

		private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("Crawler.SettingsFolder");
			openSettingsDialog.ShowDialog();

		}

		private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("Crawler.SettingsFolder");
			saveSettingsDialog.ShowDialog();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var spider = new Spider.Spider();
            var pageResult =  spider.CheckUrl(textBoxUrl.Text, null, textBoxUserAgent.Text);


            //var serializer = new JsonSerializer();
            textBoxResult.Text = pageResult.ToJson();
            var folder = $"{textBoxIndexDirectory.Text}";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var filePath = $"{folder}\\{pageResult.Uri.AbsoluteUri.ToFileSafeName()}.json";
            Json.SaveAsFile<PageResult>(filePath, pageResult);

            button1.Enabled = true;
        }

        private void buttonIndexDirectory_Click(object sender, EventArgs e)
        {
            folderIndexDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("Crawler.IndexFolder");
            if (folderIndexDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxIndexDirectory.Text = folderIndexDialog.SelectedPath;
                Spider.Settings.SaveRegistrySetting("Crawler.IndexFolder", folderIndexDialog.SelectedPath);
            }
        }
    }
}
