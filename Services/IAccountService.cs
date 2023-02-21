using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> AddDemoUserToDb();
        Task generateEmailResetPasswordTokenAsync(Patient user);
        Task<Patient> getUserByEmail(string email);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}