using System;

namespace Dog_school.Database.Models
{
    public class Dog
    {
        public int? Dog_ID { get; set; }
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime Date_of_birth { get; set; }

        public DateTime Date_of_death { get; set; }

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