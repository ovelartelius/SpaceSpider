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
using Spider.Models;

namespace CheckRequestedUrls
{
    public partial class Form1 : Form
    {
		private string _lastVisitedSettingsFolder;
		private string _lastVisitedDataFolder;

		public Form1()
        {
            InitializeComponent();

			_lastVisitedSettingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			_lastVisitedDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		}

        private void buttonStartWork_Click(object sender, EventArgs e)
        {
			linkLabelResultFolder.Text = string.Empty;
			textBoxLog.Text = string.Empty;
			buttonStartWork.Enabled = false;
			_workLoad = PopulateWorkLoad();

			if (!ValidateNewSiteDomain(_workLoad))
			{
				Log($"Can not start the job. New site domain {_workLoad.NewSiteDomain} is not working.");
				return;
			}

            backgroundWorkerLoadCsv.RunWorkerAsync(_workLoad);

        }

		private bool ValidateNewSiteDomain(WorkLoad workLoad)
		{
			var valid = false;
			var spider = new Spider.Spider();
			var newSiteUri = new Uri(_workLoad.NewSiteDomain);

			if (_workLoad.CheckDomainBeforeStart)
			{
				// First we check that the new site domain is working.
				var newSitePageLink = spider.CheckUrl(newSiteUri.AbsoluteUri, new List<string>(), _workLoad.UserAgent);

				if (newSitePageLink.StatusCode == System.Net.HttpStatusCode.OK)
				{
					valid = true;
				}
				else
				{
					Log($"Validate New site domain: {_workLoad.NewSiteDomain} - StatusCode:{newSitePageLink.StatusCode}, expected {System.Net.HttpStatusCode.OK}");
				}
			}
			else
			{
				valid = true;
			}

			return valid;
		}

		private WorkLoad PopulateWorkLoad()
		{
			var workLoad = new WorkLoad
			{
				CsvFile = textBoxCsvFile.Text,
				NewSiteDomain = textBoxNewSiteDomain.Text,
				UserAgent = textBoxUserAgent.Text,
				Proxy = textBoxProxy.Text,
				IgnorePatterns = new List<string>(),
				SearchUrl = textBoxSearchUrl.Text,
				OutputDirectory = textBoxOutputDirectory.Text,
				RunOverVpn = checkBoxOverVpn.Checked,
				IgnoreSearch = checkBoxIgnoreSearch.Checked,
				CheckDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked
			};

			var patterns = textBoxIgnorePatterns.Text.Split('\n');
			foreach (var pattern in patterns)
			{
				var patternValue = pattern;
				if (patternValue.Contains("\r"))
				{
					patternValue = patternValue.Replace("\r", "");
				}
				workLoad.IgnorePatterns.Add(patternValue);
			}

			return workLoad;
		}

        #region BackgroundWorkerUrlCheck
        private void backgroundWorkerUrlCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            var workLoad = e.Argument as WorkLoad;
            
            var spider = new Spider.Spider();
            //LogReset();

            //Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            //progressBarWork.Maximum = workLoad.Urls.Count;
            //progressBarWork.Value = 0;

            backgroundWorkerUrlCheck.ReportProgress(-1, workLoad.Urls.Count);

            var pageLinks = new List<SpiderPageLink>();
            var newSiteUri = new Uri(workLoad.NewSiteDomain);

            var handledUrlList = new List<string>();

            var i = 0;
            foreach (var url in workLoad.Urls)
            {
                Console.WriteLine($"Url-{i}");
                // Check duplicates
                //TODO: Add duplicate check
                if (handledUrlList.Contains(url))
                {
                    break;
                }
                else
                {
                    handledUrlList.Add(url);
                }

                try
                {
                    var oldUri = new Uri(url);
                    var newUrl = string.Empty;
                    if (newSiteUri.Port != 80 && newSiteUri.Port != 443)
                    {
                        newUrl = $"{newSiteUri.Scheme}://{newSiteUri.Host}:{newSiteUri.Port}{oldUri.PathAndQuery}";
                    }
                    else
                    {
                        newUrl = $"{newSiteUri.Scheme}://{newSiteUri.Host}{oldUri.PathAndQuery}";
                    }
                    

                    //Log($"Convert {value.Url} to {newUrl}");
                    SpiderPageLink spiderPageLink = new SpiderPageLink { Url = newUrl };


                    //var imagePatterns = spiderManifest.ImageContentTypeRegexPatternList;
                    foreach (var pattern in workLoad.IgnorePatterns)
                    {
                        if (Regex.IsMatch(newUrl, pattern))
                        {
                            // We should ignore to test this URL.
                            spiderPageLink.StatusCode = HttpStatusCode.SeeOther;
                            spiderPageLink.Ignored = true;
                            break;
                        }
                    }

                    if (!spiderPageLink.Ignored)
                    {
                        spiderPageLink = spider.CheckUrl(newUrl, new List<string>(), workLoad.UserAgent);

                        if (!workLoad.IgnoreSearch)
                        {
                            // Go a check against the search.
                            var oldUrlPathAndQuery = WebUtility.UrlEncode(oldUri.PathAndQuery);
                            dynamic something;
                            var searchUrl = string.Empty;
                            //var searchUrl = $"https://pws-search1-tst.sebank.se/rest/apps/stats/searchers/paths?hits=20&q={oldUrlPathAndQuery}&range!gte!@timestamp=now-30d/d";
                            if (workLoad.RunOverVpn)
                            {
                                searchUrl = "http://epicms-hostbucket.sebank.se/api/tempsearch/index?id=" + oldUrlPathAndQuery;
                                Console.WriteLine(searchUrl);
                            }
                            else
                            {
                                searchUrl = workLoad.SearchUrl.Replace("{oldUrlPathAndQuery}", oldUrlPathAndQuery);
                                Console.WriteLine(searchUrl);
                            }
                            something = spider.GetExternalData(searchUrl);

                            var totalHits = something.stats.totalHits;
                            if (totalHits != 0)
                            {
                                spiderPageLink.HistoricHits = totalHits;
                            }
                        }
                    }

                    pageLinks.Add(spiderPageLink);

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
                    var spiderPageLink = new SpiderPageLink { Url = url, Erroneous = true, Description = ex.Message };
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
            _workLoad.SpiderPageLinks = e.Result as List<SpiderPageLink>;

            var listOf200Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOf400Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.NotFound).OrderBy(x => x.Url).ToList();

            var listOf400Missing = new List<SpiderPageLink>();
            var listOf400NotMissing = new List<SpiderPageLink>();
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
                sb.AppendLine(item.Url + " => " + item.Description);
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
            var workLoad = e.Argument as WorkLoad;

            //Log($"Start load CSV file {workLoad.CsvFile}");
            List<CsvSimpleUrl> values = File.ReadAllLines(workLoad.CsvFile)
                                            .Skip(1)
                                            .Select(v => CsvSimpleUrl.FromCsv(v))
                                            .Distinct()
                                            .ToList();

            var urls = values.Select(x => x.Url).ToList();

            e.Result = urls;
        }

