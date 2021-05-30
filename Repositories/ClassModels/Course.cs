using System;

namespace Dog_school.Repositories
{
    public class Course : Database
    {
        public int Course_ID { get; set; }
        public string Name { get; set; }
        public string Intake { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Finish_date { get; set; }
        public string Note { get; set; }
        
    }
}