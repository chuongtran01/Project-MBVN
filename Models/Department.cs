using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Department
    {
        public Department()
        {
            staff = new HashSet<Staff>();
        }

        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Staff> staff { get; set; }
    }
}
