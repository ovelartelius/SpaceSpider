namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderIgnoreLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderIgnoreLinks",
                c => new
                    {
                        SpiderIgnoreLinkId = c.Int(nullable: false, identity: true),
                        SpiderRunId = c.Int(nullable: false),
                        Url = c.String(),
                        IgnorePattern = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SpiderIgnoreLinkId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpiderIgnoreLinks");
        }
    }
}
