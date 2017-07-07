using CafeteriaApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Customer/Order
        public ActionResult ReviewOrder()
        {
            ViewBag.UserId = GetUserId();
            return View();
        }
        public ActionResult OrderStatus()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        //// GET: Customer/Order/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Customer/Order/Create
        public ActionResult orderstatus()
        {
            return View();
        }

    //    //// POST: Customer/Order/Create
    //    //[HttpPost]
    //    //public ActionResult Create(FormCollection collection)
    //    //{
    //    //    try
    //    //    {
    //    //        // TODO: Add insert logic here

    //    //        return RedirectToAction("Index");
    //    //    }
    //    //    catch
    //    //    {
    //    //        return View();
    //    //    }
    //    //}

    //    // GET: Customer/Order/Edit/5
    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    // POST: Customer/Order/Edit/5
    //    [HttpPost]
    //    public ActionResult Edit(int id, FormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add update logic here

    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: Customer/Order/Delete/5
    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    // POST: Customer/Order/Delete/5
    //    [HttpPost]
    //    public ActionResult Delete(int id, FormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add delete logic here

    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    }
}
