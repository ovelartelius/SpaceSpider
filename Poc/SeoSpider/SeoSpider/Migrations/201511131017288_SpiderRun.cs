namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderRun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderRuns",
                c => new
                    {
                        SpiderRunId = c.Int(nullable: false, identity: true),
                        SpiderRunKey = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsStarted = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CompletedAt = c.DateTime(),
                        StartUrl = c.String(),
                        SettingsXml = c.String(),
                        SettingsJson = c.String(),
                    })
                .PrimaryKey(t => t.SpiderRunId);
            
            DropTable("dbo.SpiderConfigurations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SpiderConfigurations",
                c => new
                    {
                        SpiderConfigurationId = c.Int(nullable: false, identity: true),
                        SpiderConfigurationKey = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CompletedAt = c.DateTime(),
                        StartUrl = c.String(),
                        SettingsXml = c.String(),
                        SettingsJson = c.String(),
                    })
                .PrimaryKey(t => t.SpiderConfigurationId);
            
            DropTable("dbo.SpiderRuns");
        }
    }
}
