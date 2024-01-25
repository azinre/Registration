using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class IndexModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IList<AcademicRecord> AcademicRecord { get;set; } = default!;
        public string CurrentSort { get; set; }
        public string CourseSort { get; set; }
        public string StudentSort { get; set; }
        public string GradeSort { get; set; }

        public async Task OnGetAsync(string? sorting, string? delete, string? course)
        {
            CurrentSort = sorting;
            CourseSort = String.IsNullOrEmpty(sorting) || sorting == "course" ? "course_desc" : "course";
            StudentSort = sorting == "student" ? "student_desc" : "student";
            GradeSort = sorting == "grade" ? "grade_desc" : "grade";

            if (delete != null)
            {
                // DELETE ACADEMIC RECORD
                // find the record
                AcademicRecord recordToDelete = _context.AcademicRecords.FirstOrDefault(ar => ar.StudentId == delete && ar.CourseCode == course);

                if (recordToDelete != null)
                {
                    // actually remove the record
                    _context.AcademicRecords.Remove(recordToDelete);

                    // save changes to the database
                    await _context.SaveChangesAsync();
                }
            }

            IQueryable<AcademicRecord> academicRecordsQuery = _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student);

            switch (sorting)
            {
                case "course":
                    academicRecordsQuery = academicRecordsQuery.OrderBy(ar => ar.CourseCodeNavigation.Title);
                    break;
                case "course_desc":
                    academicRecordsQuery = academicRecordsQuery.OrderByDescending(ar => ar.CourseCodeNavigation.Title);
                    break;
                case "student":
                    academicRecordsQuery = academicRecordsQuery.OrderBy(ar => ar.Student.Name);
                    break;
                case "student_desc":
                    academicRecordsQuery = academicRecordsQuery.OrderByDescending(ar => ar.Student.Name);
                    break;
                case "grade":
                    academicRecordsQuery = academicRecordsQuery.OrderBy(ar => ar.Grade);
                    break;
                case "grade_desc":
                    academicRecordsQuery = academicRecordsQuery.OrderByDescending(ar => ar.Grade);
                    break;
                default:
                    academicRecordsQuery = academicRecordsQuery.OrderBy(ar => ar.CourseCodeNavigation.Title);
                    break;
            }

            AcademicRecord = await academicRecordsQuery.ToListAsync();
        }
    }
}

//        public async Task OnGetAsync(string sorting, string delete, string course)
//        {
//            if (delete != null)
//            {
//                // DELETE ACADEMIC RECORD
//                // find the record
//                AcademicRecord recordToDelete = _context.AcademicRecords.FirstOrDefault(ar => ar.StudentId == delete && ar.CourseCode == course);

//                if (recordToDelete != null)
//                {
//                    // actually remove the record
//                    _context.AcademicRecords.Remove(recordToDelete);

//                    // save changes to database
//                    _context.SaveChanges();
//                }
//            }

//            if (_context.AcademicRecords != null)
//            {
//                AcademicRecord = await _context.AcademicRecords
//                .Include(a => a.CourseCodeNavigation)
//                .Include(a => a.Student).ToListAsync();
//            }

//            if (sorting == "course")
//            {
//                AcademicRecord = AcademicRecord.OrderBy(ar => ar.GetCompleteCourse).ToList();
//            }
//            else if (sorting == "student")
//            {
//                AcademicRecord = AcademicRecord.OrderBy(ar => ar.Student.GetStudentIdName).ToList();
//            }
//            else if (sorting == "grade")
//            {
//                AcademicRecord = AcademicRecord.OrderBy(ar => ar.Grade).ToList();
//            }


//        }
//    }
//}

//            if (_context.AcademicRecords != null)
//            {
//                AcademicRecord = await _context.AcademicRecords
//                .Include(a => a.CourseCodeNavigation)
//                .Include(a => a.Student).ToListAsync();

//                if (sorting != null)
//                {
//                    if (sorting == "student")
//                    {
//                        AcademicRecord = AcademicRecord.OrderByDescending(des => des.Student.Name).ToList();
//                    }
//                    else if (sorting == "course")
//                    {
//                        AcademicRecord = AcademicRecord.OrderByDescending(des => des.CourseCodeNavigation.Title).ToList();
//                    }
//                    else if (sorting == "grade")
//                    {
//                        AcademicRecord = AcademicRecord.OrderByDescending(des => des.Grade).ToList();
//                    }
//                }
//            }
//        }
//    }
//}
