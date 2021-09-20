using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeSalaries = new HashSet<EmployeeSalaries>();
            EmployeesJobCategories = new HashSet<EmployeesJobCategories>();
            InverseSuperior = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? CountryId { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? ExitedDate { get; set; }
        public int? SuperiorId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Country Country { get; set; }
        public virtual Employee Superior { get; set; }
        public virtual ICollection<EmployeeSalaries> EmployeeSalaries { get; set; }
        public virtual ICollection<EmployeesJobCategories> EmployeesJobCategories { get; set; }
        public virtual ICollection<Employee> InverseSuperior { get; set; }
    }
}
