using Spider.Models;

namespace LinkjuiceCreator.Models
{
    public class LinkjuiceCreatorSettings : ISettings
    {
        public string SettingsType => this.ToString();
        public string CsvFilePath { get; set; }
        public bool FirstRowContainsTitle { get; set; }
        public string CsvFileSeperator { get; set; }
        public string PageVariableHeaderName { get; set; }
        public bool CleanUpNoneFoundPageIds { get; set; }
        public bool RunningOverVpn { get; set; }
        public bool CheckSiteDomainBeforeStart { get; set; }
        public string NewSiteDomain { get; set; }
        public string Proxy { get; set; }
        public string UserAgent { get; set; }
        public string OutputDirectory { get; set; }
    }
}
