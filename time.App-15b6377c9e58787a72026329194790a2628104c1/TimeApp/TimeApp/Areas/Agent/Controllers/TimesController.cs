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
        public async Task<IActionResult> CreateAsync(int userId = 1)
        {
            //todo: kur te rregullohet authentikimi e userId largohet
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username");
            var userTimes = await _context.Times.Where(time => time.User_Id == userId && time.DateTime > DateTime.Today).ToListAsync();
            var seconds = GetSeconds(userTimes);
            var hasTimerStarted = userTimes.Count() % 2 != 0;
            ViewBag.hasStarted = hasTimerStarted;
            ViewBag.Timer = seconds;
            return View();
        }

        private double GetSeconds(List<Time> userTimes)
        {
            double totalSeconds = 0;
            if (!userTimes.Any()) return 0;
            for (int i = 0; i < userTimes.Count(); i++)
            {
                if (i % 2 != 0)
                {
                    totalSeconds += ((userTimes[i].DateTime.Hour - userTimes[i - 1].DateTime.Hour) * 3600) + ((userTimes[i].DateTime.Minute - userTimes[i - 1].DateTime.Minute) * 60) + (userTimes[i].DateTime.Second - userTimes[i - 1].DateTime.Second);
                }
            }
            return totalSeconds;
        }


        // POST: Agent/Times/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,User_Id")] Time time)
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
