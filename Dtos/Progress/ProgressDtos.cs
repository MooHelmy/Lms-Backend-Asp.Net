namespace LMS.Application.DTOs.Progress
{
    public class UpdateLessonProgressDto
    {
        public int LessonId { get; set; }
        public int WatchedSeconds { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CourseProgressDto
    {
        public int CourseId { get; set; }
        public int CompletedLessons { get; set; }
        public int TotalLessons { get; set; }
        public int ProgressPercentage { get; set; }
    }
}
