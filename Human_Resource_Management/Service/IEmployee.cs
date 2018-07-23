using System;
using System.Collections.Generic;
using System.Linq;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;

namespace Human_Resource_Management.Service
{
    public interface IEmployee
    {
        List<Employee> GetAll(string position, string subCompany);
        bool CreateEmployee(EmployeeViewModel employeeViewModel);
        IQueryable GetPositionsQuery();
        IQueryable GetSubCompaniesQuery();
        List<SubCompany> GetSubCompanies();
        List<Position> GetPositions();
        EmployeeViewModel GetEmployeeViewModel(int empId);
        bool Update(EmployeeViewModel employeeViewModel);
        Employee GetEmployee(int id);
        bool Delete(Employee employee);
    }
}
