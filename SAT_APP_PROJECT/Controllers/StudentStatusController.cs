using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAT_APP_PROJECT.DATA.EF.Models;

namespace SAT_APP_PROJECT.MVC.UI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class StudentStatusController : Controller
    {
        private readonly SATContext _context;


        public StudentStatusController(SATContext context)
        {
            _context = context;
        }

        // GET: StudentStatus
        public async Task<IActionResult> Index()
        {
              return _context.StudentStatuses != null ? 
                          View(await _context.StudentStatuses.ToListAsync()) :
                          Problem("Entity set 'SATContext.StudentStatuses'  is null.");
        }

        // GET: StudentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentStatuses == null)
            {
                return NotFound();
            }

            var studentStatus = await _context.StudentStatuses
                .FirstOrDefaultAsync(m => m.Ssid == id);
            if (studentStatus == null)
            {
                return NotFound();
            }

            return View(studentStatus);
        }

        // GET: StudentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ssid,Ssname,Ssdescription")] StudentStatus studentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentStatus);
        }

        // GET: StudentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentStatuses == null)
            {
                return NotFound();
            }

            var studentStatus = await _context.StudentStatuses.FindAsync(id);
            if (studentStatus == null)
            {
                return NotFound();
            }
            return View(studentStatus);
        }

        // POST: StudentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ssid,Ssname,Ssdescription")] StudentStatus studentStatus)
        {
            if (id != studentStatus.Ssid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentStatusExists(studentStatus.Ssid))
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
            return View(studentStatus);
        }

        // GET: StudentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentStatuses == null)
            {
                return NotFound();
            }

            var studentStatus = await _context.StudentStatuses
                .FirstOrDefaultAsync(m => m.Ssid == id);
            if (studentStatus == null)
            {
                return NotFound();
            }

            return View(studentStatus);
        }

        // POST: StudentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentStatuses == null)
            {
                return Problem("Entity set 'SATContext.StudentStatuses'  is null.");
            }
            var studentStatus = await _context.StudentStatuses.FindAsync(id);
            if (studentStatus != null)
            {
                _context.StudentStatuses.Remove(studentStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentStatusExists(int id)
        {
          return (_context.StudentStatuses?.Any(e => e.Ssid == id)).GetValueOrDefault();
        }
    }
}
