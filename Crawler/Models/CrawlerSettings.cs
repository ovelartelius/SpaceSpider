﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Spider.Models;

namespace Crawler.Models
{
    public class CrawlerSettings : ISettings
    {
        public string SettingsType => this.ToString();

        public string Url { get; set; }
        public string IndexFolder { get; set; }

        public string UserAgent { get; set; }

        public string IgnoreLinksPatterns { get; set; }
        [JsonIgnore]
        public List<string> IgnoreLinksPatternsList { get; set; }

        public string IgnoreExternalHostsPatterns { get; set; }
        [JsonIgnore]
        public List<string> IgnoreExternalHostsPatternsList { get; set; }
    }
}