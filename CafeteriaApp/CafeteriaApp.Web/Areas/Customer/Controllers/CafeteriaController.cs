using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class CafeteriaController : Controller
    {
        // GET: Customer/Cafeteria
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customer/Cafeteria/5
        public ActionResult show(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
