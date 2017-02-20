using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public OrderViewModel Order { get; set; }

        public int MenuItemId { get; set; }

        public MenuItemViewModel MenuItem { get; set; }
    }
}