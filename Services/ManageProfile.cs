using System;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class ManageProfile : IManageProfile
    {
        //private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MBVNContext _context;

        public ManageProfile(MBVNContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> editProfile(ManageProfileViewModel model)
        {
            var curUser = await _context.Patients.FindAsync(model.ID);

            if (curUser != null)
            {
                curUser.Address = model.Address;
                curUser.Gender = model.Gender;
                curUser.LastVisited = DateTime.Now;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<ManageProfileViewModel> getPatient()
        {
            var curUserId = Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID"));
            var curUser = await _context.Patients.FindAsync(curUserId);

            if (curUser != null)
            {
                ManageProfileViewModel user = new ManageProfileViewModel
                {
                    ID = curUser.PatientId,
                    FirstName = curUser.Firstname,
                    Email = curUser.EmailAddress,
                    Address = curUser.Address,
                    Gender = curUser.Gender,
                };

                return user;
            }
            return null;
        }
    }
}

