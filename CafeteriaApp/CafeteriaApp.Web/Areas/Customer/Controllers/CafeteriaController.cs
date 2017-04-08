using CafeteriaApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class CafeteriaController : BaseController
    {
        // GET: Customer/Cafeteria
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customer/Cafeteria/show/5
        public ActionResult Show(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
