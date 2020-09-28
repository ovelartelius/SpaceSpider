using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvMerger.Models;
using Spider.Extensions;
using Spider.Models.Csv;

namespace CsvMerger
{
	public partial class MergeForm : Form
	{
		public MergeForm()
		{
			InitializeComponent();

			if (!string.IsNullOrEmpty(Spider.Settings.LoadRegistrySetting("CsvMerger.SettingsFile")))
			{
				var settingsFile = Spider.Settings.LoadRegistrySetting("CsvMerger.SettingsFile");
				var settings = Spider.Settings.LoadSettings<CsvMergerSettings>(settingsFile);
				Console.WriteLine($"Autoload latest settings {settingsFile}");
				PopulateFormWithSettingsValues(settings);
			}
		}

		private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CsvMerger.SettingsFolder");
			openSettingsDialog.ShowDialog();

		}

		private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CsvMerger.SettingsFolder");
			saveSettingsDialog.ShowDialog();
		}

		private void openSettingsDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Console.WriteLine(openSettingsDialog.FileName);

			var settings = Spider.Settings.LoadSettings<CsvMergerSettings>(openSettingsDialog.FileName);
			PopulateFormWithSettingsValues(settings);

			var folder = new FileInfo(openSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CsvMerger.SettingsFolder", folder);
			Spider.Settings.SaveRegistrySetting("CsvMerger.SettingsFile", openSettingsDialog.FileName);
		}

		private void saveSettingsDialog_FileOk(object sender, CancelEventArgs e)
		{
			var settings = new CsvMergerSettings();
			PopulateSettingsWithFormValues(settings);

			Spider.Settings.SaveSettings(saveSettingsDialog.FileName, settings);

			var folder = new FileInfo(saveSettingsDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CsvMerger.SettingsFolder", folder);
		}

		private void PopulateFormWithSettingsValues(CsvMergerSettings settings)
		{
			textBoxCsvFile1.Text = settings.CsvFilePath1;
			checkBoxFirstRowContainsTitleCsvFile1.Checked = settings.FirstRowContainsTitle1;
			textBoxCsvFileSeperatorCsvFile1.Text = settings.CsvFileSeperator1;

			textBoxCsvFile2.Text = settings.CsvFilePath2;
			checkBoxFirstRowContainsTitleCsvFile2.Checked = settings.FirstRowContainsTitle2;
			textBoxCsvFileSeperatorCsvFile2.Text = settings.CsvFileSeperator2;

			textBoxIgnorePatterns.Text = settings.IgnorePatterns;
			textBoxNewSiteDomain.Text = settings.NewSiteDomain;
			textBoxOutputDirectory.Text = settings.OutputDirectory;
		}

		private void PopulateSettingsWithFormValues(CsvMergerSettings settings)
		{
			settings.CsvFilePath1 = textBoxCsvFile1.Text;
			settings.FirstRowContainsTitle1 = checkBoxFirstRowContainsTitleCsvFile1.Checked;
			settings.CsvFileSeperator1 = textBoxCsvFileSeperatorCsvFile1.Text;

			settings.CsvFilePath2 = textBoxCsvFile2.Text;
			settings.FirstRowContainsTitle2 = checkBoxFirstRowContainsTitleCsvFile2.Checked;
			settings.CsvFileSeperator2 = textBoxCsvFileSeperatorCsvFile2.Text;

			settings.IgnorePatterns = textBoxIgnorePatterns.Text;
			settings.NewSiteDomain = textBoxNewSiteDomain.Text;
			settings.OutputDirectory = textBoxOutputDirectory.Text;
		}

		private void buttonLoadCsvFile1_Click(object sender, EventArgs e)
		{
			openCsvDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("CsvMerger.DataFolder1");
			openCsvDialog.ShowDialog();
		}

		private void openCsvDialog_FileOk(object sender, CancelEventArgs e)
		{
			Console.WriteLine(openCsvDialog.FileName);

			textBoxCsvFile1.Text = openCsvDialog.FileName;

			var folder = new FileInfo(openCsvDialog.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CsvMerger.DataFolder1", folder);
		}

		private void buttonLoadCsvFile2_Click(object sender, EventArgs e)
		{
			openCsvDialog2.InitialDirectory = Spider.Settings.LoadRegistrySetting("CsvMerger.DataFolder2");
			openCsvDialog2.ShowDialog();

		}

		private void openCsvDialog2_FileOk(object sender, CancelEventArgs e)
		{
			Console.WriteLine(openCsvDialog2.FileName);

			textBoxCsvFile2.Text = openCsvDialog2.FileName;

			var folder = new FileInfo(openCsvDialog2.FileName).Directory.FullName;
			Spider.Settings.SaveRegistrySetting("CsvMerger.DataFolder2", folder);

		}

		private void buttonOutputDirectory_Click(object sender, EventArgs e)
		{
			folderOutputDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("CsvMerger.OutputFolder");
			if (folderOutputDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxOutputDirectory.Text = folderOutputDialog.SelectedPath;
				Spider.Settings.SaveRegistrySetting("CsvMerger.OutputFolder", folderOutputDialog.SelectedPath);
			}

		}

		protected WorkLoad _workLoad = new WorkLoad();

		private void buttonStartWork_Click(object sender, EventArgs e)
		{
			linkLabelResultFolder.Text = string.Empty;
			LogReset();
			buttonStartWork.Enabled = false;
			_workLoad = PopulateWorkLoad();

			var settings = new CsvMergerSettings();
			PopulateSettingsWithFormValues(settings);

			backgroundWorkerLoadCsv.RunWorkerAsync(_workLoad);

		}

		private WorkLoad PopulateWorkLoad()
		{
			var workLoad = new WorkLoad
			{
				CsvFile1 = textBoxCsvFile1.Text,
				CsvFile2 = textBoxCsvFile2.Text,
				NewSiteDomain = textBoxNewSiteDomain.Text,
				//UserAgent = textBoxUserAgent.Text,
				//Proxy = textBoxProxy.Text,
				//IgnorePatterns = new List<string>(),
				//SearchUrl = textBoxSearchUrl.Text,
				OutputDirectory = textBoxOutputDirectory.Text,
				//RunOverVpn = checkBoxOverVpn.Checked,
				//IgnoreSearch = checkBoxIgnoreSearch.Checked,
				//CheckDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked
			};
			workLoad.IgnorePatterns = textBoxIgnorePatterns.Text.SplitToList();
			var settings = new CsvMergerSettings();
			PopulateSettingsWithFormValues(settings);
			workLoad.Settings = settings;

			return workLoad;
		}

		private void LogReset()
		{
			textBoxLog.Text = string.Empty;
		}

		private void Log(string message)
		{
			textBoxLog.Text = message + "\r\n" + textBoxLog.Text;
		}

		#region BackgroundWorkerLoadCsv
		private void backgroundWorkerLoadCsv_DoWork(object sender, DoWorkEventArgs e)
		{
			var workLoad = e.Argument as WorkLoad;

			List<CsvSimpleUrl> csvFile1Values = File.ReadAllLines(workLoad.Settings.CsvFilePath1)
				.Skip((workLoad.Settings.FirstRowContainsTitle1 ? 1 : 0))
				.Select(v => CsvSimpleUrl.FromCsv(v, workLoad.Settings.CsvFileSeperator1))
				.Distinct()
				.ToList();

			var urls = csvFile1Values.Select(x => x.Url).ToList();

			List<CsvSimpleUrl> csvFile2Values = File.ReadAllLines(workLoad.Settings.CsvFilePath2)
				.Skip((workLoad.Settings.FirstRowContainsTitle2 ? 1 : 0))
				.Select(v => CsvSimpleUrl.FromCsv(v, workLoad.Settings.CsvFileSeperator2))
				.Distinct()
				.ToList();

			urls.AddRange(csvFile2Values.Select(x => x.Url).ToList());

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
            var spider = new Spider.Spider();
            //
            // Receive the result from DoWork, and display it.
            //
            var tempUrls = e.Result as List<string>;

			_workLoad.Urls = new List<string>();

			Log($"Doing something.");

			foreach (var tempUrl in tempUrls)
			{
                if (!string.IsNullOrEmpty(tempUrl))
                {
                    var newUrl = tempUrl.CleanupUrl(_workLoad.NewSiteDomain);

                    if (!string.IsNullOrEmpty(newUrl) && !spider.ShouldUrlBeIgnored(newUrl, _workLoad.IgnorePatterns))
                    {
                        if (!string.IsNullOrEmpty(_workLoad.NewSiteDomain))
                        {
                            newUrl = newUrl.SwapHostname(_workLoad.NewSiteDomain);
                        }

                        _workLoad.Urls.Add(newUrl);
                    }
                }
            }

			// Remove all empty
			_workLoad.Urls = _workLoad.Urls.Where(x => !string.IsNullOrEmpty(x)).ToList();
			// Make them unique
			_workLoad.Urls = _workLoad.Urls.Distinct().ToList();
			//
			// Will display "6 3" in title Text (in this example)
			//
			Log($"Found {_workLoad.Urls.Count().ToString()} distinct URLs in the CSV files.");


			//Save the result to a CSV file.
			var resultReporter = new ResultReporter();
            resultReporter.CreateResult(_workLoad);

			Log("The end!");
			buttonStartWork.Enabled = true;

			//StartMerger();
		}

		//private void StartMerger()
		//{
		//	if (_workLoad.Urls.Count() != 0 && !backgroundWorkerMerger.IsBusy)
		//	{
		//		progressBarWork.Maximum = _workLoad.Urls.Count;
		//		progressBarWork.Value = 0;

		//		backgroundWorkerMerger.RunWorkerAsync(_workLoad);
		//	}
		//	else
		//	{
		//		Log($"Merger is running.");
		//	}
		//}

		#endregion

	}
}
