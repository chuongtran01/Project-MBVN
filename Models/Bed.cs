using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Bed
    {
        public int BedId { get; set; }
        public string? Location { get; set; }
        public byte? IsAvai { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public int? RoomId { get; set; }

        public virtual Staff? Doctor { get; set; }
        public virtual Staff? Nurse { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Room? Room { get; set; }
    }
}
