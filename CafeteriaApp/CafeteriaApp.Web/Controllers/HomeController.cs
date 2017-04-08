using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Web.Helpers;

namespace CafeteriaApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var context = new AppDb();

            //var customer = context.person.Where(i => i.Role.Name == "Customer");


            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            

            return View();
        }
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
    }
}
