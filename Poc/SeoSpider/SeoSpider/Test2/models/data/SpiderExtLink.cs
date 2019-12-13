using System.Diagnostics;

namespace SeoSpider.Test2.models.data
{
	[DebuggerDisplay("{SpiderExtLinkId}:{ExtLinkUrl}:{SpiderRunId}")]
	public class SpiderExtLink
	{
		public int SpiderExtLinkId { get; set; }

		public int SpiderRunId { get; set; }

		public string PageUrl { get; set; }

		public string ExtLinkUrl { get; set; }
	}

}
