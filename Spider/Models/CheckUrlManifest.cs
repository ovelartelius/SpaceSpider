using System.Collections.Generic;

namespace Spider.Models
{
    public class CheckUrlManifest
    {
        public CheckUrlManifest()
        {
            ContentTypesToDownload = new List<string>();
            ContentTypesToDownload.Add("text/");
            ContentTypesToDownload.Add("application/javascript");
        }

        public string Url { get; set; }

        public List<string> SourceUrls { get; set; }
        
        public string UserAgent { get; set; }
        
        public string ProxyAddress { get; set; }

        public List<string> ContentTypesToDownload { get; set; }

        // The folder where the index will be stored.
        public string IndexFolder { get; set; }

        public bool UseUserAgent
        {
            get { return !string.IsNullOrEmpty(UserAgent); }
        }

        public bool UseProxy
        {
            get { return !string.IsNullOrEmpty(ProxyAddress); }
        }

        public CheckUrlResult CheckUrlResult { get; set; }
    }
}
