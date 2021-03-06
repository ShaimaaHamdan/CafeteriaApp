﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("Cafeteria")]
    public class Cafeteria
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } 

        public virtual ICollection<Category> Categories { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
