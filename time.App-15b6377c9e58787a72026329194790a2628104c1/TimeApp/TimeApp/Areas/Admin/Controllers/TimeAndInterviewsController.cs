using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeApp.Data;
using TimeApp.Models;

namespace TimeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TimeAndInterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeAndInterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TimeAndInterviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimeAndInterviews.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/TimeAndInterviews/Details/5
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

       

        private bool TimeAndInterviewExists(int id)
        {
            return _context.TimeAndInterviews.Any(e => e.Id == id);
        }
    }
}
