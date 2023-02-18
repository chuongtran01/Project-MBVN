using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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

