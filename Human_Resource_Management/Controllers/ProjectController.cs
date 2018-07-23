using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.Utility;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class ProjectController : Controller
    {

        public ProjectController(IProject IProject)
        {
            _IProject = IProject;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //retrieving the list of subcompanies of the company
            List<Project> Projects = _IProject.GetAll();
            ProjectViewModel projectViewModal = new ProjectViewModel();
            projectViewModal.Projects = Projects;
            return View(projectViewModal);
        }

        public IActionResult Create()
        {
            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IProject.GetSubCompanies());
            return View();
        }

        //POST: Project/Create
        // To protect from overposting attacks, enabling the specific fields to bind to,for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProjectName,SubCompanyId")]ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_IProject.Create(projectViewModel))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Unable To create.";
                }
            }

            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IProject.GetSubCompanies());
            return View(projectViewModel);
                    
        }


        // GET: Project/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectViewModel projectViewModel = _IProject.GetProjectViewModel((int)id);
            if (projectViewModel == null)
            {
                return NotFound();
            }

            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IProject.GetSubCompanies());

            return View(projectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("Id,ProjectName,SubCompanyId")] ProjectViewModel projectViewModel)
        {
            if (id != projectViewModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (_IProject.Update(projectViewModel))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Unable To Update.";
                }
            }

            ViewBag.SubCompanies = UtilityService.CreateSubCompanyListItem(_IProject.GetSubCompanies());
            return View(projectViewModel);    
                       
        }

        // GET: Project/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = _IProject.GetProject((int)id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        //GET : Project/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Project project = _IProject.GetProject((int)id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        //Post : Project/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Project project = _IProject.GetProject(id);
            if (_IProject.Delete(project))
            {
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Unable To Delete.";
            return View(project);
        }


        private IProject _IProject;
    }
}
