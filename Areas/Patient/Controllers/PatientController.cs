using HospitalManagementSystem.Utils.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Patient.Controllers
{
    [Area("Patient")]
    [TypeFilter(typeof(PatientAuthorization))]
    public abstract class PatientController : Controller
    {
    }
}
