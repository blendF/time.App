using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeApp.Data;
using TimeApp.Infrastructure.TimerService;
using TimeApp.Models;

namespace TimeApp.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class TimesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TimerService _timerService;

        public TimesController(ApplicationDbContext context, TimerService timerService)
        {
            _context = context;
            _timerService = timerService;
        }

        // GET: Agent/Times
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Times.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agent/Times/Create
        public async Task<IActionResult> CreateAsync(int userId = 1)
        {
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username");
            var userTimes = await _context.Times.Where(time => time.User_Id == userId && time.DateTime > DateTime.Today).ToListAsync();
            var seconds = _timerService.Seconds(userTimes);
            var isTimerStarted = userTimes.Count() % 2 != 0;
            ViewBag.IsStarted = isTimerStarted;
            ViewBag.Timer = seconds;
            return View();
        }


        // POST: Agent/Times/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,User_Id")] Time time, bool isStarted)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            
            time.DateTime = DateTime.Now;
            _context.Times.Add(time);
            await _context.SaveChangesAsync();


            //return RedirectToAction(nameof());
            return await CreateAsync(time.User_Id);
        }

        private bool TimeExists(int id)
        {
            return _context.Times.Any(e => e.Id == id);
        }
    }
}
