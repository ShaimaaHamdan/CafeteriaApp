﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("AspNetRoles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Persons { get; set; }
    }
}
