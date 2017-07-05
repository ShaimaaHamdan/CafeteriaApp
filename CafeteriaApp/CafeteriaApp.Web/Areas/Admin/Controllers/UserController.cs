using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/User/Details/5
        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
