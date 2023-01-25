using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Staff
    {
        public Staff()
        {
            BedDoctors = new HashSet<Bed>();
            BedNurses = new HashSet<Bed>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int StaffId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Midname { get; set; }
        public int? RoleId { get; set; }
        public string? Gender { get; set; }
        public string? Field { get; set; }
        public DateTime? Birthday { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Bed> BedDoctors { get; set; }
        public virtual ICollection<Bed> BedNurses { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
