namespace LMS.Application.DTOs.Exams
{
    public class ExamCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int PassingScore { get; set; }
        public int CourseId { get; set; }
    }

    public class ExamUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int PassingScore { get; set; }
    }

    // النسخة اللي بترجع للـ Instructor/Admin - فيها IsCorrect
    public class ExamDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int PassingScore { get; set; }
        public List<QuestionDetailsDto> Questions { get; set; } = new();
    }

    // النسخة اللي بتترجع للطالب وقت الامتحان - من غير IsCorrect
    public class ExamForStudentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public List<QuestionForStudentDto> Questions { get; set; } = new();
    }

    public class QuestionCreateDto
    {
        public string Text { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Order { get; set; }
        public List<AnswerCreateDto> Answers { get; set; } = new();
    }

    public class QuestionDetailsDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Order { get; set; }
        public List<AnswerDetailsDto> Answers { get; set; } = new();
    }

    public class QuestionForStudentDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Points { get; set; }
        public List<AnswerForStudentDto> Answers { get; set; } = new();
    }

    public class AnswerCreateDto
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class AnswerDetailsDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class AnswerForStudentDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
