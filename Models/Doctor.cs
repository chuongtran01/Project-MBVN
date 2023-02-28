using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Field { get; set; }
        public DateTime? Birthday { get; set; }
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        [Phone(ErrorMessage = "Please enter valid Phone number")]
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
