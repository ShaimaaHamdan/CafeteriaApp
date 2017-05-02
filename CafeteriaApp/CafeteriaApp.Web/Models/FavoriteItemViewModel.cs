using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class FavoriteItemViewModel
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        public MenuItemViewModel MenuItem { get; set; }

        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}