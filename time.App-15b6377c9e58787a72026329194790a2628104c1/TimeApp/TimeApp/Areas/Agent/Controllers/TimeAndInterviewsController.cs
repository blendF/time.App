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
    public class TimeAndInterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeAndInterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agent/TimeAndInterviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimeAndInterviews.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());


        }

        // GET: Agent/TimeAndInterviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeAndInterview = await _context.TimeAndInterviews
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeAndInterview == null)
            {
                return NotFound();
            }

            return View(timeAndInterview);
        }

        // GET: Agent/TimeAndInterviews/Create
        public IActionResult Create()
        {
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Password");
            return View();
        }

        // POST: Agent/TimeAndInterviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Interview,Data,User_Id")] TimeAndInterview timeAndInterview)
        {
            if (ModelState.IsValid)
            {
                timeAndInterview.Data = DateTime.Now;
                _context.Add(timeAndInterview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Password", timeAndInterview.User_Id);
            return View(timeAndInterview);
        }

        // GET: Agent/TimeAndInterviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeAndInterview = await _context.TimeAndInterviews.FindAsync(id);
            if (timeAndInterview == null)
            {
                return NotFound();
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Password", timeAndInterview.User_Id);
            return View(timeAndInterview);
        }

        // POST: Agent/TimeAndInterviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Interview,Data,User_Id")] TimeAndInterview timeAndInterview)
        {
            if (id != timeAndInterview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeAndInterview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeAndInterviewExists(timeAndInterview.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Password", timeAndInterview.User_Id);
            return View(timeAndInterview);
        }

        // GET: Agent/TimeAndInterviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeAndInterview = await _context.TimeAndInterviews
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeAndInterview == null)
            {
                return NotFound();
            }

            return View(timeAndInterview);
        }

        // POST: Agent/TimeAndInterviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeAndInterview = await _context.TimeAndInterviews.FindAsync(id);
            _context.TimeAndInterviews.Remove(timeAndInterview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeAndInterviewExists(int id)
        {
            return _context.TimeAndInterviews.Any(e => e.Id == id);
        }
    }
}
