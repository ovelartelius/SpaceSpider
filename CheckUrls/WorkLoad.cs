using System.Collections.Generic;
using CheckRequestedUrls.Models;
using Spider.Models;

namespace CheckRequestedUrls
{
    public class WorkLoad
    {
		public CheckUrlsSettings Settings { get; set; }
        public string CsvFile { get; set; }
        public string NewSiteDomain { get; set; }
        public string UserAgent { get; set; }
        public string Proxy { get; set; }
        public string SearchUrl { get; set; }
        public string OutputDirectory { get; set; }
        public List<string> Urls { get; set; }
        public List<CheckUrlResult> SpiderPageLinks { get; set; }
        public List<string> IgnorePatterns { get; set; }
        public bool RunOverVpn { get; set; }
        public bool IgnoreSearch { get; set; }
        public bool CheckDomainBeforeStart { get; set; }

		///// <summary>
		///// Method to run to check that all the data in this object is ok to start.
		///// </summary>
		///// <returns></returns>
		//public ValidationResult Validate()
		//{ 

		//}
    }
}
