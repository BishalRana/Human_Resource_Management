using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;

namespace Human_Resource_Management.Service
{
    public interface IProject
    {
        List<Project> GetAll();
        bool Create(ProjectViewModel projectViewModel);
        ProjectViewModel GetProjectViewModel(int pjtId);
        bool Update(ProjectViewModel projectViewModel);
        Project GetProject(int pjtId);
        bool Delete(Project project);
        SubCompany GetSubCompany(int id);
        List<SubCompany> GetSubCompanies();
    }
}
