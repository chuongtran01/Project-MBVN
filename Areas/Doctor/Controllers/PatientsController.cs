using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Areas.Doctor.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Areas.Doctor.Controllers
{
    public class PatientsController : DoctorController
    {
        private readonly IDatabaseService _service;

        public PatientsController(IDatabaseService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _service.GetPatientsWithAppointments();
              return View(patients);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _service.GetPatient(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            var prescriptions = await _service.GetPrescriptions(id.Value);
            PatientViewModel model = new()
            {
                Patient = patient,
                Prescriptions = prescriptions
            };
            ViewData["MedicineId"] = _service.GetMedicineDropdown();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrescription(PatientViewModel model)
        {
            try
            {
                Prescription p = model.NewPrescription;
                p.PatientId = model.Patient.PatientId;
                var result = await _service.AddPrescription(p);
                return RedirectToAction("Details", new { id = model.Patient.PatientId });
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}