using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using CheckUrls.Models;
using CheckUrls.Models.Csv;
using Spider.Extensions;
using Spider.Models;
using Spider.Models.Csv;

namespace CheckUrls
{
    public partial class CsvForm : Form
    {
		public CsvForm()
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

            backgroundWorkerLoadCsv.RunWorkerAsync(_workLoad);
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
            workLoad.IgnorePatterns = textBoxIgnorePatterns.Text.SplitToList();
            var settings = new CheckUrlsSettings();
            PopulateSettingsWithFormValues(settings);
            workLoad.Settings = settings;


            return workLoad;
		}

        #region BackgroundWorkerUrlCheck
        private UrlsCheckerManifest CreateUrlsCheckerManifest(WorkLoad workLoad)
        {
            var urlsCheckManifest = new UrlsCheckerManifest
            {
                UserAgent = workLoad.UserAgent,
                IgnorePatterns = workLoad.IgnorePatterns,
                NewSiteDomain = workLoad.NewSiteDomain,
                Urls = workLoad.Urls,

                IgnoreSearch = workLoad.IgnoreSearch,
                RunOverVpn = workLoad.RunOverVpn,
                SearchUrl = workLoad.SearchUrl
            };
            return urlsCheckManifest;
        }

        private void backgroundWorkerUrlCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            var workLoad = e.Argument as WorkLoad;
            
            backgroundWorkerUrlCheck.ReportProgress(-1, workLoad.Urls.Count);

            var urlChecker = new Spider.UrlsChecker();
            var urlsCheckManifest = CreateUrlsCheckerManifest(workLoad);
            var pageLinks = urlChecker.CheckUrls(urlsCheckManifest, backgroundWorkerUrlCheck);

            e.Result = pageLinks;
        }

        private void backgroundWorkerUrlCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            if (e.ProgressPercentage == -1)
                progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            else
                progressBarWork.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerUrlCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            _workLoad.SpiderPageLinks = e.Result as List<CheckUrlResult>;

            var resultReporter = new ResultReporter();
            var resultLog = resultReporter.CreateResult(_workLoad);
            foreach (var logItem in resultLog)
            {
                Log(logItem);
            }

			linkLabelResultFolder.Text = _workLoad.OutputDirectory;
			Log("The end!");
			buttonStartWork.Enabled = true;
			progressBarWork.Value = 0;
		}
        #endregion

        #region BackgroundWorkerLoadCsv
        private void backgroundWorkerLoadCsv_DoWork(object sender, DoWorkEventArgs e)
        {
            var workLoad = e.Argument as WorkLoad;

            //List<CsvSimpleUrl> values = File.ReadAllLines(workLoad.CsvFile)
            //                                .Skip(1)
            //                                .Select(v => CsvSimpleUrl.FromCsv(v))
            //                                .Distinct()
            //                                .ToList();
            List<CsvSimpleUrl> values = File.ReadAllLines(workLoad.Settings.CsvFilePath)
                .Skip((workLoad.Settings.FirstRowContainsTitle ? 1 : 0))
                .Select(v => CsvSimpleUrl.FromCsv(v, workLoad.Settings.CsvFileSeperator))
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
			textBoxCsvFile.Text = settings.CsvFilePath;
            checkBoxFirstRowContainsTitle.Checked = settings.FirstRowContainsTitle;
            textBoxCsvFileSeperator.Text = settings.CsvFileSeperator;
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
            settings.FirstRowContainsTitle = checkBoxFirstRowContainsTitle.Checked;
            settings.CsvFileSeperator = textBoxCsvFileSeperator.Text;
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

        private void sitemapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new SitemapForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            //frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }
}
