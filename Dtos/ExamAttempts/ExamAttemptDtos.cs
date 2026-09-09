namespace LMS.Application.DTOs.ExamAttempts
{
    public class StartExamAttemptDto
    {
        public int ExamId { get; set; }
    }

    public class SubmitExamAttemptDto
    {
        public int AttemptId { get; set; }
        public List<SubmittedAnswerDto> Answers { get; set; } = new();
    }

    public class SubmittedAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

    public class ExamAttemptResultDto
    {
        public int AttemptId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; }
        public bool Passed { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
