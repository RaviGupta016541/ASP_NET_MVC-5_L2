using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMVC.Data;
using StudentMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        
        readonly StudentContext _context;
        public StudentController(StudentContext context)
        {
            _context = context;
        }
        public IActionResult Index(string search, int? Page, string? sortOrder, string searchBy = "Sname")
        {
            if (Page == null || Page <= 0)
            {
                Page = 1;
                ViewData["CurrentPage"] = Page;
            }

            IList<Student> l = new List<Student>();

            if (searchBy == "Sname")
            {
                l = _context.Student.Where(s => s.Sname.StartsWith(search) || search == null).ToList();
            }
            if (searchBy == "Std")
            {
                l = _context.Student.Where(s => s.Std.ToString() == search || search == null).ToList();
            }

            int limit = 3;
            int start = (int)(Page - 1) * limit;

            float totalStudents = l.Count();
            int totalNumberOfPage = (int)Math.Ceiling(totalStudents / 3);

            ViewData["CurrentPage"] = Page;
            ViewData["CurrentsearchBy"] = searchBy;
            ViewData["CurrentSearch"] = search;
            

            if (Page > totalNumberOfPage)
            {
                ViewData["CurrentPage"] = totalNumberOfPage;
                start = start - limit;
            }

            ViewBag.totalNumberOfPage = totalNumberOfPage;
            if (sortOrder == "Sname")
            {

                return View(l.Skip(start).Take(limit).OrderBy(s => s.Sname));
            }
            if (sortOrder == "Std")
            {
                return View(l.Skip(start).Take(limit).OrderBy(s => s.Std));
            }

            return View(l.Skip(start).Take(limit));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Rollno,Sname,Std")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Student.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
       

        public async Task<IActionResult> Delete(int? id)
        {

            
            if (id == null )
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Rollno == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int RollNo)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set Student  is null.");
            }
            var student = await _context.Student.FindAsync(RollNo);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var Student = await _context.Student.FindAsync(id);
            if (Student == null)
            {
                return NotFound();
            }
            return View(Student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rollno,Sname,Std")] Student Student)
        {
            if (id != Student.Rollno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    _context.Update(Student);
                    await _context.SaveChangesAsync();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(Student);
        }


    }

}
