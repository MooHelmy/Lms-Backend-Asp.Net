namespace LMS.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Order { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
