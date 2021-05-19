using System;

namespace Dog_school.Repositories
{
    public class Diploma
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public DateTime DateOfExam { get; set; }
        public string Note { get; set; }
    }
}