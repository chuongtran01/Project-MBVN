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
        private static List<LogInViewModel> models = new List<LogInViewModel>
        {
            new LogInViewModel
            {
                Username = "chuongtran",
                Password = "1234"
            },
            new LogInViewModel
            {
                Username = "chuongtran02",
                Password = "1234"
            }

        };
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
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
                //encryptedPassword = GetMD5(model.Password);
                var result = models.Where(s => s.Username.Equals(model.Username) && s.Password.Equals(model.Password)).FirstOrDefault();

                if (result != null)
                {
                    //Session["Username"] = result.Username;
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

        //public ActionResult Logout()
        //{
        //    Session.Clear();
        //    return RedirectToAction("Login");
        //}

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            Console.WriteLine(model.Username);
            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(model.confirmPassword))
                {
                    ViewBag.error = "Confirmed password does not match";
                    return View();
                }
                var check = models.Where(s => s.Username == model.Username).FirstOrDefault();

                if (check == null)
                {
                    LogInViewModel newUser = new LogInViewModel()
                    {
                        Username = model.Username,
                        //Password = GetMD5(model.Password),
                        Password = model.Password,

                    };
                    models.Add(newUser);
                    
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
            _accountService.AddDemoUserToDb();
            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.getUserByEmail(model.Email);
                if (user != null)
                {
                    await _accountService.generateEmailResetPasswordTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                UserId = uid,
                Token = token
            };
            return View(resetPasswordModel);
        }
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountService.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.isSuccessful = true;
                    return View(model);
                }
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
    }
}

