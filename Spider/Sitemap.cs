using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Spider
{
    public static class Sitemap
    {
        public static List<string> GetSitemapUrls(string url)
        {
            var sitemapUrls = new List<string>();
            try
            {
                Uri sitemapUri;
                if (!Uri.TryCreate(url, UriKind.Absolute, out sitemapUri)) throw new Exception("Unable to parse URI.");

                var document = XDocument.Load(url);
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

                if (document.Root.Name.LocalName == "sitemapindex")
                {
                    // The sitemap is a document that is split up in more than one file. Lets get them all.
                    var sitemaps = document.Descendants(ns + "loc").Select(element => element.Value).ToList();
                    foreach (var sitemap in sitemaps)
                    {
                        var sitemapDocument = XDocument.Load(sitemap);
                        sitemapUrls.AddRange(GetUrlsFromDocument(sitemapDocument));
                    }
                }
                else
                {
                    sitemapUrls.AddRange(GetUrlsFromDocument(document));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Could not handle the sitemap {url}");
            }

            return sitemapUrls;
        }

        private static List<string> GetUrlsFromDocument(XDocument document)
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            return document.Descendants(ns + "loc").Select(element => element.Value).ToList();

        }
    }
}
