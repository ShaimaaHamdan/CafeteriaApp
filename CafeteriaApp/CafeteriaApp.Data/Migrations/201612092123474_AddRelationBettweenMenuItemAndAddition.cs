namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationBettweenMenuItemAndAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemAddition",
                c => new
                    {
                        MenuItemId = c.Int(nullable: false),
                        AdditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItemId, t.AdditionId })
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .ForeignKey("dbo.Additions", t => t.AdditionId, cascadeDelete: true)
                .Index(t => t.MenuItemId)
                .Index(t => t.AdditionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemAddition", "AdditionId", "dbo.Additions");
            DropForeignKey("dbo.MenuItemAddition", "MenuItemId", "dbo.MenuItems");
            DropIndex("dbo.MenuItemAddition", new[] { "AdditionId" });
            DropIndex("dbo.MenuItemAddition", new[] { "MenuItemId" });
            DropTable("dbo.MenuItemAddition");
        }
    }
}
