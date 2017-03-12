using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserViewModel> Persons { get; internal set; } 
    }
}