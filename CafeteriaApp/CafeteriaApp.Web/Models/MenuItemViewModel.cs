﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class MenuItemViewModel
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int CategoryId { get; set; }
        public string ImageData { get; set; }

        public string alternatetext { get; set; }

        
        public CategoryViewModel Category { get; set; }


    }
}