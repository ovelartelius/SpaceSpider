using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoSpider.Test2.models.data
{
	public class SpiderPageUrls
	{
		public SpiderPageUrls()
		{
			SourceUrls = new List<string>();
			DestinationUrls = new List<SpiderPageLink>();
			ScriptUrls = new List<SpiderPageLink>();
			LinkUrls = new List<SpiderPageLink>();
			ImageUrls = new List<SpiderPageLink>();
			AlternativeLangRefs = new List<string>();
			OtherUrls = new List<SpiderPageLink>();
		}

		public List<string> SourceUrls { get; set; }

		public List<SpiderPageLink> DestinationUrls { get; set; }

		public List<SpiderPageLink> ScriptUrls { get; set; }

		public List<SpiderPageLink> LinkUrls { get; set; }

		public List<SpiderPageLink> ImageUrls { get; set; }

		public List<SpiderPageLink> OtherUrls { get; set; }

		public List<string> AlternativeLangRefs { get; set; }

	}
}
