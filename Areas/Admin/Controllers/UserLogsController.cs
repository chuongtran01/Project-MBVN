using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserLogsController : Controller
    {
        private readonly IDatabaseService _service;

        public UserLogsController(IDatabaseService service)
        {
			_service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<UserLog> userLogList = await _service.GetUserLog();
			return View(userLogList);
		}
	}
}
