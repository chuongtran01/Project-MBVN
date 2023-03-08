using System;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public interface ILogInService
    {
        public string getMD5(string str);
        public string getEncryptedPassword(string password);
        public Task<bool> LogIn(LogInViewModel model);
        public Task<bool> AdminLogIn(LogInViewModel model);
        public Task<bool> DoctorLogIn(LogInViewModel model);
        public Task<bool> SignUp(SignUpViewModel model);
        public Task<bool> AdminSignUp(SignUpViewModel model);
        public Task<bool> DoctorSignUp(SignUpViewModel model);
        public bool ConfirmPassword(SignUpViewModel model);
        public bool LogOut();
    }
}

