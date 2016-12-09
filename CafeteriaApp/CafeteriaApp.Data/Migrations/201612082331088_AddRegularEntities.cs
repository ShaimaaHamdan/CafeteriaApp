namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegularEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Additions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Credit = c.Double(nullable: false),
                        LimitedCredit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderedItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MenuItems", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.MenuItems", "Description", c => c.String());
            AddColumn("dbo.MenuItems", "Type", c => c.String());
            AddColumn("dbo.Order", "PaymentDone", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order", "OrderTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Order", "PaymentMethod", c => c.String());
            AddColumn("dbo.Order", "DeliveryPlace", c => c.String());
            AddColumn("dbo.Order", "OrderStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderStatus");
            DropColumn("dbo.Order", "DeliveryPlace");
            DropColumn("dbo.Order", "PaymentMethod");
            DropColumn("dbo.Order", "OrderTime");
            DropColumn("dbo.Order", "PaymentDone");
            DropColumn("dbo.MenuItems", "Type");
            DropColumn("dbo.MenuItems", "Description");
            DropColumn("dbo.MenuItems", "Price");
            DropTable("dbo.OrderedItems");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Additions");
        }
    }
}
