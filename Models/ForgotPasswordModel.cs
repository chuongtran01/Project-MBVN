using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class ForgotPasswordModel
    {
        public string? Email { get; set; }
        public bool? EmailSent { get; set; }
    }
}