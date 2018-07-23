using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class Project
    {
        [Key]
        [Column("Id")]
        public int _PjtId { get;set;}

        [Required]
        public string Name { get; set;}

        public SubCompany SubCompany { get; set; }

        [NotMapped]
        public bool isProjectSelected { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; } = new List<EmployeeProject>(); 

    }
}
