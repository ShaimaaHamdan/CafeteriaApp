using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class OrderNotificationViewModel
    {
        public int Id { get; set; }
        public int customerid { get; set; }
        public int orderid { get; set; }
        public string data { get; set; }
        public CustomerViewModel customer { get; set; }
        public OrderViewModel order { get; set; }
        
    }
}