using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SeoSpider.Test2.models.data;

namespace SeoSpider.Test2
{
	public interface ISpiderDataProvider
	{
		SpiderRun SaveSpiderRun(EtDataContext db, SpiderRun spiderRun);

		models.data.SpiderPage SaveSpiderPage(EtDataContext db, models.data.SpiderPage spiderPage);

		models.data.SpiderPageLink SaveSpiderPageLink(EtDataContext db, models.data.SpiderPageLink spiderPageLink);

		models.data.SpiderPageLink GetSpiderPageLink(EtDataContext db, int spiderRunId, string url);

		models.data.SpiderExtLink SaveSpiderExtLink(EtDataContext db, models.data.SpiderExtLink spiderExtLink);

		models.data.SpiderExtLink GetSpiderExtLink(EtDataContext db, int spiderRunId, string pageUrl, string extLinkUrl);

		models.data.SpiderIgnoreLink SaveSpiderIgnoreLink(EtDataContext db, models.data.SpiderIgnoreLink spiderIgnoreLink);

		models.data.SpiderIgnoreLink GetSpiderIgnoreLink(EtDataContext db, int spiderRunId, string url);

		/// <summary>
		/// Get the number pages exist in the spiderRun.
		/// </summary>
		int SpiderRunPagesCount(EtDataContext db, int spiderRunId);

		models.data.SpiderPage GetSpiderPage(EtDataContext db, int spiderRunId, string url);
	}

	public class SpiderDataProvider : ISpiderDataProvider
	{
		/// <summary>
		/// Create or Update SpiderRun. If SpiderRun contains a existing SpiderRunId it will be updated. If SpiderRunId = 0 it will be created.
		/// </summary>
		/// <param name="db">Database context that should be used.</param>
		/// <param name="spiderRun">The SpiderRun to Create/Update</param>
		/// <returns>Created/Updated SpiderRun object. Will contain created/updated information.</returns>
		public SpiderRun SaveSpiderRun(EtDataContext db, SpiderRun spiderRun)
		{
			SpiderRun result = null;

			if (spiderRun == null) throw new ArgumentException("No SpiderRun attribute provided.");

			if (spiderRun.SpiderRunId == 0)
			{
				// Create the SpiderRun
				try
				{
					db.SpiderRuns.Add(spiderRun);
					db.SaveChanges();
					result = spiderRun;
					//Log.LogDebug(String.Format("Location {0} is created.", item.Name));
					Console.WriteLine("SpiderRun is created");
				}
				catch (Exception ex)
				{
					//Log.LogError(ex, String.Format("Failed to create Location {0}.", item.Name));
					Console.WriteLine("SpiderRun could not be created");
					Console.WriteLine(ex.Message);
					throw new Exception("Could not create SpiderRun", ex);
				}
			}
			else
			{
				//Update existing
				try
				{
					var original = db.SpiderRuns.FirstOrDefault(x => x.SpiderRunId == spiderRun.SpiderRunId);
					if (original != null)
					{
						db.Entry(original).CurrentValues.SetValues(spiderRun);
						db.SaveChanges();
						result = spiderRun;
					}
					else
					{
						//Log.LogWarning(String.Format("Failed to update Location. Location is null."));
						Console.WriteLine("Could not found any SPiderRun to update with SpiderRunId {0}", spiderRun.SpiderRunId);
					}
				}
				catch (Exception ex)
				{
					//Log.LogError(ex, String.Format("Failed to update Location {0}.", item.Id));
					Console.WriteLine("SpiderRun {0} could not be updated", spiderRun.SpiderRunId);
					Console.WriteLine(ex.Message);
					throw new Exception("Could not update SpiderRun", ex);
				}

			}

			return result;
		}

