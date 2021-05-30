using System;

namespace Dog_school.Repositories
{
    public class Diploma
    {
        public int Diploma_ID { get; set; }
        public int Dog_ID { get; set; }
        public DateTime Date_of_exam { get; set; }
        public string Note { get; set; }
    }
}