using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestedUrls
{
    public class WorkLoad
    {
        public string CsvFile { get; set; }
        public string NewSiteDomain { get; set; }
        public string UserAgent { get; set; }
        public string Proxy { get; set; }
        public string SearchUrl { get; set; }
        public string OutputDirectory { get; set; }
        //public List<CsvSimpleUrl> Urls { get; set; }
        public List<string> Urls { get; set; }
        public List<SpiderPageLink> SpiderPageLinks { get; set; }
        public List<string> IgnorePatterns { get; set; }

        public bool RunOverVpn { get; set; }
        public bool IgnoreSearch { get; set; }
        public bool CheckDomainBeforeStart { get; set; }
    }
}
