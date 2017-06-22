using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Admin.Controllers
{
    [SuperAdminAuthorize(Roles = "Admin")]
    public class AdditionController : Controller
    {
        // GET: Admin/Addition
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Admin/Addition/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Addition/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }


    }
}
