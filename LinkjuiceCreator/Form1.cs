using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkjuiceCreator.Models;
using Spider.Models;

namespace LinkjuiceCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.SettingsFile")))
            {
                var settingsFile = Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.SettingsFile");
                var settings = Spider.Settings.LoadSettings<LinkjuiceCreatorSettings>(settingsFile);
                Console.WriteLine($"Autoload latest settings {settingsFile}");
                PopulateFormWithSettingsValues(settings);
            }
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.SettingsFolder");
            openSettingsDialog.ShowDialog();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSettingsDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.SettingsFolder");
            saveSettingsDialog.ShowDialog();
        }

        private void openSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            Console.WriteLine(openSettingsDialog.FileName);

            var settings = Spider.Settings.LoadSettings<LinkjuiceCreatorSettings>(openSettingsDialog.FileName);
            PopulateFormWithSettingsValues(settings);

            var folder = new FileInfo(openSettingsDialog.FileName).Directory.FullName;
            Spider.Settings.SaveRegistrySetting("LinkjuiceCreator.SettingsFolder", folder);
            Spider.Settings.SaveRegistrySetting("LinkjuiceCreator.SettingsFile", openSettingsDialog.FileName);
        }

        private void saveSettingsDialog_FileOk(object sender, CancelEventArgs e)
        {
            var settings = new LinkjuiceCreatorSettings();
            PopulateSettingsWithFormValues(settings);

            Spider.Settings.SaveSettings(saveSettingsDialog.FileName, settings);

            var folder = new FileInfo(saveSettingsDialog.FileName).Directory.FullName;
            Spider.Settings.SaveRegistrySetting("LinkjuiceCreator.SettingsFolder", folder);
        }

        private void PopulateFormWithSettingsValues(LinkjuiceCreatorSettings settings)
        {
            textBoxCsvFile.Text = settings.CsvFilePath;
            checkBoxFirstRowContainsTitle.Checked = settings.FirstRowContainsTitle;
            textBoxCsvFileSeperator.Text = settings.CsvFileSeperator;
            textBoxPageVariableHeaderName.Text = settings.PageVariableHeaderName;
            checkBoxCleanUpNoneFoundPageIds.Checked = settings.CleanUpNoneFoundPageIds;
            checkBoxOverVpn.Checked = settings.RunningOverVpn;
            checkBoxCheckDomainBeforeStart.Checked = settings.CheckSiteDomainBeforeStart;
            textBoxNewSiteDomain.Text = settings.NewSiteDomain;
            textBoxProxy.Text = settings.Proxy;
            textBoxUserAgent.Text = settings.UserAgent;
            textBoxOutputDirectory.Text = settings.OutputDirectory;
        }

        private void PopulateSettingsWithFormValues(LinkjuiceCreatorSettings settings)
        {
            settings.CsvFilePath = textBoxCsvFile.Text;
            settings.FirstRowContainsTitle = checkBoxFirstRowContainsTitle.Checked;
            settings.CsvFileSeperator = textBoxCsvFileSeperator.Text;
            settings.PageVariableHeaderName = textBoxPageVariableHeaderName.Text;
            settings.CleanUpNoneFoundPageIds = checkBoxCleanUpNoneFoundPageIds.Checked;
            settings.RunningOverVpn = checkBoxOverVpn.Checked;
            settings.CheckSiteDomainBeforeStart = checkBoxCheckDomainBeforeStart.Checked;
            settings.NewSiteDomain = textBoxNewSiteDomain.Text;
            settings.Proxy = textBoxProxy.Text;
            settings.UserAgent = textBoxUserAgent.Text;
            settings.OutputDirectory = textBoxOutputDirectory.Text;
        }

        private void buttonLoadCsv_Click(object sender, EventArgs e)
        {
            openCsvDialog.InitialDirectory = Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.DataFolder");
            openCsvDialog.ShowDialog();

        }

        private void openCsvDialog_FileOk(object sender, CancelEventArgs e)
        {
            Console.WriteLine(openCsvDialog.FileName);

            textBoxCsvFile.Text = openCsvDialog.FileName;

            var folder = new FileInfo(openCsvDialog.FileName).Directory.FullName;
            Spider.Settings.SaveRegistrySetting("LinkjuiceCreator.DataFolder", folder);

        }

        private void buttonOutputDirectory_Click(object sender, EventArgs e)
        {
            folderOutputDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("LinkjuiceCreator.OutputFolder");
            if (folderOutputDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputDirectory.Text = folderOutputDialog.SelectedPath;
                Spider.Settings.SaveRegistrySetting("LinkjuiceCreator.OutputFolder", folderOutputDialog.SelectedPath);
            }

        }

        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            LogReset();
            buttonStartWork.Enabled = false;

            var settings = new LinkjuiceCreatorSettings();
            PopulateSettingsWithFormValues(settings);

            if (settings.CheckSiteDomainBeforeStart)
            {
                var spider = new Spider.Spider();
                var validationResult = spider.ValidateUrlOnSite(settings.NewSiteDomain, settings.UserAgent);
                if (!validationResult.Result)
                {
                    Log(validationResult.ErrorMessage);
                    Log($"Can not start the job. New site domain {settings.NewSiteDomain} is not working.");
                    return;
                }
            }

            try
            {
                var content = File.ReadAllLines(settings.CsvFilePath);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                buttonStartWork.Enabled = true;
                return;
            }

            backgroundWorkerLoadCsv.RunWorkerAsync(settings);

        }

        private void LogReset()
        {
            textBoxLog.Text = string.Empty;
        }

        private void Log(string message)
        {
            textBoxLog.Text = message + "\r\n" + textBoxLog.Text;
        }

        protected Manifest _manifest = new Manifest();

        #region BackgroundWorkerDoJob
        private void backgroundWorkerDoJob_DoWork(object sender, DoWorkEventArgs e)
        {
            var manifest = e.Argument as Manifest;

            var spider = new Spider.Spider();

            backgroundWorkerDoJob.ReportProgress(-1, manifest.MappedUrls.Count);

            manifest.PageResults = new List<PageResult>();
            var newSiteUri = new Uri(manifest.Settings.NewSiteDomain);

            var handledUrlList = new List<string>();

            var i = 0;
            foreach (var mappedUrls in manifest.MappedUrls)
            {
                Console.WriteLine($"Url-{i}");
                try
                {
                    var oldUri = new Uri(mappedUrls.DestinationUrl);
                    var newUrl = string.Empty;
                    if (newSiteUri.Port != 80 && newSiteUri.Port != 443)
                    {
                        newUrl = $"{newSiteUri.Scheme}://{newSiteUri.Host}:{newSiteUri.Port}{oldUri.PathAndQuery}";
                    }
                    else
                    {
                        newUrl = $"{newSiteUri.Scheme}://{newSiteUri.Host}{oldUri.PathAndQuery}";
                    }

                    var pageResult = new PageResult { Url = newUrl };

                    // Check duplicates
                    if (handledUrlList.Contains(newUrl))
                    {
                        var oldPageResult = manifest.PageResults.SingleOrDefault(x => x.Url == newUrl);
                        if (oldPageResult != null)
                        {
                            mappedUrls.PageResult = oldPageResult;
                            mappedUrls.PageId = Int32.Parse(oldPageResult.Headers[manifest.Settings.PageVariableHeaderName]);
                        }
                    }
                    else
                    {
                        handledUrlList.Add(newUrl);

                        //foreach (var pattern in manifest.IgnorePatterns)
                        //{
                        //    if (Regex.IsMatch(newUrl, pattern))
                        //    {
                        //        // We should ignore to test this URL.
                        //        spiderPageLink.StatusCode = HttpStatusCode.SeeOther;
                        //        spiderPageLink.Ignored = true;
                        //        break;
                        //    }
                        //}

                        //if (!spiderPageLink.Ignored)
                        //{
                        pageResult = spider.CheckUrl(newUrl, new List<string>(), manifest.Settings.UserAgent);
                        mappedUrls.PageResult = pageResult;
                        mappedUrls.PageId = Int32.Parse(pageResult.Headers[manifest.Settings.PageVariableHeaderName]);
                        //}

                        manifest.PageResults.Add(pageResult);
                    }


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
                    //var pageResult = new PageResult { Url = mappedUrls.DestinationUrl, Erroneous = true, Description = ex.Message };
                    //manifest.PageResults.Add(pageResult);
                    mappedUrls.PageId = 0;
                    mappedUrls.ErrorMessage = ex.Message;
                }

                backgroundWorkerDoJob.ReportProgress(i);
                i++;
            }

            e.Result = manifest;
        }

        private void backgroundWorkerDoJob_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            }
            else
            {
                progressBarWork.Value = e.ProgressPercentage;
            }
        }


        private void backgroundWorkerDoJob_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            try
            {
                _manifest = e.Result as Manifest;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }

            if (_manifest.Settings.CleanUpNoneFoundPageIds)
            {
                _manifest.MappedUrls = _manifest.MappedUrls.Where(x => x.PageId != 0).ToList();
            }

            var sb = new StringBuilder();
            sb.AppendLine("<epinova.seo redirectsHttpStatusCode=\"Redirect\">");

            sb.AppendLine(" <linkJuice>");

            sb.AppendLine("     <domain host=\"localhost\" language=\"no\">");

            foreach (var mappedUrl in _manifest.MappedUrls)
            {
                var sourceUri = new Uri(mappedUrl.SourceUrl);

                sb.AppendLine($"            <link url=\"{sourceUri.PathAndQuery}\" contentId=\"{mappedUrl.PageId}\">");
            }
            sb.AppendLine("     </domain>");
            sb.AppendLine(" </linkJuice>");
            sb.AppendLine("</epinova.seo>");

            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");
            string filePath = $"{_manifest.Settings.OutputDirectory}\\Linkjuice_{dateTimeString}.xml";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (StreamWriter swriter = File.CreateText(filePath))
            {
                swriter.Write(sb.ToString());
            }
            Log($"File {filePath} is created.");
            //< linkJuice >
            //< domain host = "localhost" language = "no" >
            //< link url = "/legacyurl/" contentId = "1" />
            //< link pattern = "/products/*" contentId = "2" />
            //< gone >
            //< add url = "/obsolete" />
            //< add pattern = "/products/deprecated/.*" />
            //</ gone >
            //</ domain >
            //< domain host = "legacydomain.com" language = "en" >
            //< link url = "/me.png" redirectUrl = "/global/person.png" />
            //</ domain >
            //</ linkJuice >
            //</ epinova.seo > ")

            //var listOf200Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            //var listOf400Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.NotFound).OrderBy(x => x.Url).ToList();

            //var listOf400Missing = new List<PageResult>();
            //var listOf400NotMissing = new List<PageResult>();
            //if (_workLoad.IgnoreSearch)
            //{
            //    listOf400Missing = listOf400Response.OrderBy(x => x.Url).ToList();
            //}
            //else
            //{
            //    // Av de sidor som svara 404 så har dessa efterfrågats utav någon de senaste x dagarna.
            //    listOf400Missing = listOf400Response.Where(x => x.HistoricHits != 0).OrderBy(x => x.Url).ToList();

            //    // Av de sidor som svara 404 så har dessa INTE efterfrågats utav någon de senaste x dagarna.
            //    listOf400NotMissing = listOf400Response.Where(x => x.HistoricHits == 0).OrderByDescending(x => x.HistoricHits).ToList();
            //}

            //var listOf500Response = _workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.InternalServerError || x.Erroneous).OrderBy(x => x.Url).ToList();

            //var listOf301Response = _workLoad.SpiderPageLinks.Where(x => (x.StatusCode == HttpStatusCode.MovedPermanently || x.StatusCode == HttpStatusCode.Found) && !x.Erroneous).OrderBy(x => x.Url).ToList();

            //var listOfOthers = _workLoad.SpiderPageLinks.Where(x => x.StatusCode != HttpStatusCode.MovedPermanently && x.StatusCode != HttpStatusCode.Found && x.StatusCode != HttpStatusCode.InternalServerError && x.StatusCode != HttpStatusCode.NotFound && x.StatusCode != HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            //var listOfIgnored = _workLoad.SpiderPageLinks.Where(x => x.Ignored).OrderBy(x => x.Url).ToList();

            //var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");

            //_workLoad.OutputDirectory = _workLoad.OutputDirectory + "\\" + dateTimeString;
            //if (!Directory.Exists(_workLoad.OutputDirectory))
            //{
            //    Directory.CreateDirectory(_workLoad.OutputDirectory);
            //}

            //var sb = new StringBuilder();

            //sb.AppendLine($"CSV file - {_workLoad.CsvFile} {dateTimeString}");
            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Working URLs (200):");
            //var sb200 = new StringBuilder();
            //foreach (var item in listOf200Response)
            //{
            //    sb.AppendLine(item.Url);
            //    sb200.AppendLine(item.Url);
            //}
            //if (listOf200Response.Any())
            //{
            //    Log($"Found {listOf200Response.Count} 200 URLs.");
            //}
            //SaveToExcel(dateTimeString, "200", sb200);

            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Redirected URLs (301):");
            //var sb301 = new StringBuilder();
            //foreach (var item in listOf301Response)
            //{
            //    sb.AppendLine(item.Url + " => " + item.Description);
            //    sb301.AppendLine(item.Url);
            //}
            //if (listOf301Response.Any())
            //{
            //    Log($"Found {listOf301Response.Count} 301 URLs.");
            //}
            //SaveToExcel(dateTimeString, "301", sb301);

            //if (_workLoad.IgnoreSearch)
            //{
            //    sb.AppendLine("");
            //    sb.AppendLine("");
            //    sb.AppendLine("Requested missing URLs (404):");
            //    var sb404Missing = new StringBuilder();
            //    foreach (var item in listOf400Missing)
            //    {
            //        sb.AppendLine(item.Url);
            //        sb404Missing.AppendLine(item.Url);
            //    }
            //    if (listOf400Missing.Any())
            //    {
            //        Log($"Found {listOf400Missing.Count} 404 URLs.");
            //    }
            //    SaveToExcel(dateTimeString, "404Missing", sb404Missing);
            //}
            //else
            //{
            //    sb.AppendLine("");
            //    sb.AppendLine("");
            //    sb.AppendLine("Requested missing URLs (404): (URLs are requested in past and may need to be handled)");
            //    var sb404Missing = new StringBuilder();
            //    foreach (var item in listOf400Missing)
            //    {
            //        sb.AppendLine(item.Url);
            //        sb404Missing.AppendLine(item.Url);
            //    }
            //    if (listOf400Missing.Any())
            //    {
            //        Log($"Found {listOf400Missing.Count} 404 URLs.");
            //    }
            //    SaveToExcel(dateTimeString, "404Missing", sb404Missing);

            //    sb.AppendLine("");
            //    sb.AppendLine("");
            //    sb.AppendLine("Not found URLs (404): (URLs can be ignored)");
            //    var sb404 = new StringBuilder();
            //    foreach (var item in listOf400NotMissing)
            //    {
            //        sb.AppendLine(item.Url);
            //        sb404.AppendLine(item.Url);
            //    }
            //    if (listOf400NotMissing.Any())
            //    {
            //        Log($"Found {listOf400NotMissing.Count} 404 URLs that can be ignored.");
            //    }
            //    SaveToExcel(dateTimeString, "404", sb404);
            //}

            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Errors (500):");
            //var sb500 = new StringBuilder();
            //foreach (var item in listOf500Response)
            //{
            //    sb.AppendLine(item.Url + " => " + item.Description);
            //    sb500.AppendLine(item.Url);
            //}
            //if (listOf500Response.Any())
            //{
            //    Log($"Found {listOf500Response.Count} 500 URLs.");
            //}
            //SaveToExcel(dateTimeString, "500", sb500);

            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Ignored:");
            //var sbIgnored = new StringBuilder();
            //foreach (var item in listOfIgnored)
            //{
            //    sb.AppendLine(item.Url);
            //    sbIgnored.AppendLine(item.Url);
            //}
            //if (listOfIgnored.Any())
            //{
            //    Log($"Found {listOfIgnored.Count} Ignored URLs.");
            //}
            //SaveToExcel(dateTimeString, "Ignored", sbIgnored);

            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Others:");
            //var sbOthers = new StringBuilder();
            //foreach (var item in listOfOthers)
            //{
            //    sb.AppendLine(item.Url);
            //    sbOthers.AppendLine(item.Url);
            //}
            //if (listOfOthers.Any())
            //{
            //    Log($"Found {listOfOthers.Count} 'Others' URLs.");
            //}
            //SaveToExcel(dateTimeString, "Others", sbOthers);

            //sb.AppendLine("");
            //sb.AppendLine("");
            //sb.AppendLine("Rawdata:");
            //sb.AppendLine($"Url,StatusCode,Description,HistoricHits,Erroneous");
            //var sbRawData = new StringBuilder();
            //sbRawData.AppendLine($"Url;StatusCode;Description;HistoricHits;Erroneous");
            //foreach (var item in _workLoad.SpiderPageLinks)
            //{
            //    sb.AppendLine($"{item.Url},{item.StatusCode},{item.Description},{item.HistoricHits},{item.Erroneous}");
            //    sbRawData.AppendLine($"{item.Url};{item.StatusCode};{item.Description};{item.HistoricHits};{item.Erroneous}");
            //}
            //SaveToExcel(dateTimeString, "RawData", sbRawData);

            //CreateResultFile(sb, dateTimeString);

            linkLabelResultFolder.Text = _manifest.Settings.OutputDirectory;
            Log("The end!");
            buttonStartWork.Enabled = true;
            progressBarWork.Value = 0;
        }

        //private void CreateResultFile(StringBuilder sb, string dateTime)
        //{
        //    string filePath = $"{_workLoad.OutputDirectory}\\Result_{dateTime}.txt";

        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //        Log($"Deleted old file {filePath}");
        //    }

        //    //this code section write stringbuilder content to physical text file.
        //    using (StreamWriter swriter = File.CreateText(filePath))
        //    {
        //        swriter.Write(sb.ToString());
        //    }
        //    Log($"Created result file {filePath}");
        //}
        #endregion

        #region BackgroundWorkerLoadCsv
        private void backgroundWorkerLoadCsv_DoWork(object sender, DoWorkEventArgs e)
        {
            var settings = e.Argument as LinkjuiceCreatorSettings;

            backgroundWorkerLoadCsv.ReportProgress(-1, 1);

            var manifest = new Manifest { Settings = settings };

            backgroundWorkerLoadCsv.ReportProgress(0);
            List<CsvMappedUrls> values = File.ReadAllLines(manifest.Settings.CsvFilePath)
                                            .Skip((manifest.Settings.FirstRowContainsTitle ? 1 : 0))
                                            .Select(v => CsvMappedUrls.FromCsv(v, manifest.Settings.CsvFileSeperator))
                                            .Distinct()
                                            .ToList();
            manifest.MappedUrls = values;
            e.Result = manifest;
            backgroundWorkerLoadCsv.ReportProgress(1);
        }

        private void backgroundWorkerLoadCsv_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            if (e.ProgressPercentage == -1)
                progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            else
                progressBarWork.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerLoadCsv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Receive the result from DoWork, and display it.
            //
            _manifest = e.Result as Manifest;

            Log($"Found {_manifest.MappedUrls.Count().ToString()} URLs in the CSV file.");

            Log($"Clean CSV data.");
            _manifest.MappedUrls = _manifest.MappedUrls
                .Where(x => !string.IsNullOrEmpty(x.SourceUrl) && !string.IsNullOrEmpty(x.DestinationUrl)).ToList();

            // Remove all empty
            //_workLoad.Urls = _workLoad.Urls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            // Make them unique
            //_workLoad.Urls = _workLoad.Urls.Distinct().ToList();
            //
            // Will display "6 3" in title Text (in this example)
            //
            Log($"Found {_manifest.MappedUrls.Count().ToString()} URLs to check.");

            StartDoWork();
        }

        private void StartDoWork()
        {
            //if (_workLoad.Urls.Count() != 0 && !backgroundWorkerUrlCheck.IsBusy)
            //{
            //    progressBarWork.Maximum = _workLoad.Urls.Count;
            //    progressBarWork.Value = 0;

            backgroundWorkerDoJob.RunWorkerAsync(_manifest);
            //}
            //else
            //{
            //    Log($"BackgroundWorker is running.");
            //}
        }

        #endregion
    }
}
