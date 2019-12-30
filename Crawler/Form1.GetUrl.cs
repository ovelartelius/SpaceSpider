using Spider.Models;
using System.ComponentModel;
using System.Net;
using Crawler.Data;
using Crawler.Models;

namespace Crawler
{
    partial class Form1
    {
        private void backgroundWorkerGetUrl_DoWork(object sender, DoWorkEventArgs e)
        {
            var checkUrlManifest = e.Argument as CheckUrlManifest;

            var spider = new Spider.Spider();
            var checkUrlResult = spider.CheckUrl(checkUrlManifest);

            if (checkUrlResult.Url == "https://seb.no/privat")
            {
                var stop = true;
            }

            if (checkUrlResult.StatusCode == HttpStatusCode.NotFound)
            {
                DataHandler.BrokenLinks.Add(checkUrlManifest.IndexFolder, new RelatedLink { DestinationUrl = "", SourceUrl = checkUrlResult.Url });
            }

            checkUrlManifest.CheckUrlResult = checkUrlResult;

            
            e.Result = checkUrlManifest;
        }

        private void backgroundWorkerGetUrl_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Receive the result from DoWork, and display it.
            var checkUrlManifest = e.Result as CheckUrlManifest;

            var file = DataHandler.CheckUrlResults.Save(checkUrlManifest.IndexFolder, checkUrlManifest.CheckUrlResult);

            if (!DataHandler.NotAnchorParsedPages.Exist(checkUrlManifest.IndexFolder, file))
            {
                DataHandler.NotAnchorParsedPages.Add(checkUrlManifest.IndexFolder, file);
            }

            DataHandler.NotCrawled.Remove(checkUrlManifest.IndexFolder, checkUrlManifest.CheckUrlResult.Url);
            DataHandler.Crawled.Add(checkUrlManifest.IndexFolder, checkUrlManifest.CheckUrlResult.Url);
        }
    }
}
