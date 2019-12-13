using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SeoSpider.AgiltyTest
{
	public partial class AgilityForm : Form
	{
		BackgroundWorker m_oWorker;
		private Stopwatch _timer;

		public AgilityForm()
		{
			InitializeComponent();

			m_oWorker = new BackgroundWorker();

			// Create a background worker thread that ReportsProgress &
			// SupportsCancellation
			// Hook up the appropriate events.
			m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
			m_oWorker.ProgressChanged += new ProgressChangedEventHandler(m_oWorker_ProgressChanged);
			m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_oWorker_RunWorkerCompleted);
			m_oWorker.WorkerReportsProgress = true;
			m_oWorker.WorkerSupportsCancellation = true;

			
		}

		private bool ValidateUrl(string url)
		{
			var result = false;

			try
			{
				var uri = new Uri(url);
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("The provided site URL is not a valid URI.");
			}
			return result;
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{

			// if the text is Start we will try to start the spider
			if (buttonStart.Text == "Start")
			{
				if (ValidateUrl(textBoxSiteUrl.Text))
				{
					// Set startbutton to cancel
					buttonStart.Text = "Cancel";

					_timer = new Stopwatch();
					_timer.Start();

					var uri = new Uri(textBoxSiteUrl.Text);
					var domain = uri.Scheme + "://" + uri.Host + "/";

					CheckForRobotsTxt(domain);
					//CheckForSitemap();

					// Kickoff the worker thread to begin it's DoWork function.
					//m_oWorker.RunWorkerAsync(textBoxSiteUrl.Text);

				}
			}
			else
			{
				buttonStart.Text = "Start";
				if (m_oWorker.IsBusy)
				{
					// Notify the worker thread that a cancel has been requested.
					// The cancel will not actually happen until the thread in the
					// DoWork checks the m_oWorker.CancellationPending flag. 
					m_oWorker.CancelAsync();
				}

			}
		}

		private string CheckForRobotsTxt(string domain)
		{
			var sitemapUrl = string.Empty;
			var url = domain + "robots.txt";
			var uri = new Uri(url);

			var result = CheckUrl(uri, new List<string>());

			if (result.StatusCode == HttpStatusCode.OK)
			{
				var content = result.Content + System.Environment.NewLine;
				var matches = Regex.Matches(content, @"Sitemap:(.*)[\r|\n|\s]", RegexOptions.IgnoreCase);

				foreach (Match match in matches)
				{
					var sitemap = match.Groups[1].ToString();
					if (sitemap.EndsWith(@"\r")) { sitemap = sitemap.Substring(0, sitemap.Length - 2); }
					if (sitemap.EndsWith(@"\n")) { sitemap = sitemap.Substring(0, sitemap.Length - 2); }
					Console.WriteLine(sitemap);
					sitemapUrl = sitemap;
				}
			}
			else
			{
				Console.WriteLine("No {0} found.", url);
			}
			return sitemapUrl;
		}

		#region Background worker
		/// <summary>
		/// On completed do the appropriate task
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_timer.Stop();
			// The background process is complete. We need to inspect
			// our response to see if an error occurred, a cancel was
			// requested or if we completed successfully.  
			if (e.Cancelled)
			{
				labelTimer.Text = "Time: Task Cancelled. Run for " + _timer.ElapsedMilliseconds.UserTimeElapsed();
			}

			// Check to see if an error occurred in the background process.

			else if (e.Error != null)
			{
				labelTimer.Text = "Time: Error while performing background operation. Run for " + _timer.ElapsedMilliseconds.UserTimeElapsed();
			}
			else
			{

				// Everything completed normally.
				labelTimer.Text = "Time: Task Completed... It took " + _timer.ElapsedMilliseconds.UserTimeElapsed();
				progressBar.Value = progressBar.Maximum;

				//WorkBase = e.Result as WorkBase;

				//// Clean up all destination Urls
				//foreach (var workingPage in WorkBase.Pages)
				//{
				//	foreach (var workingPageLink in workingPage.DestinationUrls.Where(x => x.StatusCode == HttpStatusCode.Continue))
				//	{
				//		var cachedPageLink = WorkBase.Cache.FirstOrDefault(x => x.Url == workingPageLink.Url && x.StatusCode != HttpStatusCode.Continue);
				//		if (cachedPageLink != null)
				//		{
				//			workingPageLink.Time = cachedPageLink.Time;
				//			workingPageLink.StatusCode = cachedPageLink.StatusCode;
				//			workingPageLink.Content = cachedPageLink.Content;
				//			workingPageLink.Description = cachedPageLink.Description;
				//			workingPageLink.ContentType = cachedPageLink.ContentType;
				//			workingPageLink.Size = cachedPageLink.Size;
				//			//workingPageLink = cachedPageLink;
				//			//workingPage.DestinationUrls.Remove(workingPageLink);
				//			//workingPage.DestinationUrls.Add(cachedPageLink);
				//		}
				//	}
				//}


				//var sortedPages = WorkBase.Pages.OrderBy(x => x.Url).ToList();

				//ResultDataGridView.DataSource = null;
				//ResultDataGridView.Update();
				//ResultDataGridView.Refresh();
				//ResultDataGridView.DataSource = sortedPages;

				//dataGridViewExtResources.DataSource = _extResources;
				//dataGridViewIframes.DataSource = _iFrames;

				//btnShowReport.Visible = true;
				//btnShowReport.Enabled = true;

			}

			//Change the status of the buttons on the UI accordingly
			buttonStart.Text = "Start";
		}

		/// <summary>
		/// Notification is performed here to the progress bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// This function fires on the UI thread so it's safe to edit
			// the UI control directly, no funny business with Control.Invoke :)
			// Update the progressBar with the integer supplied to us from the

			// ReportProgress() function.  

			//WorkBase = e.UserState as WorkBase;
			//var itterator = e.ProgressPercentage;

			//var totalCount = WorkBase.Pages.Count;
			//var handled = WorkBase.Pages.Count(x => x.Handled);

			//if (itterator % 50 == 0)
			//{
			//	ResultDataGridView.DataSource = null;
			//	ResultDataGridView.Update();
			//	ResultDataGridView.Refresh();
			//	ResultDataGridView.DataSource = WorkBase.Pages;
			//}
			//progressBar1.Maximum = totalCount;
			//progressBar1.Value = handled;
			//lblStatus.Text = "Processing......" + handled + " of " + totalCount;
		}

		/// <summary>
		/// Time consuming operations go here </br>
		/// i.e. Database operations,Reporting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var url = e.Argument as string;
			var uri = new Uri(url);

			var result = CheckUrl(uri, new List<string>());

			if (!string.IsNullOrEmpty(result.Content))
			{
				XTags(result.Content, "a", "href");
				XTags(result.Content, "iframe", "src");
				XTags(result.Content, "script", "src");
				XTags(result.Content, "link", "href");
				XTags(result.Content, "img", "src");
				AlternateTags(result.Content);
				CanonicalTags(result.Content);
				IconTags(result.Content);
				MetaDescription(result.Content);
				MetaKeywords(result.Content);
				MetaData(result.Content);
				BrowserTitle(result.Content);
			}
			else
			{
				Console.WriteLine(result.StatusCode.ToString());
			}

			//var root = html.DocumentNode;
			//var anchors = root.Descendants("a");
			//var unorderedLists = root.Descendants("ul");






			//var workBase = e.Argument as WorkBase;

			//var itterator = 0;
			//while (workBase.Pages.Where(x => !x.Handled).Any())
			//{
			//	// Grab the first not handled URL in the list
			//	var page = workBase.Pages.FirstOrDefault(x => x.Handled == false);
			//	if (page != null)
			//	{
			//		HandleUrl(workBase, page);
			//		m_oWorker.ReportProgress(itterator, workBase);
			//	}



			//	// Periodically check if a cancellation request is pending.
			//	// If the user clicks cancel the line
			//	// m_AsyncWorker.CancelAsync(); if ran above.  This
			//	// sets the CancellationPending to true.
			//	// You must check this flag in here and react to it.
			//	// We react to it by setting e.Cancel to true and leaving
			//	if (m_oWorker.CancellationPending)
			//	{
			//		// Set the e.Cancel flag so that the WorkerCompleted event
			//		// knows that the process was cancelled.
			//		e.Cancel = true;
			//		m_oWorker.ReportProgress(itterator, workBase);
			//		return;
			//	}

			//	itterator++;
			//}

			////Report 100% completion on operation completed
			//m_oWorker.ReportProgress(100, workBase);

			//e.Result = workBase;
		}
		#endregion

		public void XTags(string htmlContent, string tag, string att)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants(tag).Where(x => x.Attributes.Contains(att));

			Console.WriteLine("Found {0}: {1}", tag, tags.Count());

			foreach (var htmlNode in tags)
			{
				var attribute = htmlNode.GetAttributeValue(att, "");
				Console.WriteLine("Found {0} {1}", tag, attribute);
			}
		}

		public void AlternateTags(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("link").Where(x => x.GetAttributeValue("ref", "").Equals("alternate"));

			Console.WriteLine("Found anternate links: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var hreflang = htmlNode.GetAttributeValue("hreflang", "");
				var href = htmlNode.GetAttributeValue("href", "");
				Console.WriteLine("Found {0} alternate link {1}", hreflang, href);
			}

			//https://support.google.com/webmasters/answer/189077?hl=en
			// Check common mistakes for hints to nice functions.
		}

		public void CanonicalTags(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("link").Where(x => x.GetAttributeValue("rel", "").Equals("canonical"));

			Console.WriteLine("Found canonical links: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var href = htmlNode.GetAttributeValue("href", "");
				Console.WriteLine("Found canonical link {0}", href);
			}
		}

		public void IconTags(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("link").Where(x => x.GetAttributeValue("rel", "").Equals("shortcut icon"));

			Console.WriteLine("Found shortcut icon links: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var href = htmlNode.GetAttributeValue("href", "");
				Console.WriteLine("Found shortcut icon link {0}", href);
			}
		}

		public void BrowserTitle(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;

			var tags = root.SelectNodes("//head/title");

			//var tags = root.Descendants("title");

			Console.WriteLine("Found title: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var title = htmlNode.InnerText;
				Console.WriteLine("Found title: {0}", title);
			}

		}
		public void MetaDescription(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("meta").Where(x => x.GetAttributeValue("name", "").Equals("description"));

			Console.WriteLine("Found meta description: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var content = htmlNode.Attributes["content"].Value;
				Console.WriteLine("Found meta description: {0}", content);
			}

		}

		public void MetaKeywords(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("meta").Where(x => x.GetAttributeValue("name", "").Equals("keywords"));

			Console.WriteLine("Found meta keywords: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var content = htmlNode.GetAttributeValue("content", "");
				Console.WriteLine("Found meta keywords: {0}", content);
			}

		}

		public void MetaData(string htmlContent)
		{
			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(htmlContent);
			var root = html.DocumentNode;
			var tags = root.Descendants("meta").Where(x => !x.GetAttributeValue("name", "").Equals("keywords") && !x.GetAttributeValue("name", "").Equals("description"));

			Console.WriteLine("Found meta: {0}", tags.Count());

			foreach (var htmlNode in tags)
			{
				var name = htmlNode.GetAttributeValue("name", "");
				if (!string.IsNullOrEmpty(name))
				{
					var content = htmlNode.GetAttributeValue("content", "");
					Console.WriteLine("Found meta {0}: {1}", name, content);

				}

				var charset = htmlNode.GetAttributeValue("charset", "");
				if (!string.IsNullOrEmpty(charset))
				{
					Console.WriteLine("Found meta charset: {0}", charset);
				}

				var prop = htmlNode.GetAttributeValue("property", "");
				if (!string.IsNullOrEmpty(prop))
				{
					var content = htmlNode.GetAttributeValue("content", "");
					Console.WriteLine("Found meta {0}: {1}", prop, content);

				}
			}

		}

		public PageResponse CheckUrl(Uri uri, List<string> sourceUrls)
		{
			var result = new PageResponse
			{
				StatusCode = HttpStatusCode.BadRequest,
				Url = uri.AbsoluteUri
			};

			//if (uri.AbsoluteUri == "https://www.rule.sehttps//www.rule.se//wp-login.php?action=logout&_wpnonce=839bbce889")
			//{
			//	Console.WriteLine("Start debug.");
			//}

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
					if (locationUri == uri || (sourceUrls.Any() && sourceUrls.Contains(locationUri.AbsoluteUri)))
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
						sourceUrls.Add(uri.AbsoluteUri);
						var locationResult = CheckUrl(locationUri, sourceUrls);
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

	}
}
