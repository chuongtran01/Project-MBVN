using System.Threading.Tasks;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public interface IEmailService
    {
        Task<bool> SendReplyEmail(UserEmailOptions userEmailOptions);
        Task<bool> SendResetPasswordEmail(UserEmailOptions userEmailOptions);
    }
}