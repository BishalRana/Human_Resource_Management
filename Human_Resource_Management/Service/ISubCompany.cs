using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.Service
{
    public interface ISubCompany
    {
        List<SubCompany> GetAll(int companyId);
        bool Create(SubCompany subCompany);
        Company GetCompany(int companyId);
        SubCompany GetSubCompany(int Id);
        bool Update(SubCompany subCompany);
        bool Delete(SubCompany subCompany);
    }
}
