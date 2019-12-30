using System;
using System.Collections.Generic;
using Spider;

namespace Crawler.Data
{
    public static class DataHandler
    {
        /// <summary>
        /// Create all needed static files.
        /// </summary>
        public static void Init(string indexFolder)
        {
            IgnoredLinks.InitSource(indexFolder);
            NotAnchorParsedPages.InitSource(indexFolder);
            NotCrawled.InitSource(indexFolder);
            Crawled.InitSource(indexFolder);

            BrokenLinks.InitSource(indexFolder);
        }

        /// <summary>
        /// Delete all needed static files.
        /// </summary>
        public static void Sundown(string indexFolder)
        {
            IgnoredLinks.DeleteSource(indexFolder);
            NotAnchorParsedPages.DeleteSource(indexFolder);
            NotCrawled.DeleteSource(indexFolder);
            Crawled.DeleteSource(indexFolder);

            BrokenLinks.DeleteSource(indexFolder);
        }

        private static readonly IgnoredLinks _ignoredLinks = new IgnoredLinks();
        public static IgnoredLinks IgnoredLinks => _ignoredLinks;

        private static readonly NotAnchorParsedPages _notAnchorParsedPages = new NotAnchorParsedPages();
        public static NotAnchorParsedPages NotAnchorParsedPages => _notAnchorParsedPages;

        private static readonly NotCrawled _notCrawled = new NotCrawled();
        public static NotCrawled NotCrawled => _notCrawled;

        private static readonly Crawled _crawled = new Crawled();
        public static Crawled Crawled => _crawled;

        private static readonly CheckUrlResults _checkUrlResults = new CheckUrlResults();
        public static CheckUrlResults CheckUrlResults => _checkUrlResults;

        private static readonly BrokenLinks _brokenLinks = new BrokenLinks();
        public static BrokenLinks BrokenLinks => _brokenLinks;

    }
}
