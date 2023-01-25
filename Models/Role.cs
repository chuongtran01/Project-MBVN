using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Role
    {
        public Role()
        {
            staff = new HashSet<Staff>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Staff> staff { get; set; }
    }
}
