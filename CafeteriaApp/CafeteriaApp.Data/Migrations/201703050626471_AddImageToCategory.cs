namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Image");
        }
    }
}
