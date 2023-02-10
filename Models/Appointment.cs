using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string? DoctorSpecilization { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public double? Fees { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppontmentTime { get; set; }
        public byte[]? PostingDate { get; set; }
        public int? PatientStatus { get; set; }
        public int? DoctorStatus { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
