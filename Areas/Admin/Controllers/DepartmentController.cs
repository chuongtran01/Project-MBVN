using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagementSystem.Controllers
{

    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly MBVNContext _context;

        public DepartmentController(MBVNContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> IndexAsync()
        {
            List<Department> departments = await _context.Departments.Select(d => new Department()
            {

                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Location = d.Location,
                CreationDate = d.CreationDate,
            }).ToListAsync();

            return View(departments);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentViewModel model)
        {
            var check = await _context.Departments.Where(s => s.Name.Equals(model.Name)).FirstOrDefaultAsync();

            if (check != null)
            {
                ViewBag.Error = "Department already exists";
                return View();
            }

            Department newDepartment = new Department()
            {
                Name = model.Name,
                Location = model.Location,
                CreationDate = DateTime.Now,
                UpdationDate = DateTime.Now
            };

            await _context.Departments.AddAsync(newDepartment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Department");
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            Department curDepartment = await _context.Departments.Where(d => d.DepartmentId == id).FirstOrDefaultAsync();

            EditDepartmentViewModel cur = new EditDepartmentViewModel()
            {
                DepartmentId = curDepartment.DepartmentId,
                Name = curDepartment.Name,
                Location = curDepartment.Location
            };

            return View(cur);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(EditDepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Department curDepartment = await _context.Departments.Where(d => d.DepartmentId == model.DepartmentId).FirstOrDefaultAsync();
                    curDepartment.Name = model.Name;
                    curDepartment.Location = model.Location;
                    _context.Departments.Update(curDepartment);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Department");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }

        }

        //[Route("/Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //try
            //{
            Department cur = await _context.Departments.FindAsync(id);
                //Department cur = await _context.Departments.Where(s => s.DepartmentId.Equals(id)).FirstOrDefaultAsync();
                //Department departmentToRemove = new() { DepartmentId = id };
                //_context.Departments.Attach(departmentToRemove);
                _context.Departments.Remove(cur);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Department");
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.error = ex.Message;
            //    return RedirectToAction("Index", "Department");
            //}

        }
    }
}

