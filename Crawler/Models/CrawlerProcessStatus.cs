namespace Crawler.Models
{
    public class CrawlerProcessStatus
    {
        public bool Reset { get; set; }

        public int MaxGetUrls { get; set; }

        public int NoGetUrls { get; set; }

        public int MaxPagesParsed { get; set; }

        public int NoPagesParsed { get; set; }

        //public string LatestCheckUrl { get; set; }

        //public string LatestAnchorParsedPage { get; set; }
    }
}
