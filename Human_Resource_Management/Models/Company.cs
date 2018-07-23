using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class Company
    {
        //DatabaseGenerated(DatabaseGeneratedOption.Identity)
        //[Key]
        //[Column("Id")]
        //public int _CompId { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Name should be of minimum length 1 and maximum length 60", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Chief Executive Officer")]
        [StringLength(60, ErrorMessage = "Name should be of minimum length 1 and maximum length 60", MinimumLength = 1)]
        public string Chief_Executive_Officer { get; set; }

        [Required]
        [Display(Name = "Registered Date"), DataType(DataType.Date)]
        public DateTime Registered_Date { get; set; }

        public ICollection<SubCompany> SubCompanies { get; set; } = new List<SubCompany>();

    }
}
