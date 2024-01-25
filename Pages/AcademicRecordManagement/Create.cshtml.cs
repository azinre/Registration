using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class CreateModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public CreateModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "GetFullCourseName");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "GetStudentIdName");
            return Page();
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; }

        public string message = "";
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var recordToCreate = await _context.AcademicRecords.FirstOrDefaultAsync(ar => ar.StudentId == AcademicRecord.StudentId && ar.CourseCode == AcademicRecord.CourseCode);

            if (recordToCreate == null)
            {
                _context.AcademicRecords.Add(AcademicRecord);
                await _context.SaveChangesAsync();
            }
            else
            {
                message = "The specified academic record already exists in the system";
                ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "GetFullCourseName");
                ViewData["StudentId"] = new SelectList(_context.Students, "Id", "GetStudentIdName");
                return Page();
            }



            return RedirectToPage("./Index");
        }
    }
}

