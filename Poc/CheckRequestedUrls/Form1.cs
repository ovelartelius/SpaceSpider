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

namespace CheckRequestedUrls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            workLoad = new WorkLoad {
                CsvFile = textBoxCsvFile.Text,
                NewSiteDomain = textBoxNewSiteDomain.Text,
                UserAgent = textBoxUserAgent.Text,
                Proxy = textBoxProxy.Text,
                IgnorePatterns = new List<string>(),
                SearchUrl = textBoxSearchUrl.Text,
                OutputDirectory = textBoxOutputDirectory.Text,
                RunOverVpn = checkBoxOverVpn.Checked,
                IgnoreSearch = checkBoxIgnoreSearch.Checked
            };

            var patterns = textBoxIgnorePatterns.Text.Split('\n');
            foreach (var pattern in patterns)
            {
                var patternValue = pattern;
                if (patternValue.Contains("\r"))
                {
                    patternValue = patternValue.Replace("\r", "");
                }
                //if (patternValue.Contains("\\/"))
                //{
                //    patternValue = patternValue.Replace(@"\\/", @"\/");
                //}
                workLoad.IgnorePatterns.Add(patternValue);
            }

            var spider = new Spider();
            var newSiteUri = new Uri(workLoad.NewSiteDomain);
            // First we check that the new site domain is working.
            var newSitePageLink = spider.CheckUrl(newSiteUri.AbsoluteUri, new List<string>(), workLoad.UserAgent);

            if (newSitePageLink.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Log($"Can not start the job. New site domain is not working: {workLoad.NewSiteDomain} - StatusCode:{newSitePageLink.StatusCode}, expected {System.Net.HttpStatusCode.OK}");
                return;
            }
            else
            {
                
                backgroundWorkerLoadCsv.RunWorkerAsync(workLoad);
                //backgroundWorkerLoadCsv_DoWork
            }

        }

        #region BackgroundWorkerUrlCheck
        private void backgroundWorkerUrlCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            var workLoad = e.Argument as WorkLoad;
            
            var spider = new Spider();
            //LogReset();

            //Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            //progressBarWork.Maximum = values.Count;
            //progressBarWork.Value = 0;

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
                    var newUrl = $"{newSiteUri.Scheme}://{newSiteUri.Host}{oldUri.PathAndQuery}";

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
                        spiderPageLink = spider.CheckUrl(newUrl, new List<string>());

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
                //backgroundWorkerUrlCheck.ReportProgress(i);
                i++;
            }


            //Log($"Found {values.Count()} URLs in the file {workLoad.CsvFile}");

            e.Result = pageLinks;
        }

        //private void backgroundWorkerUrlCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    // Change the value of the ProgressBar to the BackgroundWorker progress.
        //    //progressBarWork.Value = e.ProgressPercentage;
        //    progressBarWork.PerformStep();

        //    // Set the text.
        //    //this.Text = e.ProgressPercentage.ToString();
        //}

        private void SaveToExcel(string dateTimeString, string extraFileName, StringBuilder sb)
        {
            string filePath = $"{workLoad.OutputDirectory}Result_{dateTimeString}_{extraFileName}.csv";

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
            workLoad.SpiderPageLinks = e.Result as List<SpiderPageLink>;

            var listOf200Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOf400Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.NotFound).OrderBy(x => x.Url).ToList();

            // Av de sidor som svara 404 så har dessa efterfrågats utav någon de senaste x dagarna.
            var listOf400Missing = listOf400Response.Where(x => x.HistoricHits != 0).OrderBy(x => x.Url).ToList();

            // Av de sidor som svara 404 så har dessa INTE efterfrågats utav någon de senaste x dagarna.
            var listOf400NotMissing = listOf400Response.Where(x => x.HistoricHits == 0).OrderByDescending(x => x.HistoricHits).ToList();

            var listOf500Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.InternalServerError || x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOf301Response = workLoad.SpiderPageLinks.Where(x => (x.StatusCode == HttpStatusCode.MovedPermanently || x.StatusCode == HttpStatusCode.Found) && !x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOfOthers = workLoad.SpiderPageLinks.Where(x => x.StatusCode != HttpStatusCode.MovedPermanently && x.StatusCode != HttpStatusCode.Found && x.StatusCode != HttpStatusCode.InternalServerError && x.StatusCode != HttpStatusCode.NotFound && x.StatusCode != HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOfIgnored = workLoad.SpiderPageLinks.Where(x => x.Ignored).OrderBy(x => x.Url).ToList();

            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");

            var sb = new StringBuilder();

            sb.AppendLine($"CSV file - {workLoad.CsvFile} {DateTime.Now.ToString("yyyy-MM-dd HH:mm")}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Working URLs (200):");
            var sb200 = new StringBuilder();
            foreach (var item in listOf200Response)
            {
                sb.AppendLine(item.Url);
                sb200.AppendLine(item.Url);
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
            SaveToExcel(dateTimeString, "301", sb301);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Requested missing URLs (404): (URLs are requested in past and may need to be handled)");
            var sb404Missing = new StringBuilder();
            foreach (var item in listOf400Missing)
            {
                sb.AppendLine(item.Url);
                sb404Missing.AppendLine(item.Url);
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
            SaveToExcel(dateTimeString, "404", sb404);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Errors (500):");
            var sb500 = new StringBuilder();
            foreach (var item in listOf500Response)
            {
                sb.AppendLine(item.Url + " => " + item.Description);
                sb500.AppendLine(item.Url);
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
            SaveToExcel(dateTimeString, "Others", sbOthers);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Rawdata:");
            sb.AppendLine($"Url,StatusCode,Description,HistoricHits,Erroneous");
            var sbRawData = new StringBuilder();
            sbRawData.AppendLine($"Url;StatusCode;Description;HistoricHits;Erroneous");
            foreach (var item in workLoad.SpiderPageLinks)
            {
                sb.AppendLine($"{item.Url},{item.StatusCode},{item.Description},{item.HistoricHits},{item.Erroneous}");
                sbRawData.AppendLine($"{item.Url};{item.StatusCode};{item.Description};{item.HistoricHits};{item.Erroneous}");
            }
            SaveToExcel(dateTimeString, "RawData", sbRawData);

            //TODO: Create text file

            //string appPath = Request.PhysicalApplicationPath;
            string filePath = $"{workLoad.OutputDirectory}Result_{DateTime.Now.ToString("yyyy-MM-ddTHHmm")}.txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //this code section write stringbuilder content to physical text file.
            using (StreamWriter swriter = File.CreateText(filePath))
            {
                swriter.Write(sb.ToString());
            }


            //System.IO.File.WriteAllText(@"C:\ws\CheckRequestUrls\MyNewTextFile.txt", sb.ToString());

            //
            // Will display "6 3" in title Text (in this example)
            //
            MessageBox.Show("Finished!");
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
            workLoad.Urls = e.Result as List<string>;

            //this.Text = test.OneValue.ToString() + " " + test.TwoValue.ToString();

            // Remove all emty
            workLoad.Urls = workLoad.Urls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            // Make them unique
            workLoad.Urls = workLoad.Urls.Distinct().ToList();
            //
            // Will display "6 3" in title Text (in this example)
            //
            Log($"Found {workLoad.Urls.Count().ToString()} URLs in the CSV file.");

            StartUrlCheck();
        }

        private void StartUrlCheck()
        {
            if (workLoad.Urls.Count() != 0 && !backgroundWorkerUrlCheck.IsBusy)
            {
                progressBarWork.Maximum = workLoad.Urls.Count;
                progressBarWork.Value = 0;

                backgroundWorkerUrlCheck.RunWorkerAsync(workLoad);
            }
            else
            {
                Log($"BackgroundWorker is running.");
            }
        }

        #endregion

        //protected List<CsvSimpleUrl> urlList = new List<CsvSimpleUrl>();
        //protected List<SpiderPageLink> pageLinks = new List<SpiderPageLink>();
        protected WorkLoad workLoad = new WorkLoad();

        private void LogReset()
        {
            textBoxLog.Text = string.Empty;
        }

        private void Log(string message)
        {
            textBoxLog.Text = message + "\r\n" + textBoxLog.Text;
        }
    }
}
