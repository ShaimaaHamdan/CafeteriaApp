using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public double Credit { get; set; }

        public double LimitedCredit { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<MenuItem> Favourites { get; set; }

        public virtual ICollection<MenuItem> Restricts { get; set; }

    }
}
