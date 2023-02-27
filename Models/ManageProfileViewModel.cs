using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class ManageProfileViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Address can not be blank")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Gender can not be blank")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email can not be blank")]
        public string Email { get; set; }
    }

    public enum GenderSelection
    {
        Male,
        Female,
        Notspecified
    }
}

