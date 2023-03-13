using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalManagementSystem.Utils.Authorization
{
    public class AdminAuthorization : IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            string role = context.HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                Console.Write("test");
                context.Result = new RedirectToActionResult("Index", "Home", new { area = role??"" });
            }
        }
    }
}
