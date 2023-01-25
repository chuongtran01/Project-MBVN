using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Room
    {
        public Room()
        {
            Beds = new HashSet<Bed>();
        }

        public int RoomId { get; set; }
        public byte? IsAvailable { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Bed> Beds { get; set; }
    }
}
