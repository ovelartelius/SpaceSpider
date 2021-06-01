using System.Collections.Generic;

namespace Spider.Models
{
    public class PsCheckUrlResult
    {
        public PsCheckUrlResult()
        {
            //Headers = new NameValueCollection();
            //HeaderData = new Dictionary<string, string>();
            IgnoredLinks = new List<string>();
            DestinationUrls = new List<string>();
            ExternalUrls = new List<string>();
        }

        public string Url { get; set; }

        public int StatusCode { get; set; }

        public string Description { get; set; }

        public string Redirect { get; set; }

        public bool Erroneous { get; set; }

        //[JsonIgnore]
        //public NameValueCollection Headers { get; set; }

        //public Dictionary<string, string> HeaderData { get; set; }

        public string Content { get; set; }

        public long Time { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }

        public string Server { get; set; }

        public int HistoricHits { get; set; }

        public bool Ignored { get; set; }

        public List<string> IgnoredLinks { get; set; }

        public List<string> ErroneousLinks { get; set; }

        public List<string> DestinationUrls { get; set; }

        public List<string> ExternalUrls { get; set; }

        public bool IsRobotsTxt { get; set; }

        public bool IsSitemapXml { get; set; }

        public bool IsSiteDomain { get; set; }

        public bool HasEpiserverLicenseProblem { get; set; }
    }
}
