using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Admin.Controllers
{
    public class MenuItemController : Controller
    {
        // GET: Admin/MenuItem
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/MenuItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/MenuItem/Details/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;

            return View();
        }


    }
}
