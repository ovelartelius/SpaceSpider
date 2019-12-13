using System;
using System.Diagnostics;
using System.Net;

namespace SeoSpider.AgiltyTest
{
	[DebuggerDisplay("{Url}:{StatusCode}:{Description}")]
	public class PageResponse
	{
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
