namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomerUserRelation3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PersonId", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "PersonId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "PersonId");
            CreateIndex("dbo.Employees", "PersonId");
            AddForeignKey("dbo.Customers", "PersonId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "PersonId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PersonId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "PersonId", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "PersonId" });
            DropIndex("dbo.Customers", new[] { "PersonId" });
            DropColumn("dbo.Employees", "PersonId");
            DropColumn("dbo.Customers", "PersonId");
        }
    }
}
