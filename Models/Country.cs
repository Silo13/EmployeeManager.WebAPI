using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            Address = new HashSet<Address>();
            City = new HashSet<City>();
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
