namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.MenuItem", "photo", c => c.String());
           // AddColumn("dbo.MenuItem", "alternatetext", c => c.String());
        }
        
        public override void Down()
        {
           // DropColumn("dbo.MenuItem", "alternatetext");
           // DropColumn("dbo.MenuItem", "photo");
        }
    }
}
