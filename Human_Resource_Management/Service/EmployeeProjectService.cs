using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_Management.Service
{
    public class EmployeeProjectService : IEmployeeProject
    {
        private ManagementContext _mgmtContext;

        public EmployeeProjectService(ManagementContext _mgmtCtx)
        {
            _mgmtContext = _mgmtCtx;
        }

        public List<Project> GetProjects(int empId)
        {
            try
            {
                Employee employee = _mgmtContext.Employee.Where(e => e._EmpId == empId).Include(e => e.EmployeeProjects).Include(s => s.SubCompany).ToList()[0];
                List<Project> projects = _mgmtContext.Project.Where(p => p.SubCompany.Id == employee.SubCompany.Id).ToList();

                // return all the projects associated with sub company where employee project list is empty
                if (employee.EmployeeProjects.Count() == 0)
                {
                    return projects;
                }
                ICollection<EmployeeProject> employeeProjects = employee.EmployeeProjects;

                //making sure employee project list association is upto date with sub company project list
                for (int i = 0; i < projects.Count(); i++)
                {
                    var employeeProject = employeeProjects.Where(p => p.Project._PjtId == projects[i]._PjtId);
                    if (employeeProject.Count() != 0)
                    {
                        projects[i].isProjectSelected = true;
                    }
                    else
                    {
                        projects[i].isProjectSelected = false;
                    }

                }

                return projects;   
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
                return null;
            }

        }

        public bool UpdateEmployeeProjects(EmployeeProjectViewModel employeeProjectViewModel)
        {
            
            try
            {
                Employee emp = _mgmtContext.Employee.Where(e => e._EmpId == employeeProjectViewModel.Id).Include(e => e.EmployeeProjects).ThenInclude(e => e.Project).ToList()[0];

                // if employee project is empty then add the selected project in EmployeePoject table
                if (emp.EmployeeProjects.Count() == 0)
                {
                    foreach (var pjt in employeeProjectViewModel.Projects)
                    {
                        if (pjt.isProjectSelected )
                        {                           
                            _mgmtContext.AddRange(new EmployeeProject { employee = emp, Project = _mgmtContext.Project.FirstOrDefault(p => p._PjtId == pjt._PjtId) });
                        }
                    }    
                   
                }
                else// when employee projects are in EmployeeProject table
                {
                    foreach (var pjt in employeeProjectViewModel.Projects)
                    {
                        var empPjt = emp.EmployeeProjects.FirstOrDefault(ep => ep._PjtId == pjt._PjtId);
                        if (pjt.isProjectSelected && empPjt == null)
                        {
                            //project is selected and is not in the table so adding it to the database table 
                            emp.EmployeeProjects.Add(
                                new EmployeeProject { employee = emp,_PjtId = pjt._PjtId, Project = _mgmtContext.Project.FirstOrDefault(p => p._PjtId == pjt._PjtId) }
                            );
                        }
                        else if (!pjt.isProjectSelected && empPjt != null)
                        {
                            // project is not selected but it is in the database table so,removing it
                            emp.EmployeeProjects.Remove(empPjt);
                        }
                        else if (pjt.isProjectSelected && empPjt != null)
                        {
                            //here do nothing as employee project is already in database table
                        }
                    }
                }
                _mgmtContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
                return false;
            }                      
        }
    }
}
