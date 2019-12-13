namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPageFailed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpiderPages", "Failed", c => c.Boolean(nullable: false));
            AddColumn("dbo.SpiderPages", "FailedMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "FailedMessage");
            DropColumn("dbo.SpiderPages", "Failed");
        }
    }
}
