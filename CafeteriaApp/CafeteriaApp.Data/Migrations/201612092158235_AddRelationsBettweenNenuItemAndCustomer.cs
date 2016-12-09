namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationsBettweenNenuItemAndCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cafeterias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerFavourite",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.MenuItemId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.CustomerRestrict",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.MenuItemId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.MenuItemId);
            
            AddColumn("dbo.Category", "CafeteriaId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "MenuItemId", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Category", "CafeteriaId");
            CreateIndex("dbo.Order", "CustomerId");
            CreateIndex("dbo.OrderItems", "MenuItemId");
            AddForeignKey("dbo.Category", "CafeteriaId", "dbo.Cafeterias", "Id");
            AddForeignKey("dbo.Order", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.OrderItems", "MenuItemId", "dbo.MenuItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerRestrict", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.CustomerRestrict", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderItems", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerFavourite", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.CustomerFavourite", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Category", "CafeteriaId", "dbo.Cafeterias");
            DropIndex("dbo.CustomerRestrict", new[] { "MenuItemId" });
            DropIndex("dbo.CustomerRestrict", new[] { "CustomerId" });
            DropIndex("dbo.CustomerFavourite", new[] { "MenuItemId" });
            DropIndex("dbo.CustomerFavourite", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "MenuItemId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Category", new[] { "CafeteriaId" });
            DropColumn("dbo.Order", "CustomerId");
            DropColumn("dbo.OrderItems", "MenuItemId");
            DropColumn("dbo.Category", "CafeteriaId");
            DropTable("dbo.CustomerRestrict");
            DropTable("dbo.CustomerFavourite");
            DropTable("dbo.Cafeterias");
        }
    }
}
