using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeoSpider
{
	public class Fly
	{
		public Uri StartUrl { get; set; }
		
		public List<string> IgnoreList { get; set; } 

		// Ange hur djupt man skall köra.

		// Ange om den skall kolla ett steg in på andra domäner.
	}

	public class SpiderWeb
	{
		// Antal spindlade sidor totalt

		// Antal per extension (Pdf, Html(alla friendly url), ico, jpg etc.)

		// Antal sidor som inte har en browser title

		// Antal sidor som inte har en meta beskrivning

		// Antal sidor som har en för kort browser title

		// Antal sidor som har en dupplicerad browser title

		// Antal sidor som har en för kort meta beskrivning

		// Antal sidor som har en för lång meta beskrivning.

		// Antal sidor som har NOINDEX och/eller NOFOLLOW

		// Antal sidor som inte har en H1

		// Alla sidor
		// Sidor som är 404
		// Sidor som är 410
		// Sidor som är 500
		// Sidor som man inte har access till 403
		// Sidor som redirectar
		// Finns robots.txt
		// Finns en pekning på sitemap.xml i robots.txt?
		// Vilka sidor som inte finns med i sitemap.xml
		// Canonical ref
		// Language ref

		//Sidor som innehåller och matchar intressanta ord/mönster (i URL || content)
			// Skall alltså kunna ange flera ord/mönster och varje sådant skall 
			// samla ihop sitt eget resultat av sidor.
	}

	public class SpiderPage
	{
		public HttpStatusCode StatusCode { get; set; }
		public string StatusCodeDescription { get; set; }

		public List<string> SourceUrls { get; set; }
		public List<WorkingPageLink> DestinationUrls { get; set; }
		public string Content { get; set; }

	}

	///// <summary>
	/////  Spider Page link is used to describe link between page A and B.
	/////  The B page (destination page) could be a page on another domain.
	/////  But it is interesting to know if the B page is working.
	///// </summary>
	//[DebuggerDisplay("{Url}:{StatusCode}:{Description}")]
	//public class SpiderPageLink
	//{
	//	public string Url { get; set; }

	//	public Uri Uri
	//	{
	//		get
	//		{
	//			return new Uri(Url);
	//		}
	//	}

	//	public HttpStatusCode StatusCode { get; set; }

	//	public string Description { get; set; }

	//	public bool Erroneous { get; set; }
	//}
}
