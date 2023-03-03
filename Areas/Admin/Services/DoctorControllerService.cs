using HospitalManagementSystem.Areas.Admin.Models;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Areas.Admin.Services
{
    public class DoctorControllerService : IDoctorControllerService
    {
        private MBVNContext _context;
        public DoctorControllerService(MBVNContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctor(int doctorId)
        {
            return await _context.Doctors.Where(d => d.DoctorId == doctorId).FirstOrDefaultAsync();
        }
        public async Task<List<Doctor>> GetAllDoctor()
        {
            return await _context.Doctors.Select(d => new Doctor()
            {

                DoctorId = d.DoctorId,
                Name = d.Name,
                Field = d.Field,
                DepartmentId = d.DepartmentId
            }).ToListAsync();
        }
        public async Task<bool> UpdateDoctor(DoctorDetailModel model)
        {
            try
            {
                Doctor doctor = await _context.Doctors.Where(d => d.DoctorId == model.DoctorId).FirstOrDefaultAsync();
                doctor.PhoneNumber = model.PhoneNumber;
                doctor.Birthday = model.Birthday;
                doctor.Name = model.Name;
                doctor.EmailAddress = model.EmailAddress;
                _context.Doctors.Update(doctor);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddDoctor(CreateDoctorModel model)
        {
            var check = await _context.Doctors.Where(d => d.EmailAddress == model.EmailAddress).FirstOrDefaultAsync();
            if (check != null)
            {
                return false;
            }
            try
            {
                Doctor newDoctor = new()
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Field = model.Field,
                    Birthday = model.Birthday,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    DepartmentId = model.DepartmentId,
                    Password = model.Password,
                };
                await _context.AddAsync(newDoctor);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool DeleteDoctor(int id)
        {
            try
            {
                Doctor doctorToRemove = new() { DoctorId = id };
                _context.Doctors.Attach(doctorToRemove);
                _context.Doctors.Remove(doctorToRemove);
                _context.SaveChanges();
                return true;
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
