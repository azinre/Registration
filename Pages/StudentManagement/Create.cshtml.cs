using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Pages.StudentManagement
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
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public string message = "";
        [BindProperty]
        public bool alreadyExistsErr { get; set; } = false;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }
            var student = await _context.Students.FirstOrDefaultAsync(std => std.Id == Student.Id);
            // if student already exists then set alreadyExistsErr to true else false and add student
            if (student != null)
            {
                alreadyExistsErr = true;
                return Page();
            }
            else
            {
                alreadyExistsErr = false;
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}
