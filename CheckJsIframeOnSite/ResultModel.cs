using System.Collections.Generic;

namespace CheckJsIframeOnSite
{
	public class ResultModel
	{
		public ResultModel()
		{
			SubDtmScript = new List<string>();
		}

		public string PageUrl { get; set; }

		public List<string> Iframes { get; set; }

		public List<string> Scripts { get; set; }

		public List<string> AppIntApps { get; set; }

		public List<string> SubDtmScript { get; set; }
	}
}
