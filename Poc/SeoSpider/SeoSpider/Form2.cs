using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;
using Common.Logging.Configuration;
using SeoSpider.Test2;

namespace SeoSpider
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();

			m_oWorker = new BackgroundWorker();
			m_ReportWorker = new BackgroundWorker();

			// Create a background worker thread that ReportsProgress &
			// SupportsCancellation
			// Hook up the appropriate events.
			m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
			m_oWorker.ProgressChanged += new ProgressChangedEventHandler(m_oWorker_ProgressChanged);
			m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_oWorker_RunWorkerCompleted);
			m_oWorker.WorkerReportsProgress = true;
			m_oWorker.WorkerSupportsCancellation = true;

			m_ReportWorker.DoWork += new DoWorkEventHandler(m_ReportWorker_DoWork);
			m_ReportWorker.ProgressChanged += new ProgressChangedEventHandler(m_ReportWorker_ProgressChanged);
			m_ReportWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_ReportWorker_RunWorkerCompleted);
			m_ReportWorker.WorkerReportsProgress = true;
			m_ReportWorker.WorkerSupportsCancellation = true;

			_timer = new Stopwatch();
		}

		BackgroundWorker m_oWorker;
		BackgroundWorker m_ReportWorker;
		private Uri SiteUri;
		private Stopwatch _timer;

		private List<PageExtLink> _extResources = new List<PageExtLink>();
		private List<PageIframe> _iFrames = new List<PageIframe>();

		private void StartButton_Click(object sender, EventArgs e)
		{
			if (ValidateUrl())
			{
				// Do Job
				btnStart.Enabled = false;
				btnCancel.Enabled = true;
				btnShowReport.Visible = true;
				btnShowReport.Enabled = false;

				var workBase = new WorkBase(new Uri(Url.Text));
				workBase.MaxSiteDepth = Convert.ToInt32(numMaxFolderDepth.Value);
				workBase.MaxNumberOfPages = Convert.ToInt32(numMaxNumPages.Value);
				workBase.RobotsTxt = cbRobotsTxt.Checked;
				workBase.SitemapXml = cbSitemapXml.Checked;
				workBase.ExcludeExternalHosts = cbExcludeExtHosts.Checked;
				workBase.MatchPattern = txtMatchPattern.Text;

				_timer.Start();

				// Kickoff the worker thread to begin it's DoWork function.
				m_oWorker.RunWorkerAsync(workBase);
			}
		}

		private bool ValidateUrl()
		{
			var result = false;

			try
			{
				SiteUri = new Uri(Url.Text);
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("The provided site URL is not a valid URI.");
			}
			return result;
		}

		private WorkBase WorkBase; 

		#region BackgroundWorker stuff
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
				lblStatus.Text = "Task Cancelled. Run for " + _timer.ElapsedMilliseconds + " ms";
            }

            // Check to see if an error occurred in the background process.

            else if (e.Error != null)
            {
				lblStatus.Text = "Error while performing background operation. Run for " + _timer.ElapsedMilliseconds + " ms";
            }
            else
            {  
				
                // Everything completed normally.
                lblStatus.Text = "Task Completed... It took "+_timer.ElapsedMilliseconds +" ms";
	            progressBar1.Value = progressBar1.Maximum;

				WorkBase = e.Result as WorkBase;

				// Clean up all destination Urls
	            foreach (var workingPage in WorkBase.Pages)
	            {
		            foreach (var workingPageLink in workingPage.DestinationUrls.Where(x => x.StatusCode == HttpStatusCode.Continue))
		            {
						var cachedPageLink = WorkBase.Cache.FirstOrDefault(x => x.Url == workingPageLink.Url && x.StatusCode != HttpStatusCode.Continue);
			            if (cachedPageLink != null)
			            {
							workingPageLink.Time = cachedPageLink.Time;
							workingPageLink.StatusCode = cachedPageLink.StatusCode;
							workingPageLink.Content = cachedPageLink.Content;
							workingPageLink.Description = cachedPageLink.Description;
							workingPageLink.ContentType = cachedPageLink.ContentType;
							workingPageLink.Size = cachedPageLink.Size;
							//workingPageLink = cachedPageLink;
				            //workingPage.DestinationUrls.Remove(workingPageLink);
							//workingPage.DestinationUrls.Add(cachedPageLink);
			            }
		            }
	            }


				var sortedPages = WorkBase.Pages.OrderBy(x => x.Url).ToList();

	            ResultDataGridView.DataSource = null;
				ResultDataGridView.Update();
				ResultDataGridView.Refresh();
				ResultDataGridView.DataSource = sortedPages;

				//dataGridViewExtResources.DataSource = null;
				//dataGridViewExtResources.Update();
				//dataGridViewExtResources.Refresh();
				dataGridViewExtResources.DataSource = _extResources;
				dataGridViewIframes.DataSource = _iFrames;

				btnShowReport.Visible = true;
				btnShowReport.Enabled = true;

            }

            //Change the status of the buttons on the UI accordingly
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
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

			//var dataView = e.UserState as DataView;
	        WorkBase = e.UserState as WorkBase;

			//var paintedPages = from p in pages select new { RequestedUrl = p.Url};

	        var itterator = e.ProgressPercentage;

			var totalCount = WorkBase.Pages.Count;
			var handled = WorkBase.Pages.Count(x => x.Handled);

	        if (itterator % 50 == 0)
	        {
				ResultDataGridView.DataSource = null;
				ResultDataGridView.Update();
				ResultDataGridView.Refresh();
				ResultDataGridView.DataSource = WorkBase.Pages;

				//dataGridViewExtResources.DataSource = _extResources;
				//dataGridViewIframes.DataSource = _iFrames;

			}

			//dataGridViewExtResources.DataSource = null;
			//dataGridViewExtResources.Update();
			//dataGridViewExtResources.Refresh();
			//dataGridViewExtResources.DataSource = _extResources;

			progressBar1.Maximum = totalCount;
	        progressBar1.Value = handled;
			lblStatus.Text = "Processing......" + handled + " of " + totalCount;

        }

        /// <summary>
        /// Time consuming operations go here </br>
        /// i.e. Database operations,Reporting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {

			var workBase = e.Argument as WorkBase;

	        //var workBase = new WorkBase(siteUri);
	        //var pages = new List<WorkingPage>();

			var itterator = 0;
			while (workBase.Pages.Where(x => !x.Handled).Any())
			{
				//if (itterator % 100 == 0 && itterator != 0)
				//{
				//	var breakHere = true;
				//}


				//List<WorkingPage> pages = null;
				//Task[] taskArray = null;
				//if (workBase.Pages.Where(x => x.Handled == false).Count() > 5)
				//{
				//	pages = workBase.Pages.Where(x => x.Handled == false).Take(5).ToList();
				//	taskArray = new Task[4];
				//}
				//else
				//{

				//	pages = workBase.Pages.Where(x => x.Handled == false).ToList();
				//	taskArray = new Task[pages.Count - 1];
				//}
				//for (int i = 0; i < taskArray.Length; i++)
				//{

				//	taskArray[i] = Task.Factory.StartNew(() => HandleUrl(workBase, pages[i]), TaskCreationOptions.LongRunning);
					
				//}
				//Task.WaitAll(taskArray);


				//m_oWorker.ReportProgress(itterator, workBase);



				// Grab the first not handled URL in the list
				var page = workBase.Pages.FirstOrDefault(x => x.Handled == false);
				if (page != null)
				{
					HandleUrl(workBase, page);
					m_oWorker.ReportProgress(itterator, workBase);
				}



				// Periodically check if a cancellation request is pending.
				// If the user clicks cancel the line
				// m_AsyncWorker.CancelAsync(); if ran above.  This
				// sets the CancellationPending to true.
				// You must check this flag in here and react to it.
				// We react to it by setting e.Cancel to true and leaving
				if (m_oWorker.CancellationPending)
				{
					// Set the e.Cancel flag so that the WorkerCompleted event
					// knows that the process was cancelled.
					e.Cancel = true;
					m_oWorker.ReportProgress(itterator, workBase);
					return;
				}

				itterator++;
			}

            //Report 100% completion on operation completed
            m_oWorker.ReportProgress(100, workBase);

	        e.Result = workBase;
        }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (m_oWorker.IsBusy)
			{

				// Notify the worker thread that a cancel has been requested.

				// The cancel will not actually happen until the thread in the

				// DoWork checks the m_oWorker.CancellationPending flag. 

				m_oWorker.CancelAsync();
			}

			btnShowReport.Visible = true;
			btnShowReport.Enabled = false;

		}		
		#endregion

		private void formResize(object sender, EventArgs e)
		{
			ResultDataGridView.Height = this.Height - 217;
		}

		private void cellClick(object sender, DataGridViewCellEventArgs e)
		{
			var dgv = sender as DataGridView;
			if (dgv == null)
				return;
			if (dgv.CurrentRow.Selected)
			{
				//do you staff.
				var page = dgv.CurrentRow.DataBoundItem as WorkingPage;

				var stringbuilder = new StringBuilder();
				stringbuilder.AppendLine("Source Urls:");
				foreach (var pageLink in WorkBase.Pages.Where(x => x.DestinationUrls.Find(y => y.Url == page.Url) != null))
				{
					stringbuilder.AppendLine(pageLink.StatusCode + ":" + pageLink.Url);
				}
				stringbuilder.AppendLine("Destination Urls:");
				foreach (var destUrl in page.DestinationUrls)
				{
					stringbuilder.AppendLine(destUrl.StatusCode + ":" + destUrl.Url);
				}

				MessageBox.Show(stringbuilder.ToString());
			}
		}

		private void HandleUrl(WorkBase workBase, WorkingPage workingPage)
		{
			var uri = new Uri(workingPage.Url);

			var urlCheckResult = CheckUrl(workBase, uri);
			workingPage.PopulateUrlResult(urlCheckResult);

			if (!string.IsNullOrEmpty(workingPage.Content))
			{
				// Check that all Script files works.
				// "<script.*?src=\"(.*?)\".*?>"
				CheckResources(workBase, workingPage, workingPage.Content, workBase.ScriptRegexPattern, workingPage.ScriptUrls, 1);

				// Check that all links works.
				// "<link.*?href=\"(.*?)\".*?>"
				CheckResources(workBase, workingPage, workingPage.Content, workBase.LinkRegexPattern, workingPage.LinkUrls, 2);

				// Check that all images is working
				// "<[img|IMG].*?src=\"(.*?)\".*?>"
				CheckResources(workBase, workingPage, workingPage.Content, workBase.ImageRegexPattern, workingPage.ImageUrls, 3);

				// Handle all A anchor tags.
				HandleAnchors(workBase, workingPage, workingPage.Content);

				CheckResources(workBase, workingPage, workingPage.Content, workBase.HttpRegexPattern, workingPage.OtherUrls, 4);
				// Go through all otherUrls and remove the once that allready exist in any other list.
				var removeUrls = new List<WorkingPageLink>();
				foreach (var workingPageLink in workingPage.OtherUrls)
				{
					if (workingPage.ScriptUrls.Contains(workingPageLink))
					{
						removeUrls.Add(workingPageLink);
						continue;
					}
					if (workingPage.LinkUrls.Contains(workingPageLink))
					{
						removeUrls.Add(workingPageLink);
						continue;
					}
					if (workingPage.ImageUrls.Contains(workingPageLink))
					{
						removeUrls.Add(workingPageLink);
						continue;
					}
					if (workingPage.DestinationUrls.Contains(workingPageLink))
					{
						removeUrls.Add(workingPageLink);
					}
				}
				foreach (var workingPageLink in removeUrls)
				{
					workingPage.OtherUrls.Remove(workingPageLink);	
				}
				
				// Gå igenom alla other och kolla vilka som pekar externt
				foreach (var workingPageLink in workingPage.OtherUrls)
				{
					// Check if external resource
					RunExternalResourceCheck(workingPage, workingPageLink.Url);
				}

				// Kontrollera om det finns några IFrame på sidan.
				CheckForIframes(workingPage);
			}

			workingPage.Handled = true;

			// Check if the page match the pattern.
			if (!string.IsNullOrEmpty(workBase.MatchPattern) && !string.IsNullOrEmpty(workingPage.Content))
			{
				if (Regex.IsMatch(workingPage.Content, workBase.MatchPattern, RegexOptions.IgnoreCase))
				{
					workingPage.MatchPattern = true;
				}
			}
		}

		private string CleanLink(WorkingPage workingPage, string link, bool excludeExternalHosts = false)
		{
			// if the link starts with // we will add the same scheme on the link as the existing scheme for the site.
			if (link.StartsWith("//"))
			{
				link = string.Format("{0}:{1}", workingPage.Uri.Scheme, link);
			}

			if (link.StartsWith("/"))
			{
				// We need to add the host for the link.
				link = string.Format("{0}://{1}{2}", workingPage.Uri.Scheme, workingPage.Uri.Host, link);
			}

			// Handle links with ../ in the start.
			if (link.StartsWith("../"))
			{
				link = HandleDotDotDashLink(workingPage, link);
			}

			// if the link is same as the workingPage we should cancel.
			if (link == workingPage.Url)
			{
				link = string.Empty;
			}

			// If the link is equal to the querystring for the workingpage it is a link to it self.
			if (link.StartsWith("?") && workingPage.Uri.Query == link)
			{
				link = string.Empty;
			}

			if (link.StartsWith("?") && !workingPage.Uri.AbsoluteUri.Contains("?"))
			{
				link = string.Format("{0}{1}", workingPage.Uri.AbsoluteUri, link);
			}
			else if (link.StartsWith("?") && workingPage.Uri.AbsoluteUri.Contains("?"))
			{
				var linkUri = new Uri(workingPage.Uri.AbsoluteUri.Replace(workingPage.Uri.Query, link));

				link = linkUri.AbsoluteUri;
			}

			if (excludeExternalHosts && link.StartsWith("http"))
			{
				try
				{
					var linkUri = new Uri(link);
					if (!linkUri.Host.EndsWith(workingPage.Uri.Host))
					{
						// the link is not the same as the workingPage host. We will exclude the link.
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

		private bool IsExternal(WorkingPage workingPage, string link)
		{
			if (link.StartsWith("/global"))
			{
				var stopMe = true;
			}

			var isExternal = false;
			try
			{
				var linkUri = new Uri(link);
				if (!linkUri.Host.EndsWith(workingPage.Uri.Host))
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

		private void CheckForIframes(WorkingPage workingPage)
		{
			var matches = Regex.Matches(workingPage.Content, "<iframe.*?src=\"(.*?)\".*?", RegexOptions.IgnoreCase);

			foreach (Match match in matches)
			{
				var iframeLink = match.Groups[1].ToString();
                var pageIframe = new PageIframe { PageUrl = workingPage.Url, IframeUrl = iframeLink };
				_iFrames.Add(pageIframe);
				workingPage.IframeLinks.Add(iframeLink);
            }
		}


		private void RunExternalResourceCheck(WorkingPage workingPage, string link)
		{
			if (IsExternal(workingPage, link))
			{
				if (link.StartsWith("http://") || link.StartsWith("//"))
				{
					var pageExtLinkComparer = new PageExtLinkComparer();
					var pageExtLink = new PageExtLink { PageUrl = workingPage.Url, ExtLinkUrl = link };
					if (!_extResources.Contains(pageExtLink, pageExtLinkComparer))
					{
						_extResources.Add(pageExtLink);
					}

				}
			}
		}

		private void CheckResources(WorkBase workBase, WorkingPage workingPage, string content, string pattern, List<WorkingPageLink> resourceList, int linkType)
		{
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
					RunExternalResourceCheck(workingPage, link);
				}
					
				//_extResources

				// Check if the link matching any ignore pattern.
				link = IgnoreLinkCheck(workBase, link);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = CleanLink(workingPage, link, workBase.ExcludeExternalHosts);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = IgnoreExternalHostCheck(workBase, link);
				if (string.IsNullOrEmpty(link)) { continue; }

				// Check if the link startswith "~/link/".
				if (link.StartsWith("~/") || link.Contains("~/link/"))
				{
					var workingPageLink = workBase.Cache.FirstOrDefault(x => x.Url == link);

					if (workingPageLink == null)
					{
						workingPageLink = resourceList.FirstOrDefault(x => x.Url == link);

						if (workingPageLink == null)
						{
							workingPageLink = new WorkingPageLink
							{
								Url = link,
								Description = "EpiServer erroneous link.",
								StatusCode = HttpStatusCode.InternalServerError,
								Erroneous = true
							};
							resourceList.Add(workingPageLink);
						}
					}
					else
					{
						if (!resourceList.Any(x => x.Url == link))
						{
							resourceList.Add(workingPageLink);
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
						if (link.StartsWith(@"https:\/\/"))
						{
							link = link.Replace(@"https:\/\/", "https://");
						}
						if (link.StartsWith(@"http:\/\/"))
						{
							link = link.Replace(@"http:\/\/", "http://");
						}

						linkUri = new Uri(link);
					}
					catch
					{
						link = string.Format("{0}://{1}{2}", workingPage.Uri.Scheme, workingPage.Uri.Host, link);
						linkUri = new Uri(link);
					}

					var checkResult = CheckUrl(workBase, linkUri);
					if (!resourceList.Any(x => x.Url == linkUri.AbsoluteUri))
					{
						resourceList.Add(checkResult);
					}
				}
			}
		}


		/// <summary>
		/// Check if the link should be ignored or not.
		/// If the link should be ignored the result from the method will be empty.
		/// </summary>
		private string IgnoreLinkCheck(WorkBase workBase, string link)
		{
			// Check if the link matching any ignore pattern.
			if (workBase.IgnoreLinkUrls.Contains(link))
			{
				Debug.Print(string.Format("Ignored URL {0}.", link));
				link = string.Empty;
			}
			else
			{
				foreach (var pattern in workBase.IgnoreLinkList)
				{
					if (Regex.IsMatch(link, pattern))
					{
						workBase.IgnoreLinkUrls.Add(link);
						Debug.Print(string.Format("Ignore URL {0} for pattern {1}", link, pattern));
						link = string.Empty;
					}
				}
			}

			return link;
		}

		private string IgnoreDestinationCheck(WorkBase workBase, string link)
		{
			// Check if the link matching any ignore pattern.
			if (workBase.IgnoreDestinationUrls.Contains(link))
			{
				Debug.Print(string.Format("Ignored destination URL {0}.", link));
				link = string.Empty;
			}
			else
			{
				foreach (var pattern in workBase.IgnoreDestinationList)
				{
					if (Regex.IsMatch(link, pattern))
					{
						workBase.IgnoreDestinationUrls.Add(link);
						Debug.Print(string.Format("Ignore destination URL {0} for pattern {1}", link, pattern));
						link = string.Empty;
					}
				}
			}

			return link;
		}

		private string IgnoreExternalHostCheck(WorkBase workBase, string link)
		{
			// Check if the link matching any ignore pattern.
			if (workBase.IgnoreExternalHostUrls.Contains(link))
			{
				Debug.Print(string.Format("Ignored external host URL {0}.", link));
				link = string.Empty;
			}
			else
			{
				foreach (var pattern in workBase.IgnoreExternalHostList)
				{
					if (Regex.IsMatch(link, pattern))
					{
						workBase.IgnoreExternalHostUrls.Add(link);
						Debug.Print(string.Format("Ignore external host URL {0} for pattern {1}", link, pattern));
						link = string.Empty;
					}
				}
			}

			return link;
		}

		private void HandleAnchors(WorkBase workBase, WorkingPage workingPage, string content)
		{
			// "<a[^>]*href=\"([^\"]*)\"[^>]*>"
			var matches = Regex.Matches(content, workBase.AnchorRegexPattern, RegexOptions.IgnoreCase);

			foreach (Match match in matches)
			{
				var link = match.Groups[1].ToString();

				// Check if the link matching any ignore pattern.
				link = IgnoreLinkCheck(workBase, link);
				if (string.IsNullOrEmpty(link)) { continue; }

				link = CleanLink(workingPage, link, workBase.ExcludeExternalHosts);
				if (string.IsNullOrEmpty(link)) { continue; }

				// Check if the link startswith "~/link/".
				if (link.StartsWith("~/") || link.Contains("~/link/"))
				{
					var workingPageLink = workBase.Cache.FirstOrDefault(x => x.Url == link);

					if (workingPageLink == null)
					{
						workingPageLink = workingPage.DestinationUrls.FirstOrDefault(x => x.Url == link);

						if (workingPageLink == null)
						{
							workingPageLink = new WorkingPageLink
							{
								Url = link,
								Description = "EpiServer erroneous link.",
								StatusCode = HttpStatusCode.InternalServerError,
								Erroneous = true
							};
							workingPage.DestinationUrls.Add(workingPageLink);
						}
					}
					else
					{
						if (!workingPage.DestinationUrls.Any(x => x.Url == link))
						{
							workingPage.DestinationUrls.Add(workingPageLink);
						}
					}
					// Clear the link so that we stop handle the page.
					link = string.Empty;
					continue;
				}
				if (string.IsNullOrEmpty(link)) { continue; }

				// We need to make sure that we stay on the same host.
				//if (!string.IsNullOrEmpty(link) && !link.StartsWith("?") && !link.StartsWith(string.Format("{0}://{1}", workingPage.Uri.Scheme, workingPage.Uri.Host)))
				if (!string.IsNullOrEmpty(link) && !link.StartsWith(string.Format("{0}://{1}", workingPage.Uri.Scheme, workingPage.Uri.Host)))
				{
					// External host.
					link = IgnoreExternalHostCheck(workBase, link);
					if (string.IsNullOrEmpty(link)) { continue; }

					try
					{
						var linkUri = new Uri(link);

						var checkResult = CheckUrl(workBase, linkUri);

						if (!workingPage.DestinationUrls.Any(x => x.Url == linkUri.AbsoluteUri))
						{
							workingPage.DestinationUrls.Add(checkResult);
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

					if (!workingPage.DestinationUrls.Where(x => x.Url == linkUri.AbsoluteUri).Any())
					{
						workingPage.DestinationUrls.Add(new WorkingPageLink { Url = linkUri.AbsoluteUri, StatusCode = HttpStatusCode.Continue});
					}

					// We should also create a new page so that we should crawl the found link.
					// Only if it not already exists.
					if (!workBase.Pages.Where(x => x.Url == linkUri.AbsoluteUri).Any())
					{
						// Need to check the depth of the segment. If it is to deep we should not add it.
						if (workBase.MaxSiteDepth > 0 && (linkUri.Segments.Count() - 1) > workBase.MaxSiteDepth)
						{
							// We should not index this it is to deep.
							Console.WriteLine("MaxSiteDepth reached on URL " + linkUri.AbsoluteUri);
							continue;
						}
						if (workBase.MaxNumberOfPages > 0 && workBase.Pages.Count > workBase.MaxNumberOfPages)
						{
							// We have reached max number of pages
							Console.WriteLine("Max number of pages reached will ignoe URL " + linkUri.AbsoluteUri);
							continue;
							
						}
						workBase.Pages.Add(new WorkingPage { Url = linkUri.AbsoluteUri, StatusCode = HttpStatusCode.Continue });
					}

				}

			}
			
		}

		public WorkingPageLink CheckUrl(WorkBase workBase, Uri uri)
		{
			// first check if we have information in the cache.
			var urlCheckResult = workBase.Cache.FirstOrDefault(x => x.Url == uri.AbsoluteUri);
			if (urlCheckResult == null)
			{
				urlCheckResult = CheckUrl(uri, new List<string>());
				// Add to cache so that we can reuse.
				workBase.Cache.Add(urlCheckResult);
			}
			else
			{
				Console.WriteLine("Get from cache " + uri.AbsoluteUri);
			}

			return urlCheckResult;
		}

		public WorkingPageLink CheckUrl(Uri uri, List<string> sourceUrls)
		{
			var result = new WorkingPageLink
			{
				StatusCode = HttpStatusCode.BadRequest,
				Url = uri.AbsoluteUri
			};

			if (uri.AbsoluteUri == "https://www.rule.sehttps//www.rule.se//wp-login.php?action=logout&_wpnonce=839bbce889")
			{
				Console.WriteLine("Start debug.");
			}

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

		//public WorkingPageLink CheckUrl(Uri uri, string sourceUrl = "")
		//{
		//	var result = new WorkingPageLink
		//	{
		//		StatusCode = HttpStatusCode.BadRequest,
		//		Url = uri.AbsoluteUri
		//	};

		//	if (uri.AbsoluteUri == "https://www.rule.sehttps//www.rule.se//wp-login.php?action=logout&_wpnonce=839bbce889")
		//	{
		//		Console.WriteLine("Start debug.");
		//	}

		//	Console.WriteLine("CheckUrl:" + uri.AbsoluteUri);

		//	HttpWebResponse webResponse = null;
		//	try
		//	{
		//		var webRequest = (HttpWebRequest) WebRequest.Create(uri);
		//		webRequest.AllowAutoRedirect = false;

		//		// Start timer to check how long time it takes to get response.
		//		var stopWatch = new Stopwatch();
		//		stopWatch.Start();

		//		webResponse = (HttpWebResponse) webRequest.GetResponse();
		//		result.Size = webResponse.ContentLength;
		//		result.ContentType = webResponse.ContentType;
		//		result.StatusCode = webResponse.StatusCode;

		//		// Only download content for ContentType that are related to HTml, text, xml etc.
		//		if (result.ContentType.StartsWith("text/"))
		//		{
		//			var responseStream = webResponse.GetResponseStream();
		//			if (responseStream != null)
		//			{
		//				using (var reader = new StreamReader(responseStream, Encoding.Default))
		//				{
		//					result.Content = reader.ReadToEnd();
		//				}
		//			}
		//		}

		//		stopWatch.Stop();
		//		// Report the response time.
		//		result.Time = stopWatch.ElapsedMilliseconds;

		//		// Check if we got activity that we need to lookup more.
		//		if (result.StatusCode == HttpStatusCode.MovedPermanently || result.StatusCode == HttpStatusCode.Found ||
		//		    result.StatusCode == HttpStatusCode.TemporaryRedirect)
		//		{
		//			// Get more information from the response.
		//			result.Description = webResponse.Headers["Location"];

		//			var locationUri = new Uri(uri, webResponse.Headers["Location"]);
		//			// Check if we are in a nestled loop
		//			if (locationUri == uri || (!string.IsNullOrEmpty(sourceUrl) && locationUri == new Uri(sourceUrl)))
		//			{
		//				// Nestled redirection loop.
		//				result.Erroneous = true;
		//				result.Description = string.Format("Nestled redirection loop. Redirect to it self ({0}).", locationUri);
		//			}
		//			else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
		//			{
		//				// We have been sent to a EPi server login page. We do not have access to this page.
		//				result.Erroneous = true;
		//				result.Description = string.Format("Redirect user to EPi Server login page. ({0}).", locationUri);
		//			}
		//			else
		//			{
		//				// We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
		//				//var locationUri = new Uri(uri, webResponse.Headers["Location"]);
		//				var locationResult = CheckUrl(locationUri, uri.AbsoluteUri);
		//				if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
		//				    locationResult.StatusCode == HttpStatusCode.InternalServerError)
		//				{
		//					result.Erroneous = true;
		//					result.Description =
		//						string.Format("Redirected to {0}. Erroneous redirection. Page {0} response a 404/410/500 status.",
		//							webResponse.Headers["Location"]);
		//				}

		//				// We should also check if the location conatins aspxerrorpath
		//				if (result.Erroneous == false && result.Description.Contains("aspxerrorpath="))
		//				{
		//					// We have been redirected to a ASPX custom error page.
		//					result.Erroneous = true;
		//					result.Description =
		//						string.Format("Redirected to {0} ASPX custom error page. Page {0} response a soft 500 status.",
		//							webResponse.Headers["Location"]);
		//				}

		//				if (locationResult.Erroneous)
		//				{
		//					// Something is wrong but we have not classsified what. Report back the information we got from server.
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
		//	catch (Exception catchEx)
		//	{
		//		result.StatusCode = HttpStatusCode.BadRequest;
		//		result.Description = "Could not request " + uri.AbsoluteUri;
		//	}

		//	if (webResponse != null)
		//	{
		//		webResponse.Close();
		//	}

		//	return result;
		//}

		private void Form2_Load(object sender, EventArgs e)
		{
			LoadOldReports();
		}

		private void LoadOldReports()
		{
			var currentDirectory = Directory.GetCurrentDirectory() + "\\";

			cbOldReports.Items.Clear();
			cbOldReports.Items.Add("");
			var files = Directory.GetFiles(currentDirectory, "*.xml");
			foreach (var file in files)
			{
				cbOldReports.Items.Add(file.Replace(currentDirectory, ""));

			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var link = "../../account/login";
			var tempPageUri = new Uri("http://test.com/jadda/jadda");

			var itterator = 0;
			while (link.StartsWith("../"))
			{
				// Remove the first ../ from the link
				link = link.Substring(3);

				// Remove the last segment from the tempPageUri.
				var tempPageUrl = tempPageUri.AbsoluteUri.Substring(0, tempPageUri.AbsoluteUri.Length - tempPageUri.Segments[tempPageUri.Segments.Count() - 1].Length);
				tempPageUri = new Uri(tempPageUrl);

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

			var result = tempPageUri.AbsoluteUri;
		}

		private string HandleDotDotDashLink(WorkingPage workingPage, string link)
		{
			if (link.StartsWith("../"))
			{
				try
				{
					var tempPageUri = new Uri(workingPage.Uri.AbsoluteUri);
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

		private void btnShowReport_Click(object sender, EventArgs e)
		{
			if (WorkBase != null)
			{
				progressBar1.Maximum = 100;
				m_ReportWorker.RunWorkerAsync(WorkBase);
			}
			else
			{
				MessageBox.Show("No result to show.");
			}
		}

		private void ShowReport(object sender, EventArgs e)
		{
			if (cbOldReports.SelectedIndex != 0)
			{
				try
				{
					var currentDirectory = Directory.GetCurrentDirectory() + "\\";
					var fileName = currentDirectory + cbOldReports.Text;

					var serializer = new XmlSerializer(typeof(SpiderReport));

					var reader = new FileStream(fileName, FileMode.Open);

					var spiderReport = (SpiderReport)serializer.Deserialize(reader);
					reader.Close();

					var seoReport = new SeoReport();
					seoReport.Show();
					seoReport.Report = spiderReport;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to load report. Message:" + ex.Message);
				}

			}
		}

		void m_ReportWorker_DoWork(object sender, DoWorkEventArgs e)
		{

			var workBase = e.Argument as WorkBase;
			var spiderReport = new SpiderReport();
			spiderReport.NumberOfPages = workBase.Pages.Count;

			var sb = new StringBuilder();
			sb.AppendLine("Number of pages: " + workBase.Pages.Count);

			var tempPages = new List<WorkingPage>();

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsBrowserTitleEmpty).ToList();
			if (tempPages.Any())
			{
				spiderReport.NoBrowserTitle = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages without browser title (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.MultipleBrowserTitlesFound).ToList();
			if (tempPages.Any())
			{
				spiderReport.MultipleBrowserTitle = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages with more then one browser title (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsBrowserTitleToShort).ToList();
			if (tempPages.Any())
			{
				spiderReport.ShortBrowserTitle = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages with to short browser title (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri + " Title:" + source.BrowserTitle);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsBrowserTitleToLong).ToList();
			if (tempPages.Any())
			{
				spiderReport.LongBrowserTitle = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages with to long browser title (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri + " Title:" + source.BrowserTitle);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsMetaDescriptionEmpty).ToList();
			if (tempPages.Any())
			{
				spiderReport.NoMetaDescription = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages without meta description (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsMetaDescriptionToShort).ToList();
			if (tempPages.Any())
			{
				spiderReport.ShortMetaDescription = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages with to short meta description (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
					//sb.AppendLine(source.Uri.AbsoluteUri + " Description:" + source.MetaDescription);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsMetaDescriptionToLong).ToList();
			if (tempPages.Any())
			{
				spiderReport.LongMetaDescription = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages with to long meta description (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
					//sb.AppendLine(source.Uri.AbsoluteUri + " Description:" + source.MetaDescription);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.IsMetaKeywordsEmpty).ToList();
			if (tempPages.Any())
			{
				spiderReport.NoMetaKeywords = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue}).ToList();

				sb.AppendLine("Pages without meta keywords (" + tempPages.Count() + "):");
				foreach (var source in tempPages)
				{
					sb.AppendLine(source.Uri.AbsoluteUri);
				}
			}

			sb.AppendLine("");
			var errResourcePages = workBase.Pages.Where(x => x.ScriptUrls.Any(y => y.StatusCode != HttpStatusCode.OK && y.StatusCode != HttpStatusCode.MovedPermanently)).ToList();
			errResourcePages.AddRange(workBase.Pages.Where(x => x.LinkUrls.Any(y => y.StatusCode != HttpStatusCode.OK && y.StatusCode != HttpStatusCode.MovedPermanently)).ToList());
			errResourcePages.AddRange(workBase.Pages.Where(x => x.DestinationUrls.Any(y => y.StatusCode != HttpStatusCode.OK && y.StatusCode != HttpStatusCode.MovedPermanently)).ToList());
			if (errResourcePages.Any())
			{
				spiderReport.ContainsErrResource = new List<ReportPage>();

				sb.AppendLine("Pages with errouneos links (" + errResourcePages.Count() + "):");
				foreach (var page in errResourcePages)
				{
					var reportPage = new ReportPage { Url = page.Url, SubPages = new List<ReportPage>(), HttpStatusCode = HttpStatusCode.Continue };

					sb.AppendLine(page.Uri.AbsoluteUri);
					foreach (var errLink in page.ScriptUrls.Where(x => x.StatusCode != HttpStatusCode.OK && x.StatusCode != HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage{Url=errLink.Url, HttpStatusCode = errLink.StatusCode});
						sb.AppendLine(" - " + errLink.Url + " Status: " + errLink.StatusCode);
					}
					foreach (var errLink in page.LinkUrls.Where(x => x.StatusCode != HttpStatusCode.OK && x.StatusCode != HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode });
						sb.AppendLine(" - " + errLink.Url + " Status: " + errLink.StatusCode);
					}
					foreach (var errLink in page.DestinationUrls.Where(x => x.StatusCode != HttpStatusCode.OK && x.StatusCode != HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode });
						sb.AppendLine(" - " + errLink.Url + " Status: " + errLink.StatusCode);
					}
					spiderReport.ContainsErrResource.Add(reportPage);
				}
			}

			sb.AppendLine("");
			var movedPermanentlyPages = workBase.Pages.Where(x => x.ScriptUrls.Any(y => y.StatusCode == HttpStatusCode.MovedPermanently)).ToList();
			movedPermanentlyPages.AddRange(workBase.Pages.Where(x => x.LinkUrls.Any(y => y.StatusCode == HttpStatusCode.MovedPermanently)).ToList());
			movedPermanentlyPages.AddRange(workBase.Pages.Where(x => x.DestinationUrls.Any(y => y.StatusCode == HttpStatusCode.MovedPermanently)).ToList());
			if (errResourcePages.Any())
			{
				spiderReport.LinksToMovedPermanently = new List<ReportPage>();

				sb.AppendLine("Pages links to URLs that are moved permanently (" + movedPermanentlyPages.Count() + "):");
				foreach (var page in movedPermanentlyPages)
				{
					var reportPage = new ReportPage { Url = page.Url, SubPages = new List<ReportPage>(), HttpStatusCode = HttpStatusCode.Continue};

					sb.AppendLine(page.Uri.AbsoluteUri);
					foreach (var errLink in page.ScriptUrls.Where(x => x.StatusCode == HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode, Description = errLink.Description });
						sb.AppendLine(" - " + errLink.Uri.AbsoluteUri + " moved to " + errLink.Description);
					}
					foreach (var errLink in page.LinkUrls.Where(x => x.StatusCode == HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode, Description = errLink.Description});
						sb.AppendLine(" - " + errLink.Uri.AbsoluteUri + " moved to " + errLink.Description);
					}
					foreach (var errLink in page.DestinationUrls.Where(x => x.StatusCode == HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode, Description = errLink.Description });
						sb.AppendLine(" - " + errLink.Uri.AbsoluteUri + " moved to " + errLink.Description);
					}
					spiderReport.LinksToMovedPermanently.Add(reportPage);
				}
			}

			sb.AppendLine("");
			var errImgPages = workBase.Pages.Where(x => x.ImageUrls.Any(y => y.StatusCode != HttpStatusCode.OK && y.StatusCode != HttpStatusCode.MovedPermanently)).ToList();
			if (errImgPages.Any())
			{
				spiderReport.ContainsErrImageLinks = new List<ReportPage>();

				sb.AppendLine("Pages with errouneos img links (" + errImgPages.Count() + "):");
				foreach (var page in errImgPages)
				{
					var reportPage = new ReportPage { Url = page.Url, SubPages = new List<ReportPage>() };

					sb.AppendLine(page.Uri.AbsoluteUri);
					foreach (var errLink in page.ImageUrls.Where(x => x.StatusCode != HttpStatusCode.OK && x.StatusCode != HttpStatusCode.MovedPermanently))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, HttpStatusCode = errLink.StatusCode });
						sb.AppendLine(" - " + errLink.Uri.AbsoluteUri);
					}
					spiderReport.ContainsErrImageLinks.Add(reportPage);
				}
			}

			//sb.AppendLine("");
			//var errDestPages = workBase.Pages.Where(x => x.DestinationUrls.Any(y => y.StatusCode != HttpStatusCode.OK)).ToList();
			//if (errDestPages.Any())
			//{
			//	sb.AppendLine("Pages with errouneos links (" + errDestPages.Count() + "):");
			//	foreach (var page in errDestPages)
			//	{
			//		sb.AppendLine(page.Uri.AbsoluteUri);
			//		foreach (var errLink in page.DestinationUrls.Where(x => x.StatusCode != HttpStatusCode.OK))
			//		{
			//			sb.AppendLine(" - " + errLink.Uri.AbsoluteUri);
			//		}
			//	}
			//}

			sb.AppendLine("");
			var largeImgPages = workBase.Pages.Where(x => x.ImageUrls.Any(y => y.Size > workBase.ImageSizeLimit)).ToList();
			if (largeImgPages.Any())
			{
				spiderReport.ContainsLargeImages = new List<ReportPage>();

				sb.AppendLine("Pages using large images (" + largeImgPages.Count() + "):");
				foreach (var page in largeImgPages)
				{
					var reportPage = new ReportPage { Url = page.Url, SubPages = new List<ReportPage>() };

					sb.AppendLine(page.Uri.AbsoluteUri);
					foreach (var errLink in page.ImageUrls.Where(x => x.Size > workBase.ImageSizeLimit))
					{
						reportPage.SubPages.Add(new ReportPage { Url = errLink.Url, Description = errLink.Size.ToString(), HttpStatusCode = HttpStatusCode.Continue });
						sb.AppendLine(" - " + errLink.Uri.AbsoluteUri);
					}
					spiderReport.ContainsLargeImages.Add(reportPage);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.Size > workBase.PageSizeLimit).ToList();
			if (tempPages.Any())
			{
				spiderReport.LargePages = (from x in tempPages select new ReportPage { Url = x.Url, Description = x.Size.ToString(), HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages that are large (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri + " Size: " + page.Size);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.Time > workBase.PageSpeedHighLimit).ToList();
			if (tempPages.Any())
			{
				spiderReport.HighSpeedPages = (from x in tempPages select new ReportPage { Url = x.Url, Description = x.Time.ToString(), HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages that takes more then 1 second to load (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri + " Time: " + page.Time);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.Time < workBase.PageSpeedHighLimit && x.Time > (workBase.PageSpeedWarningLimit - 1)).ToList();
			if (tempPages.Any())
			{
				spiderReport.WarningSpeedPages = (from x in tempPages select new ReportPage { Url = x.Url, Description = x.Time.ToString(), HttpStatusCode = HttpStatusCode.Continue}).ToList();

				sb.AppendLine("Pages that takes between 500 ms and 1 second to load (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri + " Time: " + page.Time);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(
					x =>
						(x.StatusCode == HttpStatusCode.Redirect || x.StatusCode == HttpStatusCode.TemporaryRedirect || 
						 x.StatusCode == HttpStatusCode.Found) && !string.IsNullOrEmpty(x.StatusCodeDescription)).ToList();
			if (tempPages.Any())
			{
				spiderReport.ErrRedirectPages = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = x.StatusCode, Description = x.StatusCodeDescription }).ToList();

				sb.AppendLine("Pages with erronoues redirect (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri + " Status: " + page.StatusCode + " Description: " + page.StatusCodeDescription);
				}
			}

			sb.AppendLine("");
			tempPages =
				workBase.Pages.Where(
					x =>
						x.StatusCode == HttpStatusCode.Unauthorized || x.StatusCode == HttpStatusCode.Forbidden ||
						x.StatusCode == HttpStatusCode.NotFound || x.StatusCode == HttpStatusCode.RequestTimeout ||
						x.StatusCode == HttpStatusCode.InternalServerError || (!string.IsNullOrEmpty(x.StatusCodeDescription) && x.StatusCode != HttpStatusCode.MovedPermanently)).ToList();
			if (tempPages.Any())
			{
				spiderReport.FailedPages = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = x.StatusCode, Description = x.StatusCodeDescription }).ToList();

				sb.AppendLine("Pages failed (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri + " Status: " + page.StatusCode + " Description: " + page.StatusCodeDescription);
				}
			}

			sb.AppendLine("");
			tempPages = workBase.Pages.Where(x => x.Uri.Scheme != workBase.StartUrl.Scheme).ToList();
			if (tempPages.Any())
			{
				spiderReport.ChangedSchemaPages = (from x in tempPages select new ReportPage { Url = x.Url, HttpStatusCode = HttpStatusCode.Continue }).ToList();

				sb.AppendLine("Pages using different URI scheme then starting URI " + workBase.StartUrl.Scheme + " (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					sb.AppendLine(page.Uri.AbsoluteUri);
				}
			}

			sb.AppendLine("");
			tempPages =
				workBase.Pages.Where(x => x.Uri.Scheme == workBase.StartUrl.Scheme && x.Uri.Host != workBase.StartUrl.Host).ToList();
			if (tempPages.Any())
			{
				spiderReport.ChangedHostPages = new List<ReportPage>();

				sb.AppendLine("Pages switching to another host " + workBase.StartUrl.Host + " (" + tempPages.Count() + "):");
				foreach (var page in tempPages)
				{
					var reportPage = new ReportPage { Url = page.Url, HttpStatusCode = HttpStatusCode.Continue, SubPages = new List<ReportPage>() };
					sb.AppendLine(page.Uri.AbsoluteUri);
					foreach (var pageLink in WorkBase.Pages.Where(x => x.DestinationUrls.Find(y => y.Url == page.Url) != null))
					{
						reportPage.SubPages.Add(new ReportPage { Url = pageLink.Url, HttpStatusCode = HttpStatusCode.Continue });
						sb.AppendLine(" - " + pageLink.Uri.AbsoluteUri);
					}
					spiderReport.ChangedHostPages.Add(reportPage);
				}
			}

			sb.AppendLine("");
			tempPages =
				workBase.Pages.Where(x => x.AlternativeLangRefs.Any()).ToList();
			if (tempPages.Any())
			{
				spiderReport.ErrAltLangHrefNoSelfPoint = new List<ReportPage>();

				sb.AppendLine("Pages contains alt lang ref but does not point to it self:");
				foreach (var page in tempPages)
				{
					var match = false;
					// Check if the alt lang ref is pointing to it self.
					foreach (var altLink in page.AlternativeLangRefs)
					{
						if (page.Url.Contains(altLink))
						{
							match = true;
						}
					}

					if (!match)
					{
						var reportPage = new ReportPage { Url = page.Url, HttpStatusCode = HttpStatusCode.Continue};
						spiderReport.ErrAltLangHrefNoSelfPoint.Add(reportPage);
						sb.AppendLine(page.Uri.AbsoluteUri);
					}
				}
			}

			sb.AppendLine("");
			tempPages =
				workBase.Pages.Where(x => x.AlternativeLangRefs.Any()).ToList();
			if (tempPages.Any())
			{
				spiderReport.ErrAltLangHrefNoPointBack = new List<ReportPage>();

				sb.AppendLine("Alt lang ref pages does not point back to source:");
				foreach (var page in tempPages)
				{
					
					// Check if the alt lang ref is pointing to it self.
					foreach (var altLink in page.AlternativeLangRefs)
					{
						var destinationPage = workBase.Pages.FirstOrDefault(x => x.Url.Contains(altLink));
						if (destinationPage != null)
						{
							var match = false;

							foreach (var alternativeLangRef in destinationPage.AlternativeLangRefs)
							{
								if (page.Url.Contains(alternativeLangRef))
								{
									match = true;
								}
							}

							if (!match)
							{
								var reportPage = new ReportPage { Url = altLink, HttpStatusCode = HttpStatusCode.Continue, SubPages = new List<ReportPage>{new ReportPage{ Url = page.Url}}};
								spiderReport.ErrAltLangHrefNoPointBack.Add(reportPage);
								sb.AppendLine(page.Uri.AbsoluteUri);
							}
						}


					}

				}
			}


			spiderReport.ReportText = sb.ToString();

			var itterator = 0;
			while (workBase.Pages.Where(x => !x.Handled).Any())
			{
				// Periodically check if a cancellation request is pending.
				// If the user clicks cancel the line
				// m_AsyncWorker.CancelAsync(); if ran above.  This
				// sets the CancellationPending to true.
				// You must check this flag in here and react to it.
				// We react to it by setting e.Cancel to true and leaving
				if (m_ReportWorker.CancellationPending)
				{
					// Set the e.Cancel flag so that the WorkerCompleted event
					// knows that the process was cancelled.
					e.Cancel = true;
					m_ReportWorker.ReportProgress(itterator);
					return;
				}

				itterator++;
			}

			//Report 100% completion on operation completed
			m_ReportWorker.ReportProgress(100);

			e.Result = spiderReport;
		}

		void m_ReportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// The background process is complete. We need to inspect
			// our response to see if an error occurred, a cancel was
			// requested or if we completed successfully.  
			if (e.Cancelled)
			{
				lblStatus.Text = "Report generation cancelled.";
			}

			// Check to see if an error occurred in the background process.

			else if (e.Error != null)
			{
				lblStatus.Text = "Error while performing report.";
			}
			else
			{
				// Everything completed normally.
				lblStatus.Text = "Report Completed...";
				progressBar1.Value = progressBar1.Maximum;

				var spiderReport = e.Result as SpiderReport;

				try
				{
					// Save the object to xml on disc.
					var xmlSerializer = new System.Xml.Serialization.XmlSerializer(spiderReport.GetType());
					var filePath = string.Format("{0}\\{1}_{2}.xml", Directory.GetCurrentDirectory(), WorkBase.StartUrl.AbsoluteUri.MakeFileNameFriendly(), DateTime.Now.ToUniversalSortableDateTime());
					var wfile = new StreamWriter(filePath);
					xmlSerializer.Serialize(wfile, spiderReport);
					wfile.Close();

				}
				catch (Exception ex)
				{
					Console.WriteLine("Error when try to serialize SpiderReport: ex: " + ex.Message);
				}


				var seoReport = new SeoReport();
				seoReport.Show();
				seoReport.Report = spiderReport;

			}

		}

		void m_ReportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{

			// This function fires on the UI thread so it's safe to edit

			// the UI control directly, no funny business with Control.Invoke :)

			// Update the progressBar with the integer supplied to us from the

			// ReportProgress() function.  

			var itterator = e.ProgressPercentage;

			progressBar1.Value = e.ProgressPercentage;
			lblStatus.Text = "Generating report......" + e.ProgressPercentage + " %";


		}

		private void button2_Click(object sender, EventArgs e)
		{
			var form = new SpiderConfigForm();
			form.Show();
			//form.Report = spiderReport;
		}
		
	}
}
