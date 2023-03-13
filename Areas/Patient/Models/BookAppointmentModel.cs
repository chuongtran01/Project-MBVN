using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Areas.Admin.Models
{
    public class BookAppointmentModel
{
        public int PatientId { get; set; }
        public int? DoctorId { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }
    }
}