using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.ViewModel
{
    public class ProjectViewModel
    {
        public List<Project> Projects { get; set; }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Project is Required")]
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

    
        [Display(Name = "Sub Company")]
        public string NameOfSubCompany { get; set; }

        [Required]
        public int SubCompanyId { get; set; }

        public ProjectViewModel()
        {
        }
    }
}
