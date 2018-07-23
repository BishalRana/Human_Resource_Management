using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;

namespace Human_Resource_Management.Service
{
    public interface IEmployeeProject
    {
		List<Project> GetProjects(int empId);
        bool UpdateEmployeeProjects(EmployeeProjectViewModel employeeProjectViewModel);
    
    }
}
