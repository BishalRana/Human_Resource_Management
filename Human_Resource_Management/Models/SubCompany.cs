using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class SubCompany
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60,ErrorMessage = "Name should be of minimum length 1 and maximum length 60",MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Chief Executive Officer")]
        [StringLength(60, ErrorMessage = "Name should be of minimum length 1 and maximum length 60", MinimumLength = 1)]
        public string Chief_Executive_Officer { get; set; }

        [Required(ErrorMessage = "Registered Date is required")]
        [Display(Name = "Registered Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Registered_Date { get; set; }

        public  Company Company { get; set; }
        public List<Project> Projects { get; set; }
        public List<Employee> Employees { get; set; }

        [NotMapped]
        public string CompanyName { get; set; }

        [NotMapped]
        public int _CompanyId { get; set; }
    }
}
