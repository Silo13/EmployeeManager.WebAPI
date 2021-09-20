using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class EmployeeSalaries
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
