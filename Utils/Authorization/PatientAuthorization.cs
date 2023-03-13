using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalManagementSystem.Utils.Authorization
{
    public class PatientAuthorization : IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            string role = context.HttpContext.Session.GetString("Role");
            if (role != "Patient")
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { area = role??"" });
            }
        }
    }
}
