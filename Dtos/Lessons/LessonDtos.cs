using LMS.Domain.Entities;

namespace LMS.Application.DTOs.Lessons
{
    public class LessonCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public LessonContentType ContentType { get; set; }
        public string? VideoUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public int Order { get; set; }
        public int SectionId { get; set; }
    }

    public class LessonUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public int Order { get; set; }
    }

    public class LessonResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public int Order { get; set; }
        public int SectionId { get; set; }
    }
}
