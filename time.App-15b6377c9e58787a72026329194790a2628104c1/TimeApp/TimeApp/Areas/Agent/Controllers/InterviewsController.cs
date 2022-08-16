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

        // GET: Agent/Interviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
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
                interview.Data = DateTime.Now;
                _context.Add(interview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", interview.User_Id);
            return View(interview);
        }

        // GET: Agent/Interviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", interview.User_Id);
            return View(interview);
        }

        // POST: Agent/Interviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Interviews,User_Id,Data")] Interview interview)
        {
            if (id != interview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.Id))
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
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Password", interview.User_Id);
            return View(interview);
        }

        // GET: Agent/Interviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // POST: Agent/Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.Id == id);
        }
    }
}
