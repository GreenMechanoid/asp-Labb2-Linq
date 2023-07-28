using Labb2_Linq.Data;
using Labb2_Linq.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace Labb2_Linq.Controllers
{
    public class InfoViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfoViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        //get : get's teacher for the course Programmering 1
        public async Task<IActionResult> TeacherCourse(string? queryString)
        {

            if (queryString == null)
            {
            var queryList = await (from t in _context.Teachers
                                join ic in _context.InfoConnection on t.TeacherID equals ic.FK_teachersID
                                join c in _context.Course on ic.FK_CourseID equals c.CourseID
                                    select new InfoViewModel
                                {
                                    TeacherName = t.TeacherFullname,
                                    CourseName = c.CourseName,
                                    CourseID = c.CourseID,
                                    TeacherID = t.TeacherID
                                }).Distinct().ToListAsync();
            ViewBag.TeacherList = new SelectList(_context.Teachers, "TeacherID", "FirstName");
            ViewBag.CourseList = new SelectList(_context.Course, "CourseName", "CourseID");
            return View(queryList.ToList());

            }
            else if (queryString != null)
            {
                var queryList = await (from t in _context.Teachers
                                       join ic in _context.InfoConnection on t.TeacherID equals ic.FK_teachersID
                                       join c in _context.Course on ic.FK_CourseID equals c.CourseID
                                       where c.CourseName == queryString
                                       select new InfoViewModel
                                       {
                                           TeacherName = t.TeacherFullname,
                                           CourseName = c.CourseName,
                                           CourseID = c.CourseID,
                                       }).Distinct().ToListAsync();

                return View(queryList.ToList());

            }
            else
            {
                return NotFound();
            }
            
        }


            //get all students and teachers
        public async Task<IActionResult> StudentWithTeachers()
        {
            var queryList = await (from s in _context.Student
                                   join ic in _context.InfoConnection on s.StudentID equals ic.FK_StudentID
                                   join t in _context.Teachers on ic.FK_teachersID equals t.TeacherID
                                   join c in _context.Course on ic.FK_CourseID equals c.CourseID
                                   select new InfoViewModel 
                                   {
                                       
                                       StudentName = s.StudentFullname,
                                       TeacherName = t.TeacherFullname,
                                       TeacherID = t.TeacherID,
                                       CourseName = c.CourseName,
                                       CourseID = c.CourseID,
                                       ConnectionID = ic.ConnectionID
                                       
                                   }).Distinct().ToListAsync();
            ViewBag.ConnectionList = new SelectList(_context.InfoConnection, "ConnectionID");
            return View(queryList);
        }

        public async Task<IActionResult> StudentsInCourse()
        {
            var queryList = await (from s in _context.Student
                                   join ic in _context.InfoConnection on s.StudentID equals ic.FK_StudentID
                                   join c in _context.Course on ic.FK_CourseID equals c.CourseID
                                   join t in _context.Teachers on ic.FK_teachersID equals t.TeacherID
                                   where c.CourseName == "Programmering 1"
                                   select new InfoViewModel
                                   {
                                       StudentName = s.StudentFullname,
                                       CourseName = c.CourseName,
                                       TeacherName = t.TeacherFullname,
                                       TeacherID = t.TeacherID
                                   }).ToListAsync();
            ViewBag.TeacherList = new SelectList(_context.Teachers, "TeacherID", "FirstName");
            ViewBag.CourseList = new SelectList(_context.Course, "CourseName", "CourseID");
            return View(queryList);

        }
        [HttpGet]
        public async Task<IActionResult> ChangeTeacher(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTeacher(int id, [Bind("ConnectionID,FK_StudentID,FK_teachersID,FK_classesID,FK_CourseID")] InfoConnection infoConnection)
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
                return RedirectToAction(nameof(StudentWithTeachers));
            }
            ViewData["FK_CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", infoConnection.FK_CourseID);
            ViewData["FK_classesID"] = new SelectList(_context.Set<Classes>(), "ClassesID", "ClassesID", infoConnection.FK_classesID);
            ViewData["FK_StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", infoConnection.FK_StudentID);
            ViewData["FK_teachersID"] = new SelectList(_context.Set<Teachers>(), "TeacherID", "TeacherID", infoConnection.FK_teachersID);
            return View(infoConnection);
        }
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {

            return View(course);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int id, [Bind("CourseID, CourseName,Description")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(TeacherCourse));
            }
            ModelState.AddModelError(string.Empty, "Error Occurred while Saving");
            return View(course);
        }

        private bool CourseExists(int id)
        {
            return (_context.Course?.Any(e => e.CourseID == id)).GetValueOrDefault();
        }
        private bool InfoConnectionExists(int id)
        {
            return (_context.InfoConnection?.Any(e => e.ConnectionID == id)).GetValueOrDefault();
        }
    }
}