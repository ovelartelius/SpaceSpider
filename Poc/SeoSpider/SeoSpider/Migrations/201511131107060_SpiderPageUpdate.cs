namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPageUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpiderPages", "UrlsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "UrlsJson");
        }
    }
}
