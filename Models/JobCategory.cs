using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class JobCategory
    {
        public JobCategory()
        {
            EmployeesJobCategories = new HashSet<EmployeesJobCategories>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<EmployeesJobCategories> EmployeesJobCategories { get; set; }
    }
}
