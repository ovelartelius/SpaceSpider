using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SeoSpider
{
	
	public class WorkBase
	{
		[XmlIgnore]
		public Uri StartUrl { get; set; }

		[XmlIgnore]
		public List<WorkingPageLink> Cache { get; set; }

		[XmlIgnore]
		public List<string> IgnoreLinkUrls { get; set; }

		/// <summary>
		/// List of Regex patterns that should be ignored when we found this link. Anchor or resource does not matter.
		/// </summary>
		[XmlIgnore]
		public List<string> IgnoreLinkList { get; set; }

		[XmlIgnore]
		public List<string> IgnoreDestinationUrls { get; set; }

		/// <summary>
		/// List of regex that should be ignored when add as destination url.
		/// </summary>
		[XmlIgnore]
		public List<string> IgnoreDestinationList { get; set; }

		[XmlIgnore]
		public List<string> IgnoreExternalHostUrls { get; set; }

		/// <summary>
		/// List of regex that should be ignored when found external host url.
		/// </summary>
		[XmlIgnore]
		public List<string> IgnoreExternalHostList { get; set; }

		public WorkBase()
		{
		}

		public WorkBase(Uri startUrl)
		{
			StartUrl = startUrl;

			IgnoreLinkUrls = new List<string>();

			IgnoreLinkList = new List<string>();
			IgnoreLinkList.Add("^#");
			IgnoreLinkList.Add("^{{");
			IgnoreLinkList.Add("^javascript:");
			IgnoreLinkList.Add("^mailto:");
			IgnoreLinkList.Add("^tel:");
			IgnoreLinkList.Add("^sms:");
			IgnoreLinkList.Add("^data:image");
			IgnoreLinkList.Add("^@Model");
			IgnoreLinkList.Add("^\\?");
			//IgnoreList.Add(".pdf$");
			//IgnoreList.Add(".xls$");
			//IgnoreList.Add(".xlsx$");
			//IgnoreList.Add(".doc$");
			//IgnoreList.Add(".docx$");

			IgnoreDestinationUrls = new List<string>();

			IgnoreDestinationList = new List<string>();

			IgnoreExternalHostUrls = new List<string>();

			IgnoreExternalHostList = new List<string>();
			IgnoreExternalHostList.Add("^https:\\/\\/accounts.google.com\\/");
			IgnoreExternalHostList.Add("^http:\\/\\/www.facebook.com\\/");
			IgnoreExternalHostList.Add("^http:\\/\\/twitter.com\\/");
			IgnoreExternalHostList.Add("^http:\\/\\/www.linkedin.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/www.linkedin.com\\/");
			IgnoreExternalHostList.Add("^http:\\/\\/plus.google.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/instagram.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/www.flickr.com\\/");
			IgnoreExternalHostList.Add("^http:\\/\\/www.flickr.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/dreambroker.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/plus.google.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/accounts.google.com\\/");
			IgnoreExternalHostList.Add("^https:\\/\\/www.youtube.com\\/$");


			Pages = new List<WorkingPage>();

			Cache = new List<WorkingPageLink>();

			Pages.Add(new WorkingPage { Url = startUrl.AbsoluteUri });

			AnchorRegexPattern = "<a[^>]*href=\"([^\"]*)\"[^>]*>";

			ScriptRegexPattern = "<script.*?src=\"(.*?)\".*?>";

			LinkRegexPattern = "<link.*?href=\"(.*?)\".*?>";

			ImageRegexPattern = "<[img|IMG].*?src=\"(.*?)\".*?>";

			HttpRegexPattern = "\"(http.*?)\"|\"(\\/.*?)\"|'(http.*?)'|'(\\/.*?)";

			MaxSiteDepth = 1;

			MaxNumberOfPages = 10;

			ImageSizeLimit = 50000;

			PageSizeLimit = 100000;

			PageSpeedHighLimit = 1000;

			PageSpeedWarningLimit = 500;

			
		}

		public List<WorkingPage> Pages { get; set; }

		public bool RobotsTxt { get; set; }

		public bool SitemapXml { get; set; }

		public bool ExcludeExternalHosts { get; set; }

		public string AnchorRegexPattern { get; set; }

		public string ScriptRegexPattern { get; set; }

		public string LinkRegexPattern { get; set; }

		public string ImageRegexPattern { get; set; }

		public string HttpRegexPattern { get; set; }

		public int MaxSiteDepth { get; set; }

		public int MaxNumberOfPages { get; set; }

		public string MatchPattern { get; set; }

		public long ImageSizeLimit { get; set; }

		public long PageSizeLimit { get; set; }

		public long PageSpeedHighLimit { get; set; }

		public long PageSpeedWarningLimit { get; set; }
	}

	[DebuggerDisplay("{Url}:{Handled}{StatusCode}")]
	public class WorkingPage
	{
		public WorkingPage()
		{
			SourceUrls = new List<string>();
			DestinationUrls = new List<WorkingPageLink>();
			ScriptUrls = new List<WorkingPageLink>();
			LinkUrls = new List<WorkingPageLink>();
			ImageUrls = new List<WorkingPageLink>();
			AlternativeLangRefs = new List<string>();
			OtherUrls = new List<WorkingPageLink>();
			IframeLinks = new List<string>();
		}

		public void PopulateUrlResult(WorkingPageLink urlCheckResult)
		{
			Time = urlCheckResult.Time;
			StatusCode = urlCheckResult.StatusCode;
			Content = urlCheckResult.Content;
			StatusCodeDescription = urlCheckResult.Description;
			ContentType = urlCheckResult.ContentType;
			Size = urlCheckResult.Size;
		}


		public string Url { get; set; }

		[Browsable(false)]
		public Uri Uri
		{
			get
			{
				return new Uri(Url);
			}
		}

		[Browsable(false)]
		public bool CheckedOut { get; set; }

		private bool _handled = false;
		[Browsable(false)]
		public bool Handled {
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
					var stringBuilder = new StringBuilder();
					//var matches = Regex.Matches(Content, "<link.*?rel=\"alternate\".*?href=\"(.*?)\".*?>", RegexOptions.IgnoreCase);
					var matches = Regex.Matches(Content, "<link rel=\"alternate\" href=\"(.*?)\".*?>", RegexOptions.IgnoreCase);
					foreach (Match match in matches)
					{
						var link = match.Groups[1].ToString();
						AlternativeLangRefs.Add(link);
						stringBuilder.AppendLine(link);
					}
					//matches = Regex.Matches(Content, "<link.*?href=\"(.*?)\".*?rel=\"alternate\".*?>", RegexOptions.IgnoreCase);
					matches = Regex.Matches(Content, "<link href=\"(.*?)\" rel=\"alternate\".*?>", RegexOptions.IgnoreCase);
					foreach (Match match in matches)
					{
						var link = match.Groups[1].ToString();
						AlternativeLangRefs.Add(link);
						stringBuilder.AppendLine(link);
					}
					tempString = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(tempString))
					{
						AlternativeLangRef = tempString;
					}
					else
					{
						if (Content.Contains("rel=\"alternate\""))
						{
							Console.WriteLine(string.Format("Warning: alternate may not be found in {0}", Url));
						}
					}
				}

				tempString = string.Empty;
				//<link.*?rel="canonical".*?href="(.*?)".*?>|<link.*?href="(.*?)".*?rel="canonical".*?>
				// http://googlewebmastercentral.blogspot.se/2013/04/5-common-mistakes-with-relcanonical.html
				// https://support.google.com/webmasters/answer/139066?hl=en
				// https://moz.com/blog/rel-confused-answers-to-your-rel-canonical-questions
				if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
				{
					//var matches = Regex.Matches(Content, "<link.*?rel=\"canonical\".*?href=\"(.*?)\".*?>", RegexOptions.IgnoreCase);
					var matches = Regex.Matches(Content, "<link rel=\"canonical\" href=\"(.*?)\".*?>", RegexOptions.IgnoreCase);

					if (matches.Count == 1)
					{
						tempString = matches[0].Groups[1].ToString();
					}
					//matches = Regex.Matches(Content, "<link.*?href=\"(.*?)\".*?rel=\"canonical\".*?>", RegexOptions.IgnoreCase);
					matches = Regex.Matches(Content, "<link href=\"(.*?)\" rel=\"canonical\".*?>", RegexOptions.IgnoreCase);

					if (matches.Count == 1)
					{
						tempString = matches[0].Groups[1].ToString();
					}
					if (!string.IsNullOrEmpty(tempString))
					{
						CanonicalRef = tempString;
					}
					else
					{
						if (Content.Contains("rel=\"canonical\""))
						{
							Console.WriteLine(string.Format("Warning: canonical may not be found in {0}", Url));
						}

					}
				}
			} 
		}

		public long Time { get; set; }

		public long Size { get; set; }

		public string ContentType { get; set; }

		public HttpStatusCode StatusCode { get; set; }
		
		public string StatusCodeDescription { get; set; }

		[Browsable(false)]
		public List<string> SourceUrls { get; set; }

		[Browsable(false)]
		public List<WorkingPageLink> DestinationUrls { get; set; }

		[Browsable(false)]
		public List<WorkingPageLink> ScriptUrls { get; set; }

		[Browsable(false)]
		public List<WorkingPageLink> LinkUrls { get; set; }

		[Browsable(false)]
		public List<WorkingPageLink> ImageUrls { get; set; }

		[Browsable(false)]
		public List<WorkingPageLink> OtherUrls { get; set; }

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
					if (Content.Contains("<title"))
					{
						Console.WriteLine(string.Format("Warning: title may not be found in {0}", Url));
					}
				}
			}
			
		}

		public string BrowserTitle { get; set; }

		public bool IsBrowserTitleEmpty { get; set; }

		//private string _browserTitle = String.Empty;
		//public string BrowserTitle
		//{
		//	get
		//	{
		//		var result = _browserTitle;

		//		////"<title[^>]*>(.*?)<\/title>"
		//		//if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
		//		//{
		//		//	var matches = Regex.Matches(Content, "<title[^>]*>\\s*(.*?)\\s*<\\/title>", RegexOptions.IgnoreCase);

		//		//	if (matches.Count == 1)
		//		//	{
		//		//		result = matches[0].Groups[1].ToString();
		//		//	}
		//		//	else
		//		//	{
		//		//		// Find the first browser title that conatins content.
		//		//		foreach (Match match in matches)
		//		//		{
		//		//			if (!string.IsNullOrEmpty(match.Groups[1].ToString()))
		//		//			{
		//		//				result = match.Groups[1].ToString();
		//		//				break;
		//		//			}
		//		//		}
		//		//	}
		//		//	if (!string.IsNullOrEmpty(result))
		//		//	{
		//		//		_browserTitle = result;
		//		//	}
		//		//}

		//		return result;
		//	}
		//}

		public bool MultipleBrowserTitlesFound { get; set; }

		//private bool _multipleBrowserTitles = false;
		//public bool MultipleBrowserTitlesFound
		//{
		//	get
		//	{
		//		//var result = false;

		//		//if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
		//		//{
		//		//	var matches = Regex.Matches(Content, "<title[^>]*>\\s*(.*?)\\s*<\\/title>", RegexOptions.IgnoreCase);

		//		//	if (matches.Count > 1)
		//		//	{
		//		//		result = true;
		//		//	}
		//		//}

		//		//return result;
		//		return _multipleBrowserTitles;
		//	}			
		//}

		public bool IsBrowserTitleToShort { get; set; }

		//private bool _isBrowserTitleToShort = false;
		//[Browsable(false)]
		//public bool IsBrowserTitleToShort { get { return _isBrowserTitleToShort; } }

		public bool IsBrowserTitleToLong { get; set; }

		//private bool _isBrowserTitleToLong = false;
		//[Browsable(false)]
		//public bool IsBrowserTitleToLong { get { return _isBrowserTitleToLong; } }

		private void SetMetaDescription()
		{
			var result = string.Empty;

			if (!string.IsNullOrEmpty(Content) && ContentType.StartsWith("text/html"))
			{
				var matches = Regex.Matches(Content, "<meta name=\"description\" content=\"(.*?\\s?)\".*?>", RegexOptions.IgnoreCase);

				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
				}
				matches = Regex.Matches(Content, "<meta content=\"(.*?\\s?)\" name=\"description\".*?>", RegexOptions.IgnoreCase);
				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
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
					// Check if we can find something to warn about.
					if (Content.Contains("name=\"description\""))
					{
						Console.WriteLine(string.Format("Warning: meta description may not be found in {0}", Url));
					}
				}
			}
			
		}

		public string MetaDescription { get; set; }

		public bool IsMetaDescriptionEmpty { get; set; }
		//private string _metaDescription = String.Empty;
		//public string MetaDescription
		//{
		//	get
		//	{
		//		var result = _metaDescription;

		//		//if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
		//		//{
		//		//	var matches = Regex.Matches(Content, "<meta.*?name=\"description\".*?content=\"(.*?\\s?)\".*?>|<meta.*?content=\"(.*?\\s?)\".*?name=\"description\".*?>", RegexOptions.IgnoreCase);

		//		//	if (matches.Count == 1)
		//		//	{
		//		//		result = matches[0].Groups[1].ToString();
		//		//	}
		//		//	if (!string.IsNullOrEmpty(result))
		//		//	{
		//		//		_metaDescription = result;
		//		//	}
		//		//}

		//		return result;
		//	}
		//}

		public bool IsMetaDescriptionToShort { get; set; }

		public bool IsMetaDescriptionToLong { get; set; }

		private void SetMetaKeywords()
		{
			var result = string.Empty;

			if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
			{
				var matches = Regex.Matches(Content, "<meta name=\"keywords\" content=\"(.*?)\".*?>", RegexOptions.IgnoreCase);
				if (matches.Count == 1)
				{
					result = matches[0].Groups[1].ToString();
				}
				matches = Regex.Matches(Content, "<meta content=\"(.*?)\" name=\"keywords\".*?>", RegexOptions.IgnoreCase);
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
					// Check if we can find something to warn about.
					if (Content.Contains("name=\"keywords\""))
					{
						Console.WriteLine(string.Format("Warning: meta keywords may not be found in {0}", Url));
					}
				}
			}
			
		}

		public string MetaKeywords { get; set; }

		public bool IsMetaKeywordsEmpty { get; set; }

		//private string _metaKeywords = String.Empty;
		//public string MetaKeywords
		//{
		//	get
		//	{
		//		var result = _metaKeywords;

		//		if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
		//		{
		//			var matches = Regex.Matches(Content, "<meta.*?name=\"keywords\".*?content=\"(.*?)\".*?>|<meta.*?content=\"(.*?)\".*?name=\"keywords\".*?>", RegexOptions.IgnoreCase);

		//			if (matches.Count == 1)
		//			{
		//				result = matches[0].Groups[1].ToString();
		//			}
		//			if (!string.IsNullOrEmpty(result))
		//			{
		//				_metaKeywords = result;
		//			}
		//		}

		//		return result;
		//	}
		//}

		public string CanonicalRef { get; set; }

		//private string _canonicalRef = string.Empty;
		////<link.*?rel="canonical".*?href="(.*?)".*?>|<link.*?href="(.*?)".*?rel="canonical".*?>
		//// http://googlewebmastercentral.blogspot.se/2013/04/5-common-mistakes-with-relcanonical.html
		//// https://support.google.com/webmasters/answer/139066?hl=en
		//// https://moz.com/blog/rel-confused-answers-to-your-rel-canonical-questions
		//public string CanonicalRef
		//{
		//	get
		//	{
		//		var result = _canonicalRef;

		//		if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
		//		{
		//			var matches = Regex.Matches(Content, "<link.*?rel=\"canonical\".*?href=\"(.*?)\".*?>|<link.*?href=\"(.*?)\".*?rel=\"canonical\".*?>", RegexOptions.IgnoreCase);

		//			if (matches.Count == 1)
		//			{
		//				result = matches[0].Groups[1].ToString();
		//			}
		//			if (!string.IsNullOrEmpty(result))
		//			{
		//				_canonicalRef = result;
		//			}
		//		}

		//		return result;
		//	}
		//}

		//private string _scriptFiles = string.Empty;
		//public string ScriptFiles
		//{
		//	get
		//	{
		//		//var result = _scriptFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _scriptFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in ScriptUrls)
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_scriptFiles = result;
		//			}
					
		//		}
		//		return result;
		//	}
		//}

		//private string _errorScriptFiles = string.Empty;
		//public string ErrorScriptFiles
		//{
		//	get
		//	{
		//		//var result = _scriptFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _errorScriptFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in ScriptUrls.Where(x => x.StatusCode != HttpStatusCode.OK))
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_errorScriptFiles = result;
		//			}
		//			//_scriptFiles = result;

		//		}
		//		return result;
		//	}
		//}

		//private string _linkFiles = string.Empty;
		//public string LinkFiles
		//{
		//	get
		//	{
		//		//var result = _linkFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _linkFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in LinkUrls)
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_linkFiles = result;
		//			}
		//		}
		//		return result;
		//	}
		//}

		//private string _errorLinkFiles = string.Empty;
		//public string ErrorLinkFiles
		//{
		//	get
		//	{
		//		//var result = _linkFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _errorLinkFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in LinkUrls.Where(x => x.StatusCode != HttpStatusCode.OK))
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_errorLinkFiles = result;
		//			}
		//		}
		//		return result;
		//	}
		//}

		//private string _imageFiles = string.Empty;
		//public string ImageFiles
		//{
		//	get
		//	{
		//		//var result = _imageFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _imageFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in ImageUrls)
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_imageFiles = result;
		//			}
		//		}
		//		return result;
		//	}
		//}

		//private string _errorImageFiles = string.Empty;
		//public string ErrorImageFiles
		//{
		//	get
		//	{
		//		//var result = _imageFiles;
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _errorImageFiles;
		//		}
		//		if (string.IsNullOrEmpty(result))
		//		{
		//			var stringBuilder = new StringBuilder();
		//			foreach (var workingPageLink in ImageUrls.Where(x => x.StatusCode != HttpStatusCode.OK))
		//			{
		//				stringBuilder.AppendLine(string.Format("{0}-{1}", workingPageLink.StatusCode, workingPageLink.Url));
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_errorImageFiles = result;
		//			}
		//		}
		//		return result;
		//	}
		//}

		public string AlternativeLangRef { get; set; }

		public List<string> AlternativeLangRefs { get; set; }

		//private string _altLangRef = string.Empty;
		//public string AlternativeLangRef
		//{
		//	get
		//	{
		//		var result = string.Empty;
		//		if (Handled)
		//		{
		//			result = _altLangRef;
		//		}
		//		if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(Content))
		//		{
		//			//<link.*?rel="alternate".*?hreflang="(.*?)".*?href="(.*?)".*?>
		//			var matches = Regex.Matches(Content, "<link.*?rel=\"alternate\".*?href=\"(.*?)\".*?>|<link.*?href=\"(.*?)\".*?rel=\"alternate\".*?>", RegexOptions.IgnoreCase);
		//			var stringBuilder = new StringBuilder();
		//			foreach (Match match in matches)
		//			{
		//				stringBuilder.AppendLine(match.Groups[1].ToString());
		//			}
		//			result = stringBuilder.ToString();
		//			if (Handled)
		//			{
		//				_altLangRef = result;
		//			}
		//		}
		//		return result;
		//	}
		//}

		public bool MatchPattern { get; set; }

		[XmlIgnore]
		public List<string> IframeLinks { get; set; }

	}

	[DebuggerDisplay("{Url}:{StatusCode}:{Description}")]
	public class WorkingPageLink
	{
		//public WorkingPageLink()
		//{
			
		//}
		//public WorkingPageLink(UrlCheckResult checkResult)
		//{
		//	StatusCode = checkResult.StatusCode;
		//	Description = checkResult.Description;
		//	Erroneous = checkResult.Erroneous;
		//	Content = checkResult.Content;
		//	Time = checkResult.Time;
		//	Size = checkResult.Size;
		//}

		public string Url { get; set; }

		public Uri Uri
		{
			get
			{
				Uri uri = null;

				try
				{
					uri = new Uri(Url);
				}
				catch
				{
					uri = null;
				}

				return uri;
			}
		}
		public HttpStatusCode StatusCode { get; set; }

		public string Description { get; set; }

		public bool Erroneous { get; set; }

		public string Content { get; set; }

		public long Time { get; set; }

		public long Size { get; set; }

		public string ContentType { get; set; }

	}
}
