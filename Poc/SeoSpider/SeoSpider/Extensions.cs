using System;

namespace SeoSpider
{
	public static class Extensions
	{
		public static string ToUniversalSortableDateTime(this DateTime orgDateTime)
		{
			var result = string.Format("{0:s}Z", orgDateTime.ToUniversalTime()).Replace("-", "").Replace(":", "");
			return result;
		}

		public static string MakeFileNameFriendly(this string orgFileName)
		{
			var result = string.Empty;

			result = orgFileName.Replace("http://", "");
			result = result.Replace("https://", "");
			result = result.Replace("/", "");

			return result;
		}

	}
}
