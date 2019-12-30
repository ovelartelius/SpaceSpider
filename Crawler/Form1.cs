using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Crawler.Data;
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
            textBoxIndexDirectory.Text = settings.IndexFolder;
            textBoxTimeoutInMs.Text = settings.TimeoutInMs.ToString();

            textBoxUserAgent.Text = settings.UserAgent;

            textBoxIgnoreLinksPatterns.Text = settings.IgnoreLinksPatterns;
            textBoxIgnoreExternalHostsRegExPatterns.Text = settings.IgnoreExternalHostsPatterns;

        }

        private void PopulateSettingsWithFormValues(CrawlerSettings settings)
		{
			settings.Url = textBoxUrl.Text;
            settings.IndexFolder = textBoxIndexDirectory.Text;
            settings.TimeoutInMs = Convert.ToInt32(textBoxTimeoutInMs.Text);

            settings.UserAgent = textBoxUserAgent.Text;

            settings.IgnoreLinksPatterns = textBoxIgnoreLinksPatterns.Text;
            settings.IgnoreExternalHostsPatterns = textBoxIgnoreExternalHostsRegExPatterns.Text;
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

        private void buttonIndexDirectory_Click(object sender, EventArgs e)
        {
            folderIndexDialog.SelectedPath = Spider.Settings.LoadRegistrySetting("Crawler.IndexFolder");
            if (folderIndexDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxIndexDirectory.Text = folderIndexDialog.SelectedPath;
                Spider.Settings.SaveRegistrySetting("Crawler.IndexFolder", folderIndexDialog.SelectedPath);
            }
        }

        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            buttonStartWork.Enabled = false;
            LogReset();

            var manifest = new CrawlerSettings();
            PopulateSettingsWithFormValues(manifest);
            manifest.IgnoreLinksPatternsList = textBoxIgnoreLinksPatterns.Text.SplitToList();
            manifest.IgnoreExternalHostsPatternsList = textBoxIgnoreExternalHostsRegExPatterns.Text.SplitToList();

            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");
            manifest.IndexFolder = manifest.IndexFolder + "\\" + dateTimeString;

            backgroundWorkerMaster.RunWorkerAsync(manifest);

        }

        

        //private bool IsIgnoredLink(string indexFolder, string link)
        //{
        //    var ignore = false;
        //    var filePath = GetIgnoredLinksFilePath(indexFolder);
        //    var ignoredUrls = Json.LoadJson<List<string>>(filePath);
        //    if (ignoredUrls.Contains(link))
        //    {
        //        ignore = true;
        //    }
        //    return ignore;
        //}

        //private string GetIgnoredLinksFilePath(string indexFolder)
        //{
        //    var filePath = $"{indexFolder}\\_ignoredlinks.json";
        //    return filePath;
        //}

        //private void InitIgnoredLinks(string indexFolder)
        //{
        //    var filePath = GetIgnoredLinksFilePath(indexFolder);
        //    var ignoredLinks = new List<string>();

        //    if (!Json.SaveAsFile(filePath, ignoredLinks))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        //private void AddLinkToIgnoreLinks(string indexFolder, string link)
        //{
        //    var filePath = GetIgnoredLinksFilePath(indexFolder);
        //    var ignoredLinks = Json.LoadJson<List<string>>(filePath);

        //    ignoredLinks.Add(link);

        //    if (!Json.SaveAsFile(filePath, ignoredLinks))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}




        //private string GetCheckUrlResultFilePath(string indexFolder, CheckUrlResult checkUrlResult)
        //{
        //    var filePath = $"{indexFolder}\\{checkUrlResult.Uri.AbsoluteUri.ToFileSafeName()}.json";
        //    return filePath;
        //}

        //private string GetNotAnchorParsedPagesFilePath(string indexFolder)
        //{
        //    var filePath = $"{indexFolder}\\_notanchorparsedpages.json";
        //    return filePath;
        //}

        //private void InitNotAnchorParsedPage(string indexFolder)
        //{
        //    var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
        //    var notParsedPages = new List<string>();

        //    if (!Json.SaveAsFile(filePath, notParsedPages))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        //private void AddNotAnchorParsedPage(string indexFolder, CheckUrlResult checkUrlResult)
        //{
        //    var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
        //    var notParsedPages = Json.LoadJson<List<string>>(filePath);

        //    var pageFilePath = GetCheckUrlResultFilePath(indexFolder, checkUrlResult);
        //    notParsedPages.Add(pageFilePath);

        //    if (Json.SaveAsFile(filePath, notParsedPages))
        //    {
        //        Console.WriteLine($"Added {checkUrlResult.Url} to {filePath}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Could not save {filePath}. Stop trying.");
        //    }
        //}

        //private string CheckOutNotAnchorParsedPage(string indexFolder)
        //{
        //    var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
        //    var notParsedPages = Json.LoadJson<List<string>>(filePath);
        //    var uri = string.Empty;
        //    if (notParsedPages != null && notParsedPages.Any())
        //    {
        //        uri = notParsedPages[0];
        //    }

        //    return uri;
        //}

        //private void RemoveNotAnchorParsedPage(string indexFolder, string uri)
        //{
        //    var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
        //    var notParsedPages = Json.LoadJson<List<string>>(filePath);

        //    notParsedPages.Remove(uri);

        //    if (!Json.SaveAsFile(filePath, notParsedPages))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        //private string GetNotCrawledUrlsFilePath(string indexFolder)
        //{
        //    var filePath = $"{indexFolder}\\_notcrawledurls.json";
        //    return filePath;
        //}

        //private void InitNotCrawledUrl(string indexFolder)
        //{
        //    var filePath = GetNotCrawledUrlsFilePath(indexFolder);
        //    var notCrawledUrls = new List<string>();

        //    if (!Json.SaveAsFile(filePath, notCrawledUrls))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        //private void AddNotCrawledUrl(string indexFolder, string url)
        //{
        //    var filePath = GetNotCrawledUrlsFilePath(indexFolder);
        //    var notCrawledUrls = Json.LoadJson<List<string>>(filePath);

        //    notCrawledUrls.Add(url);

        //    if (!Json.SaveAsFile(filePath, notCrawledUrls))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        //private string CheckOutNotCrawledUrl(string indexFolder)
        //{
        //    var filePath = GetNotCrawledUrlsFilePath(indexFolder);
        //    var notCrawledUrls = Json.LoadJson<List<string>>(filePath);
        //    var url = string.Empty;
        //    if (notCrawledUrls != null && notCrawledUrls.Any())
        //    {
        //        url = notCrawledUrls[0];
        //    }

        //    return url;
        //}

        //private void RemoveNotCrawledUrl(string indexFolder, string url)
        //{
        //    var filePath = GetNotCrawledUrlsFilePath(indexFolder);
        //    var notCrawledUrls = Json.LoadJson<List<string>>(filePath);

        //    notCrawledUrls.Remove(url);

        //    if (!Json.SaveAsFile(filePath, notCrawledUrls))
        //    {
        //        Console.WriteLine($"Could not save {filePath}");
        //    }
        //}

        #region BackgroundWorkerMaster

        private void backgroundWorkerMaster_DoWork(object sender, DoWorkEventArgs e)
        {
            var crawlerManifest = e.Argument as CrawlerSettings;

            DataHandler.Init(crawlerManifest.IndexFolder);

            // Add the starting url
            var startUrl = crawlerManifest.Url;
            DataHandler.NotCrawled.Add(crawlerManifest.IndexFolder, startUrl);

            var crawledUrls = new List<string>();
            var anchorParsedPages = new List<string>();

            var finished = false;
            var sw = new Stopwatch();
            sw.Start();
            while (finished != true)
            {
                

                var notCrawledUrl = DataHandler.NotCrawled.Checkout(crawlerManifest.IndexFolder);
                if (DataHandler.Crawled.Exist(crawlerManifest.IndexFolder, notCrawledUrl))
                {
                    DataHandler.NotCrawled.Remove(crawlerManifest.IndexFolder, notCrawledUrl);
                    notCrawledUrl = string.Empty;
                }
                if (!string.IsNullOrEmpty(notCrawledUrl) && !backgroundWorkerGetUrl.IsBusy)
                {
                    var checkUrlManifest = new CheckUrlManifest();
                    checkUrlManifest.Url = notCrawledUrl;
                    checkUrlManifest.SourceUrls = new List<string>();
                    checkUrlManifest.UserAgent = crawlerManifest.UserAgent;
                    checkUrlManifest.IndexFolder = crawlerManifest.IndexFolder;
                    backgroundWorkerGetUrl.RunWorkerAsync(checkUrlManifest);
                    //DataHandler.NotCrawled.Remove(crawlerManifest.IndexFolder, notCrawledUrl);
                    crawledUrls.Add(notCrawledUrl);
                }

                var notAnchorParsedPage = DataHandler.NotAnchorParsedPages.Checkout(crawlerManifest.IndexFolder);
                if (anchorParsedPages.Contains(notAnchorParsedPage))
                {
                    DataHandler.NotAnchorParsedPages.Remove(crawlerManifest.IndexFolder, notAnchorParsedPage);
                    notAnchorParsedPage = string.Empty;
                }
                if (!string.IsNullOrEmpty(notAnchorParsedPage) && !backgroundWorkerAnchorParsePage.IsBusy)
                {
                    var anchorParsePageManifest = new AnchorParsePageManifest();
                    anchorParsePageManifest.IndexFolder = crawlerManifest.IndexFolder;
                    anchorParsePageManifest.AnchorRegexPattern = crawlerManifest.AnchorRegexPattern;
                    anchorParsePageManifest.FileToParse = notAnchorParsedPage;
                    anchorParsePageManifest.IgnoreLinksPatternsList = crawlerManifest.IgnoreLinksPatternsList;
                    anchorParsePageManifest.ErroneousLinkPatternsList = crawlerManifest.ErroneousLinkPatternsList;
                    backgroundWorkerAnchorParsePage.RunWorkerAsync(anchorParsePageManifest);
                    DataHandler.NotAnchorParsedPages.Remove(crawlerManifest.IndexFolder, notAnchorParsedPage);
                    anchorParsedPages.Add(notAnchorParsedPage);
                }

                var processStatus = new CrawlerProcessStatus();
                processStatus.MaxGetUrls = DataHandler.NotCrawled.Count(crawlerManifest.IndexFolder) + DataHandler.Crawled.Count(crawlerManifest.IndexFolder);
                processStatus.NoGetUrls = crawledUrls.Count;
                processStatus.MaxPagesParsed = DataHandler.NotAnchorParsedPages.Count(crawlerManifest.IndexFolder) + anchorParsedPages.Count;
                processStatus.NoPagesParsed = anchorParsedPages.Count;
                //processStatus.LatestCheckUrl = notCrawledUrl;
                //processStatus.LatestAnchorParsedPage = notAnchorParsedPage;

                backgroundWorkerMaster.ReportProgress(-1, processStatus);

                if (!backgroundWorkerGetUrl.IsBusy && 
                    !backgroundWorkerAnchorParsePage.IsBusy && 
                    string.IsNullOrEmpty(notCrawledUrl) && 
                    string.IsNullOrEmpty(notAnchorParsedPage) && 
                    sw.ElapsedMilliseconds > 15000 &&
                    DataHandler.NotCrawled.Count(crawlerManifest.IndexFolder) == 0 &&
                    DataHandler.NotAnchorParsedPages.Count(crawlerManifest.IndexFolder) == 0)
                {
                    finished = true;
                }

                if (sw.ElapsedMilliseconds > crawlerManifest.TimeoutInMs) //Timeout
                {
                    finished = true;
                    crawlerManifest.TimedOut = true;
                }
            }

            ////Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            e.Result = crawlerManifest;
        }

        private void backgroundWorkerMaster_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            var processStatus = e.UserState as CrawlerProcessStatus;

            progressBarGetUrls.Maximum = (processStatus.MaxGetUrls > processStatus.NoGetUrls ? processStatus.MaxGetUrls : processStatus.NoGetUrls);
            progressBarGetUrls.Value = processStatus.NoGetUrls;

            progressBarParsePages.Maximum = (processStatus.MaxPagesParsed > processStatus.NoPagesParsed ? processStatus.MaxPagesParsed : processStatus.NoPagesParsed);
            progressBarParsePages.Value = processStatus.NoPagesParsed;

            //Log(processStatus.LatestCheckUrl);
            //Log(processStatus.LatestAnchorParsedPage);
            //if (e.ProgressPercentage == -1)
            //    progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            //else
            //    progressBarWork.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerMaster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Receive the result from DoWork, and display it.
            var crawlerManifest = e.Result as CrawlerSettings;

            var crawled = DataHandler.Crawled.LoadSource(crawlerManifest.IndexFolder);
            crawled = crawled.Distinct().ToList();
            DataHandler.Crawled.SaveSource(crawlerManifest.IndexFolder, crawled);

            var ignoredLinks = DataHandler.IgnoredLinks.LoadSource(crawlerManifest.IndexFolder);
            ignoredLinks = ignoredLinks.Distinct().ToList();
            DataHandler.IgnoredLinks.SaveSource(crawlerManifest.IndexFolder, ignoredLinks);

            //linkLabelResultFolder.Text = _workLoad.OutputDirectory;
            if (crawlerManifest.TimedOut)
            {
                Log("Crawler timed out!");
            }
            Log("The end!");
            buttonStartWork.Enabled = true;
            progressBarGetUrls.Value = 0;
            progressBarParsePages.Value = 0;
        }

        #endregion

        private void LogReset()
        {
            textBoxResult.Text = string.Empty;
        }

        private void Log(string message)
        {
            textBoxResult.Text = message + "\r\n" + textBoxResult.Text;
        }
    }
}
