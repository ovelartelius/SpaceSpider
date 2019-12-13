using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SeoSpider.Test2.models.data;

namespace SeoSpider.Test2
{
	public class SpiderBitch
	{
		private readonly IEtDataContext _db;
		private readonly ISpiderDataProvider _data;
		private readonly SpiderRun _spiderRun;

		public EtDataContext DataContext { get { return _db as EtDataContext; } }
		public ISpiderDataProvider DataProvider { get { return _data; } }
		public SpiderRun SpiderRun { get { return _spiderRun; } }

		public SpiderBitch(IEtDataContext db, ISpiderDataProvider data, SpiderRun spiderRun)
		{
			_db = db;
			_data = data;
			_spiderRun = spiderRun;
		}

		public bool IsExternal(models.data.SpiderPage spiderPage, string link)
		{
			if (link.StartsWith("/global"))
			{
				//TODO: Kontrollera varför denna inte kontrolleras. Känns som att det finns en tanke som inte har avslutats rätt.
				var stopMe = true;
			}

			var isExternal = false;
			try
			{
				var linkUri = new Uri(link);
				if (!linkUri.Host.EndsWith(spiderPage.Uri.Host))
				{
					isExternal = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not check if link  " + link + " is external. Error: " + ex.Message);
			}

			return isExternal;
		}

		public string CleanLink(models.data.SpiderPage spiderPage, string link, bool excludeExternalHosts = false)
		{
			// if the link starts with // we will add the same scheme on the link as the existing scheme for the site.
			if (link.StartsWith("//"))
			{
				link = string.Format("{0}:{1}", spiderPage.Uri.Scheme, link);
			}

			if (link.StartsWith("/"))
			{
				// We need to add the host for the link.
				link = string.Format("{0}://{1}{2}", spiderPage.Uri.Scheme, spiderPage.Uri.Host, link);
			}

			// Handle links with ../ in the start.
			if (link.StartsWith("../"))
			{
				link = HandleDotDotDashLink(spiderPage, link);
			}

			// if the link is same as the spiderPage we should cancel.
			if (link == spiderPage.Url)
			{
				link = string.Empty;
			}

			// If the link is equal to the querystring for the workingpage it is a link to it self.
			if (link.StartsWith("?") && spiderPage.Uri.Query == link)
			{
				link = string.Empty;
			}

			if (link.StartsWith("?") && !spiderPage.Uri.AbsoluteUri.Contains("?"))
			{
				link = string.Format("{0}{1}", spiderPage.Uri.AbsoluteUri, link);
			}
			else if (link.StartsWith("?") && spiderPage.Uri.AbsoluteUri.Contains("?"))
			{
				var linkUri = new Uri(spiderPage.Uri.AbsoluteUri.Replace(spiderPage.Uri.Query, link));

				link = linkUri.AbsoluteUri;
			}

			if (excludeExternalHosts && link.StartsWith("http"))
			{
				try
				{
					var linkUri = new Uri(link);
					if (!linkUri.Host.EndsWith(spiderPage.Uri.Host))
					{
						// the link is not the same as the spiderPage host. We will exclude the link.
						link = string.Empty;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Could not clean link " + link + ". Error: " + ex.Message);
				}
			}

			return link;
		}

		public string HandleDotDotDashLink(models.data.SpiderPage spiderPage, string link)
		{
			if (link.StartsWith("../"))
			{
				try
				{
					var tempPageUri = new Uri(spiderPage.Uri.AbsoluteUri);
					var itterator = 0;
					while (link.StartsWith("../"))
					{
						// Remove the first ../ from the link
						link = link.Substring(3);

						// Remove the last segment from the tempPageUri.
						var tempPageUrl = tempPageUri.AbsoluteUri.Substring(0, tempPageUri.AbsoluteUri.Length - tempPageUri.Segments[tempPageUri.Segments.Count() - 1].Length);
						tempPageUri = new Uri(tempPageUrl);

						// If there is no ../ left in the link we should add the link to the tempUrl
						if (!link.StartsWith("../"))
						{
							tempPageUrl = tempPageUri.AbsoluteUri + link;
							tempPageUri = new Uri(tempPageUrl);
						}

						itterator++;
						if (itterator > 10)
						{
							// We will stop trying.
							link = string.Empty;
						}
					}
					link = tempPageUri.AbsoluteUri;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Chould not handle ../ link (" + link + ")." + ex.Message);
					link = string.Empty;
				}
			}
			return link;
		}

		public List<SpiderPageLink> CheckResources(models.data.SpiderPage spiderPage, string pattern, List<SpiderPageLink> resourceList, int linkType)
		{
			var content = spiderPage.Content;

			var matches = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);

			foreach (Match match in matches)
			{
				var link = match.Groups[1].ToString();

				// Check if the link is a resource that are used is external and starts without https://
				// Used to check when you should go to https and wants to find all external resources that are ref via http://
				var checkExtLink = false;

				// Check if the resource is the right type to check
				switch (linkType)
				{
					case 1: // Script
						checkExtLink = true;
						break;
					case 2: // Link
						checkExtLink = true;
						break;
					case 3: // Image
						checkExtLink = true;
						break;
					case 4: // Other
						checkExtLink = false;
						break;
				}

				if (checkExtLink)
				{
					// Check if external resource
					RunExternalResourceCheck(spiderPage, link);
				}

				//_extResources

				// Check if the link matching any ignore pattern.
				link = IgnoreLinkCheck(link);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = CleanLink(spiderPage, link, _spiderRun.Settings.ExcludeExternalHosts);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = IgnoreExternalHostCheck(link);
				if (string.IsNullOrEmpty(link)) { continue; }

				// Check if the link startswith "~/link/".
				if (link.StartsWith("~/") || link.Contains("~/link/"))
				{
					var spiderPageLink = _data.GetSpiderPageLink(DataContext, _spiderRun.SpiderRunId, link);

					if (spiderPageLink == null)
					{
						spiderPageLink = resourceList.FirstOrDefault(x => x.Url == link);

						if (spiderPageLink == null)
						{
							spiderPageLink = new SpiderPageLink
							{
								Url = link,
								Description = "EpiServer erroneous link.",
								StatusCode = HttpStatusCode.InternalServerError,
								Erroneous = true
							};
							resourceList.Add(spiderPageLink);
						}
					}
					else
					{
						if (!resourceList.Any(x => x.Url == link))
						{
							resourceList.Add(spiderPageLink);
						}
					}
					// Clear the link so that we stop handle the page.
					link = string.Empty;
					continue;
				}
				if (string.IsNullOrEmpty(link)) { continue; }

				if (!string.IsNullOrEmpty(link))
				{
					Uri linkUri;

					try
					{
						linkUri = new Uri(link);
					}
					catch
					{
						link = string.Format("{0}://{1}{2}", spiderPage.Uri.Scheme, spiderPage.Uri.Host, link);
						linkUri = new Uri(link);
					}

					var spiderPageLink = CheckUrl(linkUri);
					if (spiderPageLink != null)
					{
						spiderPageLink.SpiderRunId = _spiderRun.SpiderRunId;
						_data.SaveSpiderPageLink(DataContext, spiderPageLink);
					}

					// Check if the resource page link exist in the resourceList. If not, add.
					if (resourceList.All(x => x.Url != linkUri.AbsoluteUri))
					{
						resourceList.Add(spiderPageLink);
					}
				}
			}

			return resourceList;
		}

		public void RunExternalResourceCheck(models.data.SpiderPage spiderPage, string link)
		{
			if (IsExternal(spiderPage, link))
			{
				if (link.StartsWith("http://") || link.StartsWith("//"))
				{
					var spiderExtLink = new SpiderExtLink { PageUrl = spiderPage.Url, SpiderRunId = spiderPage.SpiderRunId, ExtLinkUrl = link };

					// Check if it exist
					if (_data.GetSpiderExtLink(DataContext, spiderExtLink.SpiderRunId, spiderExtLink.PageUrl, spiderExtLink.ExtLinkUrl) == null)
					{
						_data.SaveSpiderExtLink(DataContext, spiderExtLink);
					}

					//var pageExtLinkComparer = new PageExtLinkComparer();
					//var pageExtLink = new PageExtLink { PageUrl = spiderPage.Url, ExtLinkUrl = link };
					//if (!_extResources.Contains(pageExtLink, pageExtLinkComparer))
					//{
					//	_extResources.Add(pageExtLink);
					//}

				}
			}
		}

		public models.data.SpiderPageLink CheckUrl(Uri uri, string sourceUrl = "")
		{
			var result = new models.data.SpiderPageLink
			{
				StatusCode = HttpStatusCode.BadRequest,
				Url = uri.AbsoluteUri
			};

			Console.WriteLine("CheckUrl:" + uri.AbsoluteUri);

			HttpWebResponse webResponse = null;
			try
			{
				var webRequest = (HttpWebRequest)WebRequest.Create(uri);
				webRequest.AllowAutoRedirect = false;

				// Start timer to check how long time it takes to get response.
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				webResponse = (HttpWebResponse)webRequest.GetResponse();
				result.Size = webResponse.ContentLength;
				result.ContentType = webResponse.ContentType;
				result.StatusCode = webResponse.StatusCode;

				// Only download content for ContentType that are related to HTml, text, xml etc.
				if (result.ContentType.StartsWith("text/"))
				{
					var responseStream = webResponse.GetResponseStream();
					if (responseStream != null)
					{
						using (var reader = new StreamReader(responseStream, Encoding.Default))
						{
							result.Content = reader.ReadToEnd();
						}
					}
				}

				stopWatch.Stop();
				// Report the response time.
				result.Time = stopWatch.ElapsedMilliseconds;

				// Check if we got activity that we need to lookup more.
				if (result.StatusCode == HttpStatusCode.MovedPermanently || result.StatusCode == HttpStatusCode.Found ||
					result.StatusCode == HttpStatusCode.TemporaryRedirect)
				{
					// Get more information from the response.
					result.Description = webResponse.Headers["Location"];

					var locationUri = new Uri(uri, webResponse.Headers["Location"]);
					// Check if we are in a nestled loop
					if (locationUri == uri || (!string.IsNullOrEmpty(sourceUrl) && locationUri == new Uri(sourceUrl)))
					{
						// Nestled redirection loop.
						result.Erroneous = true;
						result.Description = string.Format("Nestled redirection loop. Redirect to it self ({0}).", locationUri);
					}
					else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
					{
						// We have been sent to a EPi server login page. We do not have access to this page.
						result.Erroneous = true;
						result.Description = string.Format("Redirect user to EPi Server login page. ({0}).", locationUri);
					}
					else
					{
						// We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
						//var locationUri = new Uri(uri, webResponse.Headers["Location"]);
						var locationResult = CheckUrl(locationUri, uri.AbsoluteUri);
						if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
							locationResult.StatusCode == HttpStatusCode.InternalServerError)
						{
							result.Erroneous = true;
							result.Description =
								string.Format("Redirected to {0}. Erroneous redirection. Page {0} response a 404/410/500 status.",
									webResponse.Headers["Location"]);
						}

						// We should also check if the location conatins aspxerrorpath
						if (result.Erroneous == false && result.Description.Contains("aspxerrorpath="))
						{
							// We have been redirected to a ASPX custom error page.
							result.Erroneous = true;
							result.Description =
								string.Format("Redirected to {0} ASPX custom error page. Page {0} response a soft 500 status.",
									webResponse.Headers["Location"]);
						}

						if (locationResult.Erroneous)
						{
							// Something is wrong but we have not classsified what. Report back the information we got from server.
							result.Erroneous = true;
							result.Description = locationResult.Description;
						}
					}
				}
			}
			catch (WebException webEx)
			{
				if (webEx.Message.Contains("404"))
				{
					result.StatusCode = HttpStatusCode.NotFound;
				}
				else if (webEx.Message.Contains("410"))
				{
					result.StatusCode = HttpStatusCode.Gone;
				}
				else if (webEx.Message.Contains("401"))
				{
					result.StatusCode = HttpStatusCode.Unauthorized;
				}
				else if (webEx.Message.Contains("403"))
				{
					result.StatusCode = HttpStatusCode.Forbidden;
				}
				else if (webEx.Message.Contains("408"))
				{
					result.StatusCode = HttpStatusCode.RequestTimeout;
				}
				else if (webEx.Message.Contains("500"))
				{
					result.StatusCode = HttpStatusCode.InternalServerError;
				}
				else if (webEx.Message.Contains("timeout") || webEx.Message.Contains("timed out"))
				{
					//_logger.LogError(webEx, "Operation timeout against the server. Not a regular 408 timeout!");
					result.StatusCode = HttpStatusCode.RequestTimeout;
				}
			}
			catch (Exception catchEx)
			{
				result.StatusCode = HttpStatusCode.BadRequest;
				result.Description = "Could not request " + uri.AbsoluteUri;
			}

			if (webResponse != null)
			{
				webResponse.Close();
			}

			return result;
		}

