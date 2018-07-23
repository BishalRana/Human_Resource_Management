using System;
namespace Human_Resource_Management.Models
{
    public class EmployeeProject
    {
        public int Id { get; set; }
        
        public int _EmpId { get; set; }
        public Employee employee { get; set; }

        public int _PjtId { get; set; }
        public Project Project {get; set;}
    }
}
