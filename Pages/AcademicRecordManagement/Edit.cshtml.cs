using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class EditModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public EditModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string studentId, string courseCode)
        {
            if (studentId == null || courseCode == null)
            {
                return NotFound();
            }

            AcademicRecord = await _context.AcademicRecords
                .Include(ar => ar.Student)
                .Include(ar => ar.CourseCodeNavigation)
                .FirstOrDefaultAsync(m => m.StudentId == studentId && m.CourseCode == courseCode);

            if (AcademicRecord == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AcademicRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicRecordExists(AcademicRecord.StudentId, AcademicRecord.CourseCode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AcademicRecordExists(string studentId, string courseCode)
        {
            return _context.AcademicRecords.Any(e => e.StudentId == studentId && e.CourseCode == courseCode);
        }
    }
}

