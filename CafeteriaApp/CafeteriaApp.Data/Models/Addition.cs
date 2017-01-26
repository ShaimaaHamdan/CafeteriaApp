using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("Addition")]

    public class Addition
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        public virtual ICollection<MenuItem> MenuItems { get; set; }
      

    }
}
