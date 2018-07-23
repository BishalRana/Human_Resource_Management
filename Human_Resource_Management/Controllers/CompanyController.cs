using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class CompanyController : Controller
    {
        public CompanyController(ICompany iCompany, ManagementContext managementContext)
        {
            _ICompany = iCompany;
            _mgmtCtxt = managementContext;

        }

        //[BindProperty]
        //public Company company { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            HomeModel homeModel = new HomeModel();

            homeModel.Companies = _ICompany.GetAll();
            return View(homeModel);
        }

        //GET : Company/Create
        public IActionResult Create()
        {
           
            return View();
        }

        //POST: Company/Create
        // To protect from overposting attacks, enabling the specific fields to bind to,for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Chief_Executive_Officer,Registered_Date")]Company company)
        {
            if (ModelState.IsValid)
            {
                if(_ICompany.Create(company))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Unable To Create";
                }
            }
            return View(company);
        }

        // GET: Company/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company company = _ICompany.GetCompany((int)id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        //GET : Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();   
            }

            //Company company = _ICompany.GetCompany((int)id);
            Company company = await _mgmtCtxt.Company.SingleOrDefaultAsync(m => m.Id == id);
        

            if(company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        //POST: Company/Edit/5
        // To protect from overposting attacks, enabling the specific fields to bind to,for
        //[FromForm]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Chief_Executive_Officer,Registered_Date")]Company company)
        {

            if(id != company.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {                
                if(_ICompany.Update(company))
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    Debug.WriteLine("Unable To Update"); 
                }
            }

            return View(company);
        }

        //GET : Company/Delete/5
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Company company = _ICompany.GetCompany((int)id);

            if(company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        //Post : Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Company company = _ICompany.GetCompany(id);
            
            if(_ICompany.Delete(company))
            {
                return RedirectToAction(nameof(Index));
            }
           
            ViewBag.Message = "Unable To Delete";

            return View(company);
        }


        public IActionResult RouteToSubCompanyController(int? id)
        {
            return RedirectToAction("Index", "SubCompany",new {Id = (int)id });
        }

        private ICompany _ICompany;
        private ManagementContext _mgmtCtxt;
    }
}
