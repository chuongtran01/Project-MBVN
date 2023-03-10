using HospitalManagementSystem.Areas.Admin.Models;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    public class DoctorController : AdminController
    {
        private readonly IDatabaseService _service;
        public DoctorController(IDatabaseService service)
        {
            _service = service;
        }
        public async Task<ViewResult> Index()
        {
            List<HospitalManagementSystem.Models.Doctor> doctors = await _service.GetAllDoctor();
            return View(doctors);
        }

        public async Task<ViewResult> Details(int id)
        {
            HospitalManagementSystem.Models.Doctor model = await _service.GetDoctor(id);
            return View(model);
        }
        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            HospitalManagementSystem.Models.Doctor doctor = await _service.GetDoctor(id);
            return View(doctor);
        }
        [HttpPost]
        public async Task<RedirectToActionResult> Edit(DoctorDetailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.UpdateDoctor(model);
                    if (result)
                    {
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("Details", new { id = model.DoctorId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Details", new { id = model.DoctorId });
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Create(CreateDoctorModel model)
        {
            try
            {
                var result = await _service.AddDoctor(model);
                if (result)
                {
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpDelete]
        public RedirectToActionResult Delete(int id)
        {
            try
            {
                var result = _service.DeleteDoctor(id);
                if (result)
                {
                    ModelState.Clear();
                }
				return RedirectToAction("Index");
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
				return RedirectToAction("Index");
			}
        }
    }
}
