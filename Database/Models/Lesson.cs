using System;

namespace Dog_school.Database.Models
{
    public class Lesson
    {
        public int? Lesson_ID { get; set; }
        public int Course_ID { get; set; }
        public string Name { get; set; }
        public DateTime Start_date { get; set; }
        public int Repeat { get; set; }
    }
}