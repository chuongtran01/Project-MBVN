using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementSystem.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IEmailService _emailService;

    private readonly IConfiguration _configuration;

    public LoginController(ILogger<LoginController> logger, IEmailService emailService, IConfiguration configuration)
    {
        //_logger = logger;
        //_emailService = emailService;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }
    [AllowAnonymous, HttpGet("forgot-password")]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [AllowAnonymous, HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            // var user = await GetUserByEmailAsync(model.Email);
            // if (user != null)
            // {
            //   await  GenerateForgotPasswordTokenAsync(user);
            // }

            ApplicationUser testUser = new ApplicationUser()
            {
                Id = "123",
                FirstName = "JohnsonA",
                Email = model.Email
            };
            string testToken = "lksajdilwck";
            await SendResetPasswordEmail(testUser, testToken);

            ModelState.Clear();
            model.EmailSent = true;
        }
        return View(model);
    }

    public IActionResult ResetPassword()
    {
        return View();
    }
    // ----For future use----
    // public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    // {
    //     //test data
    //     ApplicationUser user = new ApplicationUser()
    //         {
    //             Id = "fakeID",
    //             FirstName = "John",
    //             Email = email
    //         };
    //     return user;
    // }
    // public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
    // {
    //     test data
    //     var token = "faketoken";
    //     if (!string.IsNullOrEmpty(token))
    //         {
    //             await SendResetPasswordEmail(user, token);
    //         }
    // }

    private async Task SendResetPasswordEmail(ApplicationUser user, string token)
    {
        string appDomain = _configuration.GetSection("Application:AppDomain").Value;
        string resetPasswordLink = _configuration.GetSection("Application:ForgotPassword").Value;
        UserEmailOptions options = new UserEmailOptions
        {
            toEmail = user.Email,
            PlaceHolders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("{{username}}", user.FirstName),
                new KeyValuePair<string, string>("{{link}}",
                    string.Format(appDomain + resetPasswordLink, user.Id, token))
            }
        };
        await _emailService.SendResetPasswordEmail(options);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
