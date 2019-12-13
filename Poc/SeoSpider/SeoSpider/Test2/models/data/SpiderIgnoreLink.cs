using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace SeoSpider.Test2.models.data
{
	/// <summary>
	/// Used as a cache and information about which links that are found in content that we ignore and pattern that dicides why.
	/// </summary>
	[DebuggerDisplay("{SpiderIgnoreLinkId}:{Url}:{IgnorePattern}")]
	public class SpiderIgnoreLink
	{
		public int SpiderIgnoreLinkId { get; set; }

		public int SpiderRunId { get; set; }

		// The link that shouls be ignored.
		public string Url { get; set; }

		/// <summary>
		/// The Regex pattern that dicided to ignore this pattern.
		/// </summary>
		public string IgnorePattern { get; set; }

		public string Description { get; set; }

	}

}
