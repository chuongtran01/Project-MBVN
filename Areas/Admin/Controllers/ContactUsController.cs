using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    public class ContactUsController : AdminController
    {
        private readonly IDatabaseService _service;
        private readonly IEmailService _email;

        public ContactUsController(IDatabaseService service, IEmailService email)
        {
            _service = service;
            _email = email;
        }

        public async Task<IActionResult> Index()
        {
            var queries = await _service.GetAllQueries();
              return View(queries);
        }

        public async Task<IActionResult> Details(int? id)
        {

            var contactU = await _service.GetQuery(id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(int queryId, string emailContent)
        {
            var query = await _service.GetQuery(queryId);
            UserEmailOptions options = new()
            {
                toEmail = query.Email,
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{fullname}}", query.Fullname),
                    new KeyValuePair<string, string>("{{message}}", query.Message),
                    new KeyValuePair<string, string>("{{reply}}", emailContent)
                }
            };
            var result = await _email.SendReplyEmail(options);
            if (result)
            {
                await _service.UpdateQuery(query.Id);
            }
            return RedirectToAction("Index", "ContactUs", null);
        }
    }
}
