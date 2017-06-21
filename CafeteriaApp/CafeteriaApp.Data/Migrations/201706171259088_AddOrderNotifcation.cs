namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderNotifcation : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Comment",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CustomerId = c.Int(nullable: false),
            //            MenuItemId = c.Int(nullable: false),
            //            Data = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId)
            //    .ForeignKey("dbo.MenuItem", t => t.MenuItemId)
            //    .Index(t => t.CustomerId)
            //    .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.OrderNotification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        customerid = c.Int(nullable: false),
                        orderid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.customerid)
                .ForeignKey("dbo.Order", t => t.orderid)
                .Index(t => t.customerid)
                .Index(t => t.orderid);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Comment", "MenuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.OrderNotification", "orderid", "dbo.Order");
            DropForeignKey("dbo.OrderNotification", "customerid", "dbo.Customer");
            //DropForeignKey("dbo.Comment", "CustomerId", "dbo.Customer");
            DropIndex("dbo.OrderNotification", new[] { "orderid" });
            DropIndex("dbo.OrderNotification", new[] { "customerid" });
            //DropIndex("dbo.Comment", new[] { "MenuItemId" });
            //DropIndex("dbo.Comment", new[] { "CustomerId" });
            DropTable("dbo.OrderNotification");
            //DropTable("dbo.Comment");
        }
    }
}
