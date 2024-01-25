using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.StudentManagement
{
    public class IndexModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync(string sorting)
        {
            Student = await _context.Students.Include(n => n.AcademicRecords).ToListAsync(); //
            if (sorting != null)
            {
                if (sorting == "name")
                {
                    Student = Student.OrderBy(z => z.Name).ToList();
                }
                else if (sorting == "avgGrade")
                {
                    Student = Student.OrderBy(z => z.AcademicRecords.Average(a => a.Grade)).ToList();
                }
                else if (sorting == "numberOfCourses")
                {

                    Student = Student.OrderBy(z => z.AcademicRecords.Count()).ToList();
                }
            }
        }
        public async Task<IActionResult> OnPostAsync(string stdId)
        {
            if (stdId == null || _context.Students == null)
            {              
                return NotFound();
            }            
            var studentToBeDeleted = await _context.Students.Include(i => i.AcademicRecords).Where(idx => idx.Id == stdId).FirstOrDefaultAsync();
            if (studentToBeDeleted != null)
            {
                _context.AcademicRecords.RemoveRange(studentToBeDeleted.AcademicRecords);
                await _context.SaveChangesAsync();
                _context.Students.Remove(studentToBeDeleted);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/StudentManagement/Index");
        }
    }
    
}