		public string IgnoreLinkCheck(string link)
		{
			// Check if the link matching any ignore pattern.
			var existingIgnoreLink = _data.GetSpiderIgnoreLink(DataContext, _spiderRun.SpiderRunId, link);
			//if (_spiderRun.Settings.IgnoreLinkUrls.Contains(link))
			if (existingIgnoreLink != null)
			{
				Console.WriteLine("Ignored URL {0} for pattern {1} (from cache).", existingIgnoreLink.Url, existingIgnoreLink.IgnorePattern);
				link = string.Empty;
			}
			else
			{
				foreach (var pattern in _spiderRun.Settings.IgnoreLinkList)
				{
					if (Regex.IsMatch(link, pattern))
					{
						_data.SaveSpiderIgnoreLink(DataContext, new SpiderIgnoreLink { SpiderRunId = _spiderRun.SpiderRunId, Url = link, IgnorePattern = pattern });
						//_spiderRun.Settings.IgnoreLinkUrls.Add(link);
						Console.WriteLine("Ignore URL {0} for pattern {1}", link, pattern);
						link = string.Empty;
					}
				}
			}

			return link;
		} // SIFAS

		public string IgnoreExternalHostCheck(string link)
		{
			// Check if the link matching any ignore pattern.
			var existingIgnoreLink = _data.GetSpiderIgnoreLink(DataContext, _spiderRun.SpiderRunId, link);
			//if (_spiderRun.Settings.IgnoreExternalHostUrls.Contains(link))
			if (existingIgnoreLink != null)
			{
				Console.WriteLine(string.Format("Ignored external host URL {0} for pattern (from cache).", existingIgnoreLink.Url, existingIgnoreLink.IgnorePattern));
				link = string.Empty;
			}
			else
			{
				foreach (var pattern in _spiderRun.Settings.IgnoreExternalHostList)
				{
					if (Regex.IsMatch(link, pattern))
					{
						//_spiderRun.Settings.IgnoreExternalHostUrls.Add(link);
						_data.SaveSpiderIgnoreLink(DataContext, new SpiderIgnoreLink{ SpiderRunId = _spiderRun.SpiderRunId, Url = link, IgnorePattern = pattern});
						Console.WriteLine("Ignore external host URL {0} for pattern {1}", link, pattern);
						link = string.Empty;
					}
				}
			}

			return link;
		}

