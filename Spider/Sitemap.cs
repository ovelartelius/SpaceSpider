using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

                //var locations = document.Descendants(ns + "loc")
                //    .Select(element => element.Value)
                //    .Aggregate((s1, s2) => s1 + ";" + s2);

                if (document.Root.Name.LocalName == "sitemapindex")
                {
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

                //sitemapUrls = document.Descendants(ns + "loc")
                //    .Select(element => element.Value).ToList();
            }
            catch (Exception ex)
            {
                // Dont know what to do?
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
