using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }


        public ICollection<MenuItem>  MenuItems { get; set; }

        public int CafeteriaId { get; set; }

        [ForeignKey("CafeteriaId")]
        public virtual Cafeteria Cafeteria { get; set; }
    }
}
