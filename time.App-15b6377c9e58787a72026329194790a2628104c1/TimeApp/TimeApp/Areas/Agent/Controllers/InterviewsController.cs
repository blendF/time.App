using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeApp.Data;
using TimeApp.Models;

namespace TimeApp.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class InterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agent/Interviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Interviews.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agent/Interviews/Create
        public IActionResult Create()
        {
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Agent/Interviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Interviews,User_Id,Data")] Interview interview)
        {
            if (ModelState.IsValid)
            {

                TempData["success"] = "Intervistat u shtuan";
                interview.Data = DateTime.Now;
                _context.Add(interview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", interview.User_Id);
            return View(interview);
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.Id == id);
        }
    }
}
