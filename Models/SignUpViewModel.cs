using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SignUpViewModel
    {
        //[Required(ErrorMessage = "Username can not be blank")]
        //public string? Firstname { get; set; }
        //[Required(ErrorMessage = "Lastname can not be blank")]
        //public string? Lastname { get; set; }
        //[Required(ErrorMessage = "Address can not be blank")]
        //public string? Address { get; set; }
        //[Required(ErrorMessage = "Gender can not be blank")]
        //public string? Gender { get; set; }
        [Required(ErrorMessage = "Email can not be blank")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password can not be blank")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirmed password can not be blank")]
        public string confirmPassword { get; set; }
    }

    //public enum Gender
    //{
    //    Male,
    //    Female,
    //    Nonbinary
    //}
}

