using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Prescription
    {
        public int PrescriptionId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int? MedicineId { get; set; }
        public string? Frequency { get; set; }
        public double? Dosage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PatientId1 { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Medicine? Medicine { get; set; }
        public virtual AspNetUser? PatientId1Navigation { get; set; }
    }
}
