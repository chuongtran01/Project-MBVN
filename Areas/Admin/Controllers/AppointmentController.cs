using HospitalManagementSystem.Areas.Admin.Services;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AppointmentController : Controller
	{
		private readonly IDatabaseService _service;
		public AppointmentController(IDatabaseService service)
		{
			_service = service;
		}

		public async Task<ActionResult> Index()
		{
			List<Appointment> appointments = await _service.GetAllAppointments();
			return View();
		}

		public async Task<ActionResult> Details(int id)
		{
			Appointment model = await _service.GetAppointment(id);
			return View(model);
		}
	}
}
