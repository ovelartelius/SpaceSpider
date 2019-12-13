namespace SeoSpider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpiderConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpiderConfigurations", "CompletedAt", c => c.DateTime());
            AddColumn("dbo.SpiderConfigurations", "SettingsJson", c => c.String());
            DropColumn("dbo.SpiderConfigurations", "IsDeleted");
            DropColumn("dbo.SpiderConfigurations", "DeletedAt");
            DropColumn("dbo.SpiderConfigurations", "IpAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpiderConfigurations", "IpAddress", c => c.String());
            AddColumn("dbo.SpiderConfigurations", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.SpiderConfigurations", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.SpiderConfigurations", "SettingsJson");
            DropColumn("dbo.SpiderConfigurations", "CompletedAt");
        }
    }
}
