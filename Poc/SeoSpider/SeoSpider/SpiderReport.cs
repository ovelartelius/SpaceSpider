using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SeoSpider
{
	public class SpiderReport
	{
		public int NumberOfPages { get; set; }

		[XmlIgnore]
		public string ReportText { get; set; }

		/// <summary>
		/// List of pages that does not have any browser titles.
		/// </summary>
		public List<ReportPage> NoBrowserTitle { get; set; }

		/// <summary>
		/// List of pages that contains multiple browser titles. It should only be one.
		/// </summary>
		public List<ReportPage> MultipleBrowserTitle { get; set; }

		/// <summary>
		/// List of pages that has a to short browser title.
		/// </summary>
		public List<ReportPage> ShortBrowserTitle { get; set; }

		/// <summary>
		/// List of pages that has a to long browser title.
		/// </summary>
		public List<ReportPage> LongBrowserTitle { get; set; }

		/// <summary>
		/// List of pages that has no meta description.
		/// </summary>
		public List<ReportPage> NoMetaDescription { get; set; }

		/// <summary>
		/// List of pages that has a to short meta description.
		/// </summary>
		public List<ReportPage> ShortMetaDescription { get; set; }

		/// <summary>
		/// List of pages that has a to long meta description.
		/// </summary>
		public List<ReportPage> LongMetaDescription { get; set; }

		/// <summary>
		/// List of pages that has no meta keywords.
		/// </summary>
		public List<ReportPage> NoMetaKeywords { get; set; }

		/// <summary>
		/// List of pages that contains erroneous resource files/links.
		/// </summary>
		public List<ReportPage> ContainsErrResource { get; set; }

		/// <summary>
		/// List of pages that contains links to URLs that is moved permanently.
		/// </summary>
		public List<ReportPage> LinksToMovedPermanently { get; set; }

		/// <summary>
		/// List of pages that contains link to images that is erroneous.
		/// </summary>
		public List<ReportPage> ContainsErrImageLinks { get; set; }

		/// <summary>
		/// List of pages that contains images that are very large.
		/// </summary>
		public List<ReportPage> ContainsLargeImages { get; set; }

		/// <summary>
		/// List of pages that are very large.
		/// </summary>
		public List<ReportPage> LargePages { get; set; }

		/// <summary>
		/// List of pages that takes to long time to load.
		/// </summary>
		public List<ReportPage> HighSpeedPages { get; set; }

		/// <summary>
		/// List of pages that takes medium long time to load.
		/// </summary>
		public List<ReportPage> WarningSpeedPages { get; set; }

		/// <summary>
		/// List of pages that redirect erroneous.
		/// </summary>
		public List<ReportPage> ErrRedirectPages { get; set; }

		/// <summary>
		/// List of pages that failed to load.
		/// </summary>
		public List<ReportPage> FailedPages { get; set; }

		/// <summary>
		/// List of pages that changed schema from the starting URL.
		/// </summary>
		public List<ReportPage> ChangedSchemaPages { get; set; }

		/// <summary>
		/// List of pages that changed host from the starting URL.
		/// </summary>
		public List<ReportPage> ChangedHostPages { get; set; }

		/// <summary>
		/// List of pages that contain alternate language reference but does not point to itself.
		/// </summary>
		public List<ReportPage> ErrAltLangHrefNoSelfPoint { get; set; }

		/// <summary>
		/// List of pages that does not point back on a page that use alt language ref to this page..
		/// </summary>
		public List<ReportPage> ErrAltLangHrefNoPointBack { get; set; }
	}

	public class ReportPage
	{
		public string Url { get; set; }

		public List<ReportPage> SubPages { get; set; }

		private HttpStatusCode _httpStatusCode = HttpStatusCode.Continue;

		public HttpStatusCode HttpStatusCode
		{
			get { return _httpStatusCode; }
			set { _httpStatusCode = value; }
		}

		public string Description { get; set; }
	}
}
