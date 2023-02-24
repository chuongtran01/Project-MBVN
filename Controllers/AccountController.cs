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

        public AccountController(IHttpContextAccessor httpContextAccessor, MBVNContext context, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _accountService = accountService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = GetMD5(model.Password);
                var result = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();


                if (result != null)
                {
                    HttpContext.Session.SetString("UID", result.PatientId.ToString());
                    return RedirectToAction("Index", "Home");

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
            HttpContext.Session.Clear();
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
                if (!model.Password.Equals(model.confirmPassword))
                {
                    ViewBag.error = "Confirmed password does not match";
                    return View();
                }
                var check = _context.Patients.Where(s => s.EmailAddress == model.EmailAddress).FirstOrDefault();

                if (check == null)
                {
                    Patient newUser = new Patient()
                    {
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        Midname = null,
                        Address = model.Address,
                        Gender = model.Gender,
                        EmailAddress = model.EmailAddress,
                        Password = GetMD5(model.Password),
                        PhoneNumber = null,
                        EmergencyContact = null,
                        PhotoImage = null,
                        CreatedDate = null,
                        LastVisited = null,
                    };
                    _context.Patients.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    //models.Add(newUser);
                    HttpContext.Session.SetString("UID", newUser.PatientId.ToString());

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
        // Encrypt password - create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
        }

        [HttpGet]
        public IActionResult ManageProfile()
        {
            var curUserId = Int32.Parse(HttpContext.Session.GetString("UID"));
            var curUser = _context.Patients.Where(s => s.PatientId.Equals(curUserId)).FirstOrDefault();
            
            return View(curUser);
        }
    }
}

