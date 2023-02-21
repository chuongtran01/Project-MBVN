using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Patient> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<Patient> usermanager, IConfiguration configuration, IEmailService emailservice)
        {
            _userManager = usermanager;
            _configuration = configuration;
            _emailService = emailservice;
        }
        public async Task<Patient> getUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task generateEmailResetPasswordTokenAsync(Patient user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendResetPasswordEmail(user, token);
            }
        }
        private async Task SendResetPasswordEmail(Patient user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string resetPasswordLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new()
            {
                toEmail = user.Email,
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{username}}", user.Firstname),
                    new KeyValuePair<string, string>("{{link}}",
                        string.Format(appDomain + resetPasswordLink, user.Id, token))
                }
            };
            await _emailService.SendResetPasswordEmail(options);
        }
        public async Task<IdentityResult> AddDemoUserToDb()
        {
            var user = new Patient
            {
                UserName = "test@test.com",
                Email = "test@test.com"
            };
            Console.WriteLine("AddDemoUserToDb called");
            var result = await _userManager.CreateAsync(user, "test@Test123");
            Console.WriteLine(result.Succeeded.ToString());
            return result;
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }
    }
}
