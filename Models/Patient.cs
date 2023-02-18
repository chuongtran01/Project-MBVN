using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            MedicalHistories = new HashSet<MedicalHistory>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int PatientId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Midname { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmergencyContact { get; set; }
        public byte[]? PhotoImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastVisited { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}