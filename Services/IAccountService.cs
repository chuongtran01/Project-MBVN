using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public interface IAccountService
    {
        bool ConfirmPassword(SignUpViewModel model);
        Task<bool> editProfile(ManageProfileViewModel model);
        Task<int?> GetCurrentPatientUser();
        string getEncryptedPassword(string password);
        Task<ManageProfileViewModel> getPatient();
        Task<bool> LogIn(LogInViewModel model);
        Task<bool> LogOut();
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
        Task<bool> SendResetPasswordEmail(string email);
        Task<bool> SignUp(SignUpViewModel model);
    }
}