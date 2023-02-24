using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public interface IAccountService
    {
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
        Task<bool> SendResetPasswordEmail(string email);
    }
}