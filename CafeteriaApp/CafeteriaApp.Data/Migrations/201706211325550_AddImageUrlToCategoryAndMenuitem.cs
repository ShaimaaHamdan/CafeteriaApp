namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrlToCategoryAndMenuitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "ImageUrl", c => c.String());
            AddColumn("dbo.Category", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "ImageUrl");
            DropColumn("dbo.MenuItem", "ImageUrl");
        }
    }
}
