namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNameFromPhotoToImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "Image", c => c.String());
            DropColumn("dbo.MenuItem", "photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuItem", "photo", c => c.String());
            DropColumn("dbo.MenuItem", "Image");
        }
    }
}
