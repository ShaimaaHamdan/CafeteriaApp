using CafeteriaApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class ProfileController : BaseController 
    {
        // GET: Customer/Profile
        public ActionResult Index()
        {
            ViewBag.UserId = GetUserId();
            return View();
        }
        // GET: Customer/Profile/Create
        public ActionResult Create()
        {
            ViewBag.UserId = GetUserId();
            return View();
        }

        public ActionResult EditChild(int id)
        {
            ViewBag.Id = id;
            ViewBag.UserId = GetUserId();
            return View();
        }

        public ActionResult AddChildRestriction(int id)
        {
            ViewBag.restrictionId = id;
            return View();
        }
    }
}
