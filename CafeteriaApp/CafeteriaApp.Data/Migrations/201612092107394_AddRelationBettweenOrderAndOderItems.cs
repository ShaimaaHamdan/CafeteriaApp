namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationBettweenOrderAndOderItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Order", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropColumn("dbo.OrderItems", "OrderId");
        }
    }
}
