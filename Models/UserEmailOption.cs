using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class UserEmailOptions
    {
        public string toEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}