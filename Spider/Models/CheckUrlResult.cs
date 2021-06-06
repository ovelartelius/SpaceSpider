using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace Spider.Models
{
    public class CheckUrlResult
    {
        public CheckUrlResult()
        {
            Headers = new NameValueCollection();
            HeaderData = new Dictionary<string, string>();
            IgnoredLinks = new List<string>();
            DestinationUrls = new List<string>();
            ExternalUrls = new List<string>();
            IframeUrls = new List<string>();
            ScriptUrls = new List<string>();
        }

        public string Url { get; set; }

        public Uri Uri
        {
            get
            {
                Uri uri = null;

                try
                {
                    uri = new Uri(Url);
                }
                catch
                {
                    uri = null;
                }

                return uri;
            }
        }

        //public string StatusCodeJson { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Description { get; set; }

        public string Redirect { get; set; }

        public bool Erroneous { get; set; }

        [JsonIgnore]
        public NameValueCollection Headers { get; set; }

        public Dictionary<string, string> HeaderData { get; set; }

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

        public List<string> IframeUrls { get; set; }

        public List<string> ScriptUrls { get; set; }

        public bool IsRobotsTxt => Uri.LocalPath.ToLower() == "/robots.txt";

        public bool IsSitemapXml => Content.Contains("<urlset");

        /// <summary>
        /// True of false if the URL is the startpage/domain root ex: https://mysite.com/
        /// </summary>
        public bool IsSiteDomain => Uri.LocalPath == "/";

        public bool HasEpiserverLicenseProblem => Content.Contains("NOT FOR COMMERCIAL USE");
    }
}
