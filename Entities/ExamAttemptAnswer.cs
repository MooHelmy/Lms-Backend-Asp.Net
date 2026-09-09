namespace LMS.Domain.Entities
{
    // بيسجل الطالب اختار إجابة إيه لكل سؤال في محاولة معينة
    public class ExamAttemptAnswer : BaseEntity
    {
        public int AttemptId { get; set; }
        public ExamAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int AnswerId { get; set; }
        public Answer Answer { get; set; } = null!;
    }
}
