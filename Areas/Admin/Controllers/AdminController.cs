using HospitalManagementSystem.Utils.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AdminAuthorization))]
    public abstract class AdminController : Controller
    {
    }
}
