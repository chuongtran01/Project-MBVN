using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class EditDepartmentViewModel
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Name can not be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Location can not be blank")]
        public string Location { get; set; }
    }
}

