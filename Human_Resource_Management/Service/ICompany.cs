using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.Service
{
    public interface ICompany
    {
        List<Company> GetAll();

        bool Create(Company company);

        Company GetCompany(int id);

        bool Update(Company company);

        bool Delete(Company company);
    }
}
