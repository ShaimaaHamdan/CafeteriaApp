using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Admin.Controllers
{
    //[SuperAdminAuthorize(Roles = "Admin")]
    public class CafeteriaController : Controller
    {
        // GET: Admin/Cafeteria
        public ActionResult Index()
        {
            return View();
        }

        
        // GET: Admin/Cafeteria/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Cafeteria/Details/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        } 

    }
}
