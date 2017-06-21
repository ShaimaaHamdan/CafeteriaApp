namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataToOrderNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderNotification", "data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderNotification", "data");
        }
    }
}
