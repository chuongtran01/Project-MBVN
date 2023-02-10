using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class MedicalHistory
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public double? BloodPressure { get; set; }
        public double? BloodSugar { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? Temperature { get; set; }
        public string? MedicalPres { get; set; }
        public byte[]? CreationDate { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
