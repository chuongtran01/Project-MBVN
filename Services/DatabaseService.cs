using HospitalManagementSystem.Areas.Admin.Models;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class DatabaseService : IDatabaseService
    {
        private MBVNContext _context;
        private readonly IAccountService _accountService;
        public DatabaseService(MBVNContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        // APPOINTMENT
        public async Task<List<Appointment>> GetAllAppointments()
        {
            var curUser = _accountService.GetCurrentUser();
            if (curUser == null)
            {
                return null;
            }
            var mBVNContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.PatientId == curUser);
            return await mBVNContext.ToListAsync();
        }
        public async Task<Appointment> GetAppointment(int? appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.AppointmentId == appointmentId);
        }
        public async Task<bool> BookAppointment(Appointment model)
        {
            try
            {
                Doctor doctor = await _context.Doctors.FindAsync(model.DoctorId);
                Patient patient = await _context.Patients.FindAsync(_accountService.GetCurrentUser());
                model.Doctor = doctor;
                model.Patient = patient;
                _context.Add(model);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public SelectList GetDoctorDropdown()
        {
            return new SelectList(_context.Doctors, "DoctorId", "Name");
        }

        // DOCTOR 
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
            }
            catch (Exception ex)
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
                var encryptedPassword = Utils.Password.getMD5(model.Password);
                Doctor newDoctor = new()
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Field = model.Field,
                    Birthday = model.Birthday,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    DepartmentId = model.DepartmentId,
                    Password = encryptedPassword,
                };
                await _context.AddAsync(newDoctor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // PATIENT
        public async Task<List<Patient>> GetPatientsWithAppointments()
        {
            int doctorId = _accountService.GetCurrentUser().Value;
            var patientsWithAppointments = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.Patient)
                .ToListAsync();

            return patientsWithAppointments;
        }
        public async Task<Patient> GetPatient(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
            return patient;
        }
        public async Task<List<Prescription>> GetPrescriptions(int patientId)
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medicine)
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
            return prescriptions;
        }
        public SelectList GetMedicineDropdown()
        {
            return new SelectList(_context.Medicines, "MedicineId", "Name");
        }
        public async Task<bool> AddPrescription(Prescription p)
        {
            int doctorId = _accountService.GetCurrentUser().Value;
            Medicine m = await _context.Medicines.FirstOrDefaultAsync(m=>m.MedicineId == p.MedicineId);
            p.Medicine = m;
            p.DoctorId = doctorId;
            await _context.AddAsync(p);
            _context.SaveChanges();
            return true;
        }
        // USER LOG
        public async Task<List<UserLog>> GetUserLog()
        {
            return await _context.UserLogs.ToListAsync();
        }
        public async Task<List<DoctorsLog>> GetDoctorLog()
        {
            return await _context.DoctorsLogs.ToListAsync();
        }

        // CONTACT US QUERY
        public async Task<List<ContactU>> GetAllQueries()
        {
            return await _context.ContactUs.ToListAsync();
        }

        public async Task<ContactU> GetQuery(int? id)
        {
            return await _context.ContactUs.FindAsync(id);
        }
        public async Task<bool> UpdateQuery(int id)
        {
            try
            {
                ContactU query = await _context.ContactUs.Where(q => q.Id == id).FirstOrDefaultAsync();
                query.LastUpdation = DateTime.Now;
                _context.ContactUs.Update(query);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
