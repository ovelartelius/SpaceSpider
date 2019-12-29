using System.Collections.Generic;
using Newtonsoft.Json;
using Spider.Models;

namespace Crawler.Models
{
    public class AnchorParsePageManifest
    {
        public string AnchorRegexPattern { get; set; }
        public string IndexFolder { get; set; }
        public string FileToParse { get; set; }
        public CheckUrlResult CheckUrlResult { get; set; }
        [JsonIgnore]
        public List<string> IgnoreLinksPatternsList { get; set; }
        [JsonIgnore]
        public List<string> ErroneousLinkPatternsList { get; set; }
    }
}
