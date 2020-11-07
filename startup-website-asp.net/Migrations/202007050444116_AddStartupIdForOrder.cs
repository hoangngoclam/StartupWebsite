namespace startup_website_asp.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStartupIdForOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("ORDER.Order", "StartupId", c => c.Long(nullable: true));
            CreateIndex("ORDER.Order", "StartupId");
            AddForeignKey("ORDER.Order", "StartupId", "STARTUP.Startup", "StartupId");
        }
        
        public override void Down()
        {
            DropForeignKey("ORDER.Order", "StartupId", "STARTUP.Startup");
            DropIndex("ORDER.Order", new[] { "StartupId" });
            DropColumn("ORDER.Order", "StartupId");
        }
    }
}