		/// <summary>
		/// Create or Update SpiderPage. If SpiderPage contains a existing SpiderPageId it will be updated. If SpiderPageId = 0 it will be created.
		/// </summary>
		/// <param name="db">Database context that should be used.</param>
		/// <param name="spiderPage">The SpiderPage to Create/Update</param>
		/// <returns>Created/Updated SpiderPage object. Will contain created/updated information.</returns>
		public models.data.SpiderPage SaveSpiderPage(EtDataContext db, SeoSpider.Test2.models.data.SpiderPage spiderPage)
		{
			models.data.SpiderPage result = null;

			if (spiderPage == null) throw new ArgumentException("No SpiderPage attribute provided.");

			if (spiderPage.SpiderPageId == 0)
			{
				// Create the SpiderPage
				try
				{
					db.SpiderPages.Add(spiderPage);
					db.SaveChanges();
					result = spiderPage;
					Console.WriteLine("SpiderPage is created");
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderPage could not be created");
					Console.WriteLine(ex.Message);
					throw new Exception("Could not create SpiderPage", ex);
				}
			}
			else
			{
				//Update existing
				try
				{
					var original = db.SpiderPages.FirstOrDefault(x => x.SpiderPageId == spiderPage.SpiderPageId);
					if (original != null)
					{
						db.Entry(original).CurrentValues.SetValues(spiderPage);
						db.SaveChanges();
						result = spiderPage;
					}
					else
					{
						Console.WriteLine("Could not found any SpiderPage to update with SpiderPageId {0}", spiderPage.SpiderPageId);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderPage {0} could not be updated", spiderPage.SpiderPageId);
					Console.WriteLine(ex.Message);
					throw new Exception("Could not update SpiderPage", ex);
				}

			}

			return result;
		}

		/// <summary>
		/// Create or Update SpiderPageLink. If SpiderPage contains a existing SpiderPageLinkId it will be updated. If SpiderPageLinkId = 0 it will be created.
		/// </summary>
		/// <param name="db">Database context that should be used.</param>
		/// <param name="spiderPageLink">The SpiderPageLink to Create/Update</param>
		/// <returns>Created/Updated SpiderPageLink object. Will contain created/updated information.</returns>
		public models.data.SpiderPageLink SaveSpiderPageLink(EtDataContext db, models.data.SpiderPageLink spiderPageLink)
		{
			models.data.SpiderPageLink result = null;

			if (spiderPageLink == null) throw new ArgumentException("No SpiderPageLink attribute provided.");

			if (spiderPageLink.SpiderPageLinkId == 0)
			{
				// Create the SpiderPageLink
				try
				{
					db.SpiderPageLinks.Add(spiderPageLink);
					db.SaveChanges();
					result = spiderPageLink;
					Console.WriteLine("SpiderPageLink is created");
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderPageLink could not be created");
					Console.WriteLine(ex.Message);
					throw new Exception("Could not create SpiderPageLink", ex);
				}
			}
			else
			{
				//Update existing
				try
				{
					var original = db.SpiderPageLinks.FirstOrDefault(x => x.SpiderPageLinkId == spiderPageLink.SpiderPageLinkId);
					if (original != null)
					{
						db.Entry(original).CurrentValues.SetValues(spiderPageLink);
						db.SaveChanges();
						result = spiderPageLink;
					}
					else
					{
						Console.WriteLine("Could not found any SpiderPageLink to update with SpiderPageLinkId {0}", spiderPageLink.SpiderPageLinkId);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderPageLink {0} could not be updated", spiderPageLink.SpiderPageLinkId);
					Console.WriteLine(ex.Message);
					throw new Exception("Could not update SpiderPageLink", ex);
				}

			}

			return result;
		}

		public models.data.SpiderPageLink GetSpiderPageLink(EtDataContext db, int spiderRunId, string url)
		{

			models.data.SpiderPageLink item = null;

			const string query = "SELECT TOP 1 * FROM SpiderPageLinks WHERE SpiderRunId = @SpiderRunId AND Url = @Url";
			var parameters = new DynamicParameters();
			parameters.Add("SpiderRunId", spiderRunId);
			parameters.Add("Url", url);

			IEnumerable<models.data.SpiderPageLink> items = db.Database.Connection.Query<models.data.SpiderPageLink>(query, parameters);
			if (items != null && items.Count() != 0)
			{
				item = items.First();
			}


			return item;
		}

		/// <summary>
		/// Create or Update SpiderExtLink. If SpiderExtLink contains a existing SpiderExtLinkId it will be updated. If SpiderExtLinkId = 0 it will be created.
		/// </summary>
		/// <param name="db">Database context that should be used.</param>
		/// <param name="spiderExtLink">The SpiderExtLink to Create/Update</param>
		/// <returns>Created/Updated SpiderExtLink object. Will contain created/updated information.</returns>
		public models.data.SpiderExtLink SaveSpiderExtLink(EtDataContext db, models.data.SpiderExtLink spiderExtLink)
		{
			models.data.SpiderExtLink result = null;

			if (spiderExtLink == null) throw new ArgumentException("No SpiderExtLink attribute provided.");

			if (spiderExtLink.SpiderExtLinkId == 0)
			{
				// Create the SpiderExtLink
				try
				{
					db.SpiderExtLinks.Add(spiderExtLink);
					db.SaveChanges();
					result = spiderExtLink;
					Console.WriteLine("SpiderExtLink is created");
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderExtLink could not be created");
					Console.WriteLine(ex.Message);
					throw new Exception("Could not create SpiderExtLink", ex);
				}
			}
			else
			{
				//Update existing
				try
				{
					var original = db.SpiderExtLinks.FirstOrDefault(x => x.SpiderExtLinkId == spiderExtLink.SpiderExtLinkId);
					if (original != null)
					{
						db.Entry(original).CurrentValues.SetValues(spiderExtLink);
						db.SaveChanges();
						result = spiderExtLink;
					}
					else
					{
						Console.WriteLine("Could not found any SpiderExtLink to update with SpiderExtLinkId {0}", spiderExtLink.SpiderExtLinkId);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderExtLink {0} could not be updated", spiderExtLink.SpiderExtLinkId);
					Console.WriteLine(ex.Message);
					throw new Exception("Could not update SpiderExtLink", ex);
				}
			}

			return result;
		}

		public models.data.SpiderExtLink GetSpiderExtLink(EtDataContext db, int spiderRunId, string pageUrl, string extLinkUrl)
		{

			models.data.SpiderExtLink item = null;

			const string query = "SELECT TOP 1 * FROM SpiderExtLinks WHERE SpiderRunId = @SpiderRunId AND PageUrl = @PageUrl AND ExtLinkUrl = @ExtLinkUrl";
			var parameters = new DynamicParameters();
			parameters.Add("SpiderRunId", spiderRunId);
			parameters.Add("PageUrl", pageUrl);
			parameters.Add("ExtLinkUrl", extLinkUrl);

			IEnumerable<models.data.SpiderExtLink> items = db.Database.Connection.Query<models.data.SpiderExtLink>(query, parameters);
			if (items != null && items.Count() != 0)
			{
				item = items.First();
			}

			return item;
		}

		/// <summary>
		/// Create or Update SpiderIgnoreLink. If SpiderIgnoreLink contains a existing SpiderIgnoreLinkId it will be updated. If SpiderIgnoreLinkId = 0 it will be created.
		/// </summary>
		/// <param name="db">Database context that should be used.</param>
		/// <param name="spiderIgnoreLink">The SpiderIgnoreLink to Create/Update</param>
		/// <returns>Created/Updated SpiderIgnoreLink object. Will contain created/updated information.</returns>
		public models.data.SpiderIgnoreLink SaveSpiderIgnoreLink(EtDataContext db, models.data.SpiderIgnoreLink spiderIgnoreLink)
		{
			models.data.SpiderIgnoreLink result = null;

			if (spiderIgnoreLink == null) throw new ArgumentException("No SpiderIgnoreLink attribute provided.");

			if (spiderIgnoreLink.SpiderIgnoreLinkId == 0)
			{
				// Create the SpiderIgnoreLink
				try
				{
					db.SpiderIgnoreLinks.Add(spiderIgnoreLink);
					db.SaveChanges();
					result = spiderIgnoreLink;
					Console.WriteLine("SpiderIgnoreLink is created");
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderIgnoreLink could not be created");
					Console.WriteLine(ex.Message);
					throw new Exception("Could not create SpiderIgnoreLink", ex);
				}
			}
			else
			{
				//Update existing
				try
				{
					var original = db.SpiderIgnoreLinks.FirstOrDefault(x => x.SpiderIgnoreLinkId == spiderIgnoreLink.SpiderIgnoreLinkId);
					if (original != null)
					{
						db.Entry(original).CurrentValues.SetValues(spiderIgnoreLink);
						db.SaveChanges();
						result = spiderIgnoreLink;
					}
					else
					{
						Console.WriteLine("Could not found any SpiderIgnoreLink to update with SpiderIgnoreLinkId {0}", spiderIgnoreLink.SpiderIgnoreLinkId);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("SpiderIgnoreLink {0} could not be updated", spiderIgnoreLink.SpiderIgnoreLinkId);
					Console.WriteLine(ex.Message);
					throw new Exception("Could not update SpiderIgnoreLink", ex);
				}
			}

			return result;
		}

		public models.data.SpiderIgnoreLink GetSpiderIgnoreLink(EtDataContext db, int spiderRunId, string url)
		{

			models.data.SpiderIgnoreLink item = null;

			const string query = "SELECT TOP 1 * FROM SpiderIgnoreLinks WHERE SpiderRunId = @SpiderRunId AND Url = @Url";
			var parameters = new DynamicParameters();
			parameters.Add("SpiderRunId", spiderRunId);
			parameters.Add("Url", url);

			IEnumerable<models.data.SpiderIgnoreLink> items = db.Database.Connection.Query<models.data.SpiderIgnoreLink>(query, parameters);
			if (items != null && items.Count() != 0)
			{
				item = items.First();
			}

			return item;
		}

		public int SpiderRunPagesCount(EtDataContext db, int spiderRunId)
		{
			var numberOfPages = 0;

			const string query = "SELECT COUNT(SpiderPageId) FROM SpiderPages WHERE SpiderRunId = @SpiderRunId";
			var parameters = new DynamicParameters();
			parameters.Add("SpiderRunId", spiderRunId);

			IEnumerable<int> numbers = db.Database.Connection.Query<int>(query, parameters);
			if (numbers != null && numbers.Count() != 0)
			{
				numberOfPages = numbers.First();
			}

			return numberOfPages;
		}

		public models.data.SpiderPage GetSpiderPage(EtDataContext db, int spiderRunId, string url)
		{

			models.data.SpiderPage item = null;

			const string query = "SELECT TOP 1 * FROM SpiderPages WHERE SpiderRunId = @SpiderRunId AND Url = @Url";
			var parameters = new DynamicParameters();
			parameters.Add("SpiderRunId", spiderRunId);
			parameters.Add("Url", url);

			IEnumerable<models.data.SpiderPage> items = db.Database.Connection.Query<models.data.SpiderPage>(query, parameters);
			if (items != null && items.Count() != 0)
			{
				item = items.First();
			}

			return item;
		}
	}
}
