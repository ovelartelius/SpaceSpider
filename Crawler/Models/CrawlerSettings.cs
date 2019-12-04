using Spider.Models;

namespace Crawler.Models
{
    public class CrawlerSettings : ISettings
    {
        public string SettingsType => this.ToString();
        public string Url { get; set; }
    }
}
