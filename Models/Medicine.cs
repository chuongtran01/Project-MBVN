using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int MedicineId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SideEffect { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
