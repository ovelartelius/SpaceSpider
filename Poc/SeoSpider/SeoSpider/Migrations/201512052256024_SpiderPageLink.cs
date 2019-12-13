namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPageLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderPageLinks",
                c => new
                    {
                        SpiderPageLinkId = c.Int(nullable: false, identity: true),
                        SpiderRunId = c.Int(nullable: false),
                        Url = c.String(),
                        StatusCode = c.Int(nullable: false),
                        Description = c.String(),
                        Erroneous = c.Boolean(nullable: false),
                        Content = c.String(),
                        Time = c.Long(nullable: false),
                        Size = c.Long(nullable: false),
                        ContentType = c.String(),
                    })
                .PrimaryKey(t => t.SpiderPageLinkId);
            
            AddColumn("dbo.SpiderPages", "ErroneousUrl", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "ErroneousUrl");
            DropTable("dbo.SpiderPageLinks");
        }
    }
}
