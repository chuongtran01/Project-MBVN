using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Areas.Admin.Models
{
    public class CreateDoctorModel
{
        public string? Name { get; set; }
        public string? Field { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string? EmailAddress { get; set; }
        [Phone(ErrorMessage = "Please enter valid Phone number")]
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public int? DepartmentId { get; set; }
    }
}