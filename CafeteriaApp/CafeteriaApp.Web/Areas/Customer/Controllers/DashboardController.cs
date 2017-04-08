using CafeteriaApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Customer/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}
