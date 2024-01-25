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
    public class DetailsModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public DetailsModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

      public Student Student { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id, string sorting)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(idx => idx.AcademicRecords).ThenInclude(t => t.CourseCodeNavigation).FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                if (sorting != null)
                {
                    if (sorting == "course")
                    {
                        student.AcademicRecords = student.AcademicRecords.OrderBy(std => std.CourseCodeNavigation.Title).ToList();
                    }
                    else if (sorting == "Grade")
                    {
                        student.AcademicRecords = student.AcademicRecords.OrderBy(std => std.Grade).ToList();
                    }
                }
                Student = student;
            }
            return Page();
        }
    }
}
