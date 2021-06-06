using System;
using System.IO;
using System.Windows.Forms;
using Spider;
using Spider.Models;

namespace CreateJsonFromUrl
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var pageUrl = textBoxSourceUrl.Text;
			var saveToFile = textBoxSaveToFile.Text;

			var spider = new Spider.Spider();
			var checkUrlManifest = new CheckUrlManifest
			{
				Url = pageUrl,
				UserAgent = "SpaceSpider"
			};

			var result = spider.CheckUrl(checkUrlManifest);

			var jsonResult = Json.ToJson(result);
			File.WriteAllText(saveToFile, jsonResult);

			MessageBox.Show("Done");
		}
	}
}
