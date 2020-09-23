using System.Collections.Generic;

namespace CsvMerger.Models
{
	public class WorkLoad
	{
		public CsvMergerSettings Settings { get; set; }
		public string CsvFile1 { get; set; }
		public string CsvFile2 { get; set; }
		//public string CsvFile { get; set; }
		public string NewSiteDomain { get; set; }
		//public string UserAgent { get; set; }
		//public string Proxy { get; set; }
		//public string SearchUrl { get; set; }
		public string OutputDirectory { get; set; }
		
		//public List<CheckUrlResult> SpiderPageLinks { get; set; }
		public List<string> IgnorePatterns { get; set; }
		//public bool RunOverVpn { get; set; }
		//public bool IgnoreSearch { get; set; }
		//public bool CheckDomainBeforeStart { get; set; }

		public List<string> Urls { get; set; }

		//public string SitemapUrl { get; set; }

		///// <summary>
		///// Method to run to check that all the data in this object is ok to start.
		///// </summary>
		///// <returns></returns>
		//public ValidationResult Validate()
		//{ 

		//}
	}
}
