﻿using System;
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

		public HomeController()
		{
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
	}
}

