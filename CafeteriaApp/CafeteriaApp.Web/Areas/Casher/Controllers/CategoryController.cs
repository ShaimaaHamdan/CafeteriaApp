using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeteriaApp.Web.Controllers;

namespace CafeteriaApp.Web.Areas.Casher.Controllers
{
    public class CategoryController : BaseController
    {
        //// GET: Casher/Category
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Casher/Category/Details/5
        public ActionResult Show(int id)
        {
            ViewBag.Id = id;
            ViewBag.UserId = GetUserId();
            return View();
        }

        //// GET: Casher/Category/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Casher/Category/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Casher/Category/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Casher/Category/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Casher/Category/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Casher/Category/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
