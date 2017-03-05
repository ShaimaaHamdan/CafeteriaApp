namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToCafeteria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cafeteria", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cafeteria", "Image");
        }
    }
}
