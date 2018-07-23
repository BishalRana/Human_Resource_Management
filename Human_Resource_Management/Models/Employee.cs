using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class Employee
    {
        [Key]
        [Column("Id")]
        public int _EmpId { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Employee should be of minimum length 10 and maximum length 60", MinimumLength = 10)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Salary is required")]
        [Range(1200, 100000)]
        public decimal Salary { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Joined_Date { get; set; }

        public Position Positions { get; set; }

        public SubCompany SubCompany { get; set; }

        public ICollection<Phone> Phones { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; } = new List<EmployeeProject>(); 

    }
}
