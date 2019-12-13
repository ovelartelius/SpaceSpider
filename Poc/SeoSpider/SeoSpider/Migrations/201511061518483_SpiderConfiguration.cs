namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpiderConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpiderConfigurations",
                c => new
                    {
                        SpiderConfigurationId = c.Int(nullable: false, identity: true),
                        SpiderConfigurationKey = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        IpAddress = c.String(),
                        StartUrl = c.String(),
                        SettingsXml = c.String(),
                    })
                .PrimaryKey(t => t.SpiderConfigurationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpiderConfigurations");
        }
    }
}
