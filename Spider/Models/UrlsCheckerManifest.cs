using System.Collections.Generic;

namespace Spider.Models
{
    public class UrlsCheckerManifest
    {
        public List<string> Urls { get; set; }
        public string NewSiteDomain { get; set; }
        public List<string> IgnorePatterns { get; set; }
        public string UserAgent { get; set; }

        public bool RunOverVpn { get; set; }
        public bool IgnoreSearch { get; set; }
        public string SearchUrl { get; set; }
    }
}
