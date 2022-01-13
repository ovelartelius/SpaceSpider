using System.Collections.Generic;

namespace FindTildeLinksOnSite
{
	public class ResultModel
	{
		public ResultModel()
		{
			TildeLinks = new List<string>();
		}

		public string PageUrl { get; set; }

		public List<string> TildeLinks { get; set; }	}
}
