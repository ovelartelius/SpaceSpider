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
			//var folder = @"E:\dev\temp\Sebse_20210621_1430\";

			var siteSitemapUrl = "https://sebgroup.com/MogulSeoManagerSitemap.aspx?https=true";
			var baseSiteUrl = "https://sebgroup.com";
			var folder = @"E:\dev\temp\Sebgroup_20210630_1130\";

			//var siteSitemapUrl = "https://seb.ie/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.ie";
			//var folder = @"E:\dev\temp\Sebie_20210621_1430\";

			//var siteSitemapUrl = "https://seb.fi/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.fi";
			//var folder = @"E:\dev\temp\Sebfi_20210621_1430\";

			//var siteSitemapUrl = "https://seb.no/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.np";
			//var folder = @"E:\dev\temp\Sebno_20210621_1430\";

			//var siteSitemapUrl = "https://seb.dk/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.dk";
			//var folder = @"E:\dev\temp\Sebdk_20210621_1430\";

			//var siteSitemapUrl = "https://seb.de/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://seb.de";
			//var folder = @"E:\dev\temp\Sebde_20210621_1430\";

			//var siteSitemapUrl = "https://sebgroup.lu/MogulSeoManagerSitemap.aspx?https=true";
			//var baseSiteUrl = "https://sebgroup.lu";
			//var folder = @"E:\dev\temp\Sebgrouplu_20210621_1430\";

			if (!Directory.Exists(folder))
			{
				// Create the folder if dows not exist.
				Directory.CreateDirectory(folder);
			}

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

			var scriptCsv = new StringBuilder();
			scriptCsv.AppendLine($"SiteUrl;IFrames;Script");


			var jsonResult = Json.ToJson(list);
			File.WriteAllText($"{folder}SebScriptAndIframes.json", jsonResult);
			AppendSb(list, scriptCsv);


			jsonResult = Json.ToJson(listCreo);
			File.WriteAllText($"{folder}Seb_CreoPages.json", jsonResult);
			AppendSb(listCreo, scriptCsv);

			jsonResult = Json.ToJson(listContentSpa);
			File.WriteAllText($"{folder}Seb_SpaPages.json", jsonResult);
			AppendSb(listContentSpa, scriptCsv);

			jsonResult = Json.ToJson(listPow);
			File.WriteAllText($"{folder}Seb_PwoPages.json", jsonResult);
			AppendSb(listPow, scriptCsv);

			jsonResult = Json.ToJson(listYoutube);
			File.WriteAllText($"{folder}Seb_YoutubePages.json", jsonResult);
			AppendSb(listYoutube, scriptCsv);

			jsonResult = Json.ToJson(listLibsyn);
			File.WriteAllText($"{folder}Seb_LibsynPages.json", jsonResult);
			AppendSb(listLibsyn, scriptCsv);

			jsonResult = Json.ToJson(listQform);
			File.WriteAllText($"{folder}Seb_QFormPages.json", jsonResult);
			AppendSb(listQform, scriptCsv);

			jsonResult = Json.ToJson(listAppIntApps);
			File.WriteAllText($"{folder}Seb_AppIntApps.json", jsonResult);
			AppendSb(listAppIntApps, scriptCsv);
			File.WriteAllText($"{folder}Seb_scripts.csv", scriptCsv.ToString());

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

		private void AppendSb(List<ResultModel> list, StringBuilder stringBuilder)
		{
			foreach (var listScript in list)
			{
				var scripts = string.Empty;
				if (listScript.Scripts != null && listScript.Scripts.Any())
				{
					scripts = string.Join(",", listScript.Scripts);
				}
				var frames = string.Empty;
				if (listScript.Iframes != null && listScript.Iframes.Any())
				{
					frames = string.Join(",", listScript.Iframes);
				}
				stringBuilder.AppendLine($"{listScript.PageUrl};{frames};{scripts}");
			}
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

			var currentSiteVersion = "1.0.0.708";

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
				//SEB
				if (scriptUrl == $"https://sebgroup.com/public-content/static/js/main.js") { ignore = true; }
				if (scriptUrl == $"/UI/V2/scripts/minified/mainheader.min.js?v=051021041114") { ignore = true; }
				if (scriptUrl == $"/Dtm/Seb-Xx/71e07657e0b1660036c6a3a1ee5aeea1a106b3e8/satelliteLib-0fb17ed6fb94a3cfcedc625cab4cdfe61592d029.js") { ignore = true; }
				if (scriptUrl == $"/UI/V2/scripts/Libraries/jquery-1.12-stable.min.js") { ignore = true; }
				if (scriptUrl == $"/UI/V2/scripts/minified/main.min.js?v=051021041114") { ignore = true; }
				if (scriptUrl == $"/public-content/static/js/main.js") { ignore = true; }
				if (scriptUrl == $"/UI/V2/scripts/minified/videobundle.min.js?d=210510161114") { ignore = true; }
				if (scriptUrl == $"/UI/V2/scripts/minified/mediabundle.min.js?d=210510161114") { ignore = true; }
				
				if (!ignore)
				{
					newList.Add(scriptUrl);
				}
			}

			return newList;
		}
	}
}
