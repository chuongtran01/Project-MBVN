using System.Threading.Tasks;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public interface IEmailService
    {
        Task<bool> SendResetPasswordEmail(UserEmailOptions userEmailOptions);
    }
}