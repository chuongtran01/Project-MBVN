using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Areas.Doctor.Models
{
    public class PatientViewModel
    {
        public HospitalManagementSystem.Models.Patient Patient { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public Prescription NewPrescription { get; set; }
    }
}
