using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class EmployeesJobCategories
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int JobCategoryId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual JobCategory JobCategory { get; set; }
    }
}
