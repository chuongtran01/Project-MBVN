using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public int? Uid { get; set; }
        public string? Username { get; set; }
        public byte[]? LoginTime { get; set; }
        public int? Status { get; set; }
    }
}
