namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToDependentAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dependent", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.Dependent", "Image");
        }
    }
}
