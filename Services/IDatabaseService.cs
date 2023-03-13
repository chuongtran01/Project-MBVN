using HospitalManagementSystem.Areas.Admin.Models;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem.Services
{
    public interface IDatabaseService
    {
        Task<Doctor> GetDoctor(int doctorId);
        Task<List<Doctor>> GetAllDoctor();
        Task<bool> AddDoctor(CreateDoctorModel model);
        bool DeleteDoctor(int id);
        Task<bool> UpdateDoctor(DoctorDetailModel model);
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment> GetAppointment(int? appointmentId);
        Task<List<UserLog>> GetUserLog();
        Task<ContactU> GetQuery(int? id);
        Task<List<ContactU>> GetAllQueries();
        Task<bool> UpdateQuery(int id);
        Task<bool> BookAppointment(Appointment model);
        SelectList GetDoctorDropdown();
        Task<List<DoctorsLog>> GetDoctorLog();
    }
}
