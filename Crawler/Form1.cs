using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Crawler.Models;
using Newtonsoft.Json;

namespace Crawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            Console.WriteLine(openSettingsDialog.FileName);

            var settings = Spider.Settings.LoadSettings<CrawlerSettings>(openSettingsDialog.FileName);
            textBoxUrl.Text = settings.Url;

        }

        private void buttonLoadSettings_Click(object sender, EventArgs e)
        {
            openSettingsDialog.InitialDirectory = @"c:\temp\SpaceSpider\Crawler";
            openSettingsDialog.ShowDialog();
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            saveSettingsDialog.InitialDirectory = @"c:\temp\SpaceSpider\Crawler";

            saveSettingsDialog.ShowDialog();

            

        }

        private void saveSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            var settings = new CrawlerSettings();
            settings.Url = textBoxUrl.Text;

            Spider.Settings.SaveSettings(saveSettingsDialog.FileName, settings);
            //using (StreamWriter file = File.CreateText(saveSettingsDialog.FileName))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    //serialize object directly into file stream
            //    serializer.Serialize(file, settings);
            //}
        }
    }
}
