using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Human_Resource_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_Management.Service
{
    public class SubCompanyService : ISubCompany
    {
        private ManagementContext _mgmtContext;

        public SubCompanyService(ManagementContext managementContext)
        {
            _mgmtContext = managementContext;
        }

        // on creating makes sure sub company object is associated with company
        public bool Create(SubCompany subCompany)
        {
            try
            {
                _mgmtContext.Add(subCompany);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch(DbUpdateException due)
            {
                Debug.WriteLine(due.Message);
                return false;
            }
           
        }

        public bool Delete(SubCompany subCompany)
        {
            try
            {
                _mgmtContext.Remove(subCompany);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception  "+ex.Message);
                return false;
            }
        }

        public List<SubCompany> GetAll(int companyId)
        {
            try
            {
                return _mgmtContext.SubCompany.Where(c => c.Company.Id == companyId).ToList();
            }
            catch(InvalidCastException ice)
            {
                Debug.WriteLine("Message " + ice.Message);
                return null;
            }
            catch(NullReferenceException nre)
            {
                Debug.WriteLine("Message "+nre.Message);
                return null;
            }
           
        }

        public Company GetCompany(int companyId)
        {
            return _mgmtContext.Company.SingleOrDefault(m => m.Id == companyId);
        }

        public SubCompany GetSubCompany(int Id)
        {
            try
            {
                List<SubCompany> subCompany = _mgmtContext.SubCompany.Where(s => s.Id == Id).Include(s => s.Company).ToList();
                return subCompany[0];
            }
            catch(InvalidCastException ice)
            {
                Debug.WriteLine("Message "+ice.Message);
                return null;
            }
            
        }

        public bool Update(SubCompany subCompany)
        {
            try
            {
                _mgmtContext.Update(subCompany);
                _mgmtContext.SaveChangesAsync();

                return true;
            }
            catch(InvalidOperationException ioe)
            {
                Debug.WriteLine("Message "+ioe.Message);
                return false;
            }
            catch (DbUpdateConcurrencyException dce)
            {
                Debug.WriteLine("Message " + dce.Message);
                return false;
            }
        }

    }
}
