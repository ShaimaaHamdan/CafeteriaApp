namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrderCategoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Name", c => c.String());
            AddColumn("dbo.Order", "DeliveryTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "DeliveryTime");
            DropColumn("dbo.Category", "Name");
        }
    }
}
