using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly MBVNContext _context;
        public AccountService(IConfiguration configuration, IEmailService emailservice, MBVNContext context)
        {
            _configuration = configuration;
            _emailService = emailservice;
            _context = context;
        }
        public async Task SendResetPasswordEmail(Patient user)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string resetPasswordLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new()
            {
                toEmail = user.EmailAddress,
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{username}}", user.Firstname),
                    new KeyValuePair<string, string>("{{link}}",
                        string.Format(appDomain + resetPasswordLink, user.PatientId))
                }
            };
            await _emailService.SendResetPasswordEmail(options);
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            Patient user = await _context.Patients.FindAsync(model.UserId);
            if (user != null)
            {
                user.Password = model.NewPassword;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
