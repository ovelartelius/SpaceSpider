using Crawler.Data;
using Crawler.Models;
using Spider;
using Spider.Extensions;
using System;
using System.ComponentModel;

namespace Crawler
{
    partial class Form1
    {
        private void backgroundWorkerAnchorParsePage_DoWork(object sender, DoWorkEventArgs e)
        {
            var anchorParsePageManifest = e.Argument as AnchorParsePageManifest;

            if (anchorParsePageManifest == null)
            {
                throw new ApplicationException("No AnchorParsePageManifest is sent.");
            }

            anchorParsePageManifest.CheckUrlResult = DataHandler.CheckUrlResults.Load(anchorParsePageManifest.FileToParse);

            if (!string.IsNullOrEmpty(anchorParsePageManifest.CheckUrlResult.Content))
            {

                var links = HtmlParser.GetAllAnchors(anchorParsePageManifest.CheckUrlResult.Content);
                //var matches = Regex.Matches(anchorParsePageManifest.CheckUrlResult.Content, anchorParsePageManifest.AnchorRegexPattern, RegexOptions.IgnoreCase);

                foreach (var linkUrl in links)  //foreach (Match match in matches)
                {
                    //var link = match.Groups[1].ToString();
                    var link = linkUrl;

                    // Check if the link matching any ignore pattern.
                    if (ShouldLinkBeIgnored(anchorParsePageManifest, link))
                    {
                        //Console.WriteLine($"Ignored URL {link}.");
                        if (!anchorParsePageManifest.CheckUrlResult.IgnoredLinks.Contains(link))
                        {
                            anchorParsePageManifest.CheckUrlResult.IgnoredLinks.Add(link);
                        }
                        link = string.Empty;
                    }

                    // Check if the link startswith "~/link/".
                    if (link.MatchAnyPattern(anchorParsePageManifest.ErroneousLinkPatternsList))
                    {
                        if (!anchorParsePageManifest.CheckUrlResult.ErroneousLinks.Contains(link))
                        {
                            anchorParsePageManifest.CheckUrlResult.ErroneousLinks.Add(link);
                        }
                        // Clear the link so that we stop handle the page.
                        link = string.Empty;
                    }


                    if (link.StartsWith("/"))
                    {
                        // We need to add the host for the link.
                        link = $"{anchorParsePageManifest.CheckUrlResult.Uri.Scheme}://{anchorParsePageManifest.CheckUrlResult.Uri.Host}{link}";
                    }

                    //// We need to make sure that we stay on the same host.
                    //if (!string.IsNullOrEmpty(link) && !link.StartsWith("?") && !link.StartsWith(string.Format("{0}://{1}", workingPage.Uri.Scheme, workingPage.Uri.Host)))
                    //{
                    //    var cachedPageLink = Cache.FirstOrDefault(x => x.Url == link);
                    //    if (cachedPageLink == null)
                    //    {
                    //        //var urlCheckResult = new UrlCheckResult();
                    //        //try
                    //        //{
                    //        //	var linkUri = new Uri(link);
                    //        //	urlCheckResult = CheckUrl(linkUri);
                    //        //}
                    //        //catch (UriFormatException uriEx)
                    //        //{
                    //        //	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
                    //        //}
                    //        //catch (NotSupportedException notEx)
                    //        //{
                    //        //	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
                    //        //}

                    //        ////if (urlCheckResult.StatusCode != HttpStatusCode.BadRequest)
                    //        ////{
                    //        ////	urlCheckResult = CheckUrl(new Uri(linkUri));
                    //        ////}

                    //        //var pageLink = new WorkingPageLink { Url = link, StatusCode = urlCheckResult.StatusCode };
                    //        //workingPage.DestinationUrls.Add(pageLink);
                    //        //Cache.Add(pageLink);
                    //    }
                    //    else
                    //    {
                    //        workingPage.DestinationUrls.Add(cachedPageLink);
                    //    }
                    //    link = string.Empty;
                    //}

                    if (!string.IsNullOrEmpty(link))
                    {
                        if (link.StartsWith("?"))
                        {
                            if (string.IsNullOrEmpty(anchorParsePageManifest.CheckUrlResult.Uri.Query))
                            {
                                link = $"{anchorParsePageManifest.CheckUrlResult.Uri.AbsoluteUri}{link}";
                            }
                            else
                            {
                                link = anchorParsePageManifest.CheckUrlResult.Uri.AbsoluteUri.Replace(
                                    anchorParsePageManifest.CheckUrlResult.Uri.Query, link);
                            }
                            
                        }

                        if (link.Contains("#") && !link.Contains("/#/"))
                        {
                            var linkUri = new Uri(link);

                            if (!string.IsNullOrEmpty(linkUri.Fragment) && string.IsNullOrEmpty(anchorParsePageManifest.CheckUrlResult.Uri.Fragment))
                            {
                                if (!anchorParsePageManifest.CheckUrlResult.IgnoredLinks.Contains(link))
                                {
                                    anchorParsePageManifest.CheckUrlResult.IgnoredLinks.Add(link);
                                }

                                link = "";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(link))
                    {
                        var uri = anchorParsePageManifest.CheckUrlResult.Uri;

                        if (!link.StartsWith($"{uri.Scheme}://{uri.Host}"))
                        {
                            // External link
                            if (!anchorParsePageManifest.CheckUrlResult.ExternalUrls.Contains(link))
                            {
                                anchorParsePageManifest.CheckUrlResult.ExternalUrls.Add(link);
                            }
                        }
                        else
                        {
                            if (!anchorParsePageManifest.CheckUrlResult.DestinationUrls.Contains(link))
                            {
                                anchorParsePageManifest.CheckUrlResult.DestinationUrls.Add(link);
                            }
                            if (!DataHandler.Crawled.Exist(anchorParsePageManifest.IndexFolder, link))
                            {
                                DataHandler.NotCrawled.Add(anchorParsePageManifest.IndexFolder, link);
                            }
                        }
                    }
                }
            }

            e.Result = anchorParsePageManifest;
        }

        private void backgroundWorkerAnchorParsePage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Receive the result from DoWork, and display it.
            var anchorParsePageManifest = e.Result as AnchorParsePageManifest;

            DataHandler.CheckUrlResults.Save(anchorParsePageManifest.IndexFolder, anchorParsePageManifest.CheckUrlResult);
        }

        private bool ShouldLinkBeIgnored(AnchorParsePageManifest manifest, string link)
        {
            var ignore = false;

            if (DataHandler.IgnoredLinks.Exist(manifest.IndexFolder, link))
            {
                ignore = true;
            }
            else if (link.MatchAnyPattern(manifest.IgnoreLinksPatternsList))
            {
                DataHandler.IgnoredLinks.Add(manifest.IndexFolder, link);
                //AddLinkToIgnoreLinks(manifest.IndexFolder, link);
                ignore = true;
            }

            return ignore;
        }
    }
}
