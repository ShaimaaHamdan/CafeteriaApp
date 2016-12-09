using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CafeteriaApp.Data.Models;


namespace CafeteriaApp.Data.Contexts
{
    public class AppDb : DbContext
    {
        public AppDb()
            : base("CafeteriaAppContext")
        {
            Configuration.LazyLoadingEnabled = true;

        }

        public DbSet<Addition> Additions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        //public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer<AppDb>(null);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            Database.CommandTimeout = 180;
        }
    }
}