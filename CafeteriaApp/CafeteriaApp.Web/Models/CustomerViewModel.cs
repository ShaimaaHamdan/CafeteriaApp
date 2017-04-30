using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public double Credit { get; set; }

        public double LimitedCredit { get; set; }

        public string UserId { get; set; }

        public UserViewModel User { get; set; }

        public List<DependentViewModel> Dependents { get; set; }

        public List<OrderViewModel> Orders { get; set; }

        public List<MenuItemViewModel> Favourites { get; set; }

        public List<MenuItemViewModel> Restricts { get; set; }

        public List<CommentViewModel> Comments { get; set; }

    }
}