        //private void backgroundWorkerLoadCsv_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    // Change the value of the ProgressBar to the BackgroundWorker progress.
        //    //progressBarWork.Value = e.ProgressPercentage;
        //    progressBarWork.PerformStep();

        //    // Set the text.
        //    this.Text = e.ProgressPercentage.ToString();
        //}

        private void backgroundWorkerLoadCsv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            _workLoad.Urls = e.Result as List<string>;

            //this.Text = test.OneValue.ToString() + " " + test.TwoValue.ToString();

            // Remove all emty
            _workLoad.Urls = _workLoad.Urls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            // Make them unique
            _workLoad.Urls = _workLoad.Urls.Distinct().ToList();
            //
            // Will display "6 3" in title Text (in this example)
            //
            Log($"Found {_workLoad.Urls.Count().ToString()} URLs in the CSV file.");

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
			textBoxCsvFile.Text = settings.CsvFilePath;
			textBoxIgnorePatterns.Text = settings.IgnorePatterns;
			textBoxSearchUrl.Text = settings.SearchUrl;
			checkBoxOverVpn.Checked = settings.RunningOverVpn;
			checkBoxIgnoreSearch.Checked = settings.IgnoreSearch;
			checkBoxCheckDomainBeforeStart.Checked = settings.CheckSiteDomainBeforeStart;
			textBoxNewSiteDomain.Text = settings.NewSiteDomain;
			textBoxProxy.Text = settings.Proxy;
			textBoxUserAgent.Text = settings.UserAgent;
			textBoxOutputDirectory.Text = settings.OutputDirectory;
		}

		private void PopulateSettingsWithFormValues(CheckUrlsSettings settings)
		{
			settings.CsvFilePath = textBoxCsvFile.Text;
			settings.IgnorePatterns = textBoxIgnorePatterns.Text;
			settings.SearchUrl = textBoxSearchUrl.Text;
			settings.RunningOverVpn = checkBoxOverVpn.Checked;
			settings.IgnoreSearch = checkBoxIgnoreSearch.Checked;
			settings.CheckSiteDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked;
			settings.NewSiteDomain = textBoxNewSiteDomain.Text;
			settings.Proxy = textBoxProxy.Text;
			settings.UserAgent = textBoxUserAgent.Text;
			settings.OutputDirectory = textBoxOutputDirectory.Text;
		}

		private void buttonLoadCsv_Click(object sender, EventArgs e)
		{
			openCsvDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CheckUrl.DataFolder");
			openCsvDialog.ShowDialog();
		}

		private void openCsvDialog_FileOk(object sender, CancelEventArgs e)
		{
			Console.WriteLine(openCsvDialog.FileName);

			textBoxCsvFile.Text = openCsvDialog.FileName;

			var folder = new FileInfo(openCsvDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CheckUrl.DataFolder", folder);
		}

		private void buttonOutputDirectory_Click(object sender, EventArgs e)
		{
			folderOutputDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("CheckUrl.OutputFolder");
			folderOutputDialog.ShowDialog();
		}

		private void folderOutputDialog_HelpRequest(object sender, EventArgs e)
		{
			textBoxOutputDirectory.Text = folderOutputDialog.SelectedPath;
			Spider.Settings.SaveRegistrySetting("CheckUrl.OutputFolder", folderOutputDialog.SelectedPath);
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{
			var folderName = linkLabelResultFolder.Text;
			Process.Start(folderName);
		}
	}
}
