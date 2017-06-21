namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cafeteria", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cafeteria", "ImageUrl");
        }
    }
}
