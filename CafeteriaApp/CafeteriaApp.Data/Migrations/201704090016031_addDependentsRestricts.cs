namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDependentsRestricts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DependentRestrict",
                c => new
                    {
                        DependentId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DependentId, t.MenuItemId })
                .ForeignKey("dbo.Dependent", t => t.DependentId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItem", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.DependentId)
                .Index(t => t.MenuItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DependentRestrict", "MenuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.DependentRestrict", "DependentId", "dbo.Dependent");
            DropIndex("dbo.DependentRestrict", new[] { "MenuItemId" });
            DropIndex("dbo.DependentRestrict", new[] { "DependentId" });
            DropTable("dbo.DependentRestrict");
        }
    }
}
