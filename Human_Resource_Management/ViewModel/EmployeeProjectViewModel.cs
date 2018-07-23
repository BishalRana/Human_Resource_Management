using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.ViewModel
{
    public class EmployeeProjectViewModel
    {
        public EmployeeProjectViewModel()
        {
        }
        public int Id { get; set; }
        public List<Project> Projects { get; set; }
    }
}
