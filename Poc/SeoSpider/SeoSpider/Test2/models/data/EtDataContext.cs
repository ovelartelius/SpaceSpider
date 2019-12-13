using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SeoSpider.Test2.models.data
{
	public interface IEtDataContext
	{
		IDbSet<SpiderRun> SpiderRuns { get; }
		IDbSet<SpiderPage> SpiderPages { get; }
		IDbSet<SpiderPageLink> SpiderPageLinks { get; }
		IDbSet<SpiderExtLink> SpiderExtLinks { get; }
		IDbSet<SpiderIgnoreLink> SpiderIgnoreLinks { get; }

		int SaveChanges();
		DbEntityEntry Entry(object entity);
		void Dispose();
	}

	public class EtDataContext : DbContext, IEtDataContext
    {
        public EtDataContext()
            : base("DefaultConnection")
        {

        }

		public virtual IDbSet<SpiderRun> SpiderRuns { get; set; }
		public virtual IDbSet<SpiderPage> SpiderPages { get; set; }
		public virtual IDbSet<SpiderPageLink> SpiderPageLinks { get; set; }
		public virtual IDbSet<SpiderExtLink> SpiderExtLinks { get; set; }
		public virtual IDbSet<SpiderIgnoreLink> SpiderIgnoreLinks { get; set; } 
	}
}