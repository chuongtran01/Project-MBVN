using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Nurse
    {
        public int NurseId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PhoneNumbner { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
