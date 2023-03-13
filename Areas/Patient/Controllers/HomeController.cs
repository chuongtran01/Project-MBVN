using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Patient.Controllers
{
    public class HomeController : PatientController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
