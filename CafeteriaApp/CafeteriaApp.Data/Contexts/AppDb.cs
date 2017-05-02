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
        public DbSet<Cafeteria> Cafeterias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
       
        public DbSet<MenuItem> MenuItems { get; set; }
        
        public DbSet<FavoriteItem> FavoriteItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<User> Persons { get; set; }

        public DbSet<Dependent> Dependents { get; set; }

        //--------------------------
        public DbSet<Comment> Comments { get; set; }
        //--------------------------

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer<AppDb>(null);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            Database.CommandTimeout = 180;

            modelBuilder.Entity<MenuItem>()
               .HasMany<Addition>(s => s.Additions)
               .WithMany(c => c.MenuItems)
               .Map(cs =>
               {
                   cs.MapLeftKey("MenuItemId");
                   cs.MapRightKey("AdditionId");
                   cs.ToTable("MenuItemAddition");
               });

            modelBuilder.Entity<Customer>()
              .HasMany<MenuItem>(s => s.Favourites)
              .WithMany(c => c.CustomersFavourite)
              .Map(cs =>
              {
                  cs.MapLeftKey("CustomerId");
                  cs.MapRightKey("MenuItemId");
                  cs.ToTable("CustomerFavourite");
              });

            modelBuilder.Entity<Customer>()
             .HasMany<MenuItem>(s => s.Restricts)
             .WithMany(c => c.CustomersRestricts)
             .Map(cs =>
             {
                 cs.MapLeftKey("CustomerId");
                 cs.MapRightKey("MenuItemId");
                 cs.ToTable("CustomerRestrict");
             });

            modelBuilder.Entity<Dependent>()
             .HasMany<MenuItem>(s => s.Restricts)
             .WithMany(c => c.DependentsRestricts)
             .Map(cs =>
             {
                 cs.MapLeftKey("DependentId");
                 cs.MapRightKey("MenuItemId");
                 cs.ToTable("DependentRestrict");
             });

            modelBuilder.Entity<User>()
           .HasMany<Role>(s => s.Roles)
           .WithMany(c => c.Persons)
           .Map(cs =>
           {
               cs.MapLeftKey("UserId");
               cs.MapRightKey("RoleId");
               cs.ToTable("AspNetUserRoles");
           });
        }
    }
}