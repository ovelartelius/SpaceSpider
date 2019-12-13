using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using Quartz;
using SeoSpider.Migrations;
using SeoSpider.Test2.models;
using SeoSpider.Test2.models.data;

namespace SeoSpider.Test2
{
	public class SpiderRunner : IJob
	{
		private readonly ISpiderDataProvider _data;
		private SpiderBitch _helper;

		public SpiderRunner()
		{
			_data = new SpiderDataProvider();
		}

		public void Execute(IJobExecutionContext context)
		{
			var dataMap = context.JobDetail.JobDataMap;
			var spiderConfigKeyString = dataMap.GetString("SpiderRunKey");
			var key = new Guid(spiderConfigKeyString);

			try
			{
				using (var db = new EtDataContext())
				{
					var stopWatch = new Stopwatch();
					stopWatch.Start();

					var spiderRun = db.SpiderRuns.FirstOrDefault(x => x.SpiderRunKey == key);

					if (spiderRun != null)
					{
						// Check if the run is started. If not wen need to make the run started and create the first URL to Run against.
						if (!spiderRun.IsStarted && !spiderRun.IsCompleted)
						{

							// Set the SpiderRun to started.
							spiderRun.IsStarted = true;
							spiderRun = _data.SaveSpiderRun(db, spiderRun);


						}

						// Start the helper
						_helper = new SpiderBitch(db, _data, spiderRun);

						if (db.SpiderPages.Where(x => x.SpiderRunKey == key && !x.Handled).Any())
						{
							// Grab the first not handled URL in the list
							var page = db.SpiderPages.FirstOrDefault(x => x.SpiderRunKey == key && !x.Handled);
							var pageUrl = string.Empty;
							if (page != null)
							{
								page.CheckedOut = true;
								page = _data.SaveSpiderPage(db, page);

								//pageUrl = page.Url;

								if (page.Url == "https://experiortools.com/articles")
								{
									var stop = true;
								}


								page = HandleUrl(page);

								// Serilize the Urls on the page.
								var urlsJson = JsonConvert.SerializeObject(page.SpiderPageUrls);
								page.UrlsJson = urlsJson;

								var scriptUrlsJson = JsonConvert.SerializeObject(page.ScriptUrls);
								page.ScriptUrlsJson = scriptUrlsJson;

								var linkUrlsJson = JsonConvert.SerializeObject(page.LinkUrls);
								page.LinkUrlsJson = linkUrlsJson;

								var imageUrlsJson = JsonConvert.SerializeObject(page.ImageUrls);
								page.ImageUrlsJson = imageUrlsJson;

								var otherUrlsJson = JsonConvert.SerializeObject(page.OtherUrls);
								page.OtherUrlsJson = otherUrlsJson;

								var destinationUrlsJson = JsonConvert.SerializeObject(page.DestinationUrls);
								page.DestinationUrlsJson = destinationUrlsJson;

								page.Handled = true;

								_data.SaveSpiderPage(db, page);
							}
						}
					}
					else
					{
						//logger.LogInfo(string.Format("Could not find specified spider config with key {0}", key));
						//MessageBox.Show(string.Format("Could not find specified spider config with key {0}", key));
						Console.WriteLine("Could not find specified spider config with key {0}", key);
					}

					//db.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				//logger.LogError(ex, string.Format("Spider report {0} failed.", spiderConfigKeyString));
				MessageBox.Show(string.Format("Spider report {0} failed.", spiderConfigKeyString));
			}

		}

		private models.data.SpiderPage HandleUrl(models.data.SpiderPage spiderPage)
		{
			Uri uri = null;

			try
			{
				uri = new Uri(spiderPage.Url);

				// Check if the CheckUrl exist in cache.
				models.data.SpiderPageLink spiderPageLink = _data.GetSpiderPageLink(_helper.DataContext, _helper.SpiderRun.SpiderRunId, uri.AbsoluteUri);
				if (spiderPageLink == null)

				{
					spiderPageLink =  _helper.CheckUrl(uri);
					if (spiderPageLink != null)
					{
						spiderPageLink.SpiderRunId = _helper.SpiderRun.SpiderRunId;
						spiderPageLink = _data.SaveSpiderPageLink(_helper.DataContext, spiderPageLink);
					}
				}
				spiderPage.PopulateUrlResult(spiderPageLink);

				if (!string.IsNullOrEmpty(spiderPage.Content))
				{
					spiderPage = _helper.HandleContent(spiderPage);
				}

				spiderPage.Handled = true;

				//TODO: Add MatchPattern logic.
				//	// Check if the page match the patern.
				//	if (!string.IsNullOrEmpty(workBase.MatchPattern) && !string.IsNullOrEmpty(spiderPage.Content))
				//	{
				//		if (Regex.IsMatch(spiderPage.Content, workBase.MatchPattern, RegexOptions.IgnoreCase))
				//		{
				//			spiderPage.MatchPattern = true;
				//		}
				//	}

			}
			catch (Exception ex)
			{
				spiderPage.Failed = true;
				spiderPage.FailedMessage = ex.Message;
				Console.WriteLine("The URL {0} is not valid.", spiderPage.Url);
				Console.WriteLine(ex.Message);
			}

			return spiderPage;
		}
	}
}
