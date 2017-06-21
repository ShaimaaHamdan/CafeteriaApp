using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("OrderNotification")]
    public class OrderNotification
    {
        public int Id { get; set; }
        public int customerid { get; set; }
        public int orderid { get; set; }
        public string data { get; set; }

        [ForeignKey("customerid")]
        public virtual Customer customer { get; set; }

        [ForeignKey("orderid")]
        public virtual Order order { get; set; }
    }
}
