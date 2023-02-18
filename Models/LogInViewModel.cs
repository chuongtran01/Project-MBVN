using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Username can not be blank")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not be blank")]
        public string Password { get; set; }
    }
}

