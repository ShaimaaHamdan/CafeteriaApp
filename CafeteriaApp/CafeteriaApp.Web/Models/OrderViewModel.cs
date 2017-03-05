using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public bool PaymentDone { get; set; }

        public DateTime OrderTime { get; set; }

        public string PaymentMethod { get; set; }

        public string DeliveryPlace { get; set; }

        public string OrderStatus { get; set; }

        public DateTime DeliveryTime { get; set; }

        public int customerid { get; set; }

        public CustomerViewModel customer { get; set; }
        public List<OrderItemViewModel> OrderItems { get; internal set; }
    }
}