using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            Appointments = new HashSet<Appointment>();
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            MedicalHistories = new HashSet<MedicalHistory>();
            Prescriptions = new HashSet<Prescription>();
            Roles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; } = null!;
        public string Discriminator { get; set; } = null!;
        public int? PatientId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Midname { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? EmergencyContact { get; set; }
        public byte[]? PhotoImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastVisited { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }

        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
