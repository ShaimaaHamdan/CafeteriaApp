using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public bool PaymentDone { get; set; } 

        public DateTime OrderTime { get; set; }

        public string PaymentMethod { get; set; }

        public string DeliveryPlace { get; set; }

        public string OrderStatus { get; set; }

        public DateTime DeliveryTime { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
