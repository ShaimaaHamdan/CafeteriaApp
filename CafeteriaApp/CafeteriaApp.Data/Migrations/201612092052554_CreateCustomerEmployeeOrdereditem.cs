namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomerEmployeeOrdereditem : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Item");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
