using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class SubCompanyController : Controller
    {
        public SubCompanyController(ISubCompany iSubCompany)
        {
            _ISubCompany = iSubCompany;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {            
            //retrieving the list of subcompanies of the company
            List<SubCompany> SubCompanies =_ISubCompany.GetAll(id);
            SubCompanyViewModel subCompanyViewModel = new SubCompanyViewModel();
            subCompanyViewModel.SubCompanies = SubCompanies;

            ViewBag.CompanyName = _ISubCompany.GetCompany(id).Name;
            subCompanyViewModel.id = id;
            return View(subCompanyViewModel);
        }

        public IActionResult RouteToSubCompanyIndex(int? id)
        {
            return RedirectToAction("Index", "SubCompany", new { Id = (int)id });
        }

        public IActionResult Create(int? id)
        {
            SubCompany subCompany = new SubCompany();
            Company company = _ISubCompany.GetCompany((int)id);
            subCompany.CompanyName = company.Name;
            subCompany._CompanyId = company.Id;

            return View(subCompany);
        }

        //POST: SubCompany/Create
        // To protect from overposting attacks, enabling the specific fields to bind to,for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id,[Bind("Name,Chief_Executive_Officer,Registered_Date,CompanyName,CompanyId")]SubCompany subCompany)
        {
            if (ModelState.IsValid)
            {

                subCompany.Company = _ISubCompany.GetCompany(id);
                if(_ISubCompany.Create(subCompany))
                {
                    return RedirectToAction("Index", "SubCompany", new { Id = id });
                }                    
            }
            ViewBag.Message = "Unable To Create.";
            return View(subCompany);
        }

        // GET: SubCompany/Edit/5/4
        [Route("SubCompany/Edit/{id}/{compId}")]
        public IActionResult Edit(int id,int compId)
        {
            if (id == 0)
            {
                return NotFound();
            }

            SubCompany subCompany = _ISubCompany.GetSubCompany((int)id);

            if (subCompany == null)
            {
                return NotFound();
            }

            return View(subCompany);
        }

        // POST: SubCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [Route("SubCompany/Edit/{id}/{compId}")]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id,int compId, [Bind("Id,Name,Chief_Executive_Officer,Registered_Date")] SubCompany subCompany)
        {
            if (id != subCompany.Id)
            {
                return NotFound();
            }

            if(compId != _ISubCompany.GetCompany(id).Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(_ISubCompany.Update(subCompany))
                {
                    return RedirectToAction("Index", "SubCompany", new { Id = compId });
                }                                              
            }
            ViewBag.Message = "Unable To Update.";
            return View(subCompany);
        }

        // GET: SubCompany/Details/5
        [Route("SubCompany/Details/{id}/{compId}")]
        public IActionResult Details(int id, int compId)
        {
            if (id == 0)
            {
                return NotFound();
            }

            SubCompany subCompany = _ISubCompany.GetSubCompany((int)id);
            if (subCompany == null)
            {
                return NotFound();
            }

            return View(subCompany);
        }

        //GET : SubCompany/Delete/{id}/{compId}
        [Route("SubCompany/Delete/{id}/{compId}")]
        public IActionResult Delete(int id, int compId)
        {
            if (id == 0)
            {
                return NotFound();
            }

            SubCompany subCompany = _ISubCompany.GetSubCompany((int)id);

            if (subCompany == null)
            {
                return NotFound();
            }

            return View(subCompany);
        }

        //Post : SubCompany/Delete/{id}/{compId}
        [HttpPost, ActionName("Delete")]
        [Route("SubCompany/Delete/{id}/{compId}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id,int compId)
        {
            SubCompany subCompany = _ISubCompany.GetSubCompany(id);
            if (_ISubCompany.Delete(subCompany))
            {
                 return RedirectToAction("Index", "SubCompany", new { Id = compId });

            }
            ViewBag.Message = "Unable To Delete.";
            return View(subCompany);
        }

        private ISubCompany _ISubCompany;
    }
}
