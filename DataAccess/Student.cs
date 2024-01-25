using System;
using System.Collections.Generic;

namespace Lab4.DataAccess
{
    public partial class Student
    {
        public Student()
        {
            AcademicRecords = new HashSet<AcademicRecord>();
            CourseCourses = new HashSet<Course>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int GetAllCourses
        {
            get
            {
                int totalCourses = 0;
                foreach (AcademicRecord ar in AcademicRecords)
                {
                    if (ar.Grade.HasValue)
                    {
                        totalCourses++;
                    }
                }
                return totalCourses;
            }
        }
        public double GetAvgGrades
        {
            get
            {
                double totalGrades = 0;
                foreach (AcademicRecord ar in AcademicRecords)
                {
                    if (ar.Grade.HasValue)
                    {
                        totalGrades += Convert.ToDouble(ar.Grade);
                    }
                }
                double avg = totalGrades / Convert.ToDouble(GetAllCourses);
                return avg;
            }
        }
        public string GetStudentIdName
        {
            get
            {
                return Id + " - " + Name;
            }
        }
        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; }

        public virtual ICollection<Course> CourseCourses { get; set; }
    }
}
