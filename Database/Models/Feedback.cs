namespace Dog_school.Database.Models
{
    public class Feedback
    {
        public int? Lesson_ID { get; set; }
        public int User_ID { get; set; }
        public string Note { get; set; }
    }
}