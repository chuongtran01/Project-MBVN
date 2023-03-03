using System;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public interface ILogInService
    {
        public string getMD5(string str);
        public string getEncryptedPassword(string password);
        public Task<bool> LogIn(LogInViewModel model);
        public Task<bool> SignUp(SignUpViewModel model);
        public bool ConfirmPassword(SignUpViewModel model);
        public Task<bool> LogOut();
    }
}

