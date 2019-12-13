using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SeoSpider.Test2.models.data
{
	[DebuggerDisplay("{Url}:{Handled}{StatusCode}")]
	public class SpiderPage
	{
		public SpiderPage()
		{
			SpiderPageUrls = new SpiderPageUrls();

			if (!string.IsNullOrEmpty(UrlsXml))
			{
				SpiderPageUrls = JsonConvert.DeserializeObject<SpiderPageUrls>(UrlsJson);
					//XmlSerializah.Deserialize<SpiderPageUrls>(UrlsXml);
			}

			ScriptUrls = new List<SpiderPageLink>();
			LinkUrls = new List<SpiderPageLink>();
			ImageUrls = new List<SpiderPageLink>();
			OtherUrls = new List<SpiderPageLink>();
			DestinationUrls = new List<SpiderPageLink>();
		}

		public int SpiderPageId { get; set; }

		public int SpiderRunId { get; set; }

		public Guid SpiderRunKey { get; set; }

		public void PopulateUrlResult(SpiderPageLink spiderPageLink)
		{
			Time = spiderPageLink.Time;
			StatusCode = spiderPageLink.StatusCode;
			Content = spiderPageLink.Content;
			StatusCodeDescription = spiderPageLink.Description;
			ContentType = spiderPageLink.ContentType;
			Size = spiderPageLink.Size;
		}

		public string Url { get; set; }

		/// <summary>
		/// Is checked if the Url is erroneous and can not be loaded in a URI object.
		/// </summary>
		public bool ErroneousUrl { get; set; }

		[NotMapped]
		public Uri Uri
		{
			get
			{
				return new Uri(Url);
			}
		}

		public bool CheckedOut { get; set; }

		private bool _handled = false;
		public bool Handled
		{
			get { return _handled; }
			set
			{
				_handled = value;

				SetBrowserTitle();

				SetMetaDescription();

				SetMetaKeywords();

				var tempString = string.Empty;
				if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
				{
					//<link.*?rel="alternate".*?hreflang="(.*?)".*?href="(.*?)".*?>
					var matches = Regex.Matches(Content, "<link.*?rel=\"alternate\".*?href=\"(.*?)\".*?>|<link.*?href=\"(.*?)\".*?rel=\"alternate\".*?>", RegexOptions.IgnoreCase);
					var stringBuilder = new StringBuilder();
					foreach (Match match in matches)
					{
						var link = match.Groups[1].ToString();
						SpiderPageUrls.AlternativeLangRefs.Add(link);
						stringBuilder.AppendLine(link);
					}
					tempString = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(tempString))
					{
						AlternativeLangRef = tempString;
					}
				}

				tempString = string.Empty;
				//<link.*?rel="canonical".*?href="(.*?)".*?>|<link.*?href="(.*?)".*?rel="canonical".*?>
				// http://googlewebmastercentral.blogspot.se/2013/04/5-common-mistakes-with-relcanonical.html
				// https://support.google.com/webmasters/answer/139066?hl=en
				// https://moz.com/blog/rel-confused-answers-to-your-rel-canonical-questions
				if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
				{
					var matches = Regex.Matches(Content, "<link.*?rel=\"canonical\".*?href=\"(.*?)\".*?>|<link.*?href=\"(.*?)\".*?rel=\"canonical\".*?>", RegexOptions.IgnoreCase);

					if (matches.Count == 1)
					{
						tempString = matches[0].Groups[1].ToString();
					}
					if (!string.IsNullOrEmpty(tempString))
					{
						CanonicalRef = tempString;
					}
				}

				try
				{
					var segmentsCount = Uri.Segments.Count();
					UrlPathDepth = segmentsCount;
				}
				catch
				{

				}
			}
		}

		// Tells the system if the content failed to parse.
		public bool Failed { get; set; }

		public string FailedMessage { get; set; }

		[NotMapped]
		public int UrlPathDepth { get; set; }

		public long Time { get; set; }

		public long Size { get; set; }

		public string ContentType { get; set; }

		public HttpStatusCode StatusCode { get; set; }

		public string StatusCodeDescription { get; set; }

		public string UrlsXml { get; set; }

		public string UrlsJson { get; set; }

		[NotMapped]
		public SpiderPageUrls SpiderPageUrls { get; set; }

		//[NotMapped]
		//public List<string> SourceUrls { get; set; }

		[NotMapped]
		public List<SpiderPageLink> DestinationUrls { get; set; }

		public string DestinationUrlsJson { get; set; }

		[NotMapped]
		public List<SpiderPageLink> ScriptUrls { get; set; }

		public string ScriptUrlsJson { get; set; }

		[NotMapped]
		public List<SpiderPageLink> LinkUrls { get; set; }

		public string LinkUrlsJson { get; set; }

		[NotMapped]
		public List<SpiderPageLink> ImageUrls { get; set; }

		public string ImageUrlsJson { get; set; }

		[NotMapped]
		public List<SpiderPageLink> OtherUrls { get; set; }

		public string OtherUrlsJson { get; set; }


		public string Content { get; set; }

		private void SetBrowserTitle()
		{
			var result = string.Empty;

			//"<title[^>]*>(.*?)<\/title>"
			if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
			{
				var matches = Regex.Matches(Content, "<title[^>]*>\\s*(.*?)\\s*<\\/title>", RegexOptions.IgnoreCase);

				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
				}
				else if (matches.Count > 1)
				{
					// Find the first browser title that conatins content.
					foreach (Match match in matches)
					{
						if (!string.IsNullOrEmpty(match.Groups[1].ToString()))
						{
							result = match.Groups[1].ToString();
							break;
						}
					}
					MultipleBrowserTitlesFound = true;
				}
				if (!string.IsNullOrEmpty(result))
				{
					BrowserTitle = result;

					if (BrowserTitle.Length < 40)
					{
						IsBrowserTitleToShort = true;
					}
					if (BrowserTitle.Length > 65)
					{
						IsBrowserTitleToLong = true;
					}
				}
				else
				{
					IsBrowserTitleEmpty = true;
				}
			}

		}

		public string BrowserTitle { get; set; }

		[NotMapped]
		public bool IsBrowserTitleEmpty { get; set; }

		[NotMapped]
		public bool MultipleBrowserTitlesFound { get; set; }

		[NotMapped]
		public bool IsBrowserTitleToShort { get; set; }

		[NotMapped]
		public bool IsBrowserTitleToLong { get; set; }

		private void SetMetaDescription()
		{
			var result = string.Empty;

			if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
			{
				var matches = Regex.Matches(Content, "<meta.*?name=\"description\".*?content=\"(.*?\\s?)\".*?>|<meta.*?content=\"(.*?\\s?)\".*?name=\"description\".*?>", RegexOptions.IgnoreCase);

				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
				}
				else if (matches.Count > 1)
				{
					// Find the first meta description that conatins content.
					foreach (Match match in matches)
					{
						if (!string.IsNullOrEmpty(match.Groups[1].ToString()))
						{
							result = match.Groups[1].ToString();
							break;
						}
					}
					MultipleMetaDescriptionFound = true;
				}
				if (!string.IsNullOrEmpty(result))
				{
					MetaDescription = result;

					if (MetaDescription.Length < 150)
					{
						IsMetaDescriptionToShort = true;
					}
					if (MetaDescription.Length > 165)
					{
						IsMetaDescriptionToLong = true;
					}
				}
				else
				{
					IsMetaDescriptionEmpty = true;
				}
			}

		}

		public string MetaDescription { get; set; }

		[NotMapped]
		public bool IsMetaDescriptionEmpty { get; set; }

		[NotMapped]
		public bool MultipleMetaDescriptionFound { get; set; }

		[NotMapped]
		public bool IsMetaDescriptionToShort { get; set; }

		[NotMapped]
		public bool IsMetaDescriptionToLong { get; set; }

		private void SetMetaKeywords()
		{
			var result = string.Empty;

			if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
			{
				var matches = Regex.Matches(Content, "<meta.*?name=\"keywords\".*?content=\"(.*?)\".*?>|<meta.*?content=\"(.*?)\".*?name=\"keywords\".*?>", RegexOptions.IgnoreCase);

				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
				}
				if (!string.IsNullOrEmpty(result))
				{
					MetaKeywords = result;
				}
				else
				{
					IsMetaKeywordsEmpty = true;
				}
			}

		}

		public string MetaKeywords { get; set; }

		[NotMapped]
		public bool IsMetaKeywordsEmpty { get; set; }

		public string CanonicalRef { get; set; }

		public string AlternativeLangRef { get; set; }

		public bool MatchPattern { get; set; }

		/// <summary>
		/// True/False if the URL is not on the same domain as start URL.
		/// </summary>
		public bool ExternalResource { get; set; }
	}
}
