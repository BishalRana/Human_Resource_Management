using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Human_Resource_Management.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position_Type { get; set; }


        //one to many relation with Employee
        public ICollection<Employee> Employees { get; set; }
    }
}
