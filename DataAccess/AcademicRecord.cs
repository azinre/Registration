using System;
using System.Collections.Generic;

namespace Lab4.DataAccess
{
    public partial class AcademicRecord
    {
        public string CourseCode { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public int? Grade { get; set; }
        public string GetCompleteCourse
        {
            get
            {
                string completeCourse = "";
                if (CourseCodeNavigation != null)
                {
                    completeCourse = CourseCode + " - " + CourseCodeNavigation.Title;
                }
                return completeCourse;
            }
        }

        public virtual Course CourseCodeNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
