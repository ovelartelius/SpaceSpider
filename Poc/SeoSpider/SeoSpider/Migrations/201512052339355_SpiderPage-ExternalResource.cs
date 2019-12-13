namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPageExternalResource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpiderPages", "ExternalResource", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "ExternalResource");
        }
    }
}
