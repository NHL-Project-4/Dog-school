using System;

namespace Dog_school.Database.Models
{
    public class Dog
    {
        public int? DogId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfDeath { get; set; }

        public Gender Gender { get; set; }
        public byte[] Photo { get; set; }
        public string Note { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}