using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    public class OrderedItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }



    }
}
