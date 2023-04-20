using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAT_APP_PROJECT.DATA.EF.Models;
using Microsoft.AspNetCore.Hosting;//added for FUP
using System.Drawing;
using SAT_APP_PROJECT.MVC.UI.Utilities;

namespace SAT_APP_PROJECT.MVC.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private readonly SATContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;//added for FUP


        public StudentsController(SATContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;//added for FUP
        }
        [Authorize(Roles = "Admin, Scheduler")]
        // GET: Students
        public async Task<IActionResult> Index()
        {
            var sATContext = _context.Students.Include(s => s.Ss);
            return View(await sATContext.ToListAsync());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin, Scheduler")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Ss)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Ssid, Image")] Student student)//added Image property for our webhostenvironment object (image file)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - CREATE
                if (student.Image != null)//file exists, process it
                {
                    //check file type & get the extension to use in my new unique random filename
                    string ext = Path.GetExtension(student.Image.FileName).ToLower();

                    //create list of valid extensions
                    string[] validExts = { ".jpg", ".jpeg", ".png" };//NO GIFS for official student photos!

                    //-verify the uploaded file is OK ext & that file doesn't exceed .NET max
                    if (validExts.Contains(ext) && student.Image.Length < 4_194_303)//hard-coded max file size in bytes 
                    {
                        //generate a guid filename
                        student.PhotoUrl = Guid.NewGuid() + ext;

                        //save the file to webserver
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullImagePath = webRootPath + "/img/students/";

                        //create a MemoryStream object to read image into server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await student.Image.CopyToAsync(memoryStream);//transfer file to MS

                            using (var img = Image.FromStream(memoryStream))//transfer that to Image type for our ImageUtility
                            {
                                int maxImageSize = 500;//FYI - could adjust if needed
                                int maxThumbSize = 100;//FYI - could adjust if needed

                                ImageUtility.ResizeImage(fullImagePath, student.PhotoUrl, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }                   
                }
                else // file was incorrect ext or too big
                {
                    //If no image assign the default filename
                    student.PhotoUrl = "noimage.jpg";
                }

                #endregion

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Ssid")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Ss)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SATContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
