namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Make_LockoutEndDateUtc_Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(nullable: false));
        }
    }
}
