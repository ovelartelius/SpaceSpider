using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spider;
using Spider.Models;

namespace CheckJsIframeOnSite
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//var siteSitemapUrl = "https://seb.se/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.se";
			var siteSitemapUrl = "https://sebgroup.com/sitemap.ashx";
			var baseSiteUrl = "https://sebgroup.com";

			var siteUrls = Spider.Sitemap.GetSitemapUrls(siteSitemapUrl);

			var list = new List<ResultModel>();
			var listCreo = new List<ResultModel>();
			var listContentSpa = new List<ResultModel>();
			var listPow = new List<ResultModel>();
			var listYoutube = new List<ResultModel>();
			var listLibsyn = new List<ResultModel>();
			var listQform = new List<ResultModel>();
			var listAppIntApps = new List<ResultModel>();

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

				result.IframeUrls = HtmlParser.GetAllIframes(result.Content);
				if (result.IframeUrls.Count > 0)
				{
					resultModel.PageUrl = pageUrl;
					resultModel.Iframes = result.IframeUrls;
					Console.WriteLine($"Url {result.Url} contains iframe {result.IframeUrls.ToString()}!");

					foreach (var iframeUrl in resultModel.Iframes)
					{
						var iframeCheckUrlManifest = new CheckUrlManifest
						{
							Url = iframeUrl,
							UserAgent = "SpaceSpider"
						};
						if (iframeUrl.StartsWith("https://www.seb.se/pow/"))
						{
							var stopHere = true;
						}
						var subResult = spider.CheckUrl(iframeCheckUrlManifest);
						var subScripts = HtmlParser.GetAllScripts(subResult.Content);
						if (subScripts != null && subScripts.Any())
						{
							subScripts = FindDtmScripts(subScripts);
							if (subScripts != null && subScripts.Any())
							{
								resultModel.SubDtmScript.AddRange(subScripts);
							}
						}
					}
					

					addResultModel = true;
				}

				//if (Regex.IsMatch(pageLink.ContentType, pattern))
				result.ScriptUrls = HtmlParser.GetAllScripts(result.Content);
				result.ScriptUrls = WashScripts(result.ScriptUrls);
				if (result.ScriptUrls.Count > 0)
				{
					resultModel.PageUrl = pageUrl;
					resultModel.Scripts = result.ScriptUrls;

					foreach (var scriptUrl in resultModel.Scripts)
					{
						var scriptCheckUrlManifest = new CheckUrlManifest
						{
							Url = scriptUrl.StartsWith("https://") ? scriptUrl : $"{baseSiteUrl}{scriptUrl}",
							UserAgent = "SpaceSpider"
						};
						var subResult = spider.CheckUrl(scriptCheckUrlManifest);
						var subScripts = HtmlParser.GetAllScripts(subResult.Content);
						subScripts = FindDtmScripts(subScripts);
						resultModel.SubDtmScript.AddRange(subScripts);
					}

					addResultModel = true;
				}

				var appIntApps = HtmlParser.GetAllScripts(result.Content, "<div data-appname=\"(.*)\" data-blockname=\"");
				if (appIntApps.Count > 0)
				{
					resultModel.PageUrl = pageUrl;
					resultModel.AppIntApps = appIntApps;
					listAppIntApps.Add(resultModel);
				}


				if (addResultModel && resultModel.Iframes != null && resultModel.Iframes[0].StartsWith("https://seb-external.creo.se"))
				{
					listCreo.Add(resultModel);
				}
				else if (addResultModel && resultModel.Iframes != null && (resultModel.Iframes[0].StartsWith("https://seb.se/pow/") || resultModel.Iframes[0].StartsWith("https://www.seb.se/pow/") || resultModel.Iframes[0].StartsWith("/pow/")))
				{
					listPow.Add(resultModel);
				}
				else if (addResultModel && resultModel.Iframes != null && (resultModel.Iframes[0].StartsWith("https://www.youtube.com/")))
				{
					listYoutube.Add(resultModel);
				}
				else if (addResultModel && resultModel.Iframes != null && (resultModel.Iframes[0].StartsWith("https://html5-player.libsyn.com")))
				{
					listLibsyn.Add(resultModel);
				}
				else if (addResultModel && resultModel.Iframes != null && (resultModel.Iframes[0].StartsWith("https://webapp.sebgroup.com/qform/")))
				{
					listQform.Add(resultModel);
				}
				else if (addResultModel && resultModel.Scripts != null && resultModel.Scripts[0].StartsWith("https://content.seb.se/"))
				{
					listContentSpa.Add(resultModel);
				}
				else if (addResultModel)
				{
					list.Add(resultModel);
				}

				Console.WriteLine(iterator);
				iterator++;
			}

			var folder = @"E:\dev\temp\Sebgroup_20210604_1600\";
			var jsonResult = Json.ToJson(list);
			File.WriteAllText($"{folder}SebScriptAndIframes.json", jsonResult);

			jsonResult = Json.ToJson(listCreo);
			File.WriteAllText($"{folder}Seb_CreoPages.json", jsonResult);

			jsonResult = Json.ToJson(listContentSpa);
			File.WriteAllText($"{folder}Seb_SpaPages.json", jsonResult);

			jsonResult = Json.ToJson(listPow);
			File.WriteAllText($"{folder}Seb_PwoPages.json", jsonResult);

			jsonResult = Json.ToJson(listYoutube);
			File.WriteAllText($"{folder}Seb_YoutubePages.json", jsonResult);

			jsonResult = Json.ToJson(listLibsyn);
			File.WriteAllText($"{folder}Seb_LibsynPages.json", jsonResult);

			jsonResult = Json.ToJson(listQform);
			File.WriteAllText($"{folder}Seb_QFormPages.json", jsonResult);

			jsonResult = Json.ToJson(listAppIntApps);
			File.WriteAllText($"{folder}Seb_AppIntApps.json", jsonResult);

			var appIntCsv = new StringBuilder();
			appIntCsv.AppendLine($"SiteUrl;AppIntName;IFrames;Script");
			foreach (var listAppIntApp in listAppIntApps)
			{
				var apps = string.Empty;
				if (listAppIntApp.AppIntApps != null && listAppIntApp.AppIntApps.Any())
				{
					apps = string.Join(",", listAppIntApp.AppIntApps);
					apps = apps.Replace("&#229;", "å");
					apps = apps.Replace("&#246;", "ö");
				}
				var scripts = string.Empty;
				if (listAppIntApp.Scripts != null && listAppIntApp.Scripts.Any())
				{
					scripts = string.Join(",", listAppIntApp.Scripts);
				}
				var frames = string.Empty;
				if (listAppIntApp.Iframes != null && listAppIntApp.Iframes.Any())
				{
					frames = string.Join(",", listAppIntApp.Iframes);
				}
				appIntCsv.AppendLine($"{listAppIntApp.PageUrl};{apps};{frames};{scripts}");
				//foreach (var script in listAppIntApp.Scripts)
				//{
				//	appIntCsv.AppendLine($"{listAppIntApp.PageUrl};{listAppIntApp.AppIntApps},{script}");
				//}

			}
			File.WriteAllText($"{folder}Seb_AppIntApps.csv", appIntCsv.ToString());

			MessageBox.Show("Done");
		}

		private List<string> FindDtmScripts(List<string> scriptList)
		{
			var newList = new List<string>();

			foreach (var scriptUrl in scriptList)
			{
				bool add = false;

				if (Regex.IsMatch(scriptUrl, @".*\/Dtm\/.*")) { add = true; }

				if (add)
				{
					newList.Add(scriptUrl);
				}
			}

			return newList;
		}

		private List<string> WashScripts(List<string> scriptList)
		{
			var newList = new List<string>();

			var currentSiteVersion = "1.0.0.672";

			foreach (var scriptUrl in scriptList)
			{
				bool ignore = false;

				if (scriptUrl == $"/Dtm/Seb-Se/71e07657e0b1660036c6a3a1ee5aeea1a106b3e8/satelliteLib-b37b01555f74a12350f20cbfc79a3936084f8f69.js?v={currentSiteVersion}") { ignore = true; }
				if (scriptUrl == $"/Static/Js/main.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/article-list/runtime.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/article-list/polyfills-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/article-list/polyfills.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/article-list/scripts.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/article-list/main.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"https://content.seb.se/ssc/adrum/adrum.js") { ignore = true; }
				//if (scriptUrl ==  $"/Static/SPA/article-list/polyfills-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/pwng-forms/runtime.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/pwng-forms/polyfills-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/pwng-forms/polyfills.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/pwng-forms/scripts.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/pwng-forms/main.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/runtime.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/runtime-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/main-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/polyfills-es5.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/polyfills.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/scripts.{currentSiteVersion}.js") { ignore = true; }
				if (scriptUrl == $"/Static/SPA/seb-form/main.{currentSiteVersion}.js") { ignore = true; }


				if (!ignore)
				{
					newList.Add(scriptUrl);
				}
			}

			return newList;
		}
	}
}
