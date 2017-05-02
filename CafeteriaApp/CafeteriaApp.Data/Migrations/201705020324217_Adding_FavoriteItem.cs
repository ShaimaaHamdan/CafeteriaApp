namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_FavoriteItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuItemId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.MenuItem", t => t.MenuItemId)
                .Index(t => t.MenuItemId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteItem", "MenuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.FavoriteItem", "CustomerId", "dbo.Customer");
            DropIndex("dbo.FavoriteItem", new[] { "CustomerId" });
            DropIndex("dbo.FavoriteItem", new[] { "MenuItemId" });
            DropTable("dbo.FavoriteItem");
        }
    }
}
