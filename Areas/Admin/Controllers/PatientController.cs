using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.BuilderProperties;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagementSystem.Controllers
{

    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly MBVNContext _context;

        public PatientController(MBVNContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> IndexAsync(string childname)
        {
            if (String.IsNullOrEmpty(childname))
            {
                List<Patient> patients = await _context.Patients.Select(d => new Patient()
                {

                    PatientId = d.PatientId,
                    Firstname = d.Firstname,
                    Lastname = d.Lastname,
                    Gender = d.Gender,
                    EmailAddress = d.EmailAddress,
                    PhoneNumber = d.PhoneNumber,
                    Birthday = d.Birthday
                }).ToListAsync();

                return View(patients);
            }
            else
            {
                var check = isDigitsOnly(childname);

                if (check == false)
                {
                    List<Patient> searchPatients = await _context.Patients.Select(d => new Patient()
                    {

                        PatientId = d.PatientId,
                        Firstname = d.Firstname,
                        Lastname = d.Lastname,
                        Gender = d.Gender,
                        EmailAddress = d.EmailAddress,
                        PhoneNumber = d.PhoneNumber,
                        Birthday = d.Birthday
                    }).Where(s => s.Firstname.Contains(childname)).ToListAsync();

                    return View(searchPatients);
                }
                else
                {
                    List<Patient> searchPatients = await _context.Patients.Select(d => new Patient()
                    {

                        PatientId = d.PatientId,
                        Firstname = d.Firstname,
                        Lastname = d.Lastname,
                        Gender = d.Gender,
                        EmailAddress = d.EmailAddress,
                        PhoneNumber = d.PhoneNumber,
                        Birthday = d.Birthday
                    }).Where(s => s.PhoneNumber.Contains(childname)).ToListAsync();

                    return View(searchPatients);
                }
            }
            
        }

        static bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        [HttpGet]
        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(AddPatientViewModel model)
        {
            try
            {   
                if (ModelState.IsValid)
                {
                    var check = await _context.Patients.Where(s => s.EmailAddress.Equals(model.EmailAddress)).FirstOrDefaultAsync();

                    if (check != null)
                    {
                        ViewBag.error = "Email already exists.";
                        return View();
                    }
                    
                    Patient newPatient = new Patient()
                    {
                        Firstname = model.FirstName,
                        Lastname = model.LastName,
                        EmailAddress = model.EmailAddress,
                        Password = model.Password,
                        Gender = model.Gender,
                        PhoneNumber = model.PhoneNumber,
                        EmergencyContact = model.EmergencyContact,
                        Birthday = model.Birthday,
                        Address = model.Address,
                        CreatedDate = DateTime.Now,
                        LastVisited = DateTime.Now
                    };

                    await _context.Patients.AddAsync(newPatient);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    ViewBag.error = "ModelState is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> EditPatient(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Patient curPatient = await _context.Patients.Where(d => d.PatientId == id).FirstOrDefaultAsync();

                    EditPatientViewModel cur = new EditPatientViewModel()
                    {
                        PatientId = curPatient.PatientId,
                        FirstName = curPatient.Firstname,
                        LastName = curPatient.Lastname,
                        EmailAddress = curPatient.EmailAddress,
                        Password = curPatient.Password,
                        Gender = curPatient.Gender,
                        PhoneNumber = curPatient.PhoneNumber,
                        EmergencyContact = curPatient.EmergencyContact,
                        Birthday = curPatient.Birthday,
                        Address = curPatient.Address,
                    };

                    return View(cur);
                }
                else
                {
                    ViewBag.error = "Model state is invalid!";
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> EditPatient(EditPatientViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Patient curPatient = await _context.Patients.Where(d => d.PatientId == model.PatientId).FirstOrDefaultAsync();

                    if (curPatient != null)
                    {
                        curPatient.Firstname = model.FirstName;
                        curPatient.Lastname = model.LastName;
                        curPatient.EmailAddress = model.EmailAddress;
                        curPatient.Password = model.Password;
                        curPatient.Gender = model.Gender;
                        curPatient.PhoneNumber = model.PhoneNumber;
                        curPatient.EmergencyContact = model.EmergencyContact;
                        curPatient.Birthday = model.Birthday;
                        curPatient.Address = model.Address;
                        //curPatient.Name = model.Name;
                        //curPatient.Location = model.Location;
                        _context.Patients.Update(curPatient);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        ViewBag.error = "Something wrong happened!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Model State is invalid!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }

        }

        //[Route("/Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Patient cur = await _context.Patients.Where(s => s.PatientId.Equals(Id)).FirstOrDefaultAsync();
                _context.Patients.Remove(cur);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Patient");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index", "Patient");
            }

        }
    }
}

