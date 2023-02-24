using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
		private readonly MBVNContext _context;

		public HomeController(MBVNContext context)
		{
			_context = context;
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
		public async Task<ViewResult> ViewDoctor()
		{
			List<Doctor> doctors = await _context.Doctors.Select(d => new Doctor()
			{

						DoctorId = d.DoctorId,
						Name = d.Name,
						Field = d.Field,
						DepartmentId = d.DepartmentId
			}).ToListAsync();
			return View(doctors);
		}

		public async Task<ViewResult> DoctorDetails(int id)
		{
			Doctor model = await _context.Doctors.Where(d => d.DoctorId == id).FirstOrDefaultAsync();
			Console.WriteLine(id);
			Console.WriteLine(model.DoctorId);
			return View(model);
		}
		[HttpGet]
		public async Task<ViewResult> Edit(int id)
		{
			Doctor doctor = await _context.Doctors.Where(d => d.DoctorId == id).FirstOrDefaultAsync();
			return View(doctor);
		}
		[HttpPost]
		public async Task<RedirectToActionResult> Edit(DoctorDetailModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Doctor doctor = await _context.Doctors.Where(d => d.DoctorId == model.DoctorId).FirstOrDefaultAsync();
					doctor.PhoneNumber = model.PhoneNumber;
					doctor.Birthday = model.Birthday;
					doctor.Name = model.Name;
					doctor.EmailAddress = model.EmailAddress;
					_context.Doctors.Update(doctor);
					_context.SaveChanges();
				}
				return RedirectToAction("DoctorDetails", new { id = model.DoctorId });
			}
			catch(Exception e)
			{
				return RedirectToAction("DoctorDetails", new { id = model.DoctorId });
			}
		}
	}
}

