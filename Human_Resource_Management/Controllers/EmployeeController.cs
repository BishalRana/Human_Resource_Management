using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.Utility;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployee iEmployee)
        {
            _IEmployee = iEmployee;
        }

        // GET: /<controller>/
        public IActionResult Index(string position, string subCompany)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Employees = _IEmployee.GetAll(position,subCompany);
            ViewBag.subCompany = new SelectList(_IEmployee.GetSubCompaniesQuery());
            ViewBag.position = new SelectList(_IEmployee.GetPositionsQuery());
            return View(employeeViewModel);
        }

        public IActionResult Create()
        {
            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IEmployee.GetSubCompanies());
            ViewBag.Positions = UtilityService.CreatePositionListItemn(_IEmployee.GetPositions());
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Addresses = new List<string>();
            employeeViewModel.PhoneNumbers = new List<string>();
            return View(employeeViewModel);
        }

        //POST: Employee/Create
        // To protect from overposting attacks, enabling the specific fields to bind to,for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeName,ProjectName,Salary,SubCompanyId,PositionId,Joined_Date,_Address,_PhoneNumber,Addresses,PhoneNumbers")]EmployeeViewModel employeeViewModel)
        {
           
            if (ModelState.IsValid)
            {
                if(_IEmployee.CreateEmployee(employeeViewModel))
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    ViewBag.Message = "Unable To Create";
                }              
            }
           
            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IEmployee.GetSubCompanies());
            ViewBag.Positions = UtilityService.CreatePositionListItemn(_IEmployee.GetPositions());
            if(employeeViewModel.Addresses == null)
            {
                employeeViewModel.Addresses = new List<string>();
            }

            if(employeeViewModel.PhoneNumbers == null)
            {
                employeeViewModel.PhoneNumbers = new List<string>();
            }
            return View(employeeViewModel);

        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {                
                if (id == null)
                {
                    return NotFound();
                }

                EmployeeViewModel employeeViewModel = _IEmployee.GetEmployeeViewModel((int)id);
                if (employeeViewModel == null)
                {
                    return NotFound();
                }

                ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IEmployee.GetSubCompanies());
                ViewBag.Positions = UtilityService.CreatePositionListItemn(_IEmployee.GetPositions());

                return View(employeeViewModel);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message "+ex.Message);
                ViewBag.subCompany = new SelectList(_IEmployee.GetSubCompaniesQuery());
                ViewBag.position = new SelectList(_IEmployee.GetPositionsQuery());
                return View(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EmployeeName,Salary,SubCompanyId,PositionId,Joined_Date,Addresses,_Address,_PhoneNumber,PhoneNumbers")] EmployeeViewModel employeeViewModel)
        {
            
            if (id != employeeViewModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                if(_IEmployee.Update(employeeViewModel))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                  ViewBag.Message = "Unable To Update.";
                }                                              
            }


            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IEmployee.GetSubCompanies());
            ViewBag.Positions = UtilityService.CreatePositionListItemn(_IEmployee.GetPositions());
            return View(employeeViewModel);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = _IEmployee.GetEmployee((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        //GET : Employee/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Employee employee = _IEmployee.GetEmployee((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        //Post : Employee/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Employee employee = _IEmployee.GetEmployee(id);
            if (_IEmployee.Delete(employee))
            {
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Unable To Delete.";
            return View(employee);
        }

        public IActionResult RouteToEmployeePjtController(int? id)
        {
            return RedirectToAction("Create", "EmployeeProject", new { Id = (int)id});
        } 

        public PartialViewResult AddNewAddress([Bind("Addresses")]EmployeeViewModel employeeViewModel)
        {
            employeeViewModel.Addresses.Add("");
            return PartialView("_Address",employeeViewModel);
        }

        public PartialViewResult RemoveNewAddress([Bind("Addresses")]EmployeeViewModel employeeViewModel)
        {
            return PartialView("_Address", employeeViewModel);
        }

        private IEmployee _IEmployee;
    }
}
