using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CheckUrls.Models;
using Spider.Models;

namespace CheckUrls
{
    public class ResultReporter
    {
        private List<string> _resultLog = new List<string>();

        public List<string> CreateResult(WorkLoad workLoad)
        {
            var listOf200Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOf400Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.NotFound).OrderBy(x => x.Url).ToList();

            var listOf400Missing = new List<CheckUrlResult>();
            var listOf400NotMissing = new List<CheckUrlResult>();
            if (workLoad.IgnoreSearch)
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

            var listOf500Response = workLoad.SpiderPageLinks.Where(x => x.StatusCode == HttpStatusCode.InternalServerError || x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOf301Response = workLoad.SpiderPageLinks.Where(x => (x.StatusCode == HttpStatusCode.MovedPermanently || x.StatusCode == HttpStatusCode.Found) && !x.Erroneous).OrderBy(x => x.Url).ToList();

            var listOfOthers = workLoad.SpiderPageLinks.Where(x => x.StatusCode != HttpStatusCode.MovedPermanently && x.StatusCode != HttpStatusCode.Found && x.StatusCode != HttpStatusCode.InternalServerError && x.StatusCode != HttpStatusCode.NotFound && x.StatusCode != HttpStatusCode.OK).OrderBy(x => x.Url).ToList();

            var listOfIgnored = workLoad.SpiderPageLinks.Where(x => x.Ignored).OrderBy(x => x.Url).ToList();

            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");

            workLoad.OutputDirectory = workLoad.OutputDirectory + "\\" + dateTimeString;
            if (!Directory.Exists(workLoad.OutputDirectory))
            {
                Directory.CreateDirectory(workLoad.OutputDirectory);
            }

            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(workLoad.CsvFile))
            {
                sb.AppendLine($"CSV file - {workLoad.CsvFile} {dateTimeString}");
            }
            else
            {
                sb.AppendLine($"Sitemap - {workLoad.SitemapUrl} {dateTimeString}");
            }
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
            SaveToExcel(dateTimeString, "200", sb200, workLoad.OutputDirectory);

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
            SaveToExcel(dateTimeString, "301", sb301, workLoad.OutputDirectory);

            if (workLoad.IgnoreSearch)
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
                SaveToExcel(dateTimeString, "404Missing", sb404Missing, workLoad.OutputDirectory);
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
                SaveToExcel(dateTimeString, "404Missing", sb404Missing, workLoad.OutputDirectory);

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
                SaveToExcel(dateTimeString, "404", sb404, workLoad.OutputDirectory);
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
            SaveToExcel(dateTimeString, "500", sb500, workLoad.OutputDirectory);

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
            SaveToExcel(dateTimeString, "Ignored", sbIgnored, workLoad.OutputDirectory);

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
            SaveToExcel(dateTimeString, "Others", sbOthers, workLoad.OutputDirectory);

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Rawdata:");
            sb.AppendLine($"Url,StatusCode,Description,HistoricHits,Erroneous,ResponseTime");
            var sbRawData = new StringBuilder();
            sbRawData.AppendLine($"Url;StatusCode;Description;HistoricHits;Erroneous,ResponseTime");
            foreach (var item in workLoad.SpiderPageLinks)
            {
                sb.AppendLine($"{item.Url},{item.StatusCode},{item.Description},{item.HistoricHits},{item.Erroneous};{item.Time}");
                sbRawData.AppendLine($"{item.Url};{item.StatusCode};{item.Description};{item.HistoricHits};{item.Erroneous};{item.Time}");
            }
            SaveToExcel(dateTimeString, "RawData", sbRawData, workLoad.OutputDirectory);

            CreateResultFile(sb, dateTimeString, workLoad.OutputDirectory);

            return _resultLog;
        }

        private void SaveToExcel(string dateTimeString, string extraFileName, StringBuilder sb, string outputDirectory)
        {
            string filePath = $"{outputDirectory}\\Result_{dateTimeString}_{extraFileName}.csv";

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

        private void CreateResultFile(StringBuilder sb, string dateTime, string outputDirectory)
        {
            var filePath = $"{outputDirectory}\\Result_{dateTime}.txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Log($"Deleted old file {filePath}");
            }

            //this code section write stringbuilder content to physical text file.
            using (var swriter = File.CreateText(filePath))
            {
                swriter.Write(sb.ToString());
            }
            Log($"Created result file {filePath}");
        }

        private void Log(string message)
        {
            _resultLog.Add(message);
        }
    }
}
