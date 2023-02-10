using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Department
    {
        public Department()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