		public models.data.SpiderPage HandleContent(models.data.SpiderPage spiderPage)
		{
			List<SpiderPageLink> resourceList;

			try
			{
				// Check that all Script files works.
				// "<script.*?src=\"(.*?)\".*?>"
				resourceList = CheckResources(spiderPage, _spiderRun.Settings.ScriptRegexPattern, spiderPage.ScriptUrls, 1);
				spiderPage.ScriptUrls = resourceList;
			}
			catch (Exception ex)
			{
				spiderPage.Failed = true;
				spiderPage.FailedMessage = string.Format("Failed to handle script resources. {0}", ex.Message);
			}

			try
			{
				// Check that all links works.
				// "<link.*?href=\"(.*?)\".*?>"
				resourceList = CheckResources(spiderPage, _spiderRun.Settings.LinkRegexPattern, spiderPage.LinkUrls, 2);
				spiderPage.LinkUrls = resourceList;
			}
			catch (Exception ex)
			{
				spiderPage.Failed = true;
				spiderPage.FailedMessage = string.Format("Failed to handle links. {0}", ex.Message);
			}

			try
			{
				// Check that all images is working
				// "<[img|IMG].*?src=\"(.*?)\".*?>"
				resourceList = CheckResources(spiderPage, _spiderRun.Settings.ImageRegexPattern, spiderPage.ImageUrls, 3);
				spiderPage.ImageUrls = resourceList;
			}
			catch (Exception ex)
			{
				spiderPage.Failed = true;
				spiderPage.FailedMessage = string.Format("Failed to handle images. {0}", ex.Message);
			}

			// Handle all A anchor tags.
			spiderPage = HandleAnchors(spiderPage);

			// Match all URLs on the page and populate in the OtherUrls list.
			resourceList = CheckResources(spiderPage, _spiderRun.Settings.HttpRegexPattern, spiderPage.OtherUrls, 4);
			spiderPage.OtherUrls = resourceList;

			// Remove URLs from the OtherUrls list that already exists in any other of the URL lists.
			spiderPage = RemoveDuplicateOtherUrls(spiderPage);

			//// Gå igenom alla other och kolla vilka som pekar externt
			//foreach (var workingPageLink in spiderPage.OtherUrls)
			//{
			//	// Check if external resource
			//	RunExternalResourceCheck(spiderPage, workingPageLink.Url);
			//}

			return spiderPage;
		}

