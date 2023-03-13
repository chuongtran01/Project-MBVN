using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly MBVNContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IConfiguration configuration, IEmailService emailservice, MBVNContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _emailService = emailservice;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> SendResetPasswordEmail(string email)
        {
            var user = _context.Patients.Where(p => p.EmailAddress == email).FirstOrDefault();
            if (user != null)
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
				return await _emailService.SendResetPasswordEmail(options);
			}
            return false;
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            Patient user = await _context.Patients.FindAsync(model.UserId);
            if (user != null)
            {
                string encryptedPassword = getEncryptedPassword(model.NewPassword);
                user.Password = encryptedPassword;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public int? GetCurrentUser()
        {
            var curUserId = Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID"));

            if (curUserId != null)
            {
                return curUserId;
            }
            return null;

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

            var user = _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UID", user.PatientId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Role", "Patient");
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
        public async Task<bool> AdminLogIn(LogInViewModel model)
        {
            string encryptedPassword = getEncryptedPassword(model.Password);

            var user = _context.Admins.Where(s => s.Email.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UID", user.AdminId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Role", "Admin");
                long bNow = DateTime.Now.ToBinary();
                byte[] arrayNow = BitConverter.GetBytes(bNow);
                UserLog userLog = new()
                {
                    Uid = user.AdminId,
                    Username = user.Username,
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

        public async Task<bool> DoctorLogIn(LogInViewModel model)
        {
            string encryptedPassword = getEncryptedPassword(model.Password);

            //var user = _context.Patients.Where(s => s.EmailAddress.Equals(emailAddress) && s.Password.Equals(Encrypted)).FirstOrDefault();
            var user = _context.Doctors.Where(s => s.EmailAddress.Equals(model.EmailAddress) && s.Password.Equals(encryptedPassword)).FirstOrDefault();

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UID", user.DoctorId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Role", "Doctor");
                long bNow = DateTime.Now.ToBinary();
                byte[] arrayNow = BitConverter.GetBytes(bNow);
                DoctorsLog doctorLog = new()
                {
                    Uid = user.DoctorId,
                    Username = user.EmailAddress,
                    LoginTime = arrayNow,
                };
                await _context.AddAsync(doctorLog);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> LogOut()
        {
            var role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            switch (role)
            {
                case "Patient":
                    UserLog ul = _context.UserLogs.Where(ul => ul.Uid == Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID"))).FirstOrDefault();
                    break;
                case "Doctor":
                    DoctorsLog dl = _context.DoctorsLogs.Where(dl => dl.Uid == Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID"))).FirstOrDefault();
                    break;
            }
            _context.SaveChanges();
            _httpContextAccessor.HttpContext.Session.Clear();
            if (_httpContextAccessor.HttpContext.Session.GetString("UID") == null)
            {
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
        public async Task<bool> editProfile(ManageProfileViewModel model)
        {
            var curUser = await _context.Patients.FindAsync(model.ID);

            if (curUser != null)
            {
                curUser.Address = model.Address;
                curUser.Gender = model.Gender;
                curUser.LastVisited = DateTime.Now;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<ManageProfileViewModel> getPatient()
        {
            var curUserId = Int32.Parse(_httpContextAccessor.HttpContext.Session.GetString("UID"));
            var curUser = await _context.Patients.FindAsync(curUserId);

            if (curUser != null)
            {
                ManageProfileViewModel user = new ManageProfileViewModel
                {
                    ID = curUser.PatientId,
                    FirstName = "Chuong",
                    Email = curUser.EmailAddress,
                    Address = "266 dfdas",
                    Gender = curUser.Gender,
                };

                return user;
            }
            return null;
        }
    }
}
