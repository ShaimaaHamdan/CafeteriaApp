namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_photo_and_alternatetext_to_menuitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "photo", c => c.String());
            AddColumn("dbo.MenuItem", "alternatetext", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItem", "alternatetext");
            DropColumn("dbo.MenuItem", "photo");
        }
    }
}
