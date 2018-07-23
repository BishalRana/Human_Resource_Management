using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
           
        }

        //employee id
        public int Id { get; set; }

        public List<Employee> Employees { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(60, ErrorMessage = "Address should be of minimum length 1 and maximum length 60", MinimumLength = 1)]
        public string _Address { get; set; }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string _PhoneNumber { get; set; }

        [Display(Name =  "Salary")]
        [Required(ErrorMessage = "Salary is required")]
        [Range(1200, 100000)]
        public decimal Salary { get; set; }

        [Display(Name = "Employee")]
        [Required]
        [StringLength(60, ErrorMessage = "Employee should be of minimum length 2  and maximum length 60", MinimumLength = 2)]
        public string EmployeeName { get; set; }

        [Display(Name = "Sub Company")]
        public string NameOfSubCompany { get; set; }

        [Required(ErrorMessage = "Sub Company is required")]
        public int SubCompanyId { get; set; }

        [Display(Name = "Position")]
        public string Position_Type { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int PositionId { get; set; }

        [Display(Name = "Joined Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Joined_Date { get; set; }

        //this is an optional addresses
        public List<string> Addresses { get; set; }

        //this is an optional phoneNumbers
        public List<string> PhoneNumbers { get; set; }

        public string Index { get; set; }

       
    }
}
