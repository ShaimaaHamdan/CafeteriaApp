﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    [Table("AspNetUsers")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
    }
}
