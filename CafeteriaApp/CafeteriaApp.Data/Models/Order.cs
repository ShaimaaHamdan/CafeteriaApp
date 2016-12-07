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

        public DateTime DeliveryTime { get; set; }
    }
}
