using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class ContactU
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Contactno { get; set; }
        public string? Message { get; set; }
        public byte[]? PostingDate { get; set; }
        public DateTime? LastUpdation { get; set; }
    }
}
