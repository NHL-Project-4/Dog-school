using System;

namespace Dog_school.Repositories
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Repeat { get; set; }
    }
}