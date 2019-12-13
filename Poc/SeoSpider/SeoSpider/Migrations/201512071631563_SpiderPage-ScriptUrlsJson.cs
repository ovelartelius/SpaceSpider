namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderPageScriptUrlsJson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderExtLinks",
                c => new
                    {
                        SpiderExtLinkId = c.Int(nullable: false, identity: true),
                        SpiderRunId = c.Int(nullable: false),
                        PageUrl = c.String(),
                        ExtLinkUrl = c.String(),
                    })
                .PrimaryKey(t => t.SpiderExtLinkId);
            
            AddColumn("dbo.SpiderPages", "ScriptUrlsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpiderPages", "ScriptUrlsJson");
            DropTable("dbo.SpiderExtLinks");
        }
    }
}
