using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;

[assembly: OwinStartup(typeof(CafeteriaApp.Web.Startup))]

namespace CafeteriaApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //Create Roles if not Exist

            AppDb db = new AppDb();

            var customerRole = db.Roles.Where(i => i.Name == "Customer").FirstOrDefault();
            if (customerRole == null)
            {
                db.Roles.Add(new Role() {Id="7", Name = "Customer" });
            }

            var AdminRole = db.Roles.Where(i => i.Name == "Admin").FirstOrDefault();
            if (AdminRole == null)
            {
                db.Roles.Add(new Role() {Id="8", Name = "Admin" });

            }

            var ChefRole = db.Roles.Where(i => i.Name == "Chef").FirstOrDefault();
            if (ChefRole == null)
            {
                db.Roles.Add(new Role() {Id="9", Name = "Chef" });

            }

            var CashierRole = db.Roles.Where(i => i.Name == "Cashier").FirstOrDefault();
            if (CashierRole == null)
            {
                db.Roles.Add(new Role() { Id="10" , Name = "Cashier" });

            }

            db.SaveChanges();
        }
    }
}
