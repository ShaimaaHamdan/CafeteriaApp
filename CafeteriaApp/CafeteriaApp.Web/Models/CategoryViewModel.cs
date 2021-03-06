﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CafeteriaId { get; set; }

        public string ImageData { get; set; }
        public string ImageUrl { get; set; }
        public List<MenuItemViewModel> MenuItems { get; internal set; }
        public CafeteriaViewModel Cafeteria { get; set; }
    }
}