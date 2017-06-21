using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("Customer")]

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public double Credit { get; set; }

        public double LimitedCredit { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<MenuItem> Favourites { get; set; }

        public virtual ICollection<MenuItem> Restricts { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }

        public virtual ICollection<OrderNotification> Notifications { get; set; }

        //--------------------------
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
