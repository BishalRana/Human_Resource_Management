using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resource_Management.Utility
{
    public  class UtilityService
    {
       
        public static List<SelectListItem> CreateSubCompanyListItem(List<SubCompany> SubCompanies)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach(SubCompany subCompany in SubCompanies)
            {
                listItems.Add(new SelectListItem
                {
                    Text = subCompany.Name,
                    Value = subCompany.Id.ToString()
                });
            }

            return listItems;
        }

        public static List<SelectListItem> CreatePositionListItemn(List<Position> Positions)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Position position in Positions)
            {
                listItems.Add(new SelectListItem
                {
                    Text = position.Position_Type,
                    Value = position.Id.ToString()
                });
            }

            return listItems;
        }

        public  static bool CheckListItemIsEmpty(List<string> list)
        {
            foreach(string item in list)
            {
                if(!String.IsNullOrEmpty(item))
                {
                    return false;
                }
            }

            return true;                
        }
    }
}
