using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Chef.Controllers
{
    public class OrderController : Controller
    {
        // GET: Chef/Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Chef/Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chef/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chef/Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chef/Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chef/Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chef/Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chef/Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
