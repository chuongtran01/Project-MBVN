using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Email can not be blank")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password can not be blank")]
        public string Password { get; set; }
    }
}

