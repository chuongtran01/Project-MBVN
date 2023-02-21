using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public partial class AspNetUserToken
    {
        [Key] public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        [Key] public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
