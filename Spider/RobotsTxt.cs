namespace Spider
{
	public class RobotsTxt
	{
		/// <summary>
		/// Create a robots.txt URL of a site domain URL. ex: https://seb.no => https://seb.no/robots.txt
		/// </summary>
		public string CreateRobotsTxtUrl(string url)
		{
			var result = url;

			if (!url.ToLower().EndsWith("/robots.txt"))
			{
				result = !url.EndsWith("/") ? $"{url}/robots.txt" : $"{url}robots.txt";
			}

			return result;
		}

		//public bool DisallowAll(string url)
		//{
		//	//User-agent: *	
		//	//Disallow: /
		//}
	}
}
