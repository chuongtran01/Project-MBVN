using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        //private static List<LogInViewModel> models = new List<LogInViewModel>
        //{
        //    new LogInViewModel
        //    {
        //        Username = "chuongtran",
        //        Password = "1234"
        //    },
        //    new LogInViewModel
        //    {
        //        Username = "chuongtran02",
        //        Password = "1234"
        //    }

        //};

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MBVNContext _context;
        private readonly IAccountService _accountService;

        private readonly ILogInService _logInService;
        //private readonly IManageProfile _manageProfile;

        public AccountController(IHttpContextAccessor httpContextAccessor, MBVNContext context, IAccountService accountService, ILogInService logInService)


        public AccountController(IHttpContextAccessor httpContextAccessor, MBVNContext context, IAccountService accountService)

        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _accountService = accountService;

            //_logInService = logInService;
            //_manageProfile = manageProfile;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            string role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            if(role != null)
            {
                return RedirectToAction("Index", "Home", new { area=role });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {

                //var encryptedPassword = GetMD5(model.Password);
                //var user = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

                bool success = await _accountService.LogIn(model);

                if (success == true)
                {
                    return RedirectToAction("Index", "Home", new {area="Patient"});

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            string role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            if (role != null)
            {
                return RedirectToAction("Index", "Home", new { area = role });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLoginAsync(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {

                //var encryptedPassword = GetMD5(model.Password);
                //var user = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

                bool success = await _accountService.AdminLogIn(model);

                if (success == true)
                {
                    //HttpContext.Session.SetString("UID", patientID);
                    return RedirectToAction("Index", "Home", new {area="Admin"});

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DoctorLogin()
        {
            string role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            if (role != null)
            {
                return RedirectToAction("Index", "Home", new { area = role });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoctorLoginAsync(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {

                //var encryptedPassword = GetMD5(model.Password);
                //var user = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();


                bool success = await _accountService.DoctorLogIn(model);

                if (success == true)
                {
                    //HttpContext.Session.SetString("UID", patientID);

                    return RedirectToAction("Index", "Home", new {area="Doctor"});

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            _accountService.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_accountService.ConfirmPassword(model))
                {
                    ViewBag.error = "Confirmed password does not match";
                    return View();
                }

                bool checkSignUp = await _accountService.SignUp(model);

                if (checkSignUp)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        [HttpGet]
        public IActionResult AdminSignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminSignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_logInService.ConfirmPassword(model))
                {
                    ViewBag.error = "Confirmed password does not match";
                    return View();
                }

                bool checkSignUp = await _logInService.AdminSignUp(model);

                if (checkSignUp)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        [HttpGet]
        public IActionResult DoctorSignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoctorSignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_logInService.ConfirmPassword(model))
                {
                    ViewBag.error = "Confirmed password does not match";
                    return View();
                }

                bool checkSignUp = await _logInService.DoctorSignUp(model);

                if (checkSignUp)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
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
				await _accountService.SendResetPasswordEmail(model.Email);
				ModelState.Clear();
				model.EmailSent = true;
				return View(model);
			}
            return View(model);
        }


        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(int uid)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                UserId = uid,
            };
            return View(resetPasswordModel);
        }
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ResetPasswordAsync(model);
                if (result)
                {
                    ModelState.Clear();
                    model.isSuccessful = true;
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ManageProfile()
        {
            var curUserId = Int32.Parse(HttpContext.Session.GetString("UID"));
            var curUser = _context.Patients.Where(s => s.PatientId.Equals(curUserId)).FirstOrDefault();

            ManageProfileViewModel user = new ManageProfileViewModel
            {
                ID = curUser.PatientId,
                FirstName = curUser.Firstname,
                Email = curUser.EmailAddress,
                Address = curUser.Address,
                Gender = curUser.Gender,
            };

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ManageProfile(ManageProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Patient curUser = _context.Patients.Where(s => s.PatientId.Equals(model.ID)).FirstOrDefault();

                if (curUser != null)
                {
                    curUser.Firstname = model.FirstName;
                    curUser.Address = model.Address;
                    curUser.Gender = model.Gender;
                    curUser.LastVisited = DateTime.Now;

                    _context.Patients.Update(curUser);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "User does not exist!";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Model is invalid!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUsViewModelGuest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] b = new byte[] { 10, 12, 12, 12 };
                    DateTime t = DateTime.Now;
                    Array.Copy(BitConverter.GetBytes(t.Ticks), 0, b, 0, 4);

                    ContactU newMessage = new ContactU()
                    {
                        Fullname = model.Fullname,
                        Contactno = model.Contactno,
                        Email = model.Email,
                        Message = model.Message,
                        PostingDate = null,
                        LastUpdation = DateTime.Now
                    };

                    await _context.ContactUs.AddAsync(newMessage);
                    await _context.SaveChangesAsync();

                    ViewBag.sender = model.Fullname;
                    return RedirectToAction("SuccessfullySentMessage", "Account");

                }
                else
                {
                    ViewBag.error = "Something wrong happend. Please try again!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult SuccessfullySentMessage(string sender)
        {
            //ViewBag.sender = sender;
            return View();
        }
    }
}

