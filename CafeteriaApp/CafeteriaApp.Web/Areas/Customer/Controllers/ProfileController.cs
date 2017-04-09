using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Customer/Profile
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customer/Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult EditChild(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
