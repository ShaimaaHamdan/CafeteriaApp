using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class CafeteriaViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImageData { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryViewModel> Categories { get; internal set; }
        
    }
}