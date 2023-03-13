using HospitalManagementSystem.Utils.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Doctor.Controllers
{
    [Area("Doctor")]

    [TypeFilter(typeof(DoctorAuthorization))]
    public abstract class DoctorController : Controller {}
}
