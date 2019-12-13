using System.Collections.Generic;

namespace SeoSpider.Test2.models
{
	public class SpiderSetting
	{
		public SpiderSetting() 
		{
			IgnoreLinkList = new List<string>();
			IgnoreDestinationList = new List<string>();
			IgnoreExternalHostList = new List<string>();
		}

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

		//public List<string> IgnoreLinkUrls { get; set; } //TODO: Kontrollera om inte detta skall ligga som cache eller i db. så att den inte laddas om för varje sidkörning.

		/// <summary>
		/// List of Regex patterns that should be ignored when we found this link. Anchor or resource does not matter.
		/// </summary>
		public List<string> IgnoreLinkList { get; set; }

		/// <summary>
		/// List of regex that should be ignored when add as destination url.
		/// </summary>
		public List<string> IgnoreDestinationList { get; set; }

		//public List<string> IgnoreExternalHostUrls { get; set; } //TODO: Kontrollera om inte detta skall ligga som cache eller i db. så att den inte laddas om för varje sidkörning.

		/// <summary>
		/// List of regex that should be ignored when found external host url.
		/// </summary>
		public List<string> IgnoreExternalHostList { get; set; }

	}
}
