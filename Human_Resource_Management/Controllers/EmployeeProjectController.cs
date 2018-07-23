using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class EmployeeProjectController : Controller
    {
        public EmployeeProjectController(IEmployeeProject IEmployeeProject)
        {
            _IEmployeeProject = IEmployeeProject;   
        }

        public IActionResult Create(int id)
        {
            //retrieving projects of sub company associated with employee
            EmployeeProjectViewModel employeeProjectViewModel = new EmployeeProjectViewModel();
            employeeProjectViewModel.Projects = _IEmployeeProject.GetProjects(id);
            employeeProjectViewModel.Id = id;
            return View(employeeProjectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Emp_Name,Projects")] EmployeeProjectViewModel employeeProjectViewModel)
        {

            if(_IEmployeeProject.UpdateEmployeeProjects(employeeProjectViewModel))
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewBag.Message = "Unable to associate project with employee";   
            }
            ViewBag.Message = "Unable to associate project with employee";   
            return View(employeeProjectViewModel);
        }


        public IActionResult RouteToEmployeeIndexController()
        {
            return RedirectToAction("Index", "Employee");
        } 

        private IEmployeeProject _IEmployeeProject;
    }
}
