namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderPages",
                c => new
                    {
                        SpiderPageId = c.Int(nullable: false, identity: true),
                        SpiderRunId = c.Int(nullable: false),
                        SpiderRunKey = c.Guid(nullable: false),
                        Url = c.String(),
                        CheckedOut = c.Boolean(nullable: false),
                        Handled = c.Boolean(nullable: false),
                        Time = c.Long(nullable: false),
                        Size = c.Long(nullable: false),
                        ContentType = c.String(),
                        StatusCode = c.Int(nullable: false),
                        StatusCodeDescription = c.String(),
                        UrlsXml = c.String(),
                        Content = c.String(),
                        BrowserTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        CanonicalRef = c.String(),
                        AlternativeLangRef = c.String(),
                        MatchPattern = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SpiderPageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpiderPages");
        }
    }
}
