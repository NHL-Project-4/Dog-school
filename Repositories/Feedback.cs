namespace Dog_school.Repositories
{
    public class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public string Note { get; set; }
    }
}