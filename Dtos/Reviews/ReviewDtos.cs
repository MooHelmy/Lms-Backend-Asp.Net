namespace LMS.Application.DTOs.Reviews
{
    public class ReviewCreateDto
    {
        public int CourseId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
    }

    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
