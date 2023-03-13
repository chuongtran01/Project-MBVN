using System;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public interface IManageProfile
    {
        public Task<ManageProfileViewModel> getPatient();
        public Task<bool> editProfile(ManageProfileViewModel model);
    }
}

