namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuItemAndRelationTOCateogry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItems", "CategoryId", "dbo.Category");
            DropIndex("dbo.MenuItems", new[] { "CategoryId" });
            DropTable("dbo.MenuItems");
        }
    }
}
