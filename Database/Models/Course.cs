using System;

namespace Dog_school.Database.Models
{
    public class Course
    {
        public int Course_ID { get; set; }
        public string Name { get; set; }
        public string Intake { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Finish_date { get; set; }
        public string Note { get; set; }
    }
}