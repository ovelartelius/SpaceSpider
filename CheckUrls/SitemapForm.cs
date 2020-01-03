using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CheckRequestedUrls.Models;
using CheckRequestedUrls.Models.Csv;
using Spider.Extensions;
using Spider.Models;

namespace CheckRequestedUrls
{
    public partial class SitemapForm : Form
    {
		public SitemapForm()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Spider.Settings.LoadRegistrySetting("CheckUrl.SettingsFile")))
            {
                var settingsFile = Spider.Settings.LoadRegistrySetting("CheckUrl.SettingsFile");
                var settings = Spider.Settings.LoadSettings<CheckUrlsSettings>(settingsFile);
                Console.WriteLine($"Autoload latest settings {settingsFile}");
                PopulateFormWithSettingsValues(settings);
            }
        }

        private void buttonStartWork_Click(object sender, EventArgs e)
        {
			linkLabelResultFolder.Text = string.Empty;
			LogReset();
			buttonStartWork.Enabled = false;
			_workLoad = PopulateWorkLoad();

            var settings = new CheckUrlsSettings();
            PopulateSettingsWithFormValues(settings);

            if (settings.CheckSiteDomainBeforeStart)
            {
                var spider = new Spider.Spider();
                var validationResult = spider.ValidateUrlOnSite(settings.NewSiteDomain, settings.UserAgent);
                if (!validationResult.Result)
                {
                    Log(validationResult.ErrorMessage);
                    Log($"Can not start the job. New site domain {settings.NewSiteDomain} is not working.");
                    buttonStartWork.Enabled = true;
                    return;
                }
            }

            backgroundWorkerLoadCsv.RunWorkerAsync(_workLoad.SitemapUrl);
        }

		private WorkLoad PopulateWorkLoad()
		{
			var workLoad = new WorkLoad
			{
				//CsvFile = textBoxCsvFile.Text,
				NewSiteDomain = textBoxNewSiteDomain.Text,
				UserAgent = textBoxUserAgent.Text,
				Proxy = textBoxProxy.Text,
				IgnorePatterns = new List<string>(),
				//SearchUrl = textBoxSearchUrl.Text,
				OutputDirectory = textBoxOutputDirectory.Text,
				//RunOverVpn = checkBoxOverVpn.Checked,
				//IgnoreSearch = checkBoxIgnoreSearch.Checked,
				CheckDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked,

                SitemapUrl = textBoxSitemapUrl.Text
			};
            workLoad.IgnorePatterns = textBoxIgnorePatterns.Text.SplitToList();
            var settings = new CheckUrlsSettings();
            PopulateSettingsWithFormValues(settings);
            workLoad.Settings = settings;


            return workLoad;
		}

        #region BackgroundWorkerUrlCheck

        private void backgroundWorkerUrlCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            var workLoad = e.Argument as WorkLoad;
            
            var spider = new Spider.Spider();

            backgroundWorkerUrlCheck.ReportProgress(-1, workLoad.Urls.Count);

            var pageLinks = new List<CheckUrlResult>();
            //var newSiteUri = new Uri(workLoad.NewSiteDomain);

            var handledUrlList = new List<string>();

            var i = 0;
            foreach (var url in workLoad.Urls)
            {
                Console.WriteLine($"Url-{i}");
                // Check duplicates
                if (handledUrlList.Contains(url))
                {
                    continue;
                }
                else
                {
                    handledUrlList.Add(url);
                }

                try
                {
                    var newUrl = url;

                    if (!string.IsNullOrEmpty(workLoad.NewSiteDomain))
                    {
                        newUrl = url.SwapHostname(workLoad.NewSiteDomain);
                    }

                    //Log($"Convert {value.Url} to {newUrl}");
                    var checkUrlResult = new CheckUrlResult { Url = newUrl };

                    if (spider.ShouldUrlBeIgnored(newUrl, workLoad.IgnorePatterns))
                    {
                        checkUrlResult.StatusCode = HttpStatusCode.SeeOther;
                        checkUrlResult.Ignored = true;
                    }
                    else
                    {
                        var checkUrlManifest = new CheckUrlManifest();
                        checkUrlManifest.Url = newUrl;
                        checkUrlManifest.SourceUrls = new List<string>();
                        checkUrlManifest.UserAgent = workLoad.UserAgent;
                        checkUrlResult = spider.CheckUrl(checkUrlManifest);
                    }

                    pageLinks.Add(checkUrlResult);

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.StartsWith("A connection attempt failed because the connected party did not properly respond after a period of time"))
                        {
                            throw new ApplicationException("TimedOut against search!!!", ex);
                        }
                    }
                    var spiderPageLink = new CheckUrlResult { Url = url, Erroneous = true, Description = ex.Message };
                    pageLinks.Add(spiderPageLink);
                }
                //progressBarWork.PerformStep();
                backgroundWorkerUrlCheck.ReportProgress(i);
                i++;
            }


            //Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            e.Result = pageLinks;
        }

        private void backgroundWorkerUrlCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            //progressBarWork.Value = e.ProgressPercentage;
            //progressBarWork.PerformStep();

            if (e.ProgressPercentage == -1)
                progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            else
                progressBarWork.Value = e.ProgressPercentage;

            // Set the text.
            //this.Text = e.ProgressPercentage.ToString();
        }

        private void SaveToExcel(string dateTimeString, string extraFileName, StringBuilder sb)
        {
            string filePath = $"{_workLoad.OutputDirectory}\\Result_{dateTimeString}_{extraFileName}.csv";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //this code section write stringbuilder content to physical text file.
            using (StreamWriter swriter = File.CreateText(filePath))
            {
                swriter.Write(sb.ToString());
            }
        }

        private void backgroundWorkerUrlCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            _workLoad.SpiderPageLinks = e.Result as List<CheckUrlResult>;

            var listOf200Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOf400Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.NotFound).OrderBy(x => x.Url).ToList();

            var listOf400Missing = new List<CheckUrlResult>();
            var listOf400NotMissing = new List<CheckUrlResult>();
            if (_workLoad.IgnoreSearch)
            {
                listOf400Missing = listOf400Response.OrderBy(x => x.Url).ToList();
            }
            else
            {
                // Av de sidor som svara 404 så har dessa efterfrågats utav någon de senaste x dagarna.
                listOf400Missing = listOf400Response.Where(x => x.HistoricHits != 0).OrderBy(x => x.Url).ToList();

                // Av de sidor som svara 404 så har dessa INTE efterfrågats utav någon de senaste x dagarna.
                listOf400NotMissing = listOf400Response.Where(x => x.HistoricHits == 0).OrderByDescending(x => x.HistoricHits).ToList();
            }

            var listOf500Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.InternalServerError || x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOf301Response = _workLoad.SpiderPageLinks.Where(x => (x.StatusCode == HttpStatusCode.MovedPermanently || x.StatusCode == HttpStatusCode.Found) && !x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOfOthers = _workLoad.SpiderPageLinks.Where(x => x.StatusCode != HttpStatusCode.MovedPermanently && x.StatusCode != HttpStatusCode.Found && x.StatusCode != HttpStatusCode.InternalServerError && x.StatusCode != HttpStatusCode.NotFound && x.StatusCode != HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOfIgnored = _workLoad.SpiderPageLinks.Where(x => x.Ignored).OrderBy(x => x.Url).ToList();

            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");

            _workLoad.OutputDirectory = _workLoad.OutputDirectory + "\\" + dateTimeString;
            if (!Directory.Exists(_workLoad.OutputDirectory))
            {
                Directory.CreateDirectory(_workLoad.OutputDirectory);
            }

            var sb = new StringBuilder();

            sb.AppendLine($"CSV file - {_workLoad.CsvFile} {dateTimeString}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Working URLs (200):");
            var sb200 = new StringBuilder();
            foreach (var item in listOf200Response)
            {
                sb.AppendLine(item.Url);
                sb200.AppendLine(item.Url);
            }
			if (listOf200Response.Any())
			{
				Log($"Found {listOf200Response.Count} 200 URLs.");
			}
			SaveToExcel(dateTimeString, "200", sb200);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Redirected URLs (301):");
            var sb301 = new StringBuilder();
            foreach (var item in listOf301Response)
            {
                sb.AppendLine(item.Url + " => " + item.Redirect);
                sb301.AppendLine(item.Url);
            }
			if (listOf301Response.Any())
			{
				Log($"Found {listOf301Response.Count} 301 URLs.");
			}
			SaveToExcel(dateTimeString, "301", sb301);

            if (_workLoad.IgnoreSearch)
            {
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("Requested missing URLs (404):");
                var sb404Missing = new StringBuilder();
                foreach (var item in listOf400Missing)
                {
                    sb.AppendLine(item.Url);
                    sb404Missing.AppendLine(item.Url);
                }
				if (listOf400Missing.Any())
				{
					Log($"Found {listOf400Missing.Count} 404 URLs.");
				}
				SaveToExcel(dateTimeString, "404Missing", sb404Missing);
            }
            else
            {
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("Requested missing URLs (404): (URLs are requested in past and may need to be handled)");
                var sb404Missing = new StringBuilder();
                foreach (var item in listOf400Missing)
                {
                    sb.AppendLine(item.Url);
                    sb404Missing.AppendLine(item.Url);
                }
				if (listOf400Missing.Any())
				{
					Log($"Found {listOf400Missing.Count} 404 URLs.");
				}
				SaveToExcel(dateTimeString, "404Missing", sb404Missing);

                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("Not found URLs (404): (URLs can be ignored)");
                var sb404 = new StringBuilder();
                foreach (var item in listOf400NotMissing)
                {
                    sb.AppendLine(item.Url);
                    sb404.AppendLine(item.Url);
                }
				if (listOf400NotMissing.Any())
				{
					Log($"Found {listOf400NotMissing.Count} 404 URLs that can be ignored.");
				}
				SaveToExcel(dateTimeString, "404", sb404);
            }

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Errors (500):");
            var sb500 = new StringBuilder();
            foreach (var item in listOf500Response)
            {
                sb.AppendLine(item.Url + " => " + item.Description);
                sb500.AppendLine(item.Url);
            }
			if (listOf500Response.Any())
			{
				Log($"Found {listOf500Response.Count} 500 URLs.");
			}
			SaveToExcel(dateTimeString, "500", sb500);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Ignored:");
            var sbIgnored = new StringBuilder();
            foreach (var item in listOfIgnored)
            {
                sb.AppendLine(item.Url);
                sbIgnored.AppendLine(item.Url);
            }
			if (listOfIgnored.Any())
			{
				Log($"Found {listOfIgnored.Count} Ignored URLs.");
			}
			SaveToExcel(dateTimeString, "Ignored", sbIgnored);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Others:");
            var sbOthers = new StringBuilder();
            foreach (var item in listOfOthers)
            {
                sb.AppendLine(item.Url);
                sbOthers.AppendLine(item.Url);
            }
			if (listOfOthers.Any())
			{
				Log($"Found {listOfOthers.Count} 'Others' URLs.");
			}
            SaveToExcel(dateTimeString, "Others", sbOthers);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Rawdata:");
            sb.AppendLine($"Url,StatusCode,Description,HistoricHits,Erroneous");
            var sbRawData = new StringBuilder();
            sbRawData.AppendLine($"Url;StatusCode;Description;HistoricHits;Erroneous");
            foreach (var item in _workLoad.SpiderPageLinks)
            {
                sb.AppendLine($"{item.Url},{item.StatusCode},{item.Description},{item.HistoricHits},{item.Erroneous}");
                sbRawData.AppendLine($"{item.Url};{item.StatusCode};{item.Description};{item.HistoricHits};{item.Erroneous}");
            }
			SaveToExcel(dateTimeString, "RawData", sbRawData);

			CreateResultFile(sb, dateTimeString);

			linkLabelResultFolder.Text = _workLoad.OutputDirectory;
			Log("The end!");
			buttonStartWork.Enabled = true;
			progressBarWork.Value = 0;
		}

		private void CreateResultFile(StringBuilder sb, string dateTime)
		{
			string filePath = $"{_workLoad.OutputDirectory}\\Result_{dateTime}.txt";

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				Log($"Deleted old file {filePath}");
			}

			//this code section write stringbuilder content to physical text file.
			using (StreamWriter swriter = File.CreateText(filePath))
			{
				swriter.Write(sb.ToString());
			}
			Log($"Created result file {filePath}");
		}
        #endregion

        #region BackgroundWorkerLoadCsv
        private void backgroundWorkerLoadCsv_DoWork(object sender, DoWorkEventArgs e)
        {
            var sitemapUrl = e.Argument as string;

            var urls = Spider.Sitemap.GetSitemapUrls(sitemapUrl);

            e.Result = urls;
        }

        private void backgroundWorkerLoadCsv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            _workLoad.Urls = e.Result as List<string>;

            // Remove all empty
            _workLoad.Urls = _workLoad.Urls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            // Make them unique
            _workLoad.Urls = _workLoad.Urls.Distinct().ToList();
            //
            // Will display "6 3" in title Text (in this example)
            //
            Log($"Found {_workLoad.Urls.Count().ToString()} URLs in the sitemap.");

            StartUrlCheck();
        }

        private void StartUrlCheck()
        {
            if (_workLoad.Urls.Count() != 0 && !backgroundWorkerUrlCheck.IsBusy)
            {
                progressBarWork.Maximum = _workLoad.Urls.Count;
                progressBarWork.Value = 0;

                backgroundWorkerUrlCheck.RunWorkerAsync(_workLoad);
            }
            else
            {
                Log($"BackgroundWorker is running.");
            }
        }

        #endregion

        protected WorkLoad _workLoad = new WorkLoad();

        private void LogReset()
        {
            textBoxLog.Text = string.Empty;
        }

        private void Log(string message)
        {
            textBoxLog.Text = message + "\r\n" + textBoxLog.Text;
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			openSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CheckUrl.SettingsFolder");
			openSettingsDialog.ShowDialog();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			saveSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CheckUrl.SettingsFolder");
			saveSettingsDialog.ShowDialog();
        }

        private void openSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
			Console.WriteLine(openSettingsDialog.FileName);

            var settings = Spider.Settings.LoadSettings<CheckUrlsSettings>(openSettingsDialog.FileName);
			PopulateFormWithSettingsValues(settings);

			var folder = new FileInfo(openSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CheckUrl.SettingsFolder", folder);
            Spider.Settings.SaveRegistrySetting("CheckUrl.SettingsFile", openSettingsDialog.FileName);
        }

        private void saveSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            var settings = new CheckUrlsSettings();
			PopulateSettingsWithFormValues(settings);

			Spider.Settings.SaveSettings(saveSettingsDialog.FileName, settings);

			var folder = new FileInfo(saveSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CheckUrl.SettingsFolder", folder);
		}

		private void PopulateFormWithSettingsValues(CheckUrlsSettings settings)
		{
			//textBoxCsvFile.Text = settings.CsvFilePath;
            //checkBoxFirstRowContainsTitle.Checked = settings.FirstRowContainsTitle;
            //textBoxCsvFileSeperator.Text = settings.CsvFileSeperator;
			textBoxIgnorePatterns.Text = settings.IgnorePatterns;
			//textBoxSearchUrl.Text = settings.SearchUrl;
			//checkBoxOverVpn.Checked = settings.RunningOverVpn;
			//checkBoxIgnoreSearch.Checked = settings.IgnoreSearch;
			checkBoxCheckDomainBeforeStart.Checked = settings.CheckSiteDomainBeforeStart;
			textBoxNewSiteDomain.Text = settings.NewSiteDomain;
			textBoxProxy.Text = settings.Proxy;
			textBoxUserAgent.Text = settings.UserAgent;
			textBoxOutputDirectory.Text = settings.OutputDirectory;

            textBoxSitemapUrl.Text = settings.SitemapUrl;
        }

		private void PopulateSettingsWithFormValues(CheckUrlsSettings settings)
		{
			//settings.CsvFilePath = textBoxCsvFile.Text;
            //settings.FirstRowContainsTitle = checkBoxFirstRowContainsTitle.Checked;
            //settings.CsvFileSeperator = textBoxCsvFileSeperator.Text;
            settings.IgnorePatterns = textBoxIgnorePatterns.Text;
			//settings.SearchUrl = textBoxSearchUrl.Text;
			//settings.RunningOverVpn = checkBoxOverVpn.Checked;
			//settings.IgnoreSearch = checkBoxIgnoreSearch.Checked;
			settings.CheckSiteDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked;
			settings.NewSiteDomain = textBoxNewSiteDomain.Text;
			settings.Proxy = textBoxProxy.Text;
			settings.UserAgent = textBoxUserAgent.Text;
			settings.OutputDirectory = textBoxOutputDirectory.Text;

            settings.SitemapUrl = textBoxSitemapUrl.Text;
        }

        private void buttonOutputDirectory_Click(object sender, EventArgs e)
		{
            folderOutputDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("CheckUrl.OutputFolder");
            if (folderOutputDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputDirectory.Text = folderOutputDialog.SelectedPath;
                Spider.Settings.SaveRegistrySetting("CheckUrl.OutputFolder", folderOutputDialog.SelectedPath);
            }
        }

		private void groupBox1_Enter(object sender, EventArgs e)
		{
			var folderName = linkLabelResultFolder.Text;
			Process.Start(folderName);
		}

        private void textBoxOutputDirectory_TextChanged(object sender, EventArgs e)
        {

        }

        private void cSVFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new CsvForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Show();
            this.Hide();

        }

        private void SitemapForm_Load(object sender, EventArgs e)
        {

        }
    }
}
