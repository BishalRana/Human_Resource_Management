using System;
using System.Linq;
using System.Collections.Generic;
using Human_Resource_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace Human_Resource_Management.Service
{
    public class CompanyService : ICompany
    {
        private ManagementContext _mgmtContext;

        public CompanyService(ManagementContext mgmtContext)
        {
            _mgmtContext = mgmtContext;
        }

        public bool Create(Company company)
        {
            try
            {
                _mgmtContext.Add(company);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Message "+ex.Message);
                return false;
            }           
        }

        public List<Company> GetAll()
        { 
            var companies = from m in _mgmtContext.Company
                            select m;
            return companies.ToList();
        }

        public Company GetCompany(int id)
        {
            return  _mgmtContext.Company.SingleOrDefault(m => m.Id == id);
        }

        public bool Update(Company company)
        {
            try
            {
                _mgmtContext.Update(company);
                _mgmtContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetCompany(company.Id) == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool Delete(Company company)
        {
            try
            {
                _mgmtContext.Remove(company);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch(Exception e )
            {
                Debug.WriteLine("Message : "+e.Message);
                return false;
            }
           
        }
    }
}
