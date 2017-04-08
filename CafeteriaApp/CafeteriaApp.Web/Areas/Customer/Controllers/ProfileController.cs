﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Customer.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Customer/Profile
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customer/Profile/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Admin/Cafeteria/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}