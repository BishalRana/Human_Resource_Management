using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;
using System.Diagnostics;

namespace Human_Resource_Management.Service
{
	public class ProjectService : IProject
    {
       
        public ProjectService(ManagementContext managementContext)
        {
            _mgmtContext = managementContext;
        }

        public List<Project> GetAll()
        {
            var projects = from m in _mgmtContext.Project.Include(p => p.SubCompany)
                            select m;
            return projects.ToList();
        }

        public bool Create(ProjectViewModel projectViewModel)
        {
            try
            {   //checking if same project is assigned  for the same sub company,if exists then return false              
                List<Project> projectsExist = _mgmtContext.Project.Where(p => p.SubCompany.Id == projectViewModel.SubCompanyId && p.Name == projectViewModel.ProjectName).ToList();

                if(projectsExist.Count() == 1)
                {
                    return false;
                }
                //checking if same project is assigned for the different sub company , if it exists then return false
                List<Project> duplicatePjt = _mgmtContext.Project.Where(p => p.SubCompany.Id != projectViewModel.SubCompanyId && p.Name == projectViewModel.ProjectName).ToList();

                if(duplicatePjt.Count() == 1 )
                {
                    return false;
                }
               
                Project project = new Project();
                project.Name = projectViewModel.ProjectName;

                SubCompany subCompany = GetSubCompany(projectViewModel.SubCompanyId);
                project.SubCompany = subCompany;
                _mgmtContext.Add(project);
                _mgmtContext.SaveChanges();
                return true;               
               
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
                return false;
            }
           
        }

        public SubCompany GetSubCompany(int id)
        {
            try
            {
                List<SubCompany> subCompany = _mgmtContext.SubCompany.Where(s => s.Id == id).Include(s => s.Company).ToList();
                return subCompany[0];
            }
            catch (InvalidCastException ice)
            {
                Debug.WriteLine("Message " + ice.Message);
                return null;
            }
        }

        public List<SubCompany> GetSubCompanies()
        {
            var subCompanies = from s in _mgmtContext.SubCompany
                               select s;
            return subCompanies.ToList();
        }

        public ProjectViewModel GetProjectViewModel(int pjtId)
        {
            Project project = _mgmtContext.Project.Where(p => p._PjtId == pjtId).Include(p => p.SubCompany).ToList()[0];
            ProjectViewModel projectViewModel = new ProjectViewModel();
            projectViewModel.SubCompanyId = project.SubCompany.Id;
            projectViewModel.ProjectName = project.Name;
            projectViewModel.Id = project._PjtId;
            return projectViewModel;
        }

        public bool Update(ProjectViewModel projectViewModel)
        {
            try
            {
                List<Project> pjts = _mgmtContext.Project.Where(p => p._PjtId != projectViewModel.Id && p.Name == projectViewModel.ProjectName).ToList();
                if(pjts.Count() == 1)
                {
                    return false;
                }
                
                Project project = _mgmtContext.Project.Where(p => p._PjtId == projectViewModel.Id).Include(p => p.SubCompany).ToList()[0];
                project.Name = projectViewModel.ProjectName;
                project.SubCompany = _mgmtContext.SubCompany.SingleOrDefault(s => s.Id == projectViewModel.SubCompanyId);

                _mgmtContext.Update(project);
                _mgmtContext.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
                return false;
            }
        }

        public Project GetProject(int pjtId)
        {
            Project project = null;
            try
            {
                 project = _mgmtContext.Project.Where(p => p._PjtId == pjtId).Include(p => p.SubCompany).ToList()[0];
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
            }

            return project;

        }

        public bool Delete(Project project)
        {
            try
            {
                _mgmtContext.Remove(project);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception  " + ex.Message);
                return false;
            }
        }


        private ManagementContext _mgmtContext;
    }
}
