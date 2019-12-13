using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SeoSpider.Test2.models.data
{
	public class SpiderRun
	{
		public SpiderRun()
		{
			//ReportUrls = new List<ReportUrl>();
			SpiderRunKey = Guid.NewGuid(); //GuidComb.Generate();

			//IgnoreLinkUrls = new List<string>();

			//IgnoreDestinationUrls = new List<string>();

			//IgnoreExternalHostUrls = new List<string>();

			//Pages = new List<SpiderPage>();

			//Cache = new List<SpiderPageLink>();

			//Pages.Add(new SpiderPage { Url = startUrl.AbsoluteUri });
			//AnchorRegexPattern = "<a[^>]*href=\"([^\"]*)\"[^>]*>";
			//ScriptRegexPattern = "<script.*?src=\"(.*?)\".*?>";
			//LinkRegexPattern = "<link.*?href=\"(.*?)\".*?>";
			//ImageRegexPattern = "<[img|IMG].*?src=\"(.*?)\".*?>";
			//HttpRegexPattern = "\"(http.*?)\"|\"(\\/.*?)\"|'(http.*?)'|'(\\/.*?)";
			//MaxSiteDepth = 1;
			//MaxNumberOfPages = 10;
			//ImageSizeLimit = 50000;
			//PageSizeLimit = 100000;
			//PageSpeedHighLimit = 1000;
			//PageSpeedWarningLimit = 500;
		}

		public int SpiderRunId { get; set; }

		public Guid SpiderRunKey { get; internal set; }

		//public virtual Customer Customer { get; set; }

		//public int CustomerId { get; set; }

		//[Required]
		//public Guid UserId { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool IsStarted { get; set; }

		public bool IsCompleted { get; set; }

		public DateTime? CompletedAt { get; set; }

		//public bool IsDeleted { get; set; }

		//public DateTime? DeletedAt { get; set; }

		//public string IpAddress { get; set; }

		public string StartUrl { get; set; }

		//[NotMapped]
		//public List<SpiderPageLink> Cache { get; set; }

		//[NotMapped]
		//public List<string> IgnoreLinkUrls { get; set; }

		public string SettingsXml { get; set; }

		public string SettingsJson { get; set; }

		[NotMapped]
		public SpiderSetting Settings
		{
			get
			{
				var settings = new SpiderSetting();

				if (!string.IsNullOrEmpty(SettingsXml))
				{
					settings = XmlSerializah.Deserialize<SpiderSetting>(SettingsXml);	
				}
				else if (!string.IsNullOrEmpty(SettingsJson))
				{
					settings = JsonConvert.DeserializeObject<SpiderSetting>(SettingsJson); // XmlSerializah.Deserialize<SpiderConfigurationSetting>(SettingsXml);	
				}
				
				return settings;
			}
		}

		//[NotMapped]
		//public List<string> IgnoreDestinationUrls { get; set; }

		//[NotMapped]
		//public List<string> IgnoreExternalHostUrls { get; set; }

		//public virtual ICollection<SpiderPage> Pages { get; set; }


	}
}
