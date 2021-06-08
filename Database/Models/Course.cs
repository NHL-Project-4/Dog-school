using System;

namespace Dog_school.Database.Models
{
    public class Course
    {
        public int? CourseId { get; set; }
        public string Name { get; set; }
        public string Intake { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Note { get; set; }
    }
}