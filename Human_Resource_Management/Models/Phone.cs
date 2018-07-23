using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class Phone
    {
        public int Id{ get; set;}

        public string Phone_Number { get; set;}

        public Employee Employee { get; set; }
    }
}
