using HospitalManagementSystem.Areas.Admin.Models;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Areas.Admin.Services
{
    public interface IDoctorControllerService
    {
        Task<Doctor> GetDoctor(int doctorId);
        Task<List<Doctor>> GetAllDoctor();
        Task<bool> AddDoctor(CreateDoctorModel model);
        bool DeleteDoctor(int id);
        Task<bool> UpdateDoctor(DoctorDetailModel model);
    }
}
