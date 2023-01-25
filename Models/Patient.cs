using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Beds = new HashSet<Bed>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int PatientId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Midname { get; set; }
        public string? Address { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmergencyContact { get; set; }
        public byte[]? PhotoImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastVisited { get; set; }

        public virtual ICollection<Bed> Beds { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
