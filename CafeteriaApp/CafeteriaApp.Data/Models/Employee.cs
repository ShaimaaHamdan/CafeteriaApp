using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Role { get; set; }

        //public int PersonId { get; set; }

        //[ForeignKey("PersonId")]
        //public Person Person { get; set; }


    }
}