		private models.data.SpiderPage RemoveDuplicateOtherUrls(models.data.SpiderPage spiderPage)
		{
			// Go through all otherUrls and remove the once that allready exist in any other list.
			var removeUrls = new List<SpiderPageLink>();
			foreach (var spiderPageLink in spiderPage.OtherUrls)
			{
				if (spiderPage.ScriptUrls.Contains(spiderPageLink))
				{
					removeUrls.Add(spiderPageLink);
					continue;
				}
				if (spiderPage.LinkUrls.Contains(spiderPageLink))
				{
					removeUrls.Add(spiderPageLink);
					continue;
				}
				if (spiderPage.ImageUrls.Contains(spiderPageLink))
				{
					removeUrls.Add(spiderPageLink);
					continue;
				}
				if (spiderPage.DestinationUrls.Contains(spiderPageLink))
				{
					removeUrls.Add(spiderPageLink);
				}
			}
			foreach (var workingPageLink in removeUrls)
			{
				spiderPage.OtherUrls.Remove(workingPageLink);
			}

			return spiderPage;
		}

		private models.data.SpiderPage HandleAnchors(models.data.SpiderPage spiderPage)
		{
			// "<a[^>]*href=\"([^\"]*)\"[^>]*>"
			var matches = Regex.Matches(spiderPage.Content, _spiderRun.Settings.AnchorRegexPattern, RegexOptions.IgnoreCase);

			foreach (Match match in matches)
			{
				var link = match.Groups[1].ToString();

				// Check if the link matching any ignore pattern.
				link = IgnoreLinkCheck(link);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = CleanLink(spiderPage, link, _spiderRun.Settings.ExcludeExternalHosts);
				if (string.IsNullOrEmpty(link)) { continue; }

				// Check if the link startswith "~/link/".
				if (link.StartsWith("~/") || link.Contains("~/link/"))
				{
					//var workingPageLink = workBase.Cache.FirstOrDefault(x => x.Url == link);

					//if (workingPageLink == null)
					//{
						var spiderPageLink = spiderPage.DestinationUrls.FirstOrDefault(x => x.Url == link);

						if (spiderPageLink == null)
						{
							spiderPageLink = new SpiderPageLink
							{
								Url = link,
								Description = "EpiServer erroneous link.",
								StatusCode = HttpStatusCode.InternalServerError,
								Erroneous = true
							};
							spiderPage.DestinationUrls.Add(spiderPageLink);
						}
					//}
					else
					{
						if (!spiderPage.DestinationUrls.Any(x => x.Url == link))
						{
							spiderPage.DestinationUrls.Add(spiderPageLink);
						}
					}
					// Clear the link so that we stop handle the page.
					link = string.Empty;
					continue;
				}
				if (string.IsNullOrEmpty(link)) { continue; }

				// We need to make sure that we stay on the same host.
				//if (!string.IsNullOrEmpty(link) && !link.StartsWith("?") && !link.StartsWith(string.Format("{0}://{1}", spiderPage.Uri.Scheme, spiderPage.Uri.Host)))
				if (!string.IsNullOrEmpty(link) && !link.StartsWith(string.Format("{0}://{1}", spiderPage.Uri.Scheme, spiderPage.Uri.Host)))
				{
					// External host.
					link = IgnoreExternalHostCheck(link);
					if (string.IsNullOrEmpty(link)) { continue; }

					try
					{
						var linkUri = new Uri(link);

						var checkResult = CheckUrl(linkUri);

						if (!spiderPage.DestinationUrls.Any(x => x.Url == linkUri.AbsoluteUri))
						{
							spiderPage.DestinationUrls.Add(checkResult);
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error when try to URI " + link + " Error: " + ex.Message);
					}
					link = string.Empty;
					continue;
				}

				if (!string.IsNullOrEmpty(link))
				{
					var linkUri = new Uri(link);

					if (!spiderPage.DestinationUrls.Where(x => x.Url == linkUri.AbsoluteUri).Any())
					{
						spiderPage.DestinationUrls.Add(new SpiderPageLink { Url = linkUri.AbsoluteUri, StatusCode = HttpStatusCode.Continue });
					}

					// We should also create a new page so that we should crawl the found link.
					// Only if it not already exists.
					var existingPage = _data.GetSpiderPage(DataContext, _spiderRun.SpiderRunId, linkUri.AbsoluteUri);
					if (existingPage == null)
					{
						// Need to check the depth of the segment. If it is to deep we should not add it.
						if (_spiderRun.Settings.MaxSiteDepth > 0 && (linkUri.Segments.Count() - 1) > _spiderRun.Settings.MaxSiteDepth)
						{
							// We should not index this it is to deep.
							Console.WriteLine("MaxSiteDepth reached on URL " + linkUri.AbsoluteUri);
							continue;
						}
						if (_spiderRun.Settings.MaxNumberOfPages > 0 && _data.SpiderRunPagesCount(DataContext, _spiderRun.SpiderRunId) > _spiderRun.Settings.MaxNumberOfPages)
						{
							// We have reached max number of pages
							Console.WriteLine("Max number of pages reached will ignoe URL " + linkUri.AbsoluteUri);
							continue;
						}
						//workBase.Pages.Add(new WorkingPage { Url = linkUri.AbsoluteUri, StatusCode = HttpStatusCode.Continue });

						var newSpiderPage = new models.data.SpiderPage
						{
							Url = linkUri.AbsoluteUri, 
							StatusCode = HttpStatusCode.Continue,
							SpiderRunId = _spiderRun.SpiderRunId,
							SpiderRunKey = _spiderRun.SpiderRunKey
						};
						_data.SaveSpiderPage(DataContext, newSpiderPage);
					}

				}

			}
			return spiderPage;
		}

	}


}
