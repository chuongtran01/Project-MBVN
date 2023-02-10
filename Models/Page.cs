using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Page
    {
        public int PageId { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public byte[]? UpdationDate { get; set; }
        public string? OpenningTime { get; set; }
    }
}
