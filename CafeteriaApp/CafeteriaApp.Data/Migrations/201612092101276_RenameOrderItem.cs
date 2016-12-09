namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameOrderItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderedItems", newName: "OrderItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.OrderItems", newName: "OrderedItems");
        }
    }
}
