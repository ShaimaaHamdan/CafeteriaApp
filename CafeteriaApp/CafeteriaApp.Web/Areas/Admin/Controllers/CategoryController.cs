using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Admin.Controllers
{
    [SuperAdminAuthorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }
               

        // GET: Admin/Category/Create
        public ActionResult Create(int id)
        {
            ViewBag.cafeteriaId = id;
            return View();
        }

        // GET: Admin/Category/Details/5
        public ActionResult Edit(int id) 
        {
            ViewBag.Id = id;
            return View();
        }
                
    }
}
