namespace CafeteriaApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Additions", newName: "Addition");
            RenameTable(name: "dbo.MenuItems", newName: "MenuItem");
            RenameTable(name: "dbo.Cafeterias", newName: "Cafeteria");
            RenameTable(name: "dbo.Customers", newName: "Customer");
            RenameTable(name: "dbo.OrderItems", newName: "OrderItem");
            RenameTable(name: "dbo.Employees", newName: "Employee");
            RenameColumn(table: "dbo.Customer", name: "PersonId", newName: "UserId");
            RenameColumn(table: "dbo.Employee", name: "PersonId", newName: "UserId");
            RenameIndex(table: "dbo.Customer", name: "IX_PersonId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Employee", name: "IX_PersonId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employee", name: "IX_UserId", newName: "IX_PersonId");
            RenameIndex(table: "dbo.Customer", name: "IX_UserId", newName: "IX_PersonId");
            RenameColumn(table: "dbo.Employee", name: "UserId", newName: "PersonId");
            RenameColumn(table: "dbo.Customer", name: "UserId", newName: "PersonId");
            RenameTable(name: "dbo.Employee", newName: "Employees");
            RenameTable(name: "dbo.OrderItem", newName: "OrderItems");
            RenameTable(name: "dbo.Customer", newName: "Customers");
            RenameTable(name: "dbo.Cafeteria", newName: "Cafeterias");
            RenameTable(name: "dbo.MenuItem", newName: "MenuItems");
            RenameTable(name: "dbo.Addition", newName: "Additions");
        }
    }
}
