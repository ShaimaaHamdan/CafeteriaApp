namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.MenuItem", t => t.MenuItemId)
                .Index(t => t.CustomerId)
                .Index(t => t.MenuItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "MenuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.Comment", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Comment", new[] { "MenuItemId" });
            DropIndex("dbo.Comment", new[] { "CustomerId" });
            DropTable("dbo.Comment");
        }
    }
}
