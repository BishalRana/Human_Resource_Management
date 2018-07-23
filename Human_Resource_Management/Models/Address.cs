using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resource_Management.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public Employee Employee { get; set; }
    }
}
