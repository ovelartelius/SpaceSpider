using System.Collections.Generic;

namespace Spider.Models
{
    public class CheckUrlManifest
    {
        public string Url { get; set; }
        public List<string> SourceUrls { get; set; }
        public string UserAgent { get; set; }
        public string ProxyAddress { get; set; }

        public bool UseUserAgent
        {
            get { return !string.IsNullOrEmpty(UserAgent); }
        }

        public bool UseProxy
        {
            get { return !string.IsNullOrEmpty(ProxyAddress); }
        }
    }
}
