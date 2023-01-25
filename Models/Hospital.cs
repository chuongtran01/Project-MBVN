using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Hospital
    {
        public int HospitalId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
    }
}
