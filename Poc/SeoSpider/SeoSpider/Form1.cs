using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoSpider
{
	public partial class Form1 : Form
	{
		//private BackgroundWorker _bWorker;

		public Form1()
		{
			InitializeComponent();
		}

		private List<WorkingPage> Pages { get; set; } 
		//private List<string> TildeContainingPages = new List<string>();

		private List<WorkingPageLink> Cache { get; set; }

		private List<string> IgnoreList { get; set; }

		private List<string> IgnoreUrls { get; set; } 

		private void button1_Click(object sender, EventArgs e)
		{
			var siteUrl = SiteUrl.Text;


			Pages = new List<WorkingPage>();
			//TildeContainingPages = new List<string>();
			Cache = new List<WorkingPageLink>();
			Pages.Add(new WorkingPage{ Url = siteUrl });
			IgnoreUrls = new List<string>();
			IgnoreList = new List<string>();
			IgnoreList.Add("^#.\\w");
			IgnoreList.Add("^javascript:.\\w");
			IgnoreList.Add("^mailto:.\\w");
			IgnoreList.Add(".pdf$");
			IgnoreList.Add(".xls$");
			IgnoreList.Add(".doc$");
			IgnoreList.Add(".docx$");
			IgnoreList.Add("^https:\\/\\/accounts.google.com\\/"); 
			IgnoreList.Add("^http:\\/\\/www.facebook.com\\/");
			IgnoreList.Add("^http:\\/\\/twitter.com\\/");
			IgnoreList.Add("^http:\\/\\/www.linkedin.com\\/");
			IgnoreList.Add("^http:\\/\\/plus.google.com\\/");
			IgnoreList.Add("^http:\\/\\/ted.europa.eu\\/");
			IgnoreList.Add("^https:\\/\\/instagram.com\\/"); 
			IgnoreList.Add("\\/press-and-media\\/");
            //IgnoreList.Add("\\/news-and-media\\/");
            //IgnoreList.Add("\\/Nyheder\\/");
            //IgnoreList.Add("\\/uutiset\\/");
            //IgnoreList.Add("\\/newsroom\\/");
            //IgnoreList.Add("\\/press-och-media\\/");
            //IgnoreList.Add("\\/nieuws\\/");



            PagesWith404Result.Text = string.Empty;

			//_bWorker = new BackgroundWorker();

			var itterator = 0;
			while (Pages.Where(x => !x.Handled).Any() && itterator < 10)
			{
				if (itterator%100 == 0 && itterator != 0)
				{
					var breakHere = true;
				}
				// Grab the first not handled URL in the list
				var page = Pages.FirstOrDefault(x => x.Handled == false);
				if (page != null)
				{
					//_bWorker.DoWork += _bWorker_DoWork(this, new DoWorkEventArgs(page));
					HandleUrl(page);
				}

				itterator++;
			}

			#region Show result on the form

			dataGridView1.DataSource = Pages;


			//foreach (var tildePage in TildeContainingPages)
			//{
			//	TildeLinksResult.Text += tildePage + Environment.NewLine;
			//}

			var pages404Result = new StringBuilder();
			var pagesContain404 = Pages.Where(x => x.DestinationUrls.Any(z => z.StatusCode != HttpStatusCode.OK)).ToList();
			foreach (var page in pagesContain404)
			{
				var errLinks = page.DestinationUrls.Where(z => z.StatusCode == HttpStatusCode.NotFound || z.StatusCode == HttpStatusCode.InternalServerError || z.StatusCode == HttpStatusCode.Gone);
				foreach (var pageLink in errLinks)
				{
					//PagesWith404Result.Text += string.Format("{0} contains {1} - {2}", page.Url, pageLink.StatusCode, pageLink.Url) + Environment.NewLine;
					pages404Result.AppendLine(string.Format("{0} contains {1} - {2}", page.Url, pageLink.StatusCode, pageLink.Url));
				}
			}
			PagesWith404Result.Text = pages404Result.ToString();

			MessageBox.Show("Done");
			#endregion
		}

		//private void _bWorker_DoWork(object sender, DoWorkEventArgs e)
		//{
		//	e.
		//}

		private void HandleUrl(WorkingPage workingPage)
		{
			var uri = new Uri(workingPage.Url);

			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			webRequest.AllowAutoRedirect = false;
			HttpWebResponse webResponse = null;

			try
			{
				webResponse = (HttpWebResponse)webRequest.GetResponse();

				var statusCode = webResponse.StatusCode;
				workingPage.StatusCode = statusCode;

				var responseStream = webResponse.GetResponseStream();
				var content = string.Empty;
				using (var reader = new StreamReader(responseStream, Encoding.Default))
				{
					content = reader.ReadToEnd();
					workingPage.Content = content;
				}

				var matches = Regex.Matches(content, "<a[^>]*href=\"([^\"]*)\"[^>]*>.*<\\/a>", RegexOptions.IgnoreCase);

				foreach (Match match in matches)
				{
					var link = match.Groups[1].ToString();

					// Check if the link matching any ignore pattern.
					if (IgnoreUrls.Contains(link))
					{
						Debug.Print(string.Format("Ignored URL {0}.", link));
						link = string.Empty;
						
					}
					else
					{
						foreach (var pattern in IgnoreList)
						{
							if (Regex.IsMatch(link, pattern))
							{
								IgnoreUrls.Add(link);
								Debug.Print(string.Format("Ignore URL {0} for pattern {1}", link, pattern));
								link = string.Empty;
							}
						}
						
					}
					
					// Check if the link startswith "~/link/".
					if (link.StartsWith("~/"))
					{
						//if (!TildeContainingPages.Where(x => x == workingPage.Uri.AbsoluteUri).Any())
						//{
						//	TildeContainingPages.Add(workingPage.Uri.AbsoluteUri);
						//}
						var workingPageLink = new WorkingPageLink
						{
							Url = link,
							Description = "EpiServer erroneous link.",
							StatusCode = HttpStatusCode.InternalServerError,
							Erroneous = true
						};
						workingPage.DestinationUrls.Add(workingPageLink);
						// Clear the link so that we stop handle the page.
						link = string.Empty;
					}


					if (link.StartsWith("/"))
					{
						// We need to add the host for the link.
						link = string.Format("{0}://{1}{2}", workingPage.Uri.Scheme, workingPage.Uri.Host, link);
					}

					// We need to make sure that we stay on the same host.
					if (!string.IsNullOrEmpty(link) && !link.StartsWith("?") && !link.StartsWith(string.Format("{0}://{1}", workingPage.Uri.Scheme, workingPage.Uri.Host)))
					{
						var cachedPageLink = Cache.FirstOrDefault(x => x.Url == link);
						if (cachedPageLink == null)
						{
							//var urlCheckResult = new UrlCheckResult();
							//try
							//{
							//	var linkUri = new Uri(link);
							//	urlCheckResult = CheckUrl(linkUri);
							//}
							//catch (UriFormatException uriEx)
							//{
							//	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
							//}
							//catch (NotSupportedException notEx)
							//{
							//	urlCheckResult.StatusCode = HttpStatusCode.BadRequest;
							//}

							////if (urlCheckResult.StatusCode != HttpStatusCode.BadRequest)
							////{
							////	urlCheckResult = CheckUrl(new Uri(linkUri));
							////}

							//var pageLink = new WorkingPageLink { Url = link, StatusCode = urlCheckResult.StatusCode };
							//workingPage.DestinationUrls.Add(pageLink);
							//Cache.Add(pageLink);
						}
						else
						{
							workingPage.DestinationUrls.Add(cachedPageLink);
						}
						link = string.Empty;
					}


					if (!string.IsNullOrEmpty(link))
					{
						if (link.StartsWith("?"))
						{
							link = string.Format("{0}{1}", workingPage.Uri.AbsoluteUri, link);
						}

						workingPage.DestinationUrls.Add(new WorkingPageLink { Url = link });

						// We should also create a new page so that we should crawl the found link.
						// Only if it not already exists.
						if (!Pages.Where(x => x.Url == link).Any())
						{
							Pages.Add(new WorkingPage { Url = link, StatusCode = HttpStatusCode.Continue});
						}
						
					}
					
				}

			}
			catch (WebException webEx)
			{

				if (webEx.Message.Contains("404"))
				{
					workingPage.StatusCode = HttpStatusCode.NotFound;
				}
				else if (webEx.Message.Contains("410"))
				{
					workingPage.StatusCode = HttpStatusCode.Gone;
				}
				else if (webEx.Message.Contains("401"))
				{
					workingPage.StatusCode = HttpStatusCode.Unauthorized;
				}
				else if (webEx.Message.Contains("403"))
				{
					workingPage.StatusCode = HttpStatusCode.Forbidden;
				}
				else if (webEx.Message.Contains("408"))
				{
					workingPage.StatusCode = HttpStatusCode.RequestTimeout;
				}
				else if (webEx.Message.Contains("500"))
				{
					workingPage.StatusCode = HttpStatusCode.InternalServerError;
				}
				else if (webEx.Message.Contains("timeout") || webEx.Message.Contains("timed out"))
				{
					//_logger.LogError(webEx, "Operation timeout against the server. Not a regular 408 timeout!");
					workingPage.StatusCode = HttpStatusCode.RequestTimeout;
				}
				else
				{
					workingPage.StatusCode = HttpStatusCode.BadRequest;
				}
			}
			workingPage.Handled = true;

			if (webResponse != null)
			{
				webResponse.Close();
			}			
		}

		//public UrlCheckResult CheckUrl(Uri uri)
		//{
		//	var result = new UrlCheckResult
		//	{
		//		StatusCode = HttpStatusCode.BadRequest
		//	};

		//	var webRequest = (HttpWebRequest)WebRequest.Create(uri);
		//	webRequest.AllowAutoRedirect = false;
		//	HttpWebResponse webResponse = null;

		//	try
		//	{
		//		webResponse = (HttpWebResponse)webRequest.GetResponse();

		//		result.StatusCode = webResponse.StatusCode;

		//		if (result.StatusCode == HttpStatusCode.MovedPermanently || result.StatusCode == HttpStatusCode.Found || result.StatusCode == HttpStatusCode.TemporaryRedirect)
		//		{
		//			result.Description = webResponse.Headers["Location"];

		//			var locationUri = new Uri(uri, webResponse.Headers["Location"]);
		//			if (locationUri == uri)
		//			{
		//				// Nestled redirection loop.
		//				result.Erroneous = true;
		//				result.Description = string.Format("Nestled redirection loop. Redirect to it self ({0}).", locationUri);
		//			}
		//			else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
		//			{
		//				result.Erroneous = true;
		//				result.Description = string.Format("Redirect user to EPi Server login page. ({0}).", locationUri);
		//			}
		//			else
		//			{
		//				// We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
		//				//var locationUri = new Uri(uri, webResponse.Headers["Location"]);
		//				var locationResult = CheckUrl(locationUri);
		//				if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
		//					locationResult.StatusCode == HttpStatusCode.InternalServerError)
		//				{
		//					result.Erroneous = true;
		//					result.Description =
		//						string.Format("Redirected to {0}. Erroneous redirection. Page {0} response a 404/410/500 status.",
		//							webResponse.Headers["Location"]);
		//				}

		//				// We should also check if the location conatins aspxerrorpath
		//				if (result.Erroneous == false && result.Description.Contains("aspxerrorpath="))
		//				{
		//					result.Erroneous = true;
		//					result.Description =
		//						string.Format("Redirected to {0} ASPX custom error page. Page {0} response a soft 500 status.",
		//							webResponse.Headers["Location"]);
		//				}

		//				if (locationResult.Erroneous)
		//				{
		//					result.Erroneous = true;
		//					result.Description = locationResult.Description;
		//				}
		//			}
		//		}
		//	}
		//	catch (WebException webEx)
		//	{
		//		if (webEx.Message.Contains("404"))
		//		{
		//			result.StatusCode = HttpStatusCode.NotFound;
		//		}
		//		else if (webEx.Message.Contains("410"))
		//		{
		//			result.StatusCode = HttpStatusCode.Gone;
		//		}
		//		else if (webEx.Message.Contains("401"))
		//		{
		//			result.StatusCode = HttpStatusCode.Unauthorized;
		//		}
		//		else if (webEx.Message.Contains("403"))
		//		{
		//			result.StatusCode = HttpStatusCode.Forbidden;
		//		}
		//		else if (webEx.Message.Contains("408"))
		//		{
		//			result.StatusCode = HttpStatusCode.RequestTimeout;
		//		}
		//		else if (webEx.Message.Contains("500"))
		//		{
		//			result.StatusCode = HttpStatusCode.InternalServerError;
		//		}
		//		else if (webEx.Message.Contains("timeout") || webEx.Message.Contains("timed out"))
		//		{
		//			//_logger.LogError(webEx, "Operation timeout against the server. Not a regular 408 timeout!");
		//			result.StatusCode = HttpStatusCode.RequestTimeout;
		//		}
		//	}

		//	if (webResponse != null)
		//	{
		//		webResponse.Close();
		//	}

		//	return result;
		//}

		private void ErrPagesResult_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
