using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Spider;
using Spider.Models;

namespace FindTildeLinksOnSite
{
	class Program
	{
		static void Main(string[] args)
		{
			//var siteSitemapUrl = "https://sebgroup.com/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://sebgroup.com";
			//var folder = @"E:\dev\temp\Sebgroup_tildelinks_20220113_1130\";

			var siteSitemapUrl = "https://seb.ie/MogulSeoManagerSitemap.aspx?https=true";
			var baseSiteUrl = "https://seb.ie";
			var folder = @"E:\dev\temp\Tildelinks\";

			if (!Directory.Exists(folder))
			{
				// Create the folder if dows not exist.
				Directory.CreateDirectory(folder);
			}

			var siteUrls = Spider.Sitemap.GetSitemapUrls(siteSitemapUrl);

			var listTildeLinks = new List<ResultModel>();

			var spider = new Spider.Spider();
			var iterator = 0;
			Console.WriteLine($"Found {siteUrls.Count} pages to check.");
			foreach (var pageUrl in siteUrls)
			{
				var checkUrlManifest = new CheckUrlManifest
				{
					Url = pageUrl,
					UserAgent = "SpaceSpider"
				};

				var result = spider.CheckUrl(checkUrlManifest);

				var resultModel = new ResultModel();
				var addResultModel = false;

				result.TildeLinks = HtmlParser.GetAllTildeLinks(result.Content);
				if (result.TildeLinks.Count > 0)
				{
					resultModel.PageUrl = pageUrl;
					resultModel.TildeLinks = result.TildeLinks;
					Console.WriteLine(".");
					Console.WriteLine($"Url {result.Url} contains {result.TildeLinks[0].ToString()}");

					addResultModel = true;
				}
				else
				{
					Console.Write(".");
				}

				if (addResultModel)
				{
					listTildeLinks.Add(resultModel);
				}
				
				iterator++;
			}

			var linkCsv = new StringBuilder();
			linkCsv.AppendLine($"SiteUrl;TildeLink");
			foreach (var resultModel in listTildeLinks)
			{
				foreach (var link in resultModel.TildeLinks)
				{
					linkCsv.AppendLine($"{resultModel.PageUrl};{link}");
				}
				
			}
			File.WriteAllText($"{folder}TildeLinks.csv", linkCsv.ToString());

			Console.WriteLine("Done");
		}

	}
}
