namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDependentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dependent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        DependentCredit = c.Double(nullable: false),
                        DayLimit = c.Double(nullable: false),
                        SchoolYear = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dependent", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Dependent", new[] { "CustomerId" });
            DropTable("dbo.Dependent");
        }
    }
}
