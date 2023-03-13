using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public interface IAccountService
    {
        Task<bool> AdminLogIn(LogInViewModel model);
        bool ConfirmPassword(SignUpViewModel model);
        Task<bool> DoctorLogIn(LogInViewModel model);
        Task<bool> editProfile(ManageProfileViewModel model);
        int? GetCurrentUser();
        string getEncryptedPassword(string password);
        Task<ManageProfileViewModel> getPatient();
        Task<bool> LogIn(LogInViewModel model);
        Task<bool> LogOut();
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
        Task<bool> SendResetPasswordEmail(string email);
        Task<bool> SignUp(SignUpViewModel model);
    }
}