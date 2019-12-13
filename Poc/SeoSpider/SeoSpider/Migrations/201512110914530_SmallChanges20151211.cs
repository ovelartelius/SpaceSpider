namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallChanges20151211 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpiderPages", "DestinationUrlsJson", c => c.String());
            AddColumn("dbo.SpiderPages", "LinkUrlsJson", c => c.String());
            AddColumn("dbo.SpiderPages", "ImageUrlsJson", c => c.String());
            AddColumn("dbo.SpiderPages", "OtherUrlsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "OtherUrlsJson");
            DropColumn("dbo.SpiderPages", "ImageUrlsJson");
            DropColumn("dbo.SpiderPages", "LinkUrlsJson");
            DropColumn("dbo.SpiderPages", "DestinationUrlsJson");
        }
    }
}
