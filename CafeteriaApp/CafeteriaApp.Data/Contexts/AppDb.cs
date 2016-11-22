﻿using System;
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
        public DbSet<UserAccount> UserAccounts { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer<AppDb>(null);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            Database.CommandTimeout = 180;
        }
    }
}