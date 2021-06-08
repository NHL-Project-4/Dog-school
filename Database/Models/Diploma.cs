using System;

namespace Dog_school.Database.Models
{
    public class Diploma
    {
        public int? DiplomaId { get; set; }
        public int DogId { get; set; }
        public DateTime DateOfExam { get; set; }
        public string Note { get; set; }
    }
}