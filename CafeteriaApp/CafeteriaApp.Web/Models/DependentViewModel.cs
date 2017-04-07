using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class DependentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public double DependentCredit { get; set; }

        public double DayLimit { get; set; }

        public string SchoolYear { get; set; }

        public int CustomerId { get; set; }

        public string Image { get; set; }

        public CustomerViewModel Customer { get; set; } 
    }
}