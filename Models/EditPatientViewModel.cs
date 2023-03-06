using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HospitalManagementSystem.Models
{
    public class EditPatientViewModel
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "First name can not be blank")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name can not be blank")]
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address can not be blank")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password can not be blank")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "ConfirmedPassword can not be blank")]
        public string ConfirmedPassword { get; set; }
        [Required(ErrorMessage = "Gender can not be blank")]
        public string Gender { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number can not be blank")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "DOB can not be blank")]
        public DateTime ?Birthday { get; set; }
        public string Address { get; set; }
    }
}

