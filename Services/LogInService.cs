using System;
using System.Security.Cryptography;
using System.Text;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace HospitalManagementSystem.Services
{
    public class LogInService : ILogInService
    {
        //private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MBVNContext _context;

        public LogInService(MBVNContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public bool ConfirmPassword(SignUpViewModel model)
        {
            return model.Password.Equals(model.confirmPassword);
        }

        public string getEncryptedPassword(string password)
        {
            return getMD5(password);
        }

        public string getMD5(string str)
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

        public async Task<bool> LogIn(LogInViewModel model)
        {
            string encryptedPassword = getEncryptedPassword(model.Password);
            
            //var user = _context.Patients.Where(s => s.EmailAddress.Equals(emailAddress) && s.Password.Equals(Encrypted)).FirstOrDefault();
            var user = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UID", user.PatientId.ToString());
                long bNow = DateTime.Now.ToBinary();
                byte[] arrayNow = BitConverter.GetBytes(bNow);
                UserLog userLog = new()
                {
                    Uid = user.PatientId,
                    Username = user.EmailAddress,
                    LoginTime = arrayNow,
                    Status = 1
                };
                await _context.AddAsync(userLog);
                _context.SaveChanges();
				return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LogOut()
        {
            UserLog ul = _context.UserLogs.Where(ul => ul.Uid == Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID")) && ul.Status == 1).FirstOrDefault();
            ul.Status = 0;
            _context.SaveChanges();
            _httpContextAccessor.HttpContext.Session.Clear();
            if (_httpContextAccessor.HttpContext.Session.GetString("UID") == null) {
                return true;
            }
            return false;
        }

        public async Task<bool> SignUp(SignUpViewModel model)
        {
            var check = _context.Patients.Where(s => s.EmailAddress == model.EmailAddress).FirstOrDefault();
            
            if (check == null)
            {
                Patient newUser = new Patient()
                {
                    Firstname = null,
                    Lastname = null,
                    Midname = null,
                    Address = null,
                    Gender = null,
                    EmailAddress = model.EmailAddress,
                    Password = getEncryptedPassword(model.Password),
                    PhoneNumber = null,
                    EmergencyContact = null,
                    PhotoImage = null,
                    CreatedDate = DateTime.Now,
                    LastVisited = DateTime.Now,
                };

                _context.Patients.AddAsync(newUser);
                await _context.SaveChangesAsync();
                _httpContextAccessor.HttpContext.Session.SetString("UID", newUser.PatientId.ToString());
                return true;
            }
            return false;
        }
    }
}

