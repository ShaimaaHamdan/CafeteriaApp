using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class MenuItemController : Controller
    {
        // GET: Customer/MenuItem
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/MenuItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/MenuItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/MenuItem/Create
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

        // GET: Customer/MenuItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/MenuItem/Edit/5
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

        // GET: Customer/MenuItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/MenuItem/Delete/5
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
