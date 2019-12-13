using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace SeoSpider.Test2.models.data
{
	[DebuggerDisplay("{Url}:{StatusCode}:{Description}")]
	public class SpiderPageLink
	{
		public int SpiderPageLinkId { get; set; }

		public int SpiderRunId { get; set; }

		public string Url { get; set; }

		[NotMapped]
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
