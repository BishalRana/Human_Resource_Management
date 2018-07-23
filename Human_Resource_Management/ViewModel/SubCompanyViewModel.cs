using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.ViewModel
{
    public class SubCompanyViewModel
    {
        public List<SubCompany> SubCompanies { get; set; }
        public SubCompany SubCompany { get; set; }
        public int id { get; set; }
        public SubCompanyViewModel()
        {
        }
    }
}
