using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class AddDepartmentViewModel
    {
        [Required(ErrorMessage = "Name can not be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Location can not be blank")]
        public string Location { get; set; }
    }
}

