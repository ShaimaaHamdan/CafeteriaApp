namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditeInUserEntity : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            //DropColumn("dbo.AspNetUsers", "IAccessFailedCountd");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.AspNetUsers", "IAccessFailedCountd", c => c.Int(nullable: false));
            //DropColumn("dbo.AspNetUsers", "AccessFailedCount");
        }
    }
}
