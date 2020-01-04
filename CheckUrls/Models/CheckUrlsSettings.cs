using Spider.Models;

namespace CheckUrls.Models
{
    public class CheckUrlsSettings : ISettings
    {
        public string SettingsType => this.ToString();
        public string CsvFilePath { get; set; }
        public bool FirstRowContainsTitle { get; set; }
        public string CsvFileSeperator { get; set; }
        public string IgnorePatterns { get; set; }
		public string SearchUrl { get; set; }
		public bool RunningOverVpn { get; set; }
        public bool IgnoreSearch { get; set; }
        public bool CheckSiteDomainBeforeStart { get; set; }
        public string NewSiteDomain { get; set; }
        public string Proxy { get; set; }
        public string UserAgent { get; set; }
        public string OutputDirectory { get; set; }

        public string SitemapUrl { get; set; }
    }
}
