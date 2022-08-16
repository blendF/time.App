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

        // GET: Agent/Times/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _context.Times
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (time == null)
            {
                return NotFound();
            }

            return View(time);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", time.User_Id);
            return View(time);
        }

        // GET: Agent/Times/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _context.Times.FindAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", time.User_Id);
            return View(time);
        }

        // POST: Agent/Times/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,User_Id")] Time time)
        {
            if (id != time.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(time);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeExists(time.Id))
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
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Username", time.User_Id);
            return View(time);
        }

        // GET: Agent/Times/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _context.Times
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (time == null)
            {
                return NotFound();
            }

            return View(time);
        }

        // POST: Agent/Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var time = await _context.Times.FindAsync(id);
            _context.Times.Remove(time);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeExists(int id)
        {
            return _context.Times.Any(e => e.Id == id);
        }
    }
}
