using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Areas.Patient.Controllers
{
    public class AppointmentsController : PatientController
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAccountService _accountService;

        public AppointmentsController(IDatabaseService databaseService, IAccountService accountService)
        {
            _databaseService = databaseService;
            _accountService = accountService;
        }

        // GET: Patient/Appointments
        public async Task<IActionResult> Index()
        {
            var mBVNContext = await _databaseService.GetAllAppointments();
            return View(mBVNContext);
        }

        // GET: Patient/Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _databaseService.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public IActionResult Create()
        {
            ViewData["DoctorId"] = _databaseService.GetDoctorDropdown();
            ViewData["PatientId"] = _accountService.GetCurrentPatientUser();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("DoctorId,PatientId,AppointmentDate,AppontmentTime,PostingDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _databaseService.BookAppointment(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Patient/Appointments/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _databaseService.Appointments == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _databaseService.Appointments.FindAsync(id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DoctorId"] = new SelectList(_databaseService.Doctors, "DoctorId", "EmailAddress", appointment.DoctorId);
        //    ViewData["PatientId"] = new SelectList(_databaseService.Patients, "PatientId", "PatientId", appointment.PatientId);
        //    return View(appointment);
        //}

        // POST: Patient/Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,DoctorSpecilization,DoctorId,PatientId,Fees,AppointmentDate,AppontmentTime,PostingDate,PatientStatus,DoctorStatus")] Appointment appointment)
        //{
        //    if (id != appointment.AppointmentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _databaseService.Update(appointment);
        //            await _databaseService.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AppointmentExists(appointment.AppointmentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DoctorId"] = new SelectList(_databaseService.Doctors, "DoctorId", "EmailAddress", appointment.DoctorId);
        //    ViewData["PatientId"] = new SelectList(_databaseService.Patients, "PatientId", "PatientId", appointment.PatientId);
        //    return View(appointment);
        //}
    }
}
