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
    public class TimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agent/Times
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Times.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }
      
        // GET: Agent/Times/Create
        public IActionResult Create()
        {
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Agent/Times/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,User_Id")] Time time)
        {
            if (ModelState.IsValid)
            {
                time.DateTime = DateTime.Now;
                _context.Add(time);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", time.User_Id);
            return View(time);
        }

        private bool TimeExists(int id)
        {
            return _context.Times.Any(e => e.Id == id);
        }
    }
}
