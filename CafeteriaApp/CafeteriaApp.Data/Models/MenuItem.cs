using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("MenuItem")]

    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int CategoryId { get; set; }


        public string photo { get; set; }
        public string alternatetext { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Addition> Additions { get; set; }

        public virtual ICollection<Customer> CustomersFavourite { get; set; }

        public virtual ICollection<Customer> CustomersRestricts { get; set; }
        
    }
}
