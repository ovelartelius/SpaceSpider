using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            textBoxIndexDirectory.Text = settings.IndexFolder;

            textBoxUserAgent.Text = settings.UserAgent;

            textBoxIgnoreLinksPatterns.Text = settings.IgnoreLinksPatterns;
            textBoxIgnoreExternalHostsRegExPatterns.Text = settings.IgnoreExternalHostsPatterns;

        }

        private void PopulateSettingsWithFormValues(CrawlerSettings settings)
		{
			settings.Url = textBoxUrl.Text;
            settings.IndexFolder = textBoxIndexDirectory.Text;

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

            var manifest = new CrawlerSettings();
            PopulateSettingsWithFormValues(manifest);
            manifest.IgnoreLinksPatternsList = textBoxIgnoreLinksPatterns.Text.SplitToList();
            manifest.IgnoreExternalHostsPatternsList = textBoxIgnoreExternalHostsRegExPatterns.Text.SplitToList();


            backgroundWorkerMaster.RunWorkerAsync(manifest);

        }

        #region BackgroundWorkerGetUrl()
        private void backgroundWorkerGetUrl_DoWork(object sender, DoWorkEventArgs e)
        {
            var checkUrlManifest = e.Argument as CheckUrlManifest;

            var spider = new Spider.Spider();
            var checkUrlResult = spider.CheckUrl(checkUrlManifest);

            var filePath = GetCheckUrlResultFilePath(checkUrlManifest.IndexFolder, checkUrlResult);
            Json.SaveAsFile<CheckUrlResult>(filePath, checkUrlResult);

            AddNotAnchorParsedPage(checkUrlManifest.IndexFolder, checkUrlResult);
        }
        #endregion

        private bool ShouldLinkBeIgnored(AnchorParsePageManifest manifest, string link)
        {
            var ignore = false;

            if (IsIgnoredLink(manifest.IndexFolder, link))
            {
                ignore = true;
            }
            else if (link.MatchAnyPattern(manifest.IgnoreLinksPatternsList))
            {
                AddLinkToIgnoreLinks(manifest.IndexFolder, link);
                ignore = true;
            }

            return ignore;
        }

        private bool IsIgnoredLink(string indexFolder, string link)
        {
            var ignore = false;
            var filePath = GetIgnoredLinksFilePath(indexFolder);
            var ignoredUrls = Json.LoadJson<List<string>>(filePath);
            if (ignoredUrls.Contains(link))
            {
                ignore = true;
            }
            return ignore;
        }

        private string GetIgnoredLinksFilePath(string indexFolder)
        {
            var filePath = $"{indexFolder}\\_ignoredlinks.json";
            return filePath;
        }

        private void InitIgnoredLinks(string indexFolder)
        {
            var filePath = GetIgnoredLinksFilePath(indexFolder);
            var ignoredLinks = new List<string>();

            if (!Json.SaveAsFile(filePath, ignoredLinks))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private void AddLinkToIgnoreLinks(string indexFolder, string link)
        {
            var filePath = GetIgnoredLinksFilePath(indexFolder);
            var ignoredLinks = Json.LoadJson<List<string>>(filePath);

            ignoredLinks.Add(link);

            if (!Json.SaveAsFile(filePath, ignoredLinks))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }


        #region BackgroundWorkerAnchorParsePage()
        private void backgroundWorkerAnchorParsePage_DoWork(object sender, DoWorkEventArgs e)
        {
            var anchorParsePageManifest = e.Argument as AnchorParsePageManifest;

            anchorParsePageManifest.CheckUrlResult = Json.LoadJson<CheckUrlResult>(anchorParsePageManifest.FileToParse);

            if (!string.IsNullOrEmpty(anchorParsePageManifest.CheckUrlResult.Content))
            {
                var matches = Regex.Matches(anchorParsePageManifest.CheckUrlResult.Content, anchorParsePageManifest.AnchorRegexPattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    var link = match.Groups[1].ToString();

                    // Check if the link matching any ignore pattern.
                    if (ShouldLinkBeIgnored(anchorParsePageManifest, link))
                    {
                        Console.WriteLine($"Ignored URL {link}.");
                        anchorParsePageManifest.CheckUrlResult.IgnoredLinks.Add(link);
                        link = string.Empty;
                    }

                    // Check if the link startswith "~/link/".
                    if (link.MatchAnyPattern(anchorParsePageManifest.ErroneousLinkPatternsList))
                    {
                        anchorParsePageManifest.CheckUrlResult.ErroneousLinks.Add(link);
                        // Clear the link so that we stop handle the page.
                        link = string.Empty;
                    }


                    if (link.StartsWith("/"))
                    {
                        // We need to add the host for the link.
                        link = $"{anchorParsePageManifest.CheckUrlResult.Uri.Scheme}://{anchorParsePageManifest.CheckUrlResult.Uri.Host}{link}";
                    }

                    //// We need to make sure that we stay on the same host.
                    //if (!string.IsNullOrEmpty(link) && !link.StartsWith("?") && !link.StartsWith(string.Format("{0}://{1}", workingPage.Uri.Scheme, workingPage.Uri.Host)))
                    //{
                    //    var cachedPageLink = Cache.FirstOrDefault(x => x.Url == link);
                    //    if (cachedPageLink == null)
                    //    {
                    //        //var urlCheckResult = new UrlCheckResult();
                    //        //try
                    //        //{
                    //        //	var linkUri = new Uri(link);
                    //        //	urlCheckResult = CheckUrl(linkUri);
                    //        //}
                    //        //catch (UriFormatException uriEx)
                    //        //{
                    //        //	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
                    //        //}
                    //        //catch (NotSupportedException notEx)
                    //        //{
                    //        //	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
                    //        //}

                    //        ////if (urlCheckResult.StatusCode != HttpStatusCode.BadRequest)
                    //        ////{
                    //        ////	urlCheckResult = CheckUrl(new Uri(linkUri));
                    //        ////}

                    //        //var pageLink = new WorkingPageLink { Url = link, StatusCode = urlCheckResult.StatusCode };
                    //        //workingPage.DestinationUrls.Add(pageLink);
                    //        //Cache.Add(pageLink);
                    //    }
                    //    else
                    //    {
                    //        workingPage.DestinationUrls.Add(cachedPageLink);
                    //    }
                    //    link = string.Empty;
                    //}


                    if (!string.IsNullOrEmpty(link))
                    {
                        if (link.StartsWith("?"))
                        {
                            //link = string.Format("{0}{1}", workingPage.Uri.AbsoluteUri, link);
                            link = $"{anchorParsePageManifest.CheckUrlResult.Uri.AbsoluteUri}{link}";
                        }

                        var uri = anchorParsePageManifest.CheckUrlResult.Uri;

                        if (!link.StartsWith($"{uri.Scheme}://{uri.Host}"))
                        {
                            // External link
                            anchorParsePageManifest.CheckUrlResult.ExternalUrls.Add(link);
                        }
                        else
                        {
                            anchorParsePageManifest.CheckUrlResult.DestinationUrls.Add(link);
                            AddNotCrawledUrl(anchorParsePageManifest.IndexFolder, link);
                        }
                    }

                }

                var filePath = GetCheckUrlResultFilePath(anchorParsePageManifest.IndexFolder, anchorParsePageManifest.CheckUrlResult);
                Json.SaveAsFile<CheckUrlResult>(filePath, anchorParsePageManifest.CheckUrlResult);
            }
            
        }
        #endregion

        private string GetCheckUrlResultFilePath(string indexFolder, CheckUrlResult checkUrlResult)
        {
            var filePath = $"{indexFolder}\\{checkUrlResult.Uri.AbsoluteUri.ToFileSafeName()}.json";
            return filePath;
        }

        private string GetNotAnchorParsedPagesFilePath(string indexFolder)
        {
            var filePath = $"{indexFolder}\\_notanchorparsedpages.json";
            return filePath;
        }

        private void InitNotAnchorParsedPage(string indexFolder)
        {
            var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
            var notParsedPages = new List<string>();

            if (!Json.SaveAsFile(filePath, notParsedPages))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private void AddNotAnchorParsedPage(string indexFolder, CheckUrlResult checkUrlResult)
        {
            var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
            var notParsedPages = Json.LoadJson<List<string>>(filePath);

            var pageFilePath = GetCheckUrlResultFilePath(indexFolder, checkUrlResult);
            notParsedPages.Add(pageFilePath);

            if (!Json.SaveAsFile(filePath, notParsedPages))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private string CheckOutNotAnchorParsedPage(string indexFolder)
        {
            var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
            var notParsedPages = Json.LoadJson<List<string>>(filePath);
            var uri = string.Empty;
            if (notParsedPages != null && notParsedPages.Any())
            {
                uri = notParsedPages[0];
            }

            return uri;
        }

        private void RemoveNotAnchorParsedPage(string indexFolder, string uri)
        {
            var filePath = GetNotAnchorParsedPagesFilePath(indexFolder);
            var notParsedPages = Json.LoadJson<List<string>>(filePath);

            notParsedPages.Remove(uri);

            if (!Json.SaveAsFile(filePath, notParsedPages))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private string GetNotCrawledUrlsFilePath(string indexFolder)
        {
            var filePath = $"{indexFolder}\\_notcrawledurls.json";
            return filePath;
        }

        private void InitNotCrawledUrl(string indexFolder)
        {
            var filePath = GetNotCrawledUrlsFilePath(indexFolder);
            var notCrawledUrls = new List<string>();

            if (!Json.SaveAsFile(filePath, notCrawledUrls))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private void AddNotCrawledUrl(string indexFolder, string url)
        {
            var filePath = GetNotCrawledUrlsFilePath(indexFolder);
            var notCrawledUrls = Json.LoadJson<List<string>>(filePath);

            notCrawledUrls.Add(url);

            if (!Json.SaveAsFile(filePath, notCrawledUrls))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private string CheckOutNotCrawledUrl(string indexFolder)
        {
            var filePath = GetNotCrawledUrlsFilePath(indexFolder);
            var notCrawledUrls = Json.LoadJson<List<string>>(filePath);
            var url = string.Empty;
            if (notCrawledUrls != null && notCrawledUrls.Any())
            {
                url = notCrawledUrls[0];
            }

            return url;
        }

        private void RemoveNotCrawledUrl(string indexFolder, string url)
        {
            var filePath = GetNotCrawledUrlsFilePath(indexFolder);
            var notCrawledUrls = Json.LoadJson<List<string>>(filePath);

            notCrawledUrls.Remove(url);

            if (!Json.SaveAsFile(filePath, notCrawledUrls))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        #region BackgroundWorkerMaster

        private void backgroundWorkerMaster_DoWork(object sender, DoWorkEventArgs e)
        {
            var crawlerManifest = e.Argument as CrawlerSettings;

            InitNotCrawledUrl(crawlerManifest.IndexFolder);
            InitNotAnchorParsedPage(crawlerManifest.IndexFolder);
            InitIgnoredLinks(crawlerManifest.IndexFolder);

            // Add the starting url
            var startUrl = crawlerManifest.Url;
            AddNotCrawledUrl(crawlerManifest.IndexFolder, startUrl);

            var finished = false;
            var sw = new Stopwatch();
            sw.Start();
            while (finished != true)
            {
                var processStatus = new CrawlerProcessStatus();
                //processStatus.MaxGetUrls = notCrawledUrls.Count;
                //processStatus.NoGetUrls = 0;
                //processStatus.MaxPagesParsed = notCrawledUrls.Count;
                //processStatus.NoPagesParsed = 0;

                backgroundWorkerMaster.ReportProgress(-1, processStatus);

                var notCrawledUrl = CheckOutNotCrawledUrl(crawlerManifest.IndexFolder);
                if (!string.IsNullOrEmpty(notCrawledUrl) && !backgroundWorkerGetUrl.IsBusy)
                {
                    var checkUrlManifest = new CheckUrlManifest();
                    checkUrlManifest.Url = notCrawledUrl;
                    checkUrlManifest.SourceUrls = new List<string>();
                    checkUrlManifest.UserAgent = crawlerManifest.UserAgent;
                    checkUrlManifest.IndexFolder = crawlerManifest.IndexFolder;
                    backgroundWorkerGetUrl.RunWorkerAsync(checkUrlManifest);
                    RemoveNotCrawledUrl(crawlerManifest.IndexFolder, notCrawledUrl);
                }

                var notAnchorParsedPage = CheckOutNotAnchorParsedPage(crawlerManifest.IndexFolder);
                if (!string.IsNullOrEmpty(notAnchorParsedPage) && !backgroundWorkerAnchorParsePage.IsBusy)
                {
                    var anchorParsePageManifest = new AnchorParsePageManifest();
                    anchorParsePageManifest.IndexFolder = crawlerManifest.IndexFolder;
                    anchorParsePageManifest.AnchorRegexPattern = crawlerManifest.AnchorRegexPattern;
                    anchorParsePageManifest.FileToParse = notAnchorParsedPage;
                    anchorParsePageManifest.IgnoreLinksPatternsList = crawlerManifest.IgnoreLinksPatternsList;
                    anchorParsePageManifest.ErroneousLinkPatternsList = crawlerManifest.ErroneousLinkPatternsList;
                    backgroundWorkerAnchorParsePage.RunWorkerAsync(anchorParsePageManifest);
                    RemoveNotAnchorParsedPage(crawlerManifest.IndexFolder, notAnchorParsedPage);
                }

                backgroundWorkerMaster.ReportProgress(-1, processStatus);

                if (!backgroundWorkerGetUrl.IsBusy && !backgroundWorkerAnchorParsePage.IsBusy && string.IsNullOrEmpty(notCrawledUrl) && string.IsNullOrEmpty(notAnchorParsedPage))
                {
                    finished = true;
                }

                if (sw.ElapsedMilliseconds > 600000)
                {
                    finished = true;
                }
            }

            ////Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            //e.Result = pageLinks;
        }

        private void backgroundWorkerMaster_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            var processStatus = e.UserState as CrawlerProcessStatus;

            progressBarGetUrls.Maximum = processStatus.MaxGetUrls;
            progressBarGetUrls.Value = processStatus.NoGetUrls;

            progressBarParsePages.Maximum = processStatus.MaxPagesParsed;
            progressBarParsePages.Value = processStatus.NoPagesParsed;
            //if (e.ProgressPercentage == -1)
            //    progressBarWork.Maximum = Convert.ToInt32(e.UserState);
            //else
            //    progressBarWork.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerMaster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Receive the result from DoWork, and display it.

            //linkLabelResultFolder.Text = _workLoad.OutputDirectory;
            //Log("The end!");
            buttonStartWork.Enabled = true;
            //progressBarWork.Value = 0;
        }

        #endregion
    }
}
