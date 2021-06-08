namespace Dog_school.Database.Models
{
    public class Feedback
    {
        public int? LessonId { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }
    }
}