using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Spider.Extensions;
using Spider.Models;

namespace Spider
{
    public class UrlsChecker
    {
        public List<CheckUrlResult> CheckUrls(UrlsCheckerManifest manifest, BackgroundWorker backgroundWorker)
        {
            var spider = new Spider();
            var pageLinks = new List<CheckUrlResult>();

            var i = 0;
            foreach (var url in manifest.Urls)
            {
                Console.WriteLine($"Url-{i} {url}");
                //// Check duplicates
                //if (handledUrlList.Contains(url))
                //{
                //    continue;
                //}
                //else
                //{
                //    handledUrlList.Add(url);
                //}

                try
                {
                    var newUrl = url;

                    if (!string.IsNullOrEmpty(manifest.NewSiteDomain))
                    {
                        newUrl = url.SwapHostname(manifest.NewSiteDomain);
                    }

                    //Log($"Convert {value.Url} to {newUrl}");
                    var checkUrlResult = new CheckUrlResult { Url = newUrl };

                    if (spider.ShouldUrlBeIgnored(newUrl, manifest.IgnorePatterns))
                    {
                        checkUrlResult.StatusCode = HttpStatusCode.SeeOther;
                        checkUrlResult.Ignored = true;
                    }
                    else
                    {
                        var checkUrlManifest = new CheckUrlManifest
                        {
                            Url = newUrl,
                            SourceUrls = new List<string>(),
                            UserAgent = manifest.UserAgent
                        };
                        checkUrlResult = spider.CheckUrl(checkUrlManifest);

                        if (!manifest.IgnoreSearch)
                        {
                            checkUrlResult.HistoricHits = SetHistoricalHits(manifest, spider, url);
                            //// Go a check against the search.
                            //var oldUrlPathAndQuery = WebUtility.UrlEncode(new Uri(url).PathAndQuery);
                            //dynamic something;
                            //var searchUrl = string.Empty;
                            ////var searchUrl = $"https://pws-search1-tst.sebank.se/rest/apps/stats/searchers/paths?hits=20&q={oldUrlPathAndQuery}&range!gte!@timestamp=now-30d/d";
                            //if (workLoad.RunOverVpn)
                            //{
                            //    searchUrl = "http://epicms-hostbucket.sebank.se/api/tempsearch/index?id=" + oldUrlPathAndQuery;
                            //    Console.WriteLine(searchUrl);
                            //}
                            //else
                            //{
                            //    searchUrl = workLoad.SearchUrl.Replace("{oldUrlPathAndQuery}", oldUrlPathAndQuery);
                            //    Console.WriteLine(searchUrl);
                            //}
                            //something = spider.GetExternalData(searchUrl);

                            //var totalHits = something.stats.totalHits;
                            //if (totalHits != 0)
                            //{
                            //    checkUrlResult.HistoricHits = totalHits;
                            //}
                        }
                    }

                    pageLinks.Add(checkUrlResult);

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.StartsWith("A connection attempt failed because the connected party did not properly respond after a period of time"))
                        {
                            throw new ApplicationException("TimedOut against search!!!", ex);
                        }
                    }
                    var spiderPageLink = new CheckUrlResult { Url = url, Erroneous = true, Description = ex.Message };
                    pageLinks.Add(spiderPageLink);
                }

                backgroundWorker?.ReportProgress(i);
                i++;
            }

            return pageLinks;
        }

        private int SetHistoricalHits(UrlsCheckerManifest manifest, Spider spider, string url)
        {
            // Go a check against the search.
            var oldUrlPathAndQuery = WebUtility.UrlEncode(new Uri(url).PathAndQuery);
            dynamic something;
            var searchUrl = string.Empty;
            //var searchUrl = $"https://pws-search1-tst.sebank.se/rest/apps/stats/searchers/paths?hits=20&q={oldUrlPathAndQuery}&range!gte!@timestamp=now-30d/d";
            if (manifest.RunOverVpn)
            {
                searchUrl = "http://epicms-hostbucket.sebank.se/api/tempsearch/index?id=" + oldUrlPathAndQuery;
                Console.WriteLine(searchUrl);
            }
            else
            {
                searchUrl = manifest.SearchUrl.Replace("{oldUrlPathAndQuery}", oldUrlPathAndQuery);
                Console.WriteLine(searchUrl);
            }
            something = spider.GetExternalData(searchUrl);

            var totalHits = something.stats.totalHits;
            //if (totalHits != 0)
            //{
            //    checkUrlResult.HistoricHits = totalHits;
            //}
            return totalHits;
        }
    }
}
