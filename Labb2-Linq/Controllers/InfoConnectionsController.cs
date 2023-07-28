using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2_Linq.Data;
using Labb2_Linq.Models;

namespace Labb2_Linq.Controllers
{
    public class InfoConnectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfoConnectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InfoConnections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InfoConnection.Include(i => i.courses).Include(i => i.classes).Include(i => i.students).Include(i => i.teachers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InfoConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InfoConnection == null)
            {
                return NotFound();
            }

            var infoConnection = await _context.InfoConnection
                .Include(i => i.courses)
                .Include(i => i.classes)
                .Include(i => i.students)
                .Include(i => i.teachers)
                .FirstOrDefaultAsync(m => m.ConnectionID == id);
            if (infoConnection == null)
            {
                return NotFound();
            }

            return View(infoConnection);
        }

        // GET: InfoConnections/Create
        public IActionResult Create()
        {
            ViewData["FK_CourseID"] = new SelectList(_context.Course, "CourseID", "CourseName");
            ViewData["FK_classesID"] = new SelectList(_context.Classes, "ClassesID", "ClassName");
            ViewData["FK_StudentID"] = new SelectList(_context.Student, "StudentID", "StudentFullname");
            ViewData["FK_teachersID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherFullname");

            return View();
        }

        // POST: InfoConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConnectionID,FK_StudentID,FK_teachersID,FK_classesID,FK_CourseID")] InfoConnection infoConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infoConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", infoConnection.FK_CourseID);
            ViewData["FK_classesID"] = new SelectList(_context.Set<Classes>(), "ClassesID", "ClassesID", infoConnection.FK_classesID);
            ViewData["FK_StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", infoConnection.FK_StudentID);
            ViewData["FK_teachersID"] = new SelectList(_context.Set<Teachers>(), "TeacherID", "TeacherID", infoConnection.FK_teachersID);
            return View(infoConnection);
        }

        // GET: InfoConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InfoConnection == null)
            {
                return NotFound();
            }

            var infoConnection = await _context.InfoConnection.FindAsync(id);
            if (infoConnection == null)
            {
                return NotFound();
            }
            ViewData["FK_CourseID"] = new SelectList(_context.Course, "CourseID", "CourseName", infoConnection.FK_CourseID);
            ViewData["FK_classesID"] = new SelectList(_context.Classes, "ClassesID", "ClassName", infoConnection.FK_classesID);
            ViewData["FK_StudentID"] = new SelectList(_context.Student, "StudentID", "StudentFullname", infoConnection.FK_StudentID);
            ViewData["FK_teachersID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherFullname", infoConnection.FK_teachersID);
            return View(infoConnection);
        }

        // POST: InfoConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConnectionID,FK_StudentID,FK_teachersID,FK_classesID,FK_CourseID")] InfoConnection infoConnection)
        {
            if (id != infoConnection.ConnectionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoConnectionExists(infoConnection.ConnectionID))
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
            ViewData["FK_CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", infoConnection.FK_CourseID);
            ViewData["FK_classesID"] = new SelectList(_context.Set<Classes>(), "ClassesID", "ClassesID", infoConnection.FK_classesID);
            ViewData["FK_StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", infoConnection.FK_StudentID);
            ViewData["FK_teachersID"] = new SelectList(_context.Set<Teachers>(), "TeacherID", "TeacherID", infoConnection.FK_teachersID);
            return View(infoConnection);
        }

        // GET: InfoConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InfoConnection == null)
            {
                return NotFound();
            }

            var infoConnection = await _context.InfoConnection
                .Include(i => i.courses)
                .Include(i => i.classes)
                .Include(i => i.students)
                .Include(i => i.teachers)
                .FirstOrDefaultAsync(m => m.ConnectionID == id);
            if (infoConnection == null)
            {
                return NotFound();
            }

            return View(infoConnection);
        }

        // POST: InfoConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InfoConnection == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InfoConnection'  is null.");
            }
            var infoConnection = await _context.InfoConnection.FindAsync(id);
            if (infoConnection != null)
            {
                _context.InfoConnection.Remove(infoConnection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoConnectionExists(int id)
        {
          return (_context.InfoConnection?.Any(e => e.ConnectionID == id)).GetValueOrDefault();
        }
    }
}
