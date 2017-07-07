using CafeteriaApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class CategoryController : BaseController
    {
     
        // GET: Customer/Category/show/5

        //public ActionResult Show(int id)
        //{
        //    ViewBag.Id = id;
        //    return View();
        //}

        public ActionResult Show(int id)
        {
            ViewBag.Id = id;
            ViewBag.UserId = GetUserId();
            return View();
        }


    }
}
