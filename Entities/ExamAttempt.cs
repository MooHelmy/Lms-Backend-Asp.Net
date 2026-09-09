namespace LMS.Domain.Entities
{
    public class ExamAttempt : BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public int StudentId { get; set; }
        public User Student { get; set; } = null!;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public double Score { get; set; }
        public bool Passed { get; set; }

        public ICollection<ExamAttemptAnswer> SelectedAnswers { get; set; } = new List<ExamAttemptAnswer>();
    }
}